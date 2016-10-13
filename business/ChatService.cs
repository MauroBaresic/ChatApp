using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Common;
using Common.Enums;
using Model;
using Model.ViewModels;

namespace Business
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ChatService : IChatService
    {
        private static Dictionary<User, IChatServiceCallback> _userCallbacks = new Dictionary<User, IChatServiceCallback>();

        private static List<UserVM> _registeredUsers;
        private List<UserVM> RegisteredUsers
        {
            get
            {
                if (_registeredUsers == null)
                {
                    _registeredUsers = getAllUsersList();
                }
                return _registeredUsers;
            }
        }

        private List<UserVM> getAllUsersList()
        {
            using (ChatAppDBEntities context = new ChatAppDBEntities())
            {
                var listUsers = context.AllUsers().ToList();
                List<UserVM> allUsers =
                    listUsers.Select(
                        x => new UserVM() { UserName = x.UserName, FirstName = x.FirstName, LastName = x.LastName })
                        .ToList();
                return allUsers;
            }
        }

        public int RegisterUser(User user)
        {
            if (RegisteredUsers.Exists(x => x.UserName.Equals(user.UserName, StringComparison.OrdinalIgnoreCase)))
            {
                return (int)RegistrationEnum.UsernameAlreadyExists;
            }

            user.RegistrationDate = DateTime.UtcNow;
            using (ChatAppDBEntities context = new ChatAppDBEntities())
            {
                context.RegisterUser(user.UserName, user.FirstName, user.LastName, user.Password, user.RegistrationDate);

                //context.Users.Add(user);
                //context.SaveChanges();
            }
            _registeredUsers.Add(new UserVM() {UserName = user.UserName, FirstName = user.FirstName, LastName = user.LastName, StateId = (int)UserStateEnum.Online});
            _userCallbacks.Add(user, OperationContext.Current.GetCallbackChannel<IChatServiceCallback>());

            NotifyAllUsers($"{user.UserName} joined #ChatApp!");

            return (int)RegistrationEnum.Successful;
        }

        public int LoginUser(User user)
        {
            if (!RegisteredUsers.Exists(x => x.UserName.Equals(user.UserName, StringComparison.OrdinalIgnoreCase)))
            {
                return (int)LoginEnum.WrongUsernameOrPassword;
            }

            var offlineId = (int) UserStateEnum.Offline;
            var registeredUser = RegisteredUsers.FirstOrDefault(x => x.StateId != offlineId && x.UserName.Equals(user.UserName, StringComparison.OrdinalIgnoreCase));
            if (registeredUser != null)
            {
                return (int)LoginEnum.AlreadyLoggedIn;
            }

            using (ChatAppDBEntities context = new ChatAppDBEntities())
            {
                var credentials = context.GetUserCredentials(user.UserName).ToList().FirstOrDefault();
                if (credentials != null)
                {
                    if (credentials.UserName.Equals(user.UserName, StringComparison.OrdinalIgnoreCase) && credentials.Password.Equals(user.Password))
                    {
                        
                    }
                }
            }

            return (int)LoginEnum.Successful;
        }

        public void ReceiveUserMessage(string channel, string username, string userMessage)
        {
            NotifyAllUsers($"{username} [{DateTime.UtcNow.ToShortTimeString()}] : {userMessage}");
        }

        public void UserStateChanged(string username, int stateId)
        {
            var user = RegisteredUsers.FirstOrDefault(x => x.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));
            if (user != null)
            {
                switch ((UserStateEnum) stateId)
                {
                    case UserStateEnum.Offline:
                        var connectedUser = _userCallbacks.Keys.FirstOrDefault(x => x.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));
                        if (connectedUser != null)
                        {
                            _userCallbacks.Remove(connectedUser);
                        }
                        break;
                    default:
                        break;
                }
                user.StateId = stateId;

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
            using (ChatAppDBEntities context = new ChatAppDBEntities())
            {
                var listMessages = context.GetChannelMessages(channelId, 100).ToList();
                List<MessageVM> messages =
                    listMessages.Select(
                        x => new MessageVM() { Content = x.Content, TimeSent = x.TimeSent, SenderUsername = x.UserName })
                        .ToList();
                return messages;
            }
        }

        public List<UserVM> GetAllUsers()
        {
            return RegisteredUsers;
        }

        public List<MessageVM> GetDirectMessages(string username, string usernameOther)
        {
            using (ChatAppDBEntities context = new ChatAppDBEntities())
            {
                var listMessages = context.DirectMessages(username, usernameOther, 100).ToList();
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
