using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using Common;
using Common.Enums;
using Common.ViewModels;
using Model;

namespace Business
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Single, UseSynchronizationContext = false)]
    public class ChatClient : IChatServiceCallback
    {
        private IChatService remoteProxy;
        private IChatDialog chatDialog;

        private bool _isRegistered = false;
        private string _username = "";

        private Dictionary<string, List<Message>> _directMessages = new Dictionary<string, List<Message>>();
        private Dictionary<long, List<Message>> _channelMessages = new Dictionary<long, List<Message>>();

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

        public void LogOut()
        {
            CloseConnection();
            _isRegistered = false;
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

        public void SendMessage(string message)
        {
            try
            {
                remoteProxy.ReceiveUserMessage(Username, Username, message);
            }
            catch (Exception exception)
            {
                chatDialog.ShowErrorDialog(exception.Message);
            }
        }

        public void SendChannelMessage(string message)
        {
            try
            {
                remoteProxy.ReceiveUserMessage("general", Username, message);
            }
            catch (Exception exception)
            {
                chatDialog.ShowErrorDialog(exception.Message);
            }
        }

        public void NotifyAllUsers(MessageVM message)
        {
            this.chatDialog.ShowMessage(message);
        }

        public void NotifyUserChangedState(string username, int stateId)
        {
            throw new NotImplementedException();
        }
    }
}
