using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Common.ViewModels;

namespace Business
{
    public interface IChatServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void NotifyAllUsers(MessageVM message);

        [OperationContract(IsOneWay = true)]
        void NotifyUserChangedState(string username, int stateId);
    }
}
