using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Model;

namespace Business
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ChatService : IChatService
    {
        private static List<User> _registeredUsers = new List<User>();
        private static Dictionary<User, IChatServiceCallback> _userCallbacks = new Dictionary<User, IChatServiceCallback>();

        public int RegisterUser(string username)
        {
            if (_registeredUsers.Exists(x => x.UserName.Equals(username, StringComparison.OrdinalIgnoreCase)))
            {
                return 1;
            }

            var user = new User() {UserName = username, RegistrationDate = DateTime.UtcNow};
            _registeredUsers.Add(user);
            _userCallbacks.Add(user, OperationContext.Current.GetCallbackChannel<IChatServiceCallback>());

            NotifyAllUsers($"{user.UserName} joined #ChatApp!");

            return 0;
        }

        public void ReceiveUserMessage(string channel, string username, string userMessage)
        {
            NotifyAllUsers($"{username} [{DateTime.UtcNow.ToShortTimeString()}] : {userMessage}");
        }

        public void CloseConnection(string username)
        {
            var user = _registeredUsers.FirstOrDefault(x => x.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));
            if (user != null)
            {
                _registeredUsers.Remove(user);
                _userCallbacks.Remove(user);

                NotifyAllUsers($"{user.UserName} left the conversation.");
            }
        }

        public void NotifyAllUsers(string message)
        {
            foreach (IChatServiceCallback chatServiceCallback in _userCallbacks.Values)
            {
                chatServiceCallback.NotifyAllUsers(message);
            }
        }

    }
}
