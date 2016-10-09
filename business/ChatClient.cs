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

        public void Register(string username)
        {
            var result = remoteProxy.RegisterUser(username);

            switch (result)
            {
                case 0:
                    Username = username;
                    _isRegistered = true;
                    chatDialog.Registered();
                    break;
                case 1:
                    chatDialog.ShowErrorDialog("Username already exists!");
                    break;
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
