using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Model;
using Model.ViewModels;

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
        void ReceiveUserMessage(string channel, string username, string userMessage);

        [OperationContract]
        void UserStateChanged(string username, int stateId);

        [OperationContract]
        List<Channel> GetAllChannels();

        [OperationContract]
        List<MessageVM> GetChannelMessages(int channelId);

        [OperationContract]
        List<UserVM> GetAllUsers();

        [OperationContract]
        List<MessageVM> GetDirectMessages(string username, string usernameOther);
    }
}
