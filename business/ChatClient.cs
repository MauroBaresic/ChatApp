using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Threading;
using Common;
using Common.Enums;
using Common.ViewModels;
using Model;

namespace Business
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, UseSynchronizationContext = false)]
    public class ChatClient : IChatServiceCallback
    {
        private IChatService remoteProxy;
        private IChatDialog chatDialog;

        private bool _isRegistered = false;
        private string _username = "";
        DateTime _lastReceived;

        public string Username
        {
            get
            {
                if (_isRegistered) return _username;
                else return null;
            }

            private set { _username = value; }
        }

        public ChatClient(IChatDialog inChatDialog)
        {
            this.chatDialog = inChatDialog;
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
                _lastReceived = DateTime.Parse(config.AppSettings.Settings["LastReceived"].Value);
            }
            catch (Exception)
            {
                _lastReceived = DateTime.UtcNow;
            }
        }

        public bool Register(string username, string password, string firstName, string lastName)
        {
            try
            {
                var ctx = new InstanceContext(this);
                remoteProxy = new DuplexChannelFactory<IChatService>(ctx, "ChatClientConfig").CreateChannel();

                var user = new User()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Password = password,
                    UserName = username
                };
                var result = remoteProxy.RegisterUser(user);

                switch ((RegistrationEnum)result)
                {
                    case RegistrationEnum.Successful:
                        Username = username;
                        _isRegistered = true;
                        clearCache();
                        return true;
                    case RegistrationEnum.UsernameAlreadyExists:
                        chatDialog.ShowErrorDialog("Username already exists!");
                        return false;
                    default:
                        return false;
                }
            }
            catch (System.ServiceModel.CommunicationObjectFaultedException communicationObjectFaultedException)
            {
                chatDialog.ShowErrorDialog(communicationObjectFaultedException.Message);
                return false;
            }
            catch (System.TimeoutException timeoutException)
            {
                chatDialog.ShowErrorDialog(timeoutException.Message);
                return false;
            }
            catch (System.ServiceModel.EndpointNotFoundException endpointNotFoundException)
            {
                chatDialog.ShowErrorDialog(endpointNotFoundException.Message);
                return false;
            }
            catch (Exception exception)
            {
                chatDialog.ShowErrorDialog(exception.Message);
                return false;
            }
        }

        public bool Login(string username, string password)
        {
            try
            {
                var ctx = new InstanceContext(this);
                remoteProxy = new DuplexChannelFactory<IChatService>(ctx, "ChatClientConfig").CreateChannel();

                var user = new User()
                {
                    FirstName = "",
                    LastName = "",
                    Password = password,
                    UserName = username
                };
                var result = remoteProxy.LoginUser(user);

                switch ((LoginEnum)result)
                {
                    case LoginEnum.Successful:
                        Username = username;
                        _isRegistered = true;
                        clearCache();
                        return true;
                    case LoginEnum.AlreadyLoggedIn:
                        chatDialog.ShowErrorDialog($"User {username} is already logged in!");
                        return false;
                    case LoginEnum.WrongUsernameOrPassword:
                        chatDialog.ShowErrorDialog("Wrong Username or Password!");
                        return false;
                    default:
                        return false;
                }
            }
            catch (System.ServiceModel.CommunicationObjectFaultedException communicationObjectFaultedException)
            {
                chatDialog.ShowErrorDialog(communicationObjectFaultedException.Message);
                return false;
            }
            catch (System.TimeoutException timeoutException)
            {
                chatDialog.ShowErrorDialog(timeoutException.Message);
                return false;
            }
            catch (System.ServiceModel.EndpointNotFoundException endpointNotFoundException)
            {
                chatDialog.ShowErrorDialog(endpointNotFoundException.Message);
                return false;
            }
            catch (Exception exception)
            {
                chatDialog.ShowErrorDialog(exception.Message);
                return false;
            }
        }

        private void clearCache()
        {
            sendToChannel = true;
            _endUsername = "";
            _endChannel = 0L;
        }

        public void LogOut()
        {
            CloseConnection();
            _isRegistered = false;
            clearCache();
        }

        public void CloseConnection()
        {
            if (_isRegistered)
            {
                try
                {
                    remoteProxy.UserStateChanged(Username, (int)UserStateEnum.Offline);
                }
                catch (Exception exception)
                {
                    //
                }

                try
                {
                    Configuration config = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
                    var settings = config.AppSettings.Settings;
                    var key = "LastReceived";
                    if (settings[key] == null)
                    {
                        settings.Add(key, _lastReceived.ToString());
                    }
                    else
                    {
                        settings[key].Value = _lastReceived.ToString();
                    }
                    config.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection(config.AppSettings.SectionInformation.Name);
                }
                catch (Exception)
                {
                    //
                }
            }
        }

        public List<UserVM> GetAllUsersList()
        {
            try
            {
                return remoteProxy.GetAllUsers();
            }
            catch (Exception exception)
            {
                chatDialog.ShowErrorDialog(exception.Message);
                return new List<UserVM>();
            }
        }

        public List<ChannelVM> GetAllChannelsList()
        {
            try
            {
                return remoteProxy.GetAllChannels().Select(x=>new ChannelVM() {ChannelId = x.ChannelId, ChannelName = x.ChannelName}).ToList();
            }
            catch (Exception exception)
            {
                chatDialog.ShowErrorDialog(exception.Message);
                return new List<ChannelVM>();
            }
        }

        public List<MessageVM> GetUserMessages(string usernameOther)
        {
            try
            {
                return remoteProxy.GetDirectMessages(Username, usernameOther);
            }
            catch (Exception)
            {
                return new List<MessageVM>();
            }
        }

        public List<MessageVM> GetChannelMessages(long channelId)
        {
            try
            {
                return remoteProxy.GetChannelMessages(channelId);
            }
            catch (Exception)
            {
                return new List<MessageVM>();
            }
        }

        private bool sendToChannel = true;
        private string _endUsername = "";
        public void SetEndUser(string username)
        {
            _endUsername = username;
            sendToChannel = false;
        }

        private long _endChannel = 0L;
        public void SetEndChannel(long channelId)
        {
            _endChannel = channelId;
            sendToChannel = true;
        }

        public void SendMessage(string message)
        {
            if (sendToChannel)
            {
                sendChannelMessage(message);
            }
            else
            {
                sendUserMessage(message);
            }
        }

        private void sendUserMessage(string message)
        {
            if (_endUsername == "")
            {
                chatDialog.ShowErrorDialog("Please select a channel or an user first!");
            }
            try
            {
                remoteProxy.ReceiveUserMessage(Username, _endUsername, message);
            }
            catch (Exception exception)
            {
                chatDialog.ShowErrorDialog(exception.Message);
            }
        }

        private void sendChannelMessage(string message)
        {
            if (_endChannel == 0L)
            {
                chatDialog.ShowErrorDialog("Please select a channel or an user first!");
            }
            try
            {
                remoteProxy.ReceiveChannelMessage(_endChannel, Username, message);
            }
            catch (Exception exception)
            {
                chatDialog.ShowErrorDialog(exception.Message);
            }
        }

        public void NotifyUser(string usernameOther, MessageVM message)
        {
            if (!sendToChannel && _endUsername == usernameOther)
            {
                chatDialog.ShowMessage(message);
            }
            else if(message.MessageStateId == (int)MessageStateEnum.New)
            {
                chatDialog.NotifyUserMessage(usernameOther);
            }
            _lastReceived = message.TimeSent;
        }

        public void NotifyAllChannelUsers(long channelId, MessageVM message)
        {
            if (sendToChannel && _endChannel == channelId)
            {
                chatDialog.ShowMessage(message);
            }
            else if (message.MessageStateId == (int)MessageStateEnum.New)
            {
                chatDialog.NotifyChannelMessage(channelId);
            }
            _lastReceived = message.TimeSent;
        }

        public void NotifyUserChangedState(string username, int stateId)
        {
            chatDialog.NotifyUserStateChanged(username, stateId);
        }

        public void ChangeUserState(int stateId)
        {
            try
            {
                remoteProxy.UserStateChanged(Username, stateId);
            }
            catch (Exception exception)
            {
                chatDialog.ShowErrorDialog(exception.Message);
            }
        }

        public void CheckForNewMessages()
        {
            try
            {
                var users = remoteProxy.GetUserMessageNotifications(Username, _lastReceived);
                var channels = remoteProxy.GetChannelMessageNotifications(Username, _lastReceived);

                chatDialog.NotifyNewUserMessages(users);
                chatDialog.NotifyNewChannelMessages(channels);
            }
            catch (Exception exception)
            {
                chatDialog.ShowErrorDialog(exception.Message);
            }
        }

        public void EditMessage(MessageVM message, string content)
        {
            if (sendToChannel)
            {
                EditChannelMessage(_endChannel, message.MessageId, content);
            }
            else
            {
                EditUserMessage(_endUsername, message.MessageId, content);
            }
        }
        
        public void EditUserMessage(string usernameOther, long messageId, string content)
        {
            try
            {
                remoteProxy.EditUserMessage(Username, usernameOther, messageId, content);
            }
            catch (Exception exception)
            {
                chatDialog.ShowErrorDialog(exception.Message);
            }
        }
        
        public void DeleteUserMessage(string usernameOther, long messageId)
        {
            try
            {
                remoteProxy.DeleteUserMessage(Username, usernameOther, messageId);
            }
            catch (Exception exception)
            {
                chatDialog.ShowErrorDialog(exception.Message);
            }
        }

        public void EditChannelMessage(long channelId, long messageId, string content)
        {
            try
            {
                remoteProxy.EditChannelMessage(Username, channelId, messageId, content);
            }
            catch (Exception exception)
            {
                chatDialog.ShowErrorDialog(exception.Message);
            }
        }

        public void DeleteChannelMessage(long channelID, long messageId)
        {
            try
            {
                remoteProxy.DeleteChannelMessage(Username, channelID, messageId);
            }
            catch (Exception exception)
            {
                chatDialog.ShowErrorDialog(exception.Message);
            }
        }

        public void DeleteUserConversation(string usernameOther)
        {
            try
            {
                remoteProxy.DeleteUserConversation(Username, usernameOther);
            }
            catch (Exception exception)
            {
                chatDialog.ShowErrorDialog(exception.Message);
            }
        }

        public void DeleteChannelConversation(long channelId)
        {
            try
            {
                remoteProxy.DeleteChannelConversation(Username, channelId);
            }
            catch (Exception exception)
            {
                chatDialog.ShowErrorDialog(exception.Message);
            }
        }

        public void DeleteConversation()
        {
            if (sendToChannel)
            {
                DeleteChannelConversation(_endChannel);
            }
            else
            {
                DeleteUserConversation(_endUsername);
            }
        }
    }
}
