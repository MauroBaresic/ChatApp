using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Enums;

namespace Model.ViewModels
{
    public class UserVM
    {
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int StateId { get; set; } = (int) UserStateEnum.Offline;
    }
}
