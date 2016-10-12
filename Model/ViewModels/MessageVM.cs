using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModels
{
    public class MessageVM
    {
        public string Content { get; set; }

        public string SenderUsername { get; set; }

        public DateTime TimeSent { get; set; }
    }
}
