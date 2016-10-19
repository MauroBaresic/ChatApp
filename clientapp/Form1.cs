using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Business;
using Common;
using Common.ViewModels;

namespace ClientApp
{
    public partial class Form1 : Form, IChatDialog
    {
        private ChatClient chatClient;

        private BindingList<UserVM> userList = new BindingList<UserVM>();
        private BindingList<ChannelVM> channelList = new BindingList<ChannelVM>();

        public Form1()
        {
            InitializeComponent();
            chatClient = new ChatClient(this);

            this.FormClosing += OnFormClosing;
            
            pnlMessageDialog.Visible = false;
            pnlLogin.Visible = false;
            pnlRegistration.Visible = false;

            lblNotification.Visible = false;
            pbrRegistering.Visible = false;
            lblRegistering.Visible = false;
            
            this.BackColor = ChatAppColors.BackColor;
            
            var foreColor = ChatAppColors.ForeColor;
            setForeColor(foreColor, lblNotification, lblRegistering, label1, label2, label3, label4, label5, label6, label7, label8, label9, label10, label11, label12, label13, label14, label15,
                tbxRegisterPassword, tbxConfirmPassword, tbxFirstName, tbxLastName, tbxLoginPassword, tbxLoginUsername, tbxMessage, tbxRegisterUsername,
                btnWelcomeSignUp, btnWelcomeLogIn, btnCancelSignUp, btnLogin, btnLoginCancel, btnRegister, btnSendMessage, btnLogOut,btnCloseConversation, btnSelectUser, btnEditMessage, btnDeleteConversation, btnDeleteMessage,
                cbxEnterSendsMessage, lbxChannels, lbxUsers, mlbxMessages);

            var backColor = ChatAppColors.ControlBackColor;
            setBackColor(backColor, btnWelcomeSignUp, btnWelcomeLogIn, btnCancelSignUp, btnLogin, btnLoginCancel, btnRegister, btnSendMessage, btnLogOut);

            setButtonStyle(FlatStyle.Flat, btnWelcomeSignUp, btnWelcomeLogIn, btnCancelSignUp, btnLogin, btnLoginCancel, btnRegister, btnSendMessage, btnLogOut);
        }

        private void setForeColor(Color foreColor, params Control[] controls)
        {
            foreach (var control in controls)
            {
                control.ForeColor = foreColor;
            }
        }

        private void setBackColor(Color backColor, params Control[] controls)
        {
            foreach (var control in controls)
            {
                control.BackColor = backColor;
            }
        }

        private void setButtonStyle(dynamic style, params ButtonBase[] buttons)
        {
            foreach (var button in buttons)
            {
                button.FlatStyle = style;
            }
        }

        private void OnFormClosing(object sender, FormClosingEventArgs formClosingEventArgs)
        {
            chatClient.CloseConnection();
        }

        private async void btnRegister_Click(object sender, EventArgs e)
        {
            string username = tbxRegisterUsername.Text;
            string firstName = tbxFirstName.Text;
            string lastName = tbxLastName.Text;
            string password = tbxRegisterPassword.Text;
            string confirmPassword = tbxConfirmPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                ShowErrorDialog("You must fill in all the fields!");
                return;
            }

            if (password.Length < 8)
            {
                ShowErrorDialog("Password too short! Minimum length is 8 characters.");
                return;
            }

            if (!password.Equals(confirmPassword))
            {
                ShowErrorDialog("Passwords mismatch!");
                return;
            }
            
            //start progress bar
            pbrRegistering.Visible = true;
            lblRegistering.Visible = true;
            pnlRegistration.Visible = false;

            bool registered = false;
            await Task.Run(() =>
            {
                registered = chatClient.Register(username, password, firstName, lastName);
            });

            //hide progress bar
            pbrRegistering.Visible = false;
            lblRegistering.Visible = false;

            if (registered)
            {
                this.pnlMessageDialog.Visible = true;
                this.pnlRegistration.Visible = false;
                setUp();
            }
            else
            {
                this.pnlRegistration.Visible = true;
            }
        }

        private void setUp()
        {
            mlbxMessages.Items.Clear();
            tbxMessage.Text = "";
            tbxMessage.Enabled = false;
            btnSendMessage.Enabled = false;

            lbxUsers.DataSource = null;
            userList.Clear();
            foreach (var user in chatClient.GetAllUsersList())
            {
                userList.Add(user);
            }
            lbxUsers.DataSource = userList;
            lbxUsers.ClearSelected();

            lbxChannels.DataSource = null;
            channelList.Clear();
            foreach (var channel in chatClient.GetAllChannelsList())
            {
                channelList.Add(channel);
            }
            lbxChannels.DataSource = channelList;
            lbxChannels.ClearSelected();

            lblNotification.Text = "Select a channel or a user to start the conversation.";
            lblNotification.Visible = true;
        }

        public void ShowMessage(MessageVM message)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(() => {
                    this.mlbxMessages.Items.Add(message);
                }));
            }
            else
            {
                this.mlbxMessages.Items.Add(message);
            }
        }

        public void ShowErrorDialog(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void NotifyChannelMessage(long channelId)
        {
            var channel = channelList.FirstOrDefault(x => x.ChannelId.Equals(channelId));
            if (channel == null)
            {
                //
            }

            if (channel != null)
            {
                channel.NewMessageNotification = true;
            }
        }

        public void NotifyUserMessage(string usernameOther)
        {
            var user = userList.FirstOrDefault(x => x.UserName.Equals(usernameOther));
            if (user == null)
            {
                //
            }

            if (user != null)
            {
                user.NewMessageNotification = true;
            }
        }

        #region Message Sending

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
            tbxMessage.Focus();

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

        #endregion

        #region ViewPassword

        private void lblViewPass_MouseDown(object sender, MouseEventArgs e)
        {
            showPassword(this.tbxRegisterPassword, true);
        }

        private void lblViewPass_MouseUp(object sender, MouseEventArgs e)
        {
            showPassword(this.tbxRegisterPassword, false);
        }

        private void lblViewConfirmPass_MouseDown(object sender, MouseEventArgs e)
        {
            showPassword(this.tbxConfirmPassword, true);
        }

        private void lblViewConfirmPass_MouseUp(object sender, MouseEventArgs e)
        {
            showPassword(this.tbxConfirmPassword, false);
        }

        private void showPassword(TextBox textBox, bool visible)
        {
            switch (visible)
            {
                case true:
                    textBox.UseSystemPasswordChar = false;
                    break;
                case false:
                    textBox.UseSystemPasswordChar = true;
                    break;
            }
        }

        #endregion

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string username = tbxLoginUsername.Text;
            string password = tbxLoginPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ShowErrorDialog("You must fill in all the fields!");
                return;
            }

            //start progress bar
            pbrRegistering.Visible = true;
            lblRegistering.Visible = true;
            pnlLogin.Visible = false;

            bool registered = false;
            await Task.Run(() =>
            {
                registered = chatClient.Login(username, password);
            });

            //hide progress bar
            pbrRegistering.Visible = false;
            lblRegistering.Visible = false;

            if (registered)
            {
                this.pnlMessageDialog.Visible = true;
                this.pnlLogin.Visible = false;
                setUp();
            }
            else
            {
                this.pnlLogin.Visible = true;
            }
        }

        private void lblViewLoginPass_MouseDown(object sender, MouseEventArgs e)
        {
            showPassword(this.tbxLoginPassword, true);
        }

        private void lblViewLoginPass_MouseUp(object sender, MouseEventArgs e)
        {
            showPassword(this.tbxLoginPassword, false);
        }

        private void btnWelcomeSignUp_Click(object sender, EventArgs e)
        {
            pnlRegistration.Visible = true;
            pnlWelcome.Visible = false;
        }

        private void btnWelcomeLogIn_Click(object sender, EventArgs e)
        {
            pnlLogin.Visible = true;
            pnlWelcome.Visible = false;
        }

        private void btnLoginCancel_Click(object sender, EventArgs e)
        {
            pnlLogin.Visible = false;
            pnlWelcome.Visible = true;
        }

        private void btnCancelSignUp_Click(object sender, EventArgs e)
        {
            pnlRegistration.Visible = false;
            pnlWelcome.Visible = true;
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            chatClient.LogOut();
            pnlMessageDialog.Visible = false;
            pnlLogin.Visible = true;
            userList = new BindingList<UserVM>();
            channelList = new BindingList<ChannelVM>();
        }

        private void lbxChannels_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void lbxUsers_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }
        
        private void lbxChannels_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int index = this.lbxChannels.IndexFromPoint(e.X, e.Y);

                if (index != ListBox.NoMatches &&
                    index != 65535)
                {
                    var channel = this.lbxChannels.Items[index] as ChannelVM;
                    if (channel != null)
                    {
                        lbxUsers.ClearSelected();
                        setMessages(channel.ChannelName, channel.ChannelId);
                    }
                }
            }
        }

        private void setMessages(string channelName, long channelId)
        {
            showMessages(chatClient.GetChannelMessages(channelId));
            lblNotification.Text = channelName;
            tbxMessage.Enabled = true;
            btnSendMessage.Enabled = true;
            chatClient.SetEndChannel(channelId);
            var channel = channelList.FirstOrDefault(x => x.ChannelId.Equals(channelId));
            if (channel != null)
            {
                channel.NewMessageNotification = false;
            }
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
                        lbxChannels.ClearSelected();
                        setMessages(user.UserName);
                    }
                }
            }
        }

        private void setMessages(string usernameOther)
        {
            showMessages(chatClient.GetUserMessages(usernameOther));
            lblNotification.Text = usernameOther;
            tbxMessage.Enabled = true;
            btnSendMessage.Enabled = true;
            chatClient.SetEndUser(usernameOther);
            var user = userList.FirstOrDefault(x => x.UserName.Equals(usernameOther));
            if (user != null)
            {
                user.NewMessageNotification = false;
            }
        }

        private void showMessages(List<MessageVM> messages)
        {
            this.mlbxMessages.Items.Clear();
            messages.Sort();
            foreach (var message in messages)
            {
                this.mlbxMessages.Items.Add(message);
            }
        }

        private void btnSelectUser_Click(object sender, EventArgs e)
        {
            var frm = new StartDirectMessagingDialog(chatClient.GetAllUsersList());
            frm.ShowDialog();
            if (frm.SelectedUser != null)
            {
                var user = frm.SelectedUser;
                if (!userList.ToList().Exists(x=>x.UserName.Equals(user.UserName)))
                {
                    userList.Add(user);
                }
                else
                {
                    user = userList.FirstOrDefault(x => x.UserName.Equals(user.UserName));
                }

                if (user != null)
                {
                    int index = lbxUsers.Items.IndexOf(user);
                    lbxUsers.SelectedIndex = index;
                    lbxChannels.ClearSelected();
                    setMessages(user.UserName);
                }
            }
        }

        private void btnCloseConversation_Click(object sender, EventArgs e)
        {
            
        }

        private void lbxChannels_DrawItem(object sender, DrawItemEventArgs e)
        {
            var listBox = sender as ListBox;
            if(listBox == null) return;
            e.DrawBackground();
            Graphics g = e.Graphics;
            Brush brush = ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                ? new SolidBrush(ChatAppColors.ForeColor)
                : new SolidBrush(ChatAppColors.ControlBackColor);
            g.FillRectangle(brush, e.Bounds);
            var channel = listBox.Items[e.Index] as ChannelVM;
            e.Graphics.DrawString(channel.ToString(),
                channel.NewMessageNotification
                    ? new Font(e.Font.FontFamily, e.Font.Size, FontStyle.Bold)
                    : new Font(e.Font.FontFamily, e.Font.Size, e.Font.Style),
                new SolidBrush(e.ForeColor), e.Bounds, StringFormat.GenericDefault);
            e.DrawFocusRectangle();
        }

        private void lbxUsers_DrawItem(object sender, DrawItemEventArgs e)
        {
            var listBox = sender as ListBox;
            if (listBox == null) return;
            e.DrawBackground();
            Graphics g = e.Graphics;
            Brush brush = ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                ? new SolidBrush(ChatAppColors.ForeColor)
                : new SolidBrush(ChatAppColors.ControlBackColor);
            g.FillRectangle(brush, e.Bounds);
            var user = listBox.Items[e.Index] as UserVM;
            e.Graphics.DrawString(user.ToString(),
                user.NewMessageNotification
                    ? new Font(e.Font.FontFamily, e.Font.Size, FontStyle.Bold)
                    : new Font(e.Font.FontFamily, e.Font.Size, e.Font.Style),
                new SolidBrush(e.ForeColor),
                new RectangleF(e.Bounds.X + 20, e.Bounds.Y + 3, e.Bounds.Width - 20, e.Bounds.Height - 6),
                StringFormat.GenericDefault);
            e.Graphics.DrawImage(Properties.Resources.online12, new Rectangle(e.Bounds.X + 4, e.Bounds.Y + 4, 12, 12));
            e.DrawFocusRectangle();
        }
    }
}
