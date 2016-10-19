using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientApp
{
    public class CustomListBoxControl : ListBox
    {
        public void RefreshIAlltems()
        {
            base.RefreshItems();
        }

        public void RefreshItemAt(int index)
        {
            base.RefreshItem(index);
        }
    }
}
