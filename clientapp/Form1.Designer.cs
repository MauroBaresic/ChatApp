namespace ClientApp
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnRegister = new System.Windows.Forms.Button();
            this.tbxMessages = new System.Windows.Forms.TextBox();
            this.btnSendMessage = new System.Windows.Forms.Button();
            this.tbxMessage = new System.Windows.Forms.TextBox();
            this.pnlRegistration = new System.Windows.Forms.Panel();
            this.tbxUsername = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlMessageDialog = new System.Windows.Forms.Panel();
            this.cbxEnterSendsMessage = new System.Windows.Forms.CheckBox();
            this.pnlChannelsAndUsers = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.lbxChannels = new System.Windows.Forms.ListBox();
            this.lbxUsers = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pbrRegistering = new System.Windows.Forms.ProgressBar();
            this.lblRegistering = new System.Windows.Forms.Label();
            this.pnlRegistration.SuspendLayout();
            this.pnlMessageDialog.SuspendLayout();
            this.pnlChannelsAndUsers.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRegister
            // 
            this.btnRegister.Location = new System.Drawing.Point(108, 47);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(75, 23);
            this.btnRegister.TabIndex = 0;
            this.btnRegister.Text = "Register";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // tbxMessages
            // 
            this.tbxMessages.Location = new System.Drawing.Point(18, 3);
            this.tbxMessages.Multiline = true;
            this.tbxMessages.Name = "tbxMessages";
            this.tbxMessages.ReadOnly = true;
            this.tbxMessages.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxMessages.Size = new System.Drawing.Size(352, 165);
            this.tbxMessages.TabIndex = 1;
            // 
            // btnSendMessage
            // 
            this.btnSendMessage.Location = new System.Drawing.Point(376, 188);
            this.btnSendMessage.Name = "btnSendMessage";
            this.btnSendMessage.Size = new System.Drawing.Size(75, 23);
            this.btnSendMessage.TabIndex = 2;
            this.btnSendMessage.Text = "Send";
            this.btnSendMessage.UseVisualStyleBackColor = true;
            this.btnSendMessage.Click += new System.EventHandler(this.btnSendMessage_Click);
            // 
            // tbxMessage
            // 
            this.tbxMessage.Location = new System.Drawing.Point(18, 190);
            this.tbxMessage.Name = "tbxMessage";
            this.tbxMessage.Size = new System.Drawing.Size(352, 20);
            this.tbxMessage.TabIndex = 3;
            this.tbxMessage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbxMessage_KeyPress);
            // 
            // pnlRegistration
            // 
            this.pnlRegistration.Controls.Add(this.tbxUsername);
            this.pnlRegistration.Controls.Add(this.label1);
            this.pnlRegistration.Controls.Add(this.btnRegister);
            this.pnlRegistration.Location = new System.Drawing.Point(123, 271);
            this.pnlRegistration.Name = "pnlRegistration";
            this.pnlRegistration.Size = new System.Drawing.Size(200, 100);
            this.pnlRegistration.TabIndex = 4;
            // 
            // tbxUsername
            // 
            this.tbxUsername.Location = new System.Drawing.Point(83, 11);
            this.tbxUsername.Name = "tbxUsername";
            this.tbxUsername.Size = new System.Drawing.Size(100, 20);
            this.tbxUsername.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username:";
            // 
            // pnlMessageDialog
            // 
            this.pnlMessageDialog.Controls.Add(this.cbxEnterSendsMessage);
            this.pnlMessageDialog.Controls.Add(this.tbxMessage);
            this.pnlMessageDialog.Controls.Add(this.btnSendMessage);
            this.pnlMessageDialog.Controls.Add(this.tbxMessages);
            this.pnlMessageDialog.Location = new System.Drawing.Point(123, 12);
            this.pnlMessageDialog.Name = "pnlMessageDialog";
            this.pnlMessageDialog.Size = new System.Drawing.Size(473, 253);
            this.pnlMessageDialog.TabIndex = 5;
            // 
            // cbxEnterSendsMessage
            // 
            this.cbxEnterSendsMessage.AutoSize = true;
            this.cbxEnterSendsMessage.Location = new System.Drawing.Point(18, 217);
            this.cbxEnterSendsMessage.Name = "cbxEnterSendsMessage";
            this.cbxEnterSendsMessage.Size = new System.Drawing.Size(172, 17);
            this.cbxEnterSendsMessage.TabIndex = 4;
            this.cbxEnterSendsMessage.Text = "Pressing enter sends message.";
            this.cbxEnterSendsMessage.UseVisualStyleBackColor = true;
            this.cbxEnterSendsMessage.CheckedChanged += new System.EventHandler(this.cbxEnterSendsMessage_CheckedChanged);
            // 
            // pnlChannelsAndUsers
            // 
            this.pnlChannelsAndUsers.Controls.Add(this.label3);
            this.pnlChannelsAndUsers.Controls.Add(this.lbxChannels);
            this.pnlChannelsAndUsers.Controls.Add(this.lbxUsers);
            this.pnlChannelsAndUsers.Controls.Add(this.label2);
            this.pnlChannelsAndUsers.Location = new System.Drawing.Point(12, 12);
            this.pnlChannelsAndUsers.Name = "pnlChannelsAndUsers";
            this.pnlChannelsAndUsers.Size = new System.Drawing.Size(104, 407);
            this.pnlChannelsAndUsers.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 174);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Users:";
            // 
            // lbxChannels
            // 
            this.lbxChannels.FormattingEnabled = true;
            this.lbxChannels.Location = new System.Drawing.Point(3, 16);
            this.lbxChannels.Name = "lbxChannels";
            this.lbxChannels.Size = new System.Drawing.Size(95, 95);
            this.lbxChannels.TabIndex = 2;
            // 
            // lbxUsers
            // 
            this.lbxUsers.FormattingEnabled = true;
            this.lbxUsers.Location = new System.Drawing.Point(6, 193);
            this.lbxUsers.Name = "lbxUsers";
            this.lbxUsers.Size = new System.Drawing.Size(95, 95);
            this.lbxUsers.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Channels:";
            // 
            // pbrRegistering
            // 
            this.pbrRegistering.Location = new System.Drawing.Point(12, 409);
            this.pbrRegistering.MarqueeAnimationSpeed = 15;
            this.pbrRegistering.Name = "pbrRegistering";
            this.pbrRegistering.Size = new System.Drawing.Size(685, 10);
            this.pbrRegistering.Step = 50;
            this.pbrRegistering.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pbrRegistering.TabIndex = 7;
            // 
            // lblRegistering
            // 
            this.lblRegistering.AutoSize = true;
            this.lblRegistering.Location = new System.Drawing.Point(570, 393);
            this.lblRegistering.Name = "lblRegistering";
            this.lblRegistering.Size = new System.Drawing.Size(127, 13);
            this.lblRegistering.TabIndex = 8;
            this.lblRegistering.Text = "Please wait, registering ...";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 431);
            this.Controls.Add(this.lblRegistering);
            this.Controls.Add(this.pbrRegistering);
            this.Controls.Add(this.pnlChannelsAndUsers);
            this.Controls.Add(this.pnlMessageDialog);
            this.Controls.Add(this.pnlRegistration);
            this.Name = "Form1";
            this.Text = "Form1";
            this.pnlRegistration.ResumeLayout(false);
            this.pnlRegistration.PerformLayout();
            this.pnlMessageDialog.ResumeLayout(false);
            this.pnlMessageDialog.PerformLayout();
            this.pnlChannelsAndUsers.ResumeLayout(false);
            this.pnlChannelsAndUsers.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.TextBox tbxMessages;
        private System.Windows.Forms.Button btnSendMessage;
        private System.Windows.Forms.TextBox tbxMessage;
        private System.Windows.Forms.Panel pnlRegistration;
        private System.Windows.Forms.TextBox tbxUsername;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlMessageDialog;
        private System.Windows.Forms.Panel pnlChannelsAndUsers;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox lbxChannels;
        private System.Windows.Forms.ListBox lbxUsers;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbxEnterSendsMessage;
        private System.Windows.Forms.ProgressBar pbrRegistering;
        private System.Windows.Forms.Label lblRegistering;
    }
}

