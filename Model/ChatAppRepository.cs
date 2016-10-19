using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.ViewModels;

namespace Model
{
    public class ChatAppRepository
    {
        public List<UserVM> GetAllUsers()
        {
            using (ChatAppDBEntities context = new ChatAppDBEntities())
            {
                var listUsers = context.AllUsers().ToList();
                return getUserVMs(listUsers);
            }
        }

        public void RegisterUser(User user)
        {
            using (ChatAppDBEntities context = new ChatAppDBEntities())
            {
                context.RegisterUser(user.UserName, user.FirstName, user.LastName, user.Password, user.RegistrationDate);

                //context.Users.Add(user);
                //context.SaveChanges();
            }
        }

        public List<Channel> GetAllChannels()
        {
            using (ChatAppDBEntities context = new ChatAppDBEntities())
            {
                var listChannels = context.AllChannels().ToList();
                List<Channel> allChannels =
                    listChannels.Select(x => new Channel() { ChannelId = x.ChannelId, ChannelName = x.ChannelName })
                        .ToList();
                return allChannels;
            }
        }

        public List<UserVM> GetChannelMembers(long channelId)
        {
            using (ChatAppDBEntities context = new ChatAppDBEntities())
            {
                var membersList = context.GetChannelMembers(channelId).ToList();
                return getUserVMs(membersList);
            }
        }

        public List<MessageVM> GetChannelMessages(long channelId)
        {
            using (ChatAppDBEntities context = new ChatAppDBEntities())
            {
                var listMessages = context.GetChannelMessages(channelId, 100).ToList();
                return getMessageVMs(listMessages);
            }
        }

        public List<MessageVM> GetDirectMessages(string username, string usernameOther)
        {
            using (ChatAppDBEntities context = new ChatAppDBEntities())
            {
                var listMessages = context.DirectMessages(username, usernameOther, 100).ToList();
                return getMessageVMs(listMessages);
            }
        }

        private List<UserVM> getUserVMs(dynamic usersList)
        {
            List<UserVM> userVMs = new List<UserVM>();
            foreach (var user in usersList)
            {
                userVMs.Add(new UserVM() { UserName = user.UserName, LastName = user.LastName, FirstName = user.FirstName });
            }
            return userVMs;
        }

        private List<MessageVM> getMessageVMs(dynamic messageList)
        {
            List<MessageVM> messageVMs = new List<MessageVM>();
            foreach (var message in messageList)
            {
                messageVMs.Add(new MessageVM() {MessageId = message.MessageId, Content = message.Content, TimeSent = message.TimeSent, SenderUsername = message.UserName });
            }
            return messageVMs;
        }

        public long StoreUserMessage(string username, string usernameOther, string userMessage, DateTime timeSent)
        {
            using (ChatAppDBEntities context = new ChatAppDBEntities())
            {
                return context.StoreUserMessage(userMessage, username, usernameOther, timeSent).FirstOrDefault() ?? 0;
            }
        }

        public long StoreChannelMessage(long channelId, string username, string userMessage, DateTime timeSent)
        {
            using (ChatAppDBEntities context = new ChatAppDBEntities())
            {
                return context.StoreChannelMessage(userMessage, username, channelId, timeSent).FirstOrDefault() ?? 0;
            }
        }

        public List<ChannelVM> GetChannelMessageNotifications(string username, DateTime lastReceived)
        {
            using (ChatAppDBEntities context = new ChatAppDBEntities())
            {
                return
                    context.GetChannelMessageNotifications(username, lastReceived)
                        .ToList()
                        .Select(x => new ChannelVM() {ChannelId = x ?? 0})
                        .ToList();
            }
        }

        public List<UserVM> GetUserMessageNotifications(string username, DateTime lastReceived)
        {
            using (ChatAppDBEntities context = new ChatAppDBEntities())
            {
                return
                    context.GetUserMessageNotifications(username, lastReceived)
                        .ToList()
                        .Select(x => new UserVM() {UserName = x})
                        .ToList();
            }
        }
    }
}
