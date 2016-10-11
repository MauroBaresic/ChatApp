using System;
using System.ServiceModel;
using System.Threading;
using Common;
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

        //private Dictionary<string, List<Message>> 

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
            
            var ctx = new InstanceContext(this);
            remoteProxy = new DuplexChannelFactory<IChatService>(ctx, "ChatClientConfig").CreateChannel();
        }

        public bool Register(string username)
        {
            try
            {
                var result = remoteProxy.RegisterUser(username);

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
                var ctx = new InstanceContext(this);
                remoteProxy = new DuplexChannelFactory<IChatService>(ctx, "ChatClientConfig").CreateChannel();
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
        }

        public void CloseConnection()
        {
            if (_isRegistered)
            {
                remoteProxy.CloseConnection(Username);
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
    }
}
