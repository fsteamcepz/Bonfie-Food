using Guna.UI2.WinForms;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BonfieFood
{
    public partial class Chatbot : Form
    {
        DataBase db = new DataBase();
        Huggingface huggingface = new Huggingface();

        private const int MaxTextBoxHeight = 120;
        
        private DateTime startTime;
        private bool isTimerRunning = false;
        private int currentSessionId = -1;      // поточна сесія

        private Guna2Button selectedButton;     // вибрана сесія кнопки
        private bool isFastAnswer = false;
        private bool isBotResponse = false;
        private bool isEnterPressed = false;

        private bool isMouseOver = false;
        private bool isBtnClicked = false;
        private bool isMouseOverSend = false;
        private bool isBtnSendClicked = false;

        private bool sidebarExpand = true;
        private bool historyLoadedAfterSend = false;

        public Chatbot()
        {
            InitializeComponent();
        }
        private void ChatBot_Load(object sender, EventArgs e)
        {
            ConfigureUI();
            ConfigureNewChatButton();
            ConfigureMainPage();

            messageFromUser.KeyDown += OnMessageFromUserKeyDown;
            messageFromUser.Refresh();
            LoadSessionHistory();
            messageFromUser.Refresh();
            ConfigureSidebarBtn();
                        
            UpdateTexts();
            Language.OnLanguageChanged += ChangeLanguage;
        }
        private void ChangeLanguage(string cultureCode)
        {
            UpdateTexts();
        }
        private void ConfigureUI()
        {
            panel_fill.HorizontalScroll.Enabled = false;
            guna2Panel_left.HorizontalScroll.Enabled = false;

            messageFromUser.Location = new Point(205, 452);
            btnSend.Image = Properties.Resources.send_active;
            btnSend.Location = new Point(629, 462);
            newChat_btn.Visible = false;

            // встановлення батьківських елементів
            messageFromUser.Parent = guna2GradientPanel1;
            messageFromUser.BringToFront();
            btnSend.Parent = guna2GradientPanel1;
            btnSend.BringToFront();

            timer_answerBot.Text = "";
            timer_botResponse = new Timer();
            timer_botResponse.Tick += timer_answerBot_Tick;
        }
        private void ConfigureMainPage()
        {
            Guna2PictureBox logo_bonfie = new Guna2PictureBox
            {
                Size = new Size(263, 299),
                Location = new Point(6, 66),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                SizeMode = PictureBoxSizeMode.AutoSize,
                Image = Properties.Resources.logo_chatbot
            };

            Guna2PictureBox text_bonfie = new Guna2PictureBox
            {
                Size = new Size(225, 147),
                Location = new Point(290, 133),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                SizeMode = PictureBoxSizeMode.Zoom,
                Image = Properties.Resources.bonfie
            };

            panel_fill.Controls.Add(text_bonfie);
            panel_fill.Controls.Add(logo_bonfie);
        }
        private void ChatInputReadOnly()
        {
            messageFromUser.ReadOnly = true;
            messageFromUser.Enabled = false;
            messageFromUser.DisabledState.FillColor = Color.FromArgb(22, 19, 37);
            messageFromUser.DisabledState.BorderColor = Color.FromArgb(70, 67, 83);

            btnSend.Enabled = false;
            messageFromUser.Refresh();
        }
        private void ChatInputNotReadOnly()
        {
            messageFromUser.ReadOnly = false;
            messageFromUser.Enabled = true;
            messageFromUser.DisabledState.FillColor = Color.FromArgb(22, 19, 37);
            messageFromUser.DisabledState.BorderColor = Color.FromArgb(70, 67, 83);

            btnSend.Enabled = true;
        }
        private void newChat_btn_Click(object sender, EventArgs e)
        {           
            newChat_btn.Image = Properties.Resources.newchat_inactive;

            isMouseOver = false;
            isBtnClicked = true;

            isFastAnswer = false;
            timer_answerBot.Text = "";
            messageFromUser.Text = "";
            btnSend.Image = Properties.Resources.send_active;

            panel_fill.Controls.Clear();

            if (selectedButton != null)
            {
                selectedButton.FillColor = Color.FromArgb(127, 99, 209);
                selectedButton = null;
            }

            CheckNewChatButtonClickable();
            ChatInputNotReadOnly();
            ConfigureMainPage();

            currentSessionId = -1;
        }
        private void newChat_btn_MouseEnter(object sender, EventArgs e)
        {
            if (!isBtnClicked)
            {
                newChat_btn.Image = Properties.Resources.newchat_hover;
            }
        }
        private void newChat_btn_MouseHover(object sender, EventArgs e)
        {
            newChat_btn.Image = Properties.Resources.newchat_hover;
        }
        private void newChat_btn_MouseLeave(object sender, EventArgs e)
        {
            if (!isMouseOver && !isBtnClicked)
            {
                newChat_btn.Image = Properties.Resources.newchat_active;
            }
        }
        private void timer_answerBot_Tick(object sender, EventArgs e)
        {
            if (isTimerRunning)
            {
                // розрахунок часу
                TimeSpan elapsed = DateTime.Now - startTime;

                // оновлення Label
                timer_answerBot.Text = $"{elapsed.TotalSeconds:0.00}";
            }
        }

        private void sidebarBtn_Tick(object sender, EventArgs e)
        {
            // Цільові координати та розміри для messageFromUser
            Point locCollapsed = new Point(97, 452); // Згорнутий стан
            Point locExpanded = new Point(205, 452); // Розгорнутий стан
            Size sizeCollapsed = new Size(561, 36);       // Згорнутий стан
            Size sizeExpanded = new Size(453, 36);        // Розгорнутий стан

            if (sidebarExpand)
            {
                guna2Panel_left.Width -= 10;

                if (guna2Panel_left.Width <= 33)
                {
                    sidebarExpand = false;
                    sidebarTransitions.Stop();
                    toolTip_closeSidebar.SetToolTip(sidebarBtn, Properties.Resources.toolTip_openSidebar);
                }

                label_History.Hide();
                guna2Panel_between.Hide();
                label_NotFound.Hide();
                sidebarBtn.IconChar = FontAwesome.Sharp.IconChar.ChevronRight;

                foreach (var control in guna2Panel_left.Controls)
                {
                    if (control is Guna2Button button)
                    {
                        button.Visible = sidebarExpand;
                    }
                }

                if (messageFromUser.Location.X > locCollapsed.X)
                {
                    messageFromUser.Location = new Point(messageFromUser.Location.X - 10, messageFromUser.Location.Y);
                }
                if (messageFromUser.Width < sizeCollapsed.Width)
                {
                    messageFromUser.Size = new Size(messageFromUser.Width + 10, messageFromUser.Height);
                }
            }
            else
            {
                guna2Panel_left.Width += 10;

                if (guna2Panel_left.Width >= 142)
                {
                    sidebarExpand = true;
                    toolTip_closeSidebar.SetToolTip(sidebarBtn, Properties.Resources.toolTip_closeSidebar);

                    label_History.TextAlign = ContentAlignment.BottomLeft;
                    label_History.Padding = new Padding(10, 0, 0, 0);
                    label_History.Show();

                    bool hasButton = guna2Panel_left.Controls.OfType<Guna2Button>().Any();
                    if (!hasButton)
                    {
                        label_NotFound.Show();
                    }
                    else
                    {
                        label_NotFound.Hide();
                    }

                    guna2Panel_between.Show();
                    ShowButtons();

                    if (!historyLoadedAfterSend)
                    {
                        LoadSessionHistory();
                    }

                    sidebarTransitions.Stop();
                }
                sidebarBtn.IconChar = FontAwesome.Sharp.IconChar.ChevronLeft;

                if (messageFromUser.Location.X < locExpanded.X)
                {
                    messageFromUser.Location = new Point(messageFromUser.Location.X + 10, messageFromUser.Location.Y);
                }
                if (messageFromUser.Width > sizeExpanded.Width)
                {
                    messageFromUser.Size = new Size(messageFromUser.Width - 10, messageFromUser.Height);
                }
            }
        }
        private void ShowButtons()
        {
            var timer = new Timer { Interval = 10 };
            int opacity = 0;
            timer.Tick += (s, e) =>
            {
                foreach (var control in guna2Panel_left.Controls)
                {
                    if (control is Guna2Button button)
                    {
                        button.Visible = true;
                        button.ForeColor = Color.FromArgb(opacity, button.ForeColor);
                    }
                }

                opacity += 25;
                if (opacity >= 255)
                {
                    timer.Stop();
                }
            };
            timer.Start();
        }
        private void sidebarBtn_Click(object sender, EventArgs e)
        {
            sidebarTransitions.Start();
        }
        private void ConfigureSidebarBtn()
        {
            sidebarBtn.MouseEnter += (sender, e) =>
            {
                sidebarBtn.IconColor = Color.FromArgb(100, 88, 240);
            };

            sidebarBtn.MouseLeave += (sender, e) =>
            {
                sidebarBtn.IconColor = Color.Gainsboro;
            };
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            isMouseOverSend = false;
            isBtnSendClicked = true;
            btnSend.Image = Properties.Resources.send_active;

            if (!sidebarExpand)
            {
                await HandleSendMessage();
                historyLoadedAfterSend = false;
                return;
            }

            await HandleSendMessage();
            LoadSessionHistory();
            historyLoadedAfterSend = true;
        }
        private void btnSend_MouseEnter(object sender, EventArgs e)
        {
            isMouseOverSend = true;
            if (!isBtnSendClicked)
            {
                btnSend.Image = Properties.Resources.send_hover;
            }
        }
        private void btnSend_MouseHover(object sender, EventArgs e)
        {
            isMouseOverSend = true;
            if (!isBtnSendClicked)
            {
                btnSend.Image = Properties.Resources.send_hover;
            }
        }
        private void btnSend_MouseLeave(object sender, EventArgs e)
        {
            isMouseOverSend = false;
            if (!isBtnSendClicked)
            {
                btnSend.Image = Properties.Resources.send_active;
            }
        }
        private async Task HandleSendMessage()
        {
            if (isBotResponse)
            {
                return;
            }

            string userText = messageFromUser.Text.Trim();
            if (string.IsNullOrWhiteSpace(userText))
            {
                MessageBoxError.Show("Повідомлення має містити щонайменше 1 символ.");
                messageFromUser.Text = "";
                isBtnSendClicked = false;
                return;
            }
            if (userText.Length > 3000)
            {
                MessageBoxError.Show("Повідомлення занадто довге. Максимальна довжина - 3000 символів!");
                isBtnSendClicked = false;
                return;
            }
            panel_fill.Controls.Clear();

            isTimerRunning = true;

            if (currentSessionId == -1)
            {
                currentSessionId = CreateOrGetCurrentSession();
            }

            btnSend.Image = Properties.Resources.send_inactive;
            btnSend.Enabled = false;
            btnSend.Cursor = Cursors.Default;
            ChatInputReadOnly();

            isBtnSendClicked = true;

            startTime = DateTime.Now;
            isTimerRunning = true;
            timer_botResponse.Start();

            SaveMessageToDB(currentSessionId, userText, true, DateTime.Now);

            string botAnswer = await huggingface.GetResponse(userText);

            isTimerRunning = false;
            timer_botResponse.Stop();
            TimeSpan elapsed = DateTime.Now - startTime;
            timer_answerBot.Text = $"{elapsed.TotalSeconds:0.00}";

            SaveMessageToDB(currentSessionId, botAnswer, false, DateTime.Now);

            displayQuestionAnswer(userText, botAnswer);
            messageFromUser.Clear();

            if (sidebarExpand)
            {
                LoadSessionHistory();
            }
            CheckNewChatButtonClickable();

            newChat_btn.Visible = true;
            newChat_btn.Image = Properties.Resources.newchat_active;

            isBtnSendClicked = false;
            btnSend.Image = Properties.Resources.send_active;
            btnSend.Cursor = Cursors.Hand;
            ChatInputNotReadOnly();

            isBotResponse = false;
        }
        public void SaveMessageToDB(int idSession, string messageText, bool isFromUser, DateTime messageTime)
        {
            string queryInsert = @"
                                 INSERT INTO MessagesChatbot (id_Session, messageText, isFromUser, messageTime)
                                 VALUES (@idSession, @messageText, @isFromUser, @messageTime)";
            using (SqlCommand cmd = new SqlCommand(queryInsert, db.getConnection()))
            {
                cmd.Parameters.AddWithValue("@idSession", idSession);
                cmd.Parameters.AddWithValue("@messageText", messageText);
                cmd.Parameters.AddWithValue("@isFromUser", isFromUser);
                cmd.Parameters.AddWithValue("@messageTime", messageTime);
                db.openConnection();
                cmd.ExecuteNonQuery();
                db.closeConnection();
            }
        }
        public string GetUserImgPath()
        {
            string imagePath = "";
            string sql = "SELECT profilePhotoPath FROM UserPhoto WHERE id_User = @UserId";

            using (SqlCommand cmd = new SqlCommand(sql, db.getConnection()))
            {
                cmd.Parameters.AddWithValue("@UserId", CurrentUser.UserId);
                db.openConnection();
                object result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    imagePath = result.ToString();
                }
                db.closeConnection();
            }
            return imagePath;
        }

        private List<DateTime> GetSessionDates()
        {
            List<DateTime> sessionDates = new List<DateTime>();

            string query = @"
                            SELECT startTime
                            FROM Sessions
                            WHERE id_User = @UserId
                            ORDER BY startTime DESC";

            using (SqlCommand cmd = new SqlCommand(query, db.getConnection()))
            {
                cmd.Parameters.AddWithValue("@UserId", CurrentUser.UserId);

                db.openConnection();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DateTime sessionDate = reader.GetDateTime(0);
                        sessionDates.Add(sessionDate);
                    }
                }
            }
            db.closeConnection();

            return sessionDates;
        }
        private void LoadSessionHistory()
        {
            for (int i = guna2Panel_left.Controls.Count - 1; i >= 0; i--)
            {
                if (guna2Panel_left.Controls[i] is Guna2Button)
                {
                    guna2Panel_left.Controls.RemoveAt(i);
                }
            }
            if (selectedButton != null)
            {
                selectedButton.FillColor = Color.FromArgb(127, 99, 209);
                selectedButton = null;
            }

            List<DateTime> sessionDates = GetSessionDates()
                .OrderByDescending(date => date)    // сортуємо за спаданням
                .Take(7)                            // лише перші 7 записів
                .ToList();

            if (sessionDates.Count < 1)
            {
                label_NotFound.Visible = true;
            }
            else
            {
                label_NotFound.Visible = false;
            }

            int marginTop = 10;
            int initialTop = guna2Panel_between.Bottom + marginTop;

            int sessionNumber = GetSessionDates().Count;

            foreach (DateTime sessionDate in sessionDates)
            {
                // ідентифікатор сесії за датою
                int sessionId = GetSessionIdByDate(sessionDate);

                string buttonText;
                if (sessionNumber > 99)
                {
                    buttonText = $"{Properties.Resources.textSession} {sessionNumber} {sessionDate:dd.MM}";
                }
                else if (sessionNumber > 9)
                {
                    buttonText = $"{Properties.Resources.textSession} {sessionNumber}   {sessionDate:dd.MM}";
                }
                else
                {
                    buttonText = $"{Properties.Resources.textSession} {sessionNumber}     {sessionDate:dd.MM}";
                }
                Guna2Button sessionButton = new Guna2Button
                {
                    Text = buttonText,
                    Font = new Font("Segoe UI", 9.75F),
                    ForeColor = Color.Gainsboro,
                    Animated = true,
                    BorderRadius = 15,
                    FillColor = Color.FromArgb(127, 99, 209),
                    BorderThickness = 0,
                    TextAlign = HorizontalAlignment.Left,
                    AutoSize = false,
                    Width = guna2Panel_left.Width - 20,
                    Height = 40,
                    Location = new Point(10, initialTop),
                    Tag = sessionId
                };

                sessionButton.Click += SessionButton_Click;

                guna2Panel_left.Controls.Add(sessionButton);

                sessionNumber--;

                initialTop += sessionButton.Height + marginTop;
            }
        }
        private int GetSessionIdByDate(DateTime sessionDate)
        {
            Debug.WriteLine($"Пошук ID сесії для дати: {sessionDate}");

            string query = @"
                            SELECT idSession 
                            FROM Sessions 
                            WHERE CONVERT(VARCHAR, startTime, 120) = CONVERT(VARCHAR, @StartTime, 120)
                            AND id_User = @UserId";

            using (SqlCommand cmd = new SqlCommand(query, db.getConnection()))
            {
                cmd.Parameters.AddWithValue("@StartTime", sessionDate);
                cmd.Parameters.AddWithValue("@UserId", CurrentUser.UserId);

                db.openConnection();
                object result = cmd.ExecuteScalar();
                db.closeConnection();

                int sessionId = result != null ? Convert.ToInt32(result) : -1;
                Debug.WriteLine($"ID сесії для дати {sessionDate}: {sessionId}");
                return sessionId;
            }
        }
        private void SessionButton_Click(object sender, EventArgs e)
        {
            if (isEnterPressed)
            {
                return;
            }
            isFastAnswer = true;

            isMouseOverSend = false;
            isBtnSendClicked = false;
            btnSend.Image = Properties.Resources.send_inactive;

            newChat_btn.Visible = true;
            isMouseOver = false;
            isBtnClicked = false;
            newChat_btn.Image = Properties.Resources.newchat_active;

            timer_answerBot.Text = "";
            messageFromUser.Text = "";
            
            messageFromUser.Refresh();

            if (selectedButton != null)
            {
                selectedButton.FillColor = Color.FromArgb(127, 99, 209);
            }

            selectedButton = (Guna2Button)sender;
            selectedButton.FillColor = Color.FromArgb(100, 88, 240);

            panel_fill.Controls.Clear();

            int sessionId = (int)((Guna2Button)sender).Tag;

            //MessageBoxError.Show($"Кнопка сесії натиснута. ID сесії: {sessionId}");

            LoadSessionMessages(sessionId);
            ChatInputReadOnly();
            messageFromUser.Refresh();
            ConfigureNewChatButton();
        }
        private void LoadSessionMessages(int sessionId)
        {
            // завантажимо всі повідомлення в список (була проблема з відкриттям 2го з'єднання до БД)
            List<(string messageText, bool isFromUser, DateTime messageTime)> messages = new List<(string, bool, DateTime)>();

            string query = @"
                            SELECT messageText, isFromUser, messageTime 
                            FROM MessagesChatbot 
                            WHERE id_Session = @SessionId 
                            ORDER BY messageTime";

            using (SqlCommand cmd = new SqlCommand(query, db.getConnection()))
            {
                cmd.Parameters.AddWithValue("@SessionId", sessionId);

                db.openConnection();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string messageText = reader.GetString(0);
                        bool isFromUser = reader.GetBoolean(1);
                        DateTime messageTime = reader.GetDateTime(2);
                        messages.Add((messageText, isFromUser, messageTime));
                    }
                }
                db.closeConnection();
            }

            string previousUserMessage = "";
            foreach (var message in messages)
            {
                if (message.isFromUser)
                {
                    if (!string.IsNullOrEmpty(previousUserMessage))
                    {
                        displayQuestionAnswer(previousUserMessage, "");
                    }
                    previousUserMessage = message.messageText;
                }
                else
                {
                    displayQuestionAnswer(previousUserMessage, message.messageText);
                    previousUserMessage = "";
                }
            }

            if (!string.IsNullOrEmpty(previousUserMessage))
            {
                displayQuestionAnswer(previousUserMessage, "");
            }
        }
        private int CreateOrGetCurrentSession()
        {
            if (currentSessionId != -1)
            {
                return currentSessionId;
            }

            string query = @"
                           INSERT INTO Sessions (id_User) 
                           VALUES (@id_User);
                           SELECT SCOPE_IDENTITY();";   // SCOPE_IDENTITY - номер останнього доданого запису

            using (SqlCommand cmd = new SqlCommand(query, db.getConnection()))
            {
                cmd.Parameters.AddWithValue("@id_User", CurrentUser.UserId);
                db.openConnection();
                int sessionId = Convert.ToInt32(cmd.ExecuteScalar());
                db.closeConnection();
                return sessionId;
            }
        }

        private async Task DisplayTypingEffect(Guna2Button botAnswer, string fullText)
        {
            botAnswer.Text = "";
            for (int i = 0; i <= fullText.Length; i++)
            {
                botAnswer.Text = fullText.Substring(0, i);
                await Task.Delay(20);       // швидкість друку
            }
        }
        private async void OnMessageFromUserKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Повністю блокуємо обробку клавіші Enter
                e.SuppressKeyPress = true;
                e.Handled = true;
                isEnterPressed = true;  // Встановлюємо прапорець

                if (e.Shift)
                {
                    isEnterPressed = false;
                    return;
                }
                if (!isBotResponse && messageFromUser.Enabled)
                {
                    await HandleSendMessage();
                }
                messageFromUser.Focus();
                isEnterPressed = false;
            }
        }
        
        private async void displayQuestionAnswer(string userText, string answer)
        {
            int topAva = 20;
            int rightAva = 35;
            int padding = 6;
            int maxWidthUserBtn = 400;
            int maxWidthBotBtn = 450;

            using (Graphics g = CreateGraphics())
            {
                Font fontUser = new Font("Segoe UI", 9.75F, FontStyle.Bold);
                Font fontBot = new Font("Segoe UI", 9.75F);

                string formattedUserText = FormatText(userText, maxWidthUserBtn, fontUser, g);
                string formattedBotAnswer = FormatText(answer, maxWidthBotBtn, fontBot, g);

                SizeF userTextSize = g.MeasureString(formattedUserText, fontUser, maxWidthUserBtn);
                SizeF botTextSize = g.MeasureString(formattedBotAnswer, fontBot, maxWidthBotBtn);

                int userButtonWidth = Math.Max((int)Math.Ceiling(userTextSize.Width) + 27, 50);
                userButtonWidth = Math.Min(userButtonWidth, maxWidthUserBtn);

                int botButtonWidth = Math.Max((int)Math.Ceiling(botTextSize.Width) + 29, 50);
                botButtonWidth = Math.Min(botButtonWidth, maxWidthBotBtn);

                int nextYPosition = panel_fill.Controls.Count == 0
                    ? topAva
                    : panel_fill.Controls.Cast<Control>().Max(control => control.Bottom) + topAva;

                Guna2CirclePictureBox avaUser = new Guna2CirclePictureBox
                {
                    Size = new Size(32, 32),
                    FillColor = Color.FromArgb(67, 55, 110),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Anchor = AnchorStyles.Right | AnchorStyles.Top
                };

                string imgPath = GetUserImgPath();
                if (!string.IsNullOrEmpty(imgPath) && File.Exists(imgPath))
                {
                    avaUser.Image = Image.FromFile(imgPath);
                }

                Guna2Button userQuestion = new Guna2Button
                {
                    Text = formattedUserText,
                    Font = fontUser,
                    ForeColor = Color.Gainsboro,
                    Animated = false,
                    BorderRadius = 15,
                    FillColor = Color.FromArgb(77, 56, 162),
                    BorderThickness = 0,
                    TextAlign = HorizontalAlignment.Left,
                    AutoSize = false,
                    Width = userButtonWidth,
                    Height = (int)Math.Ceiling(userTextSize.Height) + 20,
                    Anchor = AnchorStyles.Right | AnchorStyles.Top
                };

                // розташовуємо за шириною panel_fill
                avaUser.Location = new Point(panel_fill.Width - avaUser.Width - rightAva, nextYPosition);
                userQuestion.Location = new Point(avaUser.Left - userQuestion.Width - padding, nextYPosition);

                panel_fill.Controls.Add(avaUser);
                panel_fill.Controls.Add(userQuestion);

                Guna2CirclePictureBox avaBot = new Guna2CirclePictureBox
                {
                    Size = new Size(32, 32),
                    FillColor = Color.FromArgb(67, 55, 110),
                    Image = Properties.Resources.Pikachu_ChatBot,
                    SizeMode = PictureBoxSizeMode.StretchImage
                };

                Guna2Button botAnswer = new Guna2Button
                {
                    Text = "",
                    Font = fontBot,
                    ForeColor = Color.Gainsboro,
                    Animated = false,
                    BorderRadius = 15,
                    FillColor = Color.FromArgb(59, 24, 79),
                    BorderThickness = 0,
                    TextAlign = HorizontalAlignment.Left,
                    AutoSize = false,
                    Width = botButtonWidth,
                    Height = (int)Math.Ceiling(botTextSize.Height) + 20
                };

                avaBot.Location = new Point(20, userQuestion.Bottom + topAva);
                botAnswer.Location = new Point(avaBot.Right + padding, avaBot.Top);

                panel_fill.Controls.Add(avaBot);
                panel_fill.Controls.Add(botAnswer);

                bool isShortAnswer = answer.Length > 350;
                if (isFastAnswer || isShortAnswer)
                {
                    botAnswer.Text = formattedBotAnswer;
                }
                else
                {
                    await DisplayTypingEffect(botAnswer, formattedBotAnswer);
                }

                panel_fill.ScrollControlIntoView(botAnswer);
            }
        }

        private string FormatText(string text, int maxWidth, Font font, Graphics g)
        {
            var words = text.Split(' ');
            var formattedText = new StringBuilder();
            var currentLine = "";

            foreach (var word in words)
            {
                var testLine = string.IsNullOrEmpty(currentLine) ? word : $"{currentLine} {word}";
                var textSize = g.MeasureString(testLine, font, maxWidth);

                if (textSize.Width > maxWidth)
                {
                    if (!string.IsNullOrEmpty(currentLine))
                    {
                        formattedText.AppendLine(currentLine);
                    }
                    currentLine = word;
                }
                else
                {
                    currentLine = testLine;
                }
            }
            if (!string.IsNullOrEmpty(currentLine))
            {
                formattedText.AppendLine(currentLine);
            }

            return formattedText.ToString().TrimEnd();
        }
        private void messageFromUser_TextChanged(object sender, EventArgs e)
        {
            AdjustTextBoxHeight(messageFromUser);
        }
        private void AdjustTextBoxHeight(Guna2TextBox textBox)
        {
            int padding = 3;

            // визначаємо необхідну висоту для тексту
            using (Graphics g = textBox.CreateGraphics())
            {
                SizeF textSize = g.MeasureString(textBox.Text, textBox.Font, textBox.Width - padding * 2);
                int newHeight = Math.Max(36, (int)Math.Ceiling(textSize.Height) + padding * 2);

                if (newHeight > MaxTextBoxHeight)
                {
                    newHeight = MaxTextBoxHeight;
                    textBox.ScrollBars = ScrollBars.Vertical;
                }
                else
                {
                    textBox.ScrollBars = ScrollBars.None;
                }

                // змінюємо висоту TextBox
                int heightDifference = newHeight - textBox.Height;
                textBox.Height = newHeight;

                // переміщуємоTextBox, щоб воно залишалось видимим на обох панелях
                textBox.Top -= heightDifference;
            }
        }
        private void ConfigureNewChatButton()
        {
            newChat_btn.MouseEnter += (sender, e) =>
            {
                newChat_btn.ForeColor = Color.FromArgb(69, 56, 240);
            };

            newChat_btn.MouseLeave += (sender, e) =>
            {
                newChat_btn.ForeColor = Color.FromArgb(253, 235, 250);
            };

            CheckNewChatButtonClickable();
        }
        private void CheckNewChatButtonClickable()
        {
            if (panel_fill.Controls.Count > 0)
            {
                newChat_btn.Enabled = true;
                newChat_btn.Cursor = Cursors.Hand;
            }
            else
            {
                newChat_btn.Enabled = false;
                newChat_btn.Cursor = Cursors.Default;
            }
        }

        private void UpdateTexts()
        {
            label_History.Text = Properties.Resources.label_History;
            toolTip_closeSidebar.SetToolTip(sidebarBtn, Properties.Resources.toolTip_closeSidebar);
            toolTip_newChat.SetToolTip(newChat_btn, Properties.Resources.toolTip_newChat);
            toolTip_timer.SetToolTip(timer_answerBot, Properties.Resources.toolTip_timer);
            messageFromUser.PlaceholderText = Properties.Resources.messageFromUser;
        }
    }
}