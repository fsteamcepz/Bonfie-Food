namespace BonfieFood
{
    partial class Chatbot
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Chatbot));
            this.guna2GradientPanel1 = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.panel_fill = new System.Windows.Forms.Panel();
            this.guna2Panel_bottom = new Guna.UI2.WinForms.Guna2Panel();
            this.timer_answerBot = new System.Windows.Forms.Label();
            this.btnSend = new Guna.UI2.WinForms.Guna2PictureBox();
            this.messageFromUser = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2Panel_left = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Panel4 = new Guna.UI2.WinForms.Guna2Panel();
            this.sidebarBtn = new FontAwesome.Sharp.IconPictureBox();
            this.newChat_btn = new Guna.UI2.WinForms.Guna2PictureBox();
            this.label_notfound = new System.Windows.Forms.Label();
            this.guna2Panel_between = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.label_history = new System.Windows.Forms.Label();
            this.timer_botResponse = new System.Windows.Forms.Timer(this.components);
            this.sidebarTransitions = new System.Windows.Forms.Timer(this.components);
            this.toolTip_newChat = new Guna.UI2.WinForms.Guna2HtmlToolTip();
            this.toolTip_closeSidebar = new Guna.UI2.WinForms.Guna2HtmlToolTip();
            this.toolTip_timer = new Guna.UI2.WinForms.Guna2HtmlToolTip();
            this.guna2GradientPanel1.SuspendLayout();
            this.guna2Panel_bottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnSend)).BeginInit();
            this.guna2Panel_left.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sidebarBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.newChat_btn)).BeginInit();
            this.guna2Panel_between.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2GradientPanel1
            // 
            this.guna2GradientPanel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2GradientPanel1.Controls.Add(this.panel_fill);
            this.guna2GradientPanel1.Controls.Add(this.guna2Panel_bottom);
            this.guna2GradientPanel1.Controls.Add(this.guna2Panel_left);
            this.guna2GradientPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2GradientPanel1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(15)))), ((int)(((byte)(25)))));
            this.guna2GradientPanel1.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(21)))), ((int)(((byte)(45)))));
            this.guna2GradientPanel1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
            this.guna2GradientPanel1.Location = new System.Drawing.Point(0, 0);
            this.guna2GradientPanel1.Name = "guna2GradientPanel1";
            this.guna2GradientPanel1.Size = new System.Drawing.Size(720, 500);
            this.guna2GradientPanel1.TabIndex = 0;
            // 
            // panel_fill
            // 
            this.panel_fill.AutoScroll = true;
            this.panel_fill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_fill.Location = new System.Drawing.Point(142, 0);
            this.panel_fill.Name = "panel_fill";
            this.panel_fill.Size = new System.Drawing.Size(578, 442);
            this.panel_fill.TabIndex = 0;
            // 
            // guna2Panel_bottom
            // 
            this.guna2Panel_bottom.Controls.Add(this.timer_answerBot);
            this.guna2Panel_bottom.Controls.Add(this.btnSend);
            this.guna2Panel_bottom.Controls.Add(this.messageFromUser);
            this.guna2Panel_bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.guna2Panel_bottom.Location = new System.Drawing.Point(142, 442);
            this.guna2Panel_bottom.Name = "guna2Panel_bottom";
            this.guna2Panel_bottom.Size = new System.Drawing.Size(578, 58);
            this.guna2Panel_bottom.TabIndex = 98;
            // 
            // timer_answerBot
            // 
            this.timer_answerBot.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.timer_answerBot.AutoSize = true;
            this.timer_answerBot.Font = new System.Drawing.Font("NSimSun", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.timer_answerBot.ForeColor = System.Drawing.Color.Gainsboro;
            this.timer_answerBot.Location = new System.Drawing.Point(520, 23);
            this.timer_answerBot.Name = "timer_answerBot";
            this.timer_answerBot.Size = new System.Drawing.Size(35, 11);
            this.timer_answerBot.TabIndex = 55;
            this.timer_answerBot.Text = "timer";
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.BackColor = System.Drawing.Color.Transparent;
            this.btnSend.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSend.Image = global::BonfieFood.Properties.Resources.send_active;
            this.btnSend.ImageRotate = 0F;
            this.btnSend.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSend.Location = new System.Drawing.Point(489, 19);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(15, 15);
            this.btnSend.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnSend.TabIndex = 95;
            this.btnSend.TabStop = false;
            this.btnSend.UseTransparentBackground = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            this.btnSend.MouseEnter += new System.EventHandler(this.btnSend_MouseEnter);
            this.btnSend.MouseLeave += new System.EventHandler(this.btnSend_MouseLeave);
            this.btnSend.MouseHover += new System.EventHandler(this.btnSend_MouseHover);
            // 
            // messageFromUser
            // 
            this.messageFromUser.AcceptsReturn = true;
            this.messageFromUser.AcceptsTab = true;
            this.messageFromUser.Animated = true;
            this.messageFromUser.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(67)))), ((int)(((byte)(83)))));
            this.messageFromUser.BorderRadius = 10;
            this.messageFromUser.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.messageFromUser.DefaultText = "";
            this.messageFromUser.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.messageFromUser.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.messageFromUser.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.messageFromUser.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.messageFromUser.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(19)))), ((int)(((byte)(37)))));
            this.messageFromUser.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(107)))), ((int)(((byte)(123)))));
            this.messageFromUser.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.messageFromUser.ForeColor = System.Drawing.Color.Gainsboro;
            this.messageFromUser.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(107)))), ((int)(((byte)(123)))));
            this.messageFromUser.IconRight = global::BonfieFood.Properties.Resources.invisible_pas;
            this.messageFromUser.IconRightSize = new System.Drawing.Size(28, 20);
            this.messageFromUser.Location = new System.Drawing.Point(62, 10);
            this.messageFromUser.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.messageFromUser.Multiline = true;
            this.messageFromUser.Name = "messageFromUser";
            this.messageFromUser.PasswordChar = '\0';
            this.messageFromUser.PlaceholderForeColor = System.Drawing.Color.Gainsboro;
            this.messageFromUser.PlaceholderText = "Write a message...";
            this.messageFromUser.SelectedText = "";
            this.messageFromUser.Size = new System.Drawing.Size(453, 36);
            this.messageFromUser.TabIndex = 94;
            this.messageFromUser.TextOffset = new System.Drawing.Point(3, 0);
            this.messageFromUser.TextChanged += new System.EventHandler(this.messageFromUser_TextChanged);
            // 
            // guna2Panel_left
            // 
            this.guna2Panel_left.Controls.Add(this.guna2Panel4);
            this.guna2Panel_left.Controls.Add(this.sidebarBtn);
            this.guna2Panel_left.Controls.Add(this.newChat_btn);
            this.guna2Panel_left.Controls.Add(this.label_notfound);
            this.guna2Panel_left.Controls.Add(this.guna2Panel_between);
            this.guna2Panel_left.Controls.Add(this.label_history);
            this.guna2Panel_left.Dock = System.Windows.Forms.DockStyle.Left;
            this.guna2Panel_left.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel_left.Name = "guna2Panel_left";
            this.guna2Panel_left.Size = new System.Drawing.Size(142, 500);
            this.guna2Panel_left.TabIndex = 97;
            // 
            // guna2Panel4
            // 
            this.guna2Panel4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.guna2Panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(67)))), ((int)(((byte)(83)))));
            this.guna2Panel4.Location = new System.Drawing.Point(141, 0);
            this.guna2Panel4.Name = "guna2Panel4";
            this.guna2Panel4.Size = new System.Drawing.Size(1, 500);
            this.guna2Panel4.TabIndex = 116;
            // 
            // sidebarBtn
            // 
            this.sidebarBtn.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.sidebarBtn.BackColor = System.Drawing.Color.Transparent;
            this.sidebarBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.sidebarBtn.ForeColor = System.Drawing.Color.Gainsboro;
            this.sidebarBtn.IconChar = FontAwesome.Sharp.IconChar.ChevronLeft;
            this.sidebarBtn.IconColor = System.Drawing.Color.Gainsboro;
            this.sidebarBtn.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.sidebarBtn.IconSize = 22;
            this.sidebarBtn.Location = new System.Drawing.Point(115, 25);
            this.sidebarBtn.Margin = new System.Windows.Forms.Padding(3, 3, 6, 3);
            this.sidebarBtn.Name = "sidebarBtn";
            this.sidebarBtn.Size = new System.Drawing.Size(22, 22);
            this.sidebarBtn.TabIndex = 115;
            this.sidebarBtn.TabStop = false;
            this.sidebarBtn.Click += new System.EventHandler(this.sidebarBtn_Click);
            // 
            // newChat_btn
            // 
            this.newChat_btn.BackColor = System.Drawing.Color.Transparent;
            this.newChat_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.newChat_btn.Image = global::BonfieFood.Properties.Resources.newchat_active;
            this.newChat_btn.ImageRotate = 0F;
            this.newChat_btn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.newChat_btn.Location = new System.Drawing.Point(4, 461);
            this.newChat_btn.Name = "newChat_btn";
            this.newChat_btn.Size = new System.Drawing.Size(22, 22);
            this.newChat_btn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.newChat_btn.TabIndex = 96;
            this.newChat_btn.TabStop = false;
            this.newChat_btn.UseTransparentBackground = true;
            this.newChat_btn.Click += new System.EventHandler(this.newChat_btn_Click);
            this.newChat_btn.MouseEnter += new System.EventHandler(this.newChat_btn_MouseEnter);
            this.newChat_btn.MouseLeave += new System.EventHandler(this.newChat_btn_MouseLeave);
            this.newChat_btn.MouseHover += new System.EventHandler(this.newChat_btn_MouseHover);
            // 
            // label_notfound
            // 
            this.label_notfound.Font = new System.Drawing.Font("Constantia", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_notfound.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(67)))), ((int)(((byte)(83)))));
            this.label_notfound.Location = new System.Drawing.Point(0, 231);
            this.label_notfound.Name = "label_notfound";
            this.label_notfound.Size = new System.Drawing.Size(142, 49);
            this.label_notfound.TabIndex = 113;
            this.label_notfound.Text = "History not found";
            this.label_notfound.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_notfound.Visible = false;
            // 
            // guna2Panel_between
            // 
            this.guna2Panel_between.Controls.Add(this.guna2Panel1);
            this.guna2Panel_between.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2Panel_between.Location = new System.Drawing.Point(0, 49);
            this.guna2Panel_between.Name = "guna2Panel_between";
            this.guna2Panel_between.Size = new System.Drawing.Size(142, 25);
            this.guna2Panel_between.TabIndex = 0;
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(67)))), ((int)(((byte)(83)))));
            this.guna2Panel1.Location = new System.Drawing.Point(0, 17);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(142, 1);
            this.guna2Panel1.TabIndex = 105;
            // 
            // label_history
            // 
            this.label_history.Cursor = System.Windows.Forms.Cursors.Default;
            this.label_history.Dock = System.Windows.Forms.DockStyle.Top;
            this.label_history.Font = new System.Drawing.Font("Constantia", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_history.ForeColor = System.Drawing.Color.Gainsboro;
            this.label_history.Location = new System.Drawing.Point(0, 0);
            this.label_history.Name = "label_history";
            this.label_history.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.label_history.Size = new System.Drawing.Size(142, 49);
            this.label_history.TabIndex = 65;
            this.label_history.Text = "History";
            this.label_history.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // timer_botResponse
            // 
            this.timer_botResponse.Tick += new System.EventHandler(this.timer_answerBot_Tick);
            // 
            // sidebarTransitions
            // 
            this.sidebarTransitions.Interval = 1;
            this.sidebarTransitions.Tick += new System.EventHandler(this.sidebarBtn_Tick);
            // 
            // toolTip_newChat
            // 
            this.toolTip_newChat.AllowLinksHandling = true;
            this.toolTip_newChat.AutoPopDelay = 5000;
            this.toolTip_newChat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(15)))), ((int)(((byte)(40)))));
            this.toolTip_newChat.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(67)))), ((int)(((byte)(83)))));
            this.toolTip_newChat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.toolTip_newChat.InitialDelay = 100;
            this.toolTip_newChat.MaximumSize = new System.Drawing.Size(0, 0);
            this.toolTip_newChat.ReshowDelay = 100;
            // 
            // toolTip_closeSidebar
            // 
            this.toolTip_closeSidebar.AllowLinksHandling = true;
            this.toolTip_closeSidebar.AutoPopDelay = 5000;
            this.toolTip_closeSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(15)))), ((int)(((byte)(40)))));
            this.toolTip_closeSidebar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(67)))), ((int)(((byte)(83)))));
            this.toolTip_closeSidebar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.toolTip_closeSidebar.InitialDelay = 100;
            this.toolTip_closeSidebar.MaximumSize = new System.Drawing.Size(0, 0);
            this.toolTip_closeSidebar.ReshowDelay = 100;
            // 
            // toolTip_timer
            // 
            this.toolTip_timer.AllowLinksHandling = true;
            this.toolTip_timer.AutoPopDelay = 5000;
            this.toolTip_timer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(15)))), ((int)(((byte)(40)))));
            this.toolTip_timer.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(67)))), ((int)(((byte)(83)))));
            this.toolTip_timer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(250)))));
            this.toolTip_timer.InitialDelay = 100;
            this.toolTip_timer.MaximumSize = new System.Drawing.Size(0, 0);
            this.toolTip_timer.ReshowDelay = 100;
            // 
            // Chatbot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(15)))), ((int)(((byte)(26)))));
            this.ClientSize = new System.Drawing.Size(720, 500);
            this.Controls.Add(this.guna2GradientPanel1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(21)))), ((int)(((byte)(45)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Chatbot";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chatbot";
            this.Load += new System.EventHandler(this.ChatBot_Load);
            this.guna2GradientPanel1.ResumeLayout(false);
            this.guna2Panel_bottom.ResumeLayout(false);
            this.guna2Panel_bottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnSend)).EndInit();
            this.guna2Panel_left.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sidebarBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.newChat_btn)).EndInit();
            this.guna2Panel_between.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2GradientPanel guna2GradientPanel1;
        private Guna.UI2.WinForms.Guna2TextBox messageFromUser;
        private Guna.UI2.WinForms.Guna2PictureBox btnSend;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel_left;
        private System.Windows.Forms.Panel panel_fill;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel_bottom;
        private System.Windows.Forms.Label timer_answerBot;
        private System.Windows.Forms.Timer timer_botResponse;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel_between;
        private System.Windows.Forms.Label label_history;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private System.Windows.Forms.Label label_notfound;
        private Guna.UI2.WinForms.Guna2PictureBox newChat_btn;
        private FontAwesome.Sharp.IconPictureBox sidebarBtn;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel4;
        private System.Windows.Forms.Timer sidebarTransitions;
        private Guna.UI2.WinForms.Guna2HtmlToolTip toolTip_newChat;
        private Guna.UI2.WinForms.Guna2HtmlToolTip toolTip_closeSidebar;
        private Guna.UI2.WinForms.Guna2HtmlToolTip toolTip_timer;
    }
}