using System;

namespace Common.ViewModels
{
    public class MessageVM
    {
        public long MessageId { get; set; }

        public string Content { get; set; }

        public string SenderUsername { get; set; }

        public DateTime TimeSent { get; set; }

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
