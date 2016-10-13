using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enums
{
    public enum LoginEnum
    {
        None = 0,
        WrongUsernameOrPassword = 1,
        AlreadyLoggedIn = 2,
        Successful = 3
    }
}
