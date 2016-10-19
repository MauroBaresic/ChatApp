﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.ViewModels;

namespace Common
{
    public interface IChatDialog
    {
        void ShowMessage(MessageVM message);

        void ShowErrorDialog(string message);

        void NotifyChannelMessage(long channelId);

        void NotifyUserMessage(string usernameOther);
    }
}
