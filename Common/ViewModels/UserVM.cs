using System;
using Common.Enums;

namespace Common.ViewModels
{
    public class UserVM : IComparable<UserVM>
    {
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int StateId { get; set; } = (int) UserStateEnum.Offline;

        public int CompareTo(UserVM other)
        {
            if (other == null) return 1;

            return this.UserName.CompareTo(other.UserName);
        }

        public override string ToString()
        {
            return UserName;
        }
    }
}
