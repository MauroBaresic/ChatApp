using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModels
{
    public class ChannelVM
    {
        public long ChannelId { get; set; }
        public string ChannelName { get; set; }

        public override string ToString()
        {
            return ChannelName;
        }
    }
}
