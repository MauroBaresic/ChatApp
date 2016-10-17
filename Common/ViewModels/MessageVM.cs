using System;

namespace Common.ViewModels
{
    public class MessageVM : IComparable<MessageVM>
    {
        public long MessageId { get; set; }

        public string Content { get; set; }

        public string SenderUsername { get; set; }

        public DateTime TimeSent { get; set; }

        public int CompareTo(MessageVM other)
        {
            if (other == null) return 1;

            return this.TimeSent.CompareTo(other.TimeSent);
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(SenderUsername))
            {
                return Content;
            }
            return $"{SenderUsername} [{TimeSent.ToLocalTime().ToShortTimeString()}] : {Content}";
        }
    }
}
