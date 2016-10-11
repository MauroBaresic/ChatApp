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

            if (password != confirmPassword)
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
                registered = chatClient.Register(username);
            });
            changeVisibilityOnClientRegistered(registered);

            //hide progress bar
            pbrRegistering.Visible = false;
            lblRegistering.Visible = false;
        }

        public void ShowMessage(string message)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(() => {
                    this.tbxMessages.AppendText($"{message}\r\n");
                }));
            }
            else
            {
                this.tbxMessages.AppendText($"{message}\r\n");
            }
        }

        public void ShowErrorDialog(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        
        private void changeVisibilityOnClientRegistered(bool registered)
        {
            if (registered)
            {
                this.pnlChannelsAndUsers.Visible = true;
                this.pnlMessageDialog.Visible = true;
                this.pnlRegistration.Visible = false;
            }
            else
            {
                this.pnlRegistration.Visible = true;
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

        private void btnLogin_Click(object sender, EventArgs e)
        {

        }

        private void lblViewLoginPass_MouseDown(object sender, MouseEventArgs e)
        {
            showPassword(this.tbxLoginPassword, true);
        }

        private void lblViewLoginPass_MouseUp(object sender, MouseEventArgs e)
        {
            showPassword(this.tbxLoginPassword, false);
        }
    }
}
