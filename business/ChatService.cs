using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Common;
using Common.Enums;
using Common.ViewModels;
using Model;

namespace Business
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ChatService : IChatService
    {
        private ChatAppRepository _repository = new ChatAppRepository();
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
            return _repository.GetAllUsers();
        }

        public int RegisterUser(User user)
        {
            if (RegisteredUsers.Exists(x => x.UserName.Equals(user.UserName, StringComparison.OrdinalIgnoreCase)))
            {
                return (int)RegistrationEnum.UsernameAlreadyExists;
            }

            user.RegistrationDate = DateTime.UtcNow;
            _repository.RegisterUser(user);

            _registeredUsers.Add(new UserVM() {UserName = user.UserName, FirstName = user.FirstName, LastName = user.LastName, StateId = (int)UserStateEnum.Online});
            _userCallbacks.Add(user, OperationContext.Current.GetCallbackChannel<IChatServiceCallback>());

            var message = new MessageVM() { Content = $"{user.UserName} joined #ChatApp!", SenderUsername = "", MessageId = 0, TimeSent = DateTime.UtcNow };
            _repository.StoreChannelMessage(1L, message.SenderUsername, message.Content, message.TimeSent);
            NotifyChannelUsers(message, 1L);

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
                    if (!(credentials.UserName.Equals(user.UserName, StringComparison.OrdinalIgnoreCase) && credentials.Password.Equals(user.Password)))
                    {
                        return (int)LoginEnum.WrongUsernameOrPassword;
                    }
                }
                else
                {
                    return (int)LoginEnum.WrongUsernameOrPassword;
                }
            }

            var authUser = RegisteredUsers.FirstOrDefault(x => x.UserName.Equals(user.UserName, StringComparison.OrdinalIgnoreCase));
            if (authUser != null)
            {
                authUser.StateId = (int) UserStateEnum.Online;
                user.FirstName = authUser.FirstName;
                user.LastName = authUser.LastName;
                _userCallbacks.Add(user, OperationContext.Current.GetCallbackChannel<IChatServiceCallback>());
            }
            else
            {
                return (int)LoginEnum.WrongUsernameOrPassword;
            }
            return (int)LoginEnum.Successful;
        }

        public void ReceiveUserMessage(string username, string usernameOther, string userMessage)
        {
            DateTime utcNow = DateTime.UtcNow;
            var messageId = _repository.StoreUserMessage(username, usernameOther, userMessage, utcNow);
            
            NotifyUser(new MessageVM() { Content = userMessage, MessageId = messageId, SenderUsername = username, TimeSent = utcNow }, username, usernameOther);
        }

        public void ReceiveChannelMessage(long channelId, string username, string userMessage)
        {
            DateTime utcNow = DateTime.UtcNow;
            var messageId = _repository.StoreChannelMessage(channelId, username, userMessage, utcNow);

            NotifyChannelUsers(new MessageVM() { Content = userMessage, MessageId = messageId, SenderUsername = username, TimeSent = utcNow }, channelId);
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

                //NotifyAllUsers($"{user.UserName} left the conversation.");
                
            }
        }

        public List<Channel> GetAllChannels()
        {
            return _repository.GetAllChannels();
        }

        public List<UserVM> GetChannelMembers(long channelId)
        {
            return _repository.GetChannelMembers(channelId);
        }

        public List<MessageVM> GetChannelMessages(long channelId)
        {
            return _repository.GetChannelMessages(channelId);
        }

        public List<UserVM> GetAllUsers()
        {
            return RegisteredUsers;
        }

        public List<MessageVM> GetDirectMessages(string username, string usernameOther)
        {
            return _repository.GetDirectMessages(username, usernameOther);
        }

        public void NotifyUser(MessageVM message, string username, string usernameOther)
        {
            foreach (var userCallback in _userCallbacks)
            {
                if (userCallback.Key.UserName == username)
                {
                    userCallback.Value.NotifyUser(usernameOther, message);
                }
                else if (userCallback.Key.UserName == usernameOther)
                {
                    userCallback.Value.NotifyUser(username, message);
                }
            }
        }

        public void NotifyChannelUsers(MessageVM message, long channelId)
        {
            Dictionary<string, string> users = GetChannelMembers(channelId).ToDictionary(x=>x.UserName, y=>y.UserName);
            foreach (var userCallback in _userCallbacks)
            {
                if (channelId == 1L || users.ContainsKey(userCallback.Key.UserName))
                {
                    userCallback.Value.NotifyAllChannelUsers(channelId, message);
                }
            }
        }
    }
}
