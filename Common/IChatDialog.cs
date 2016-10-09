using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface IChatDialog
    {
        void ShowMessage(string message);

        void ShowErrorDialog(string message);

        void Registered();
    }
}
