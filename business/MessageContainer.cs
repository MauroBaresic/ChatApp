using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.ViewModels;

namespace Business
{
    public class MessageContainer
    {
        public List<MessageVM> Messages { get; private set; }

        public MessageContainer(List<MessageVM> inMessages)
        {
            Messages = inMessages;
        }

        public void AddMessage(MessageVM message)
        {
            Messages.Add(message);
        }

        public void ClearMessages()
        {
            Messages.Clear();
        }
    }
}
