namespace BonfieFood
{
    partial class Settings_UsernamePass
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings_UsernamePass));
            this.guna2Panel_personalInfo = new Guna.UI2.WinForms.Guna2Panel();
            this.label_ConfirmPassword = new System.Windows.Forms.Label();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.iconConfirmPass = new Guna.UI2.WinForms.Guna2PictureBox();
            this.confirmPass = new Guna.UI2.WinForms.Guna2TextBox();
            this.iconNewPass = new Guna.UI2.WinForms.Guna2PictureBox();
            this.newPass = new Guna.UI2.WinForms.Guna2TextBox();
            this.label_Message_h2 = new System.Windows.Forms.Label();
            this.label_NewEmail = new System.Windows.Forms.Label();
            this.newEmail = new Guna.UI2.WinForms.Guna2TextBox();
            this.label_Email = new System.Windows.Forms.Label();
            this.currentEmail = new Guna.UI2.WinForms.Guna2TextBox();
            this.saveEmPass = new Guna.UI2.WinForms.Guna2Button();
            this.label_EmailPassword = new System.Windows.Forms.Label();
            this.label_NewPassword = new System.Windows.Forms.Label();
            this.label_CurrentPassword = new System.Windows.Forms.Label();
            this.currentPass = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2Panel_personalInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconConfirmPass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconNewPass)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2Panel_personalInfo
            // 
            this.guna2Panel_personalInfo.BackColor = System.Drawing.Color.Transparent;
            this.guna2Panel_personalInfo.BorderRadius = 10;
            this.guna2Panel_personalInfo.Controls.Add(this.label_ConfirmPassword);
            this.guna2Panel_personalInfo.Controls.Add(this.guna2Panel1);
            this.guna2Panel_personalInfo.Controls.Add(this.iconConfirmPass);
            this.guna2Panel_personalInfo.Controls.Add(this.confirmPass);
            this.guna2Panel_personalInfo.Controls.Add(this.iconNewPass);
            this.guna2Panel_personalInfo.Controls.Add(this.newPass);
            this.guna2Panel_personalInfo.Controls.Add(this.label_Message_h2);
            this.guna2Panel_personalInfo.Controls.Add(this.label_NewEmail);
            this.guna2Panel_personalInfo.Controls.Add(this.newEmail);
            this.guna2Panel_personalInfo.Controls.Add(this.label_Email);
            this.guna2Panel_personalInfo.Controls.Add(this.currentEmail);
            this.guna2Panel_personalInfo.Controls.Add(this.saveEmPass);
            this.guna2Panel_personalInfo.Controls.Add(this.label_EmailPassword);
            this.guna2Panel_personalInfo.Controls.Add(this.label_NewPassword);
            this.guna2Panel_personalInfo.Controls.Add(this.label_CurrentPassword);
            this.guna2Panel_personalInfo.Controls.Add(this.currentPass);
            this.guna2Panel_personalInfo.CustomBorderColor = System.Drawing.Color.Transparent;
            this.guna2Panel_personalInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel_personalInfo.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(20)))), ((int)(((byte)(85)))));
            this.guna2Panel_personalInfo.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel_personalInfo.Name = "guna2Panel_personalInfo";
            this.guna2Panel_personalInfo.Size = new System.Drawing.Size(486, 470);
            this.guna2Panel_personalInfo.TabIndex = 60;
            // 
            // label_ConfirmPassword
            // 
            this.label_ConfirmPassword.AutoSize = true;
            this.label_ConfirmPassword.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_ConfirmPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.label_ConfirmPassword.Location = new System.Drawing.Point(263, 313);
            this.label_ConfirmPassword.Name = "label_ConfirmPassword";
            this.label_ConfirmPassword.Size = new System.Drawing.Size(114, 17);
            this.label_ConfirmPassword.TabIndex = 96;
            this.label_ConfirmPassword.Text = "Confirm Password";
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.guna2Panel1.Location = new System.Drawing.Point(238, 142);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(1, 230);
            this.guna2Panel1.TabIndex = 91;
            // 
            // iconConfirmPass
            // 
            this.iconConfirmPass.BackColor = System.Drawing.Color.Transparent;
            this.iconConfirmPass.Image = global::BonfieFood.Properties.Resources.password_hide;
            this.iconConfirmPass.ImageRotate = 0F;
            this.iconConfirmPass.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.iconConfirmPass.Location = new System.Drawing.Point(410, 341);
            this.iconConfirmPass.Name = "iconConfirmPass";
            this.iconConfirmPass.Size = new System.Drawing.Size(35, 26);
            this.iconConfirmPass.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.iconConfirmPass.TabIndex = 90;
            this.iconConfirmPass.TabStop = false;
            this.iconConfirmPass.UseTransparentBackground = true;
            this.iconConfirmPass.Click += new System.EventHandler(this.btnShowConfirmPassword_Click);
            // 
            // confirmPass
            // 
            this.confirmPass.Animated = true;
            this.confirmPass.AutoRoundedCorners = true;
            this.confirmPass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(2)))));
            this.confirmPass.BackgroundImage = global::BonfieFood.Properties.Resources.password_hide;
            this.confirmPass.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(116)))), ((int)(((byte)(222)))));
            this.confirmPass.BorderRadius = 17;
            this.confirmPass.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.confirmPass.DefaultText = "";
            this.confirmPass.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.confirmPass.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.confirmPass.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.confirmPass.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.confirmPass.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(20)))), ((int)(((byte)(85)))));
            this.confirmPass.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.confirmPass.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.confirmPass.ForeColor = System.Drawing.Color.AliceBlue;
            this.confirmPass.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.confirmPass.IconRight = global::BonfieFood.Properties.Resources.invisible_pas;
            this.confirmPass.IconRightOffset = new System.Drawing.Point(7, 0);
            this.confirmPass.IconRightSize = new System.Drawing.Size(33, 25);
            this.confirmPass.Location = new System.Drawing.Point(261, 336);
            this.confirmPass.Name = "confirmPass";
            this.confirmPass.PasswordChar = '●';
            this.confirmPass.PlaceholderForeColor = System.Drawing.Color.AliceBlue;
            this.confirmPass.PlaceholderText = "None";
            this.confirmPass.SelectedText = "";
            this.confirmPass.Size = new System.Drawing.Size(196, 36);
            this.confirmPass.TabIndex = 89;
            this.confirmPass.TextOffset = new System.Drawing.Point(7, 0);
            // 
            // iconNewPass
            // 
            this.iconNewPass.BackColor = System.Drawing.Color.Transparent;
            this.iconNewPass.Image = global::BonfieFood.Properties.Resources.password_hide;
            this.iconNewPass.ImageRotate = 0F;
            this.iconNewPass.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.iconNewPass.Location = new System.Drawing.Point(410, 258);
            this.iconNewPass.Name = "iconNewPass";
            this.iconNewPass.Size = new System.Drawing.Size(35, 26);
            this.iconNewPass.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.iconNewPass.TabIndex = 88;
            this.iconNewPass.TabStop = false;
            this.iconNewPass.UseTransparentBackground = true;
            this.iconNewPass.Click += new System.EventHandler(this.btnShowPassword_Click);
            // 
            // newPass
            // 
            this.newPass.Animated = true;
            this.newPass.AutoRoundedCorners = true;
            this.newPass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(2)))));
            this.newPass.BackgroundImage = global::BonfieFood.Properties.Resources.password_hide;
            this.newPass.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(116)))), ((int)(((byte)(222)))));
            this.newPass.BorderRadius = 17;
            this.newPass.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.newPass.DefaultText = "";
            this.newPass.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.newPass.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.newPass.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.newPass.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.newPass.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(20)))), ((int)(((byte)(85)))));
            this.newPass.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.newPass.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.newPass.ForeColor = System.Drawing.Color.AliceBlue;
            this.newPass.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.newPass.IconRight = global::BonfieFood.Properties.Resources.invisible_pas;
            this.newPass.IconRightOffset = new System.Drawing.Point(7, 0);
            this.newPass.IconRightSize = new System.Drawing.Size(33, 25);
            this.newPass.Location = new System.Drawing.Point(261, 253);
            this.newPass.Name = "newPass";
            this.newPass.PasswordChar = '●';
            this.newPass.PlaceholderForeColor = System.Drawing.Color.AliceBlue;
            this.newPass.PlaceholderText = "None";
            this.newPass.SelectedText = "";
            this.newPass.Size = new System.Drawing.Size(196, 36);
            this.newPass.TabIndex = 87;
            this.newPass.TextOffset = new System.Drawing.Point(7, 0);
            // 
            // label_Message_h2
            // 
            this.label_Message_h2.Font = new System.Drawing.Font("Segoe UI Variable Text", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_Message_h2.ForeColor = System.Drawing.Color.Silver;
            this.label_Message_h2.Location = new System.Drawing.Point(16, 63);
            this.label_Message_h2.Name = "label_Message_h2";
            this.label_Message_h2.Size = new System.Drawing.Size(441, 58);
            this.label_Message_h2.TabIndex = 84;
            this.label_Message_h2.Text = "You have the option to update your email address and password as needed.";
            // 
            // label_NewEmail
            // 
            this.label_NewEmail.AutoSize = true;
            this.label_NewEmail.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_NewEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.label_NewEmail.Location = new System.Drawing.Point(23, 230);
            this.label_NewEmail.Name = "label_NewEmail";
            this.label_NewEmail.Size = new System.Drawing.Size(69, 17);
            this.label_NewEmail.TabIndex = 83;
            this.label_NewEmail.Text = "New Email";
            // 
            // newEmail
            // 
            this.newEmail.Animated = true;
            this.newEmail.AutoRoundedCorners = true;
            this.newEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(2)))));
            this.newEmail.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(116)))), ((int)(((byte)(222)))));
            this.newEmail.BorderRadius = 17;
            this.newEmail.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.newEmail.DefaultText = "";
            this.newEmail.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.newEmail.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.newEmail.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.newEmail.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.newEmail.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(20)))), ((int)(((byte)(85)))));
            this.newEmail.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.newEmail.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.newEmail.ForeColor = System.Drawing.Color.AliceBlue;
            this.newEmail.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.newEmail.Location = new System.Drawing.Point(21, 254);
            this.newEmail.Name = "newEmail";
            this.newEmail.PlaceholderForeColor = System.Drawing.Color.AliceBlue;
            this.newEmail.PlaceholderText = "None";
            this.newEmail.SelectedText = "";
            this.newEmail.ShadowDecoration.Color = System.Drawing.Color.Transparent;
            this.newEmail.Size = new System.Drawing.Size(196, 36);
            this.newEmail.TabIndex = 82;
            this.newEmail.TextOffset = new System.Drawing.Point(7, 0);
            // 
            // label_Email
            // 
            this.label_Email.AutoSize = true;
            this.label_Email.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_Email.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.label_Email.Location = new System.Drawing.Point(23, 142);
            this.label_Email.Name = "label_Email";
            this.label_Email.Size = new System.Drawing.Size(39, 17);
            this.label_Email.TabIndex = 81;
            this.label_Email.Text = "Email";
            // 
            // currentEmail
            // 
            this.currentEmail.Animated = true;
            this.currentEmail.AutoRoundedCorners = true;
            this.currentEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(2)))));
            this.currentEmail.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(116)))), ((int)(((byte)(222)))));
            this.currentEmail.BorderRadius = 17;
            this.currentEmail.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.currentEmail.DefaultText = "";
            this.currentEmail.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.currentEmail.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.currentEmail.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.currentEmail.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.currentEmail.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(20)))), ((int)(((byte)(85)))));
            this.currentEmail.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.currentEmail.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.currentEmail.ForeColor = System.Drawing.Color.AliceBlue;
            this.currentEmail.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.currentEmail.Location = new System.Drawing.Point(21, 166);
            this.currentEmail.Name = "currentEmail";
            this.currentEmail.PlaceholderForeColor = System.Drawing.Color.AliceBlue;
            this.currentEmail.PlaceholderText = "None";
            this.currentEmail.SelectedText = "";
            this.currentEmail.ShadowDecoration.Color = System.Drawing.Color.Transparent;
            this.currentEmail.Size = new System.Drawing.Size(196, 36);
            this.currentEmail.TabIndex = 80;
            this.currentEmail.TextOffset = new System.Drawing.Point(7, 0);
            // 
            // saveEmPass
            // 
            this.saveEmPass.Animated = true;
            this.saveEmPass.BackColor = System.Drawing.Color.Transparent;
            this.saveEmPass.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.saveEmPass.BorderRadius = 10;
            this.saveEmPass.BorderThickness = 2;
            this.saveEmPass.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.saveEmPass.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.saveEmPass.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.saveEmPass.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.saveEmPass.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(20)))), ((int)(((byte)(85)))));
            this.saveEmPass.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.saveEmPass.ForeColor = System.Drawing.Color.White;
            this.saveEmPass.HoverState.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.saveEmPass.Location = new System.Drawing.Point(190, 410);
            this.saveEmPass.Name = "saveEmPass";
            this.saveEmPass.Size = new System.Drawing.Size(102, 32);
            this.saveEmPass.TabIndex = 75;
            this.saveEmPass.Text = "Save";
            this.saveEmPass.UseTransparentBackground = true;
            this.saveEmPass.Click += new System.EventHandler(this.saveEmPass_Click);
            // 
            // label_EmailPassword
            // 
            this.label_EmailPassword.AutoSize = true;
            this.label_EmailPassword.Font = new System.Drawing.Font("Constantia", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_EmailPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.label_EmailPassword.Location = new System.Drawing.Point(15, 14);
            this.label_EmailPassword.Name = "label_EmailPassword";
            this.label_EmailPassword.Size = new System.Drawing.Size(250, 33);
            this.label_EmailPassword.TabIndex = 56;
            this.label_EmailPassword.Text = "Email ＆ Password";
            // 
            // label_NewPassword
            // 
            this.label_NewPassword.AutoSize = true;
            this.label_NewPassword.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_NewPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.label_NewPassword.Location = new System.Drawing.Point(263, 230);
            this.label_NewPassword.Name = "label_NewPassword";
            this.label_NewPassword.Size = new System.Drawing.Size(94, 17);
            this.label_NewPassword.TabIndex = 58;
            this.label_NewPassword.Text = "New Password";
            // 
            // label_CurrentPassword
            // 
            this.label_CurrentPassword.AutoSize = true;
            this.label_CurrentPassword.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_CurrentPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.label_CurrentPassword.Location = new System.Drawing.Point(263, 142);
            this.label_CurrentPassword.Name = "label_CurrentPassword";
            this.label_CurrentPassword.Size = new System.Drawing.Size(111, 17);
            this.label_CurrentPassword.TabIndex = 56;
            this.label_CurrentPassword.Text = "Current Password";
            // 
            // currentPass
            // 
            this.currentPass.Animated = true;
            this.currentPass.AutoRoundedCorners = true;
            this.currentPass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(2)))));
            this.currentPass.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(116)))), ((int)(((byte)(222)))));
            this.currentPass.BorderRadius = 17;
            this.currentPass.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.currentPass.DefaultText = "";
            this.currentPass.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.currentPass.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.currentPass.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.currentPass.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.currentPass.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(20)))), ((int)(((byte)(85)))));
            this.currentPass.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.currentPass.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.currentPass.ForeColor = System.Drawing.Color.AliceBlue;
            this.currentPass.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.currentPass.Location = new System.Drawing.Point(261, 166);
            this.currentPass.Name = "currentPass";
            this.currentPass.PlaceholderForeColor = System.Drawing.Color.AliceBlue;
            this.currentPass.PlaceholderText = "None";
            this.currentPass.SelectedText = "";
            this.currentPass.ShadowDecoration.Color = System.Drawing.Color.Transparent;
            this.currentPass.Size = new System.Drawing.Size(196, 36);
            this.currentPass.TabIndex = 44;
            this.currentPass.TextOffset = new System.Drawing.Point(7, 0);
            // 
            // Settings_UsernamePass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(12)))), ((int)(((byte)(70)))));
            this.ClientSize = new System.Drawing.Size(486, 470);
            this.Controls.Add(this.guna2Panel_personalInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Settings_UsernamePass";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings_UsernamePass";
            this.Load += new System.EventHandler(this.Settings_UsernamePass_Load);
            this.guna2Panel_personalInfo.ResumeLayout(false);
            this.guna2Panel_personalInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconConfirmPass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconNewPass)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel_personalInfo;
        private System.Windows.Forms.Label label_EmailPassword;
        private System.Windows.Forms.Label label_NewPassword;
        private System.Windows.Forms.Label label_CurrentPassword;
        private Guna.UI2.WinForms.Guna2Button saveEmPass;
        private System.Windows.Forms.Label label_NewEmail;
        private Guna.UI2.WinForms.Guna2TextBox newEmail;
        private System.Windows.Forms.Label label_Email;
        private Guna.UI2.WinForms.Guna2TextBox currentEmail;
        private System.Windows.Forms.Label label_Message_h2;
        private Guna.UI2.WinForms.Guna2PictureBox iconNewPass;
        private Guna.UI2.WinForms.Guna2TextBox newPass;
        private Guna.UI2.WinForms.Guna2TextBox currentPass;
        private Guna.UI2.WinForms.Guna2PictureBox iconConfirmPass;
        private Guna.UI2.WinForms.Guna2TextBox confirmPass;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private System.Windows.Forms.Label label_ConfirmPassword;
    }
}