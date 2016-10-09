using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Business;
using Common;

namespace ClientApp
{
    public partial class Form1 : Form, IChatDialog
    {
        private ChatClient chatClient;
        public Form1()
        {
            InitializeComponent();
            chatClient = new ChatClient(this);

            this.FormClosing += OnFormClosing;

            pnlChannelsAndUsers.Visible = false;
            pnlMessageDialog.Visible = false;

            pbrRegistering.Visible = false;
            lblRegistering.Visible = false;
        }

        private void OnFormClosing(object sender, FormClosingEventArgs formClosingEventArgs)
        {
            chatClient.CloseConnection();
        }

        private async void btnRegister_Click(object sender, EventArgs e)
        {
            string username = tbxUsername.Text;
            if (string.IsNullOrEmpty(username))
            {
                ShowErrorDialog("You must provide a username to register!");
                return;
            }
            
            pbrRegistering.Visible = true;
            lblRegistering.Visible = true;
            pnlRegistration.Visible = false;

            await Task.Run(() =>
            {
                chatClient.Register(username);
            });

            pbrRegistering.Visible = false;
            lblRegistering.Visible = false;
        }

        public void ShowMessage(string message)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(() => {
                    this.tbxMessages.Text += $"{message}\r\n";
                }));
            }
            else
            {
                this.tbxMessages.Text += $"{message}\r\n";
            }
        }

        public void ShowErrorDialog(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void Registered()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(this.changeVisibilityOnClientRegistered));
            }
            else
            {
                this.changeVisibilityOnClientRegistered();
            }
        }

        private void changeVisibilityOnClientRegistered()
        {
            this.pnlChannelsAndUsers.Visible = true;
            this.pnlMessageDialog.Visible = true;
            this.pnlRegistration.Visible = false;
        }

        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            sendMessage();
        }

        private void tbxMessage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbxEnterSendsMessage.Checked && (int)e.KeyChar == 13)
            {
                sendMessage();
                e.Handled = true;
            }
        }

        private async void sendMessage()
        {
            string message = tbxMessage.Text;
            if (string.IsNullOrEmpty(message)) return;
            tbxMessage.Text = "";

            await Task.Run(() =>
            {
                chatClient.SendMessage(message);
            });
        }

        private void cbxEnterSendsMessage_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxEnterSendsMessage.Checked)
            {
                this.tbxMessage.Focus();
            }
        }
    }
}
