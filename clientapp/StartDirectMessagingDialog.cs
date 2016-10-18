using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common.ViewModels;

namespace ClientApp
{
    public partial class StartDirectMessagingDialog : Form
    {
        public UserVM SelectedUser { get; private set; }

        public StartDirectMessagingDialog(List<UserVM> users)
        {
            InitializeComponent();

            this.lbxUsers.DataSource = null;
            this.lbxUsers.DataSource = users;
        }

        private void lbxUsers_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int index = this.lbxUsers.IndexFromPoint(e.X, e.Y);

                if (index != ListBox.NoMatches &&
                    index != 65535)
                {
                    var user = this.lbxUsers.Items[index] as UserVM;
                    if (user != null)
                    {
                        SelectedUser = user;
                        this.Close();
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
