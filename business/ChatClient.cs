using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading;
using Common;
using Common.Enums;
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

                switch (result)
                {
                    case 0:
                        Username = username;
                        _isRegistered = true;
                        return true;
                    case 1:
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

        public void CloseConnection()
        {
            if (_isRegistered)
            {
                remoteProxy.UserStateChanged(Username, (int) UserStateEnum.Offline);
            }
        }

        public void SendMessage(string message)
        {
            remoteProxy.ReceiveUserMessage("general", Username, message);
        }
        
        public void NotifyAllUsers(string message)
        {
            this.chatDialog.ShowMessage(message);
        }

        public void NotifyUserChangedState(string username, int stateId)
        {
            throw new NotImplementedException();
        }
    }
}
