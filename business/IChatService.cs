using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Common.ViewModels;
using Model;

namespace Business
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IChatServiceCallback))]
    public interface IChatService
    {
        [OperationContract]
        int RegisterUser(User user);

        [OperationContract]
        int LoginUser(User user);

        [OperationContract]
        void ReceiveUserMessage(string username, string usernameOther, string userMessage);

        [OperationContract]
        void ReceiveChannelMessage(long channelId, string username, string userMessage);

        [OperationContract]
        void UserStateChanged(string username, int stateId);

        [OperationContract]
        List<Channel> GetAllChannels();

        [OperationContract]
        List<UserVM> GetChannelMembers(long channelId);

        [OperationContract]
        List<MessageVM> GetChannelMessages(long channelId);

        [OperationContract]
        List<UserVM> GetAllUsers();

        [OperationContract]
        List<MessageVM> GetDirectMessages(string username, string usernameOther);

        [OperationContract]
        List<UserVM> GetUserMessageNotifications(string username, DateTime lastReceived);

        [OperationContract]
        List<ChannelVM> GetChannelMessageNotifications(string username, DateTime lastReceived);

        [OperationContract]
        void EditUserMessage(string username, string usernameOther, long messageId, string content);

        [OperationContract]
        void EditChannelMessage(string username, long channelId, long messageId, string content);

        [OperationContract]
        void DeleteUserMessage(string username, string usernameOther, long messageId);

        [OperationContract]
        void DeleteChannelMessage(string username, long channelId, long messageId);

        [OperationContract]
        void DeleteUserConversation(string username, string usernameOther);

        [OperationContract]
        void DeleteChannelConversation(string username, long channelId);
    }
}
