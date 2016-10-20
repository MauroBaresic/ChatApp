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
            try
            {
                using (ChatAppDBEntities context = new ChatAppDBEntities())
                {
                    var listUsers = context.AllUsers().ToList();
                    return getUserVMs(listUsers);
                }
            }
            catch (Exception exception)
            {
                return new List<UserVM>();
            }
        }

        public void RegisterUser(User user)
        {
            try
            {
                using (ChatAppDBEntities context = new ChatAppDBEntities())
                {
                    context.RegisterUser(user.UserName, user.FirstName, user.LastName, user.Password, user.RegistrationDate);

                    //context.Users.Add(user);
                    //context.SaveChanges();
                }
            }
            catch (Exception exception)
            {
                //
            }
            
        }

        public List<Channel> GetAllChannels()
        {
            try
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
            catch (Exception exception)
            {
                return new List<Channel>();
            }
        }

        public List<UserVM> GetChannelMembers(long channelId)
        {
            try
            {
                using (ChatAppDBEntities context = new ChatAppDBEntities())
                {
                    var membersList = context.GetChannelMembers(channelId).ToList();
                    return getUserVMs(membersList);
                }
            }
            catch (Exception exception)
            {
                return new List<UserVM>();
            }
            
        }

        public List<MessageVM> GetChannelMessages(long channelId)
        {
            try
            {
                using (ChatAppDBEntities context = new ChatAppDBEntities())
                {
                    var listMessages = context.GetChannelMessages(channelId, 100).ToList();
                    return getMessageVMs(listMessages);
                }
            }
            catch (Exception exception)
            {
                return new List<MessageVM>();
            }
        }

        public List<MessageVM> GetDirectMessages(string username, string usernameOther)
        {
            try
            {
                using (ChatAppDBEntities context = new ChatAppDBEntities())
                {
                    var listMessages = context.DirectMessages(username, usernameOther, 100).ToList();
                    return getMessageVMs(listMessages);
                }
            }
            catch (Exception exception)
            {
                return  new List<MessageVM>();
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
            try
            {
                using (ChatAppDBEntities context = new ChatAppDBEntities())
                {
                    return context.StoreUserMessage(userMessage, username, usernameOther, timeSent).FirstOrDefault() ?? 0;
                }
            }
            catch (Exception exception)
            {
                return 0L;
            }
        }

        public long StoreChannelMessage(long channelId, string username, string userMessage, DateTime timeSent)
        {
            try
            {
                using (ChatAppDBEntities context = new ChatAppDBEntities())
                {
                    return context.StoreChannelMessage(userMessage, username, channelId, timeSent).FirstOrDefault() ?? 0;
                }
            }
            catch (Exception exception)
            {
                //
                return 0L;
            }
            
        }

        public List<ChannelVM> GetChannelMessageNotifications(string username, DateTime lastReceived)
        {
            try
            {
                using (ChatAppDBEntities context = new ChatAppDBEntities())
                {
                    return
                        context.GetChannelMessageNotifications(username, lastReceived)
                            .ToList()
                            .Select(x => new ChannelVM() { ChannelId = x ?? 0 })
                            .ToList();
                }
            }
            catch (Exception exception)
            {
                return new List<ChannelVM>();
            }
            
        }

        public List<UserVM> GetUserMessageNotifications(string username, DateTime lastReceived)
        {
            try
            {
                using (ChatAppDBEntities context = new ChatAppDBEntities())
                {
                    var notifications = context.GetUserMessageNotifications(username, lastReceived).ToList();
                    if (notifications.Any())
                    {
                        return notifications.Where(x => x != null).Select(x => new UserVM() { UserName = x }).ToList();
                    }
                    //return
                    //    context.GetUserMessageNotifications(username, lastReceived)
                    //        .ToList()
                    //        .Select(x => new UserVM() {UserName = x})
                    //        .ToList();
                    return new List<UserVM>();
                }
            }
            catch (Exception exception)
            {
                return new List<UserVM>();
            }
            
        }

        public void EditMessage(string username, long messageId, string content)
        {
            try
            {
                using (ChatAppDBEntities context = new ChatAppDBEntities())
                {
                    context.UpdateMessage(username, messageId, content);
                }
            }
            catch (Exception exception)
            {
                //
            }
            
        }

        public void DeleteMessage(string username, long messageId)
        {
            try
            {
                using (ChatAppDBEntities context = new ChatAppDBEntities())
                {
                    context.DeleteMessage(username, messageId);
                }
            }
            catch (Exception exception)
            {
                //
            }
            
        }

        public void DeleteUserConversation(string username, string usernameOther)
        {
            try
            {
                using (ChatAppDBEntities context = new ChatAppDBEntities())
                {
                    context.DeleteUserMessages(username, usernameOther);
                }
            }
            catch (Exception exception)
            {
                //
            }
            
        }

        public void DeleteChannelConversation(string username, long channelId)
        {
            try
            {
                using (ChatAppDBEntities context = new ChatAppDBEntities())
                {
                    context.DeleteChannelUserMessages(channelId, username);
                }
            }
            catch (Exception exception)
            {
                //
            }
            
        }
    }
}
