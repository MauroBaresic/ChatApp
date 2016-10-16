using Common.Enums;

namespace Common.ViewModels
{
    public class UserVM
    {
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int StateId { get; set; } = (int) UserStateEnum.Offline;

        public override string ToString()
        {
            return UserName;
        }
    }
}
