using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Message
    {
        public string Content { get; set; }

        public string Channel { get; set; }

        public User User { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
