﻿using System;
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
using Common.Enums;
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
            this.mlbxMessages.SetChatClient(chatClient);

            this.FormClosing += OnFormClosing;

            ToolTip ToolTip1 = new ToolTip();
            ToolTip1.SetToolTip(this.btnEditMessage, "Edit selected message");
            ToolTip1.SetToolTip(this.btnCloseConversation, "Close current conversation.");
            ToolTip1.SetToolTip(this.btnDeleteMessage, "Delete selected message");
            ToolTip1.SetToolTip(this.btnDeleteConversation, "Delete your messages in current conversation");

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
            cbxEnterSendsMessage.Enabled = false;

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

            chatClient.CheckForNewMessages();

            lblNotification.Text = "Select a channel or a user to start the conversation.";
            lblNotification.Visible = true;
        }

        public void ShowMessage(MessageVM message)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(() => { ReceiveMessage(message); }));
            }
            else
            {
                ReceiveMessage(message);
            }
        }

        private void ReceiveMessage(MessageVM message)
        {
            switch ((MessageStateEnum)message.MessageStateId)
            {
                case MessageStateEnum.New:
                    this.mlbxMessages.Items.Add(message);
                    break;
                case MessageStateEnum.Modified:
                    this.mlbxMessages.EditMessage(message);
                    break;
                case MessageStateEnum.Deleted:
                    this.mlbxMessages.DeleteMessage(message);
                    break;
                case MessageStateEnum.DeleteAll:
                    this.mlbxMessages.DeleteAllMesages(message);
                    break;
                default:
                    break;
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
                channel = chatClient.GetAllChannelsList().FirstOrDefault(x => x.ChannelId.Equals(channelId));
                if (channel != null)
                {
                    channel.NewMessageNotification = true;
                    channelList.Add(channel);
                }
            }

            if (channel != null)
            {
                channel.NewMessageNotification = true;
                lbxChannels.RefreshIAlltems();
                lbxChannels.ClearSelected();
            }
        }

        public void NotifyUserMessage(string usernameOther)
        {
            var user = userList.FirstOrDefault(x => x.UserName.Equals(usernameOther));
            if (user == null)
            {
                user = chatClient.GetAllUsersList().FirstOrDefault(x => x.UserName.Equals(usernameOther));
                if (user != null)
                {
                    user.NewMessageNotification = true;
                    userList.Add(user);
                }
            }

            if (user != null)
            {
                user.NewMessageNotification = true;
                lbxUsers.RefreshIAlltems();
                lbxUsers.ClearSelected();
            }
        }

        public void NotifyUserStateChanged(string username, int stateId)
        {
            var user = userList.FirstOrDefault(x => x.UserName.Equals(username));
            if (user == null)
            {
                user = chatClient.GetAllUsersList().FirstOrDefault(x => x.UserName.Equals(username));
                if (user != null)
                {
                    user.StateId = stateId;
                    userList.Add(user);
                }
            }

            if (user != null)
            {
                user.StateId = stateId;
                lbxUsers.RefreshIAlltems();
                lbxUsers.ClearSelected();
            }
        }

        public void NotifyNewUserMessages(List<UserVM> users)
        {
            foreach (var user in users)
            {
                NotifyUserMessage(user.UserName);
            }
        }

        public void NotifyNewChannelMessages(List<ChannelVM> channels)
        {
            foreach (var channel in channels)
            {
                if(channel.ChannelId == 0L) continue;
                NotifyChannelMessage(channel.ChannelId);
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
                        lbxChannels.RefreshItemAt(index);
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
            cbxEnterSendsMessage.Enabled = true;
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
                        lbxUsers.RefreshItemAt(index);
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
            cbxEnterSendsMessage.Enabled = true;
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
            lblNotification.Text = "Select a channel or a user to start the conversation.";
            tbxMessage.Enabled = false;
            btnSendMessage.Enabled = false;
            cbxEnterSendsMessage.Enabled = false;
            this.mlbxMessages.Items.Clear();

            chatClient.CloseConversation();
        }

        private void lbxChannels_DrawItem(object sender, DrawItemEventArgs e)
        {
            var listBox = sender as CustomListBoxControl;
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
            var listBox = sender as CustomListBoxControl;
            if (listBox == null) return;
            e.DrawBackground();
            Graphics g = e.Graphics;
            Brush brush = ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                ? new SolidBrush(ChatAppColors.ForeColor)
                : new SolidBrush(ChatAppColors.ControlBackColor);
            g.FillRectangle(brush, e.Bounds);
            var user = listBox.Items[e.Index] as UserVM;
            Image statusImage = Properties.Resources.offline12;
            switch ((UserStateEnum)user.StateId)
            {
                case UserStateEnum.Away:
                    statusImage = Properties.Resources.away12;
                    break;
                case UserStateEnum.Busy:
                    statusImage = Properties.Resources.busy12;
                    break;
                case UserStateEnum.Online:
                    statusImage = Properties.Resources.online12;
                    break;
                default:
                    break;
            }
            e.Graphics.DrawString(user.ToString(),
                user.NewMessageNotification
                    ? new Font(e.Font.FontFamily, e.Font.Size, FontStyle.Bold)
                    : new Font(e.Font.FontFamily, e.Font.Size, e.Font.Style),
                new SolidBrush(e.ForeColor),
                new RectangleF(e.Bounds.X + 20, e.Bounds.Y + 3, e.Bounds.Width - 20, e.Bounds.Height - 6),
                StringFormat.GenericDefault);
            e.Graphics.DrawImage(statusImage, new Rectangle(e.Bounds.X + 4, e.Bounds.Y + 4, 12, 12));
            e.DrawFocusRectangle();
        }

        private void btnEditMessage_Click(object sender, EventArgs e)
        {
            this.mlbxMessages.EditSelectedMessage(chatClient.Username);
        }

        private void btnDeleteMessage_Click(object sender, EventArgs e)
        {
            this.mlbxMessages.DeleteSelectedMessage(chatClient.Username);
        }

        private void btnDeleteConversation_Click(object sender, EventArgs e)
        {
            chatClient.DeleteConversation();
        }
    }
}
