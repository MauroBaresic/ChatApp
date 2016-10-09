using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Business
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IChatServiceCallback))]
    public interface IChatService
    {
        [OperationContract]
        int RegisterUser(string username);

        [OperationContract]
        void ReceiveUserMessage(string channel, string username, string userMessage);

        [OperationContract]
        void CloseConnection(string username);
    }
}
