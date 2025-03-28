namespace BonfieFood
{
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Panel_main = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            this.versionApp = new System.Windows.Forms.Label();
            this.guna2Panel_btns = new Guna.UI2.WinForms.Guna2Panel();
            this.EmailAndPass = new FontAwesome.Sharp.IconButton();
            this.personalInfo = new FontAwesome.Sharp.IconButton();
            this.guna2Panel3 = new Guna.UI2.WinForms.Guna2Panel();
            this.delete_photo = new FontAwesome.Sharp.IconPictureBox();
            this.imgBox = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.upload_img = new Guna.UI2.WinForms.Guna2Button();
            this.toolTip_delete = new Guna.UI2.WinForms.Guna2HtmlToolTip();
            this.toolTip_Version = new Guna.UI2.WinForms.Guna2HtmlToolTip();
            this.guna2Panel1.SuspendLayout();
            this.guna2Panel2.SuspendLayout();
            this.guna2Panel_btns.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.delete_photo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgBox)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.guna2Panel1.Controls.Add(this.guna2Panel_main);
            this.guna2Panel1.Controls.Add(this.guna2Panel2);
            this.guna2Panel1.Location = new System.Drawing.Point(12, 12);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(696, 476);
            this.guna2Panel1.TabIndex = 60;
            // 
            // guna2Panel_main
            // 
            this.guna2Panel_main.Location = new System.Drawing.Point(207, 3);
            this.guna2Panel_main.Name = "guna2Panel_main";
            this.guna2Panel_main.Size = new System.Drawing.Size(486, 470);
            this.guna2Panel_main.TabIndex = 61;
            // 
            // guna2Panel2
            // 
            this.guna2Panel2.BackColor = System.Drawing.Color.Transparent;
            this.guna2Panel2.BorderRadius = 10;
            this.guna2Panel2.Controls.Add(this.versionApp);
            this.guna2Panel2.Controls.Add(this.guna2Panel_btns);
            this.guna2Panel2.Controls.Add(this.guna2Panel3);
            this.guna2Panel2.Controls.Add(this.delete_photo);
            this.guna2Panel2.Controls.Add(this.imgBox);
            this.guna2Panel2.Controls.Add(this.upload_img);
            this.guna2Panel2.CustomBorderColor = System.Drawing.Color.Transparent;
            this.guna2Panel2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(20)))), ((int)(((byte)(85)))));
            this.guna2Panel2.Location = new System.Drawing.Point(3, 3);
            this.guna2Panel2.Name = "guna2Panel2";
            this.guna2Panel2.Size = new System.Drawing.Size(187, 470);
            this.guna2Panel2.TabIndex = 60;
            // 
            // versionApp
            // 
            this.versionApp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.versionApp.AutoSize = true;
            this.versionApp.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.versionApp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.versionApp.Location = new System.Drawing.Point(134, 444);
            this.versionApp.Name = "versionApp";
            this.versionApp.Size = new System.Drawing.Size(45, 15);
            this.versionApp.TabIndex = 80;
            this.versionApp.Text = "25.0.1";
            this.versionApp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // guna2Panel_btns
            // 
            this.guna2Panel_btns.Controls.Add(this.EmailAndPass);
            this.guna2Panel_btns.Controls.Add(this.personalInfo);
            this.guna2Panel_btns.Location = new System.Drawing.Point(0, 283);
            this.guna2Panel_btns.Name = "guna2Panel_btns";
            this.guna2Panel_btns.Size = new System.Drawing.Size(187, 110);
            this.guna2Panel_btns.TabIndex = 79;
            // 
            // EmailAndPass
            // 
            this.EmailAndPass.Dock = System.Windows.Forms.DockStyle.Top;
            this.EmailAndPass.FlatAppearance.BorderSize = 0;
            this.EmailAndPass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EmailAndPass.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.EmailAndPass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.EmailAndPass.IconChar = FontAwesome.Sharp.IconChar.Lock;
            this.EmailAndPass.IconColor = System.Drawing.Color.AliceBlue;
            this.EmailAndPass.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.EmailAndPass.IconSize = 31;
            this.EmailAndPass.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.EmailAndPass.Location = new System.Drawing.Point(0, 50);
            this.EmailAndPass.Name = "EmailAndPass";
            this.EmailAndPass.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.EmailAndPass.Size = new System.Drawing.Size(187, 50);
            this.EmailAndPass.TabIndex = 80;
            this.EmailAndPass.Text = "Email ＆ Password";
            this.EmailAndPass.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.EmailAndPass.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.EmailAndPass.UseVisualStyleBackColor = true;
            this.EmailAndPass.Click += new System.EventHandler(this.UsernameAndPass_Click);
            // 
            // personalInfo
            // 
            this.personalInfo.AutoSize = true;
            this.personalInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.personalInfo.FlatAppearance.BorderSize = 0;
            this.personalInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.personalInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.personalInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.personalInfo.IconChar = FontAwesome.Sharp.IconChar.UserAlt;
            this.personalInfo.IconColor = System.Drawing.Color.AliceBlue;
            this.personalInfo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.personalInfo.IconSize = 31;
            this.personalInfo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.personalInfo.Location = new System.Drawing.Point(0, 0);
            this.personalInfo.Name = "personalInfo";
            this.personalInfo.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.personalInfo.Size = new System.Drawing.Size(187, 50);
            this.personalInfo.TabIndex = 79;
            this.personalInfo.Text = "Personal Information";
            this.personalInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.personalInfo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.personalInfo.UseVisualStyleBackColor = true;
            this.personalInfo.Click += new System.EventHandler(this.personalInfo_Click);
            // 
            // guna2Panel3
            // 
            this.guna2Panel3.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.guna2Panel3.ForeColor = System.Drawing.Color.Tomato;
            this.guna2Panel3.Location = new System.Drawing.Point(11, 253);
            this.guna2Panel3.Name = "guna2Panel3";
            this.guna2Panel3.Size = new System.Drawing.Size(164, 1);
            this.guna2Panel3.TabIndex = 78;
            // 
            // delete_photo
            // 
            this.delete_photo.BackColor = System.Drawing.Color.Transparent;
            this.delete_photo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.delete_photo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.delete_photo.IconChar = FontAwesome.Sharp.IconChar.TrashAlt;
            this.delete_photo.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.delete_photo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.delete_photo.IconSize = 30;
            this.delete_photo.Location = new System.Drawing.Point(152, 195);
            this.delete_photo.Name = "delete_photo";
            this.delete_photo.Size = new System.Drawing.Size(30, 30);
            this.delete_photo.TabIndex = 77;
            this.delete_photo.TabStop = false;
            this.delete_photo.Click += new System.EventHandler(this.delete_photo_Click);
            // 
            // imgBox
            // 
            this.imgBox.BackColor = System.Drawing.Color.Transparent;
            this.imgBox.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(55)))), ((int)(((byte)(110)))));
            this.imgBox.ImageRotate = 0F;
            this.imgBox.Location = new System.Drawing.Point(11, 10);
            this.imgBox.Name = "imgBox";
            this.imgBox.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.imgBox.Size = new System.Drawing.Size(164, 164);
            this.imgBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.imgBox.TabIndex = 76;
            this.imgBox.TabStop = false;
            this.imgBox.UseTransparentBackground = true;
            // 
            // upload_img
            // 
            this.upload_img.Animated = true;
            this.upload_img.BackColor = System.Drawing.Color.Transparent;
            this.upload_img.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.upload_img.BorderRadius = 10;
            this.upload_img.BorderThickness = 2;
            this.upload_img.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.upload_img.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.upload_img.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.upload_img.DisabledState.ForeColor = System.Drawing.Color.DarkGray;
            this.upload_img.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(20)))), ((int)(((byte)(85)))));
            this.upload_img.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.upload_img.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.upload_img.HoverState.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.upload_img.Location = new System.Drawing.Point(28, 194);
            this.upload_img.Name = "upload_img";
            this.upload_img.Size = new System.Drawing.Size(118, 32);
            this.upload_img.TabIndex = 75;
            this.upload_img.Text = "Download";
            this.upload_img.UseTransparentBackground = true;
            this.upload_img.Click += new System.EventHandler(this.upload_img_Click);
            // 
            // toolTip_delete
            // 
            this.toolTip_delete.AllowLinksHandling = true;
            this.toolTip_delete.AutoPopDelay = 5000;
            this.toolTip_delete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(15)))), ((int)(((byte)(40)))));
            this.toolTip_delete.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(67)))), ((int)(((byte)(83)))));
            this.toolTip_delete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.toolTip_delete.InitialDelay = 100;
            this.toolTip_delete.MaximumSize = new System.Drawing.Size(0, 0);
            this.toolTip_delete.ReshowDelay = 100;
            // 
            // toolTip_Version
            // 
            this.toolTip_Version.AllowLinksHandling = true;
            this.toolTip_Version.AutoPopDelay = 5000;
            this.toolTip_Version.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(15)))), ((int)(((byte)(40)))));
            this.toolTip_Version.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(67)))), ((int)(((byte)(83)))));
            this.toolTip_Version.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.toolTip_Version.InitialDelay = 100;
            this.toolTip_Version.MaximumSize = new System.Drawing.Size(0, 0);
            this.toolTip_Version.ReshowDelay = 100;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(12)))), ((int)(((byte)(70)))));
            this.ClientSize = new System.Drawing.Size(720, 500);
            this.Controls.Add(this.guna2Panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel2.ResumeLayout(false);
            this.guna2Panel2.PerformLayout();
            this.guna2Panel_btns.ResumeLayout(false);
            this.guna2Panel_btns.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.delete_photo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
        private FontAwesome.Sharp.IconPictureBox delete_photo;
        private Guna.UI2.WinForms.Guna2CirclePictureBox imgBox;
        private Guna.UI2.WinForms.Guna2Button upload_img;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel3;
        private FontAwesome.Sharp.IconButton personalInfo;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel_btns;
        private FontAwesome.Sharp.IconButton EmailAndPass;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel_main;
        private Guna.UI2.WinForms.Guna2HtmlToolTip toolTip_delete;
        private System.Windows.Forms.Label versionApp;
        private Guna.UI2.WinForms.Guna2HtmlToolTip toolTip_Version;
    }
}