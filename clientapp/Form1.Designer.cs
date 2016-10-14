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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnRegister = new System.Windows.Forms.Button();
            this.tbxMessages = new System.Windows.Forms.TextBox();
            this.btnSendMessage = new System.Windows.Forms.Button();
            this.tbxMessage = new System.Windows.Forms.TextBox();
            this.pnlRegistration = new System.Windows.Forms.Panel();
            this.btnCancelSignUp = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblViewConfirmPass = new System.Windows.Forms.Label();
            this.lblViewPass = new System.Windows.Forms.Label();
            this.tbxFirstName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbxLastName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbxConfirmPassword = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbxRegisterPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxRegisterUsername = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlMessageDialog = new System.Windows.Forms.Panel();
            this.cbxEnterSendsMessage = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lbxChannels = new System.Windows.Forms.ListBox();
            this.lbxUsers = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pbrRegistering = new System.Windows.Forms.ProgressBar();
            this.lblRegistering = new System.Windows.Forms.Label();
            this.pnlLogin = new System.Windows.Forms.Panel();
            this.btnLoginCancel = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lblViewLoginPass = new System.Windows.Forms.Label();
            this.tbxLoginPassword = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbxLoginUsername = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.pnlWelcome = new System.Windows.Forms.Panel();
            this.btnWelcomeLogIn = new System.Windows.Forms.Button();
            this.btnWelcomeSignUp = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btnLogOut = new System.Windows.Forms.Button();
            this.pnlRegistration.SuspendLayout();
            this.pnlMessageDialog.SuspendLayout();
            this.pnlLogin.SuspendLayout();
            this.pnlWelcome.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRegister
            // 
            this.btnRegister.AutoSize = true;
            this.btnRegister.Location = new System.Drawing.Point(370, 376);
            this.btnRegister.MaximumSize = new System.Drawing.Size(75, 23);
            this.btnRegister.MinimumSize = new System.Drawing.Size(75, 23);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(75, 23);
            this.btnRegister.TabIndex = 0;
            this.btnRegister.Text = "Sign Up";
            this.btnRegister.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // tbxMessages
            // 
            this.tbxMessages.Location = new System.Drawing.Point(135, 73);
            this.tbxMessages.Multiline = true;
            this.tbxMessages.Name = "tbxMessages";
            this.tbxMessages.ReadOnly = true;
            this.tbxMessages.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxMessages.Size = new System.Drawing.Size(352, 165);
            this.tbxMessages.TabIndex = 1;
            // 
            // btnSendMessage
            // 
            this.btnSendMessage.Location = new System.Drawing.Point(493, 258);
            this.btnSendMessage.Name = "btnSendMessage";
            this.btnSendMessage.Size = new System.Drawing.Size(75, 23);
            this.btnSendMessage.TabIndex = 2;
            this.btnSendMessage.Text = "Send";
            this.btnSendMessage.UseVisualStyleBackColor = true;
            this.btnSendMessage.Click += new System.EventHandler(this.btnSendMessage_Click);
            // 
            // tbxMessage
            // 
            this.tbxMessage.Location = new System.Drawing.Point(135, 260);
            this.tbxMessage.Name = "tbxMessage";
            this.tbxMessage.Size = new System.Drawing.Size(352, 20);
            this.tbxMessage.TabIndex = 3;
            this.tbxMessage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbxMessage_KeyPress);
            // 
            // pnlRegistration
            // 
            this.pnlRegistration.Controls.Add(this.btnCancelSignUp);
            this.pnlRegistration.Controls.Add(this.label13);
            this.pnlRegistration.Controls.Add(this.label12);
            this.pnlRegistration.Controls.Add(this.lblViewConfirmPass);
            this.pnlRegistration.Controls.Add(this.lblViewPass);
            this.pnlRegistration.Controls.Add(this.tbxFirstName);
            this.pnlRegistration.Controls.Add(this.label9);
            this.pnlRegistration.Controls.Add(this.tbxLastName);
            this.pnlRegistration.Controls.Add(this.label8);
            this.pnlRegistration.Controls.Add(this.tbxConfirmPassword);
            this.pnlRegistration.Controls.Add(this.label7);
            this.pnlRegistration.Controls.Add(this.tbxRegisterPassword);
            this.pnlRegistration.Controls.Add(this.label4);
            this.pnlRegistration.Controls.Add(this.tbxRegisterUsername);
            this.pnlRegistration.Controls.Add(this.label1);
            this.pnlRegistration.Controls.Add(this.btnRegister);
            this.pnlRegistration.Location = new System.Drawing.Point(12, 12);
            this.pnlRegistration.Name = "pnlRegistration";
            this.pnlRegistration.Size = new System.Drawing.Size(685, 407);
            this.pnlRegistration.TabIndex = 4;
            // 
            // btnCancelSignUp
            // 
            this.btnCancelSignUp.Location = new System.Drawing.Point(289, 376);
            this.btnCancelSignUp.Name = "btnCancelSignUp";
            this.btnCancelSignUp.Size = new System.Drawing.Size(75, 23);
            this.btnCancelSignUp.TabIndex = 14;
            this.btnCancelSignUp.Text = "Cancel";
            this.btnCancelSignUp.UseVisualStyleBackColor = true;
            this.btnCancelSignUp.Click += new System.EventHandler(this.btnCancelSignUp_Click);
            // 
            // label13
            // 
            this.label13.Image = global::ClientApp.Properties.Resources.icon_logo128;
            this.label13.Location = new System.Drawing.Point(268, 30);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(144, 123);
            this.label13.TabIndex = 13;
            this.label13.Text = " ";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label12.Location = new System.Drawing.Point(227, 160);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(230, 26);
            this.label12.TabIndex = 12;
            this.label12.Text = "Create a New Account";
            // 
            // lblViewConfirmPass
            // 
            this.lblViewConfirmPass.Image = global::ClientApp.Properties.Resources.icon_view16;
            this.lblViewConfirmPass.Location = new System.Drawing.Point(451, 338);
            this.lblViewConfirmPass.Name = "lblViewConfirmPass";
            this.lblViewConfirmPass.Size = new System.Drawing.Size(16, 13);
            this.lblViewConfirmPass.TabIndex = 11;
            this.lblViewConfirmPass.Text = " ";
            this.lblViewConfirmPass.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblViewConfirmPass_MouseDown);
            this.lblViewConfirmPass.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lblViewConfirmPass_MouseUp);
            // 
            // lblViewPass
            // 
            this.lblViewPass.Image = global::ClientApp.Properties.Resources.icon_view16;
            this.lblViewPass.Location = new System.Drawing.Point(451, 308);
            this.lblViewPass.Name = "lblViewPass";
            this.lblViewPass.Size = new System.Drawing.Size(16, 13);
            this.lblViewPass.TabIndex = 10;
            this.lblViewPass.Text = " ";
            this.lblViewPass.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblViewPass_MouseDown);
            this.lblViewPass.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lblViewPass_MouseUp);
            // 
            // tbxFirstName
            // 
            this.tbxFirstName.Location = new System.Drawing.Point(325, 217);
            this.tbxFirstName.MaxLength = 50;
            this.tbxFirstName.Name = "tbxFirstName";
            this.tbxFirstName.Size = new System.Drawing.Size(120, 20);
            this.tbxFirstName.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(226, 220);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(60, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "First Name:";
            // 
            // tbxLastName
            // 
            this.tbxLastName.Location = new System.Drawing.Point(325, 247);
            this.tbxLastName.MaxLength = 50;
            this.tbxLastName.Name = "tbxLastName";
            this.tbxLastName.Size = new System.Drawing.Size(120, 20);
            this.tbxLastName.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(226, 248);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Last Name:";
            // 
            // tbxConfirmPassword
            // 
            this.tbxConfirmPassword.Location = new System.Drawing.Point(325, 335);
            this.tbxConfirmPassword.MaxLength = 14;
            this.tbxConfirmPassword.Name = "tbxConfirmPassword";
            this.tbxConfirmPassword.Size = new System.Drawing.Size(120, 20);
            this.tbxConfirmPassword.TabIndex = 5;
            this.tbxConfirmPassword.UseSystemPasswordChar = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(226, 338);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Confirm Password:";
            // 
            // tbxRegisterPassword
            // 
            this.tbxRegisterPassword.Location = new System.Drawing.Point(325, 305);
            this.tbxRegisterPassword.MaxLength = 14;
            this.tbxRegisterPassword.Name = "tbxRegisterPassword";
            this.tbxRegisterPassword.Size = new System.Drawing.Size(120, 20);
            this.tbxRegisterPassword.TabIndex = 3;
            this.tbxRegisterPassword.UseSystemPasswordChar = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(226, 308);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Password:";
            // 
            // tbxRegisterUsername
            // 
            this.tbxRegisterUsername.Location = new System.Drawing.Point(325, 276);
            this.tbxRegisterUsername.MaxLength = 50;
            this.tbxRegisterUsername.Name = "tbxRegisterUsername";
            this.tbxRegisterUsername.Size = new System.Drawing.Size(120, 20);
            this.tbxRegisterUsername.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(226, 279);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username:";
            // 
            // pnlMessageDialog
            // 
            this.pnlMessageDialog.Controls.Add(this.label3);
            this.pnlMessageDialog.Controls.Add(this.btnLogOut);
            this.pnlMessageDialog.Controls.Add(this.lbxUsers);
            this.pnlMessageDialog.Controls.Add(this.lbxChannels);
            this.pnlMessageDialog.Controls.Add(this.cbxEnterSendsMessage);
            this.pnlMessageDialog.Controls.Add(this.label2);
            this.pnlMessageDialog.Controls.Add(this.tbxMessage);
            this.pnlMessageDialog.Controls.Add(this.btnSendMessage);
            this.pnlMessageDialog.Controls.Add(this.tbxMessages);
            this.pnlMessageDialog.Location = new System.Drawing.Point(12, 12);
            this.pnlMessageDialog.Name = "pnlMessageDialog";
            this.pnlMessageDialog.Size = new System.Drawing.Size(685, 407);
            this.pnlMessageDialog.TabIndex = 5;
            // 
            // cbxEnterSendsMessage
            // 
            this.cbxEnterSendsMessage.AutoSize = true;
            this.cbxEnterSendsMessage.Location = new System.Drawing.Point(135, 287);
            this.cbxEnterSendsMessage.Name = "cbxEnterSendsMessage";
            this.cbxEnterSendsMessage.Size = new System.Drawing.Size(172, 17);
            this.cbxEnterSendsMessage.TabIndex = 4;
            this.cbxEnterSendsMessage.Text = "Pressing enter sends message.";
            this.cbxEnterSendsMessage.UseVisualStyleBackColor = true;
            this.cbxEnterSendsMessage.CheckedChanged += new System.EventHandler(this.cbxEnterSendsMessage_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(576, 243);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Users:";
            // 
            // lbxChannels
            // 
            this.lbxChannels.FormattingEnabled = true;
            this.lbxChannels.Location = new System.Drawing.Point(517, 122);
            this.lbxChannels.Name = "lbxChannels";
            this.lbxChannels.Size = new System.Drawing.Size(95, 95);
            this.lbxChannels.TabIndex = 2;
            // 
            // lbxUsers
            // 
            this.lbxUsers.FormattingEnabled = true;
            this.lbxUsers.Location = new System.Drawing.Point(576, 262);
            this.lbxUsers.Name = "lbxUsers";
            this.lbxUsers.Size = new System.Drawing.Size(95, 95);
            this.lbxUsers.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(517, 106);
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
            // pnlLogin
            // 
            this.pnlLogin.Controls.Add(this.btnLoginCancel);
            this.pnlLogin.Controls.Add(this.label14);
            this.pnlLogin.Controls.Add(this.label15);
            this.pnlLogin.Controls.Add(this.lblViewLoginPass);
            this.pnlLogin.Controls.Add(this.tbxLoginPassword);
            this.pnlLogin.Controls.Add(this.label5);
            this.pnlLogin.Controls.Add(this.tbxLoginUsername);
            this.pnlLogin.Controls.Add(this.label6);
            this.pnlLogin.Controls.Add(this.btnLogin);
            this.pnlLogin.Location = new System.Drawing.Point(12, 12);
            this.pnlLogin.Name = "pnlLogin";
            this.pnlLogin.Size = new System.Drawing.Size(685, 407);
            this.pnlLogin.TabIndex = 5;
            // 
            // btnLoginCancel
            // 
            this.btnLoginCancel.Location = new System.Drawing.Point(268, 315);
            this.btnLoginCancel.Name = "btnLoginCancel";
            this.btnLoginCancel.Size = new System.Drawing.Size(75, 23);
            this.btnLoginCancel.TabIndex = 16;
            this.btnLoginCancel.Text = "Cancel";
            this.btnLoginCancel.UseVisualStyleBackColor = true;
            this.btnLoginCancel.Click += new System.EventHandler(this.btnLoginCancel_Click);
            // 
            // label14
            // 
            this.label14.Image = global::ClientApp.Properties.Resources.icon_logo128;
            this.label14.Location = new System.Drawing.Point(268, 30);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(144, 123);
            this.label14.TabIndex = 15;
            this.label14.Text = " ";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label15.Location = new System.Drawing.Point(299, 160);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(65, 26);
            this.label15.TabIndex = 14;
            this.label15.Text = "Login";
            // 
            // lblViewLoginPass
            // 
            this.lblViewLoginPass.Image = global::ClientApp.Properties.Resources.icon_view16;
            this.lblViewLoginPass.Location = new System.Drawing.Point(430, 262);
            this.lblViewLoginPass.Name = "lblViewLoginPass";
            this.lblViewLoginPass.Size = new System.Drawing.Size(16, 13);
            this.lblViewLoginPass.TabIndex = 12;
            this.lblViewLoginPass.Text = " ";
            this.lblViewLoginPass.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblViewLoginPass_MouseDown);
            this.lblViewLoginPass.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lblViewLoginPass_MouseUp);
            // 
            // tbxLoginPassword
            // 
            this.tbxLoginPassword.Location = new System.Drawing.Point(304, 259);
            this.tbxLoginPassword.MaxLength = 14;
            this.tbxLoginPassword.Name = "tbxLoginPassword";
            this.tbxLoginPassword.Size = new System.Drawing.Size(120, 20);
            this.tbxLoginPassword.TabIndex = 3;
            this.tbxLoginPassword.UseSystemPasswordChar = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(240, 262);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Password:";
            // 
            // tbxLoginUsername
            // 
            this.tbxLoginUsername.Location = new System.Drawing.Point(304, 230);
            this.tbxLoginUsername.MaxLength = 50;
            this.tbxLoginUsername.Name = "tbxLoginUsername";
            this.tbxLoginUsername.Size = new System.Drawing.Size(120, 20);
            this.tbxLoginUsername.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(240, 233);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Username:";
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(349, 315);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 0;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // pnlWelcome
            // 
            this.pnlWelcome.Controls.Add(this.btnWelcomeLogIn);
            this.pnlWelcome.Controls.Add(this.btnWelcomeSignUp);
            this.pnlWelcome.Controls.Add(this.label11);
            this.pnlWelcome.Controls.Add(this.label10);
            this.pnlWelcome.Location = new System.Drawing.Point(12, 12);
            this.pnlWelcome.Name = "pnlWelcome";
            this.pnlWelcome.Size = new System.Drawing.Size(685, 407);
            this.pnlWelcome.TabIndex = 9;
            // 
            // btnWelcomeLogIn
            // 
            this.btnWelcomeLogIn.Image = global::ClientApp.Properties.Resources.icon_logIn32;
            this.btnWelcomeLogIn.Location = new System.Drawing.Point(250, 264);
            this.btnWelcomeLogIn.Name = "btnWelcomeLogIn";
            this.btnWelcomeLogIn.Size = new System.Drawing.Size(75, 71);
            this.btnWelcomeLogIn.TabIndex = 2;
            this.btnWelcomeLogIn.Text = "Log In";
            this.btnWelcomeLogIn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnWelcomeLogIn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnWelcomeLogIn.UseVisualStyleBackColor = true;
            this.btnWelcomeLogIn.Click += new System.EventHandler(this.btnWelcomeLogIn_Click);
            // 
            // btnWelcomeSignUp
            // 
            this.btnWelcomeSignUp.Image = global::ClientApp.Properties.Resources.icon_signUp32;
            this.btnWelcomeSignUp.Location = new System.Drawing.Point(350, 264);
            this.btnWelcomeSignUp.Name = "btnWelcomeSignUp";
            this.btnWelcomeSignUp.Size = new System.Drawing.Size(75, 71);
            this.btnWelcomeSignUp.TabIndex = 3;
            this.btnWelcomeSignUp.Text = "Sign Up";
            this.btnWelcomeSignUp.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnWelcomeSignUp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnWelcomeSignUp.UseVisualStyleBackColor = true;
            this.btnWelcomeSignUp.Click += new System.EventHandler(this.btnWelcomeSignUp_Click);
            // 
            // label11
            // 
            this.label11.Image = global::ClientApp.Properties.Resources.icon_logo128;
            this.label11.Location = new System.Drawing.Point(268, 30);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(144, 123);
            this.label11.TabIndex = 1;
            this.label11.Text = " ";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label10.Location = new System.Drawing.Point(224, 171);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(225, 26);
            this.label10.TabIndex = 0;
            this.label10.Text = "Welcome to ChatApp!";
            // 
            // btnLogOut
            // 
            this.btnLogOut.Image = global::ClientApp.Properties.Resources.icon_logOut32;
            this.btnLogOut.Location = new System.Drawing.Point(596, 16);
            this.btnLogOut.Name = "btnLogOut";
            this.btnLogOut.Size = new System.Drawing.Size(75, 71);
            this.btnLogOut.TabIndex = 5;
            this.btnLogOut.Text = "Log Out";
            this.btnLogOut.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnLogOut.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnLogOut.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 431);
            this.Controls.Add(this.pnlMessageDialog);
            this.Controls.Add(this.pnlWelcome);
            this.Controls.Add(this.pnlRegistration);
            this.Controls.Add(this.pnlLogin);
            this.Controls.Add(this.lblRegistering);
            this.Controls.Add(this.pbrRegistering);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "ChatApp";
            this.pnlRegistration.ResumeLayout(false);
            this.pnlRegistration.PerformLayout();
            this.pnlMessageDialog.ResumeLayout(false);
            this.pnlMessageDialog.PerformLayout();
            this.pnlLogin.ResumeLayout(false);
            this.pnlLogin.PerformLayout();
            this.pnlWelcome.ResumeLayout(false);
            this.pnlWelcome.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.TextBox tbxMessages;
        private System.Windows.Forms.Button btnSendMessage;
        private System.Windows.Forms.TextBox tbxMessage;
        private System.Windows.Forms.Panel pnlRegistration;
        private System.Windows.Forms.TextBox tbxRegisterUsername;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlMessageDialog;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox lbxChannels;
        private System.Windows.Forms.ListBox lbxUsers;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbxEnterSendsMessage;
        private System.Windows.Forms.ProgressBar pbrRegistering;
        private System.Windows.Forms.Label lblRegistering;
        private System.Windows.Forms.TextBox tbxRegisterPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel pnlLogin;
        private System.Windows.Forms.TextBox tbxLoginPassword;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbxLoginUsername;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox tbxConfirmPassword;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbxFirstName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbxLastName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblViewPass;
        private System.Windows.Forms.Label lblViewConfirmPass;
        private System.Windows.Forms.Label lblViewLoginPass;
        private System.Windows.Forms.Panel pnlWelcome;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnWelcomeSignUp;
        private System.Windows.Forms.Button btnWelcomeLogIn;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnCancelSignUp;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnLoginCancel;
        private System.Windows.Forms.Button btnLogOut;
    }
}

