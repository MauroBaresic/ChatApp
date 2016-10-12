using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Model;
using Model.ViewModels;

namespace Business
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ChatService : IChatService
    {
        private static List<User> _registeredUsers = new List<User>();
        private static Dictionary<User, IChatServiceCallback> _userCallbacks = new Dictionary<User, IChatServiceCallback>();

        public int RegisterUser(User user)
        {
            if (_registeredUsers.Exists(x => x.UserName.Equals(user.UserName, StringComparison.OrdinalIgnoreCase)))//TODO provjeriti u bazi
            {
                return 1;
            }

            user.RegistrationDate = DateTime.UtcNow;
            _registeredUsers.Add(user);//TODO spremiti usera u bazu
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

        public List<Channel> GetAllChannels()
        {
            using (ChatAppDBEntities context = new ChatAppDBEntities())
            {
                var listChannels = context.AllChannels().ToList();
                List<Channel> allChannels =
                    listChannels.Select(x => new Channel() {ChannelId = x.ChannelId, ChannelName = x.ChannelName})
                        .ToList();
                return allChannels;
            }
        }

        public List<MessageVM> GetChannelMessages(int channelId)
        {
            throw new NotImplementedException();
        }

        public List<UserVM> GetAllUsers()
        {
            using (ChatAppDBEntities context = new ChatAppDBEntities())
            {
                var listUsers = context.AllUsers().ToList();
                List<UserVM> allUsers =
                    listUsers.Select(
                        x => new UserVM() {UserName = x.UserName, FirstName = x.FirstName, LastName = x.LastName})
                        .ToList();
                return allUsers;
            }
        }

        public List<MessageVM> GetDirectMessages(string username, string usernameOther)
        {
            using (ChatAppDBEntities context = new ChatAppDBEntities())
            {
                var listMessages = context.DirectMessages(username, usernameOther).ToList();
                List<MessageVM> messages =
                    listMessages.Select(
                        x => new MessageVM() {Content = x.Content, TimeSent = x.TimeSent, SenderUsername = x.UserName})
                        .ToList();
                return messages;
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
