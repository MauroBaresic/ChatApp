using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class User : IComparable<User>
    {
        public int CompareTo(User other)
        {
            if (other == null) return 1;

            return this.UserName.CompareTo(other.UserName);
        }
    }
}
