using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class Message:IComparable<Message>
    {
        public int CompareTo(Message other)
        {
            if (other == null) return 1;

            return this.TimeSent.CompareTo(other.TimeSent);
        }
    }
}
