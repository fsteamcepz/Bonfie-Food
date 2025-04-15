using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;
using System.IO;
using System.Globalization;
using System.Drawing.Imaging;
using System.Reflection;
using System.Collections.Generic;
using System.Windows.Documents;

namespace BonfieFood
{
    public partial class Home : Form
    {
        DataBase db = new DataBase();

        private IconButton currentBtn;
        private Panel leftBorderBtn;
        private Color pinkBar = Color.FromArgb(220, 45, 140);
        private Form currentForm;
        private Image originalImg;
        private Timer timer;

        private int dailyRate;
        private Dictionary<string, double> caloriesOfPeriodDay = new Dictionary<string, double>();
        private int maxLineWidth = 185;
        private int lineHeight = 12;
        private int minWidth = 15;

        public Home()
        {
            InitializeComponent();
            InitializeTimer();
        }
        private void Home_Load(object sender, EventArgs e)
        {
            LoadUserImg();
            // створення панелі з межою
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(5, 50);
            guna2Panel4.Controls.Add(leftBorderBtn);

            // масив кнопок
            IconButton[] btnsOnForm = { recipesBtn, productsBtn, chatbotBtn, scannerBtn, calendarBtn, settingsBtn };
            foreach (IconButton i in btnsOnForm)
            {
                SetupButtonColorEffects(i);
            }

            LoadDataNutrition();
            LoadDataGoals();

            TransparentLabels();
            ConfigureLogOut();
            ConfigureProfileImg();
        }
        private void SettingsForm_PhotoUpdated(object sender, EventArgs e)
        {
            LoadUserImg();
            //MessageBox.Show("miniImg оновилося на формі Home!");
        }
        private void TransparentLabels()
        {
            // Харчування
            label_Nutrition.Parent = guna2PictureBox1;
            label_Nutrition.BackColor = Color.Transparent;
            label_Nutrition.Location = new Point(12, 15);

            // Назва тижня
            label_dayWeekName.Parent = guna2PictureBox2;
            label_dayWeekName.BackColor = Color.Transparent;
            label_dayWeekName.Location = new Point(0, 50);

            // Дата (September 22, 2024")
            label_date.Parent = guna2PictureBox2;
            label_date.BackColor = Color.Transparent;
            label_date.Location = new Point(-7, 75);

            // Рецепт
            label_Recipe.Parent = guna2PictureBox3;
            label_Recipe.BackColor = Color.Transparent;
            label_Recipe.Location = new Point(12, 15);

            label_shortestRecipe.Parent = guna2PictureBox3;
            label_shortestRecipe.BackColor = Color.Transparent;
            label_shortestRecipe.Location = new Point(147, 71);
            label_longestRecipe.Parent = guna2PictureBox3;
            label_longestRecipe.BackColor = Color.Transparent;
            label_longestRecipe.Location = new Point(147, 93);

            // Цілі
            label_Goals.Parent = guna2PictureBox4;
            label_Goals.BackColor = Color.Transparent;
            label_Goals.Location = new Point(12, 30);

            label_GoalsTotal.Parent = guna2PictureBox4;
            label_GoalsTotal.BackColor = Color.Transparent;
            label_GoalsTotal.Location = new Point(173, 72);
            label_GoalsCompleted.Parent = guna2PictureBox4;
            label_GoalsCompleted.BackColor = Color.Transparent;
            label_GoalsCompleted.Location = new Point(173, 93);

            // Час
            label_time.Parent = guna2PictureBox5;
            label_time.BackColor = Color.Transparent;
            label_time.Location = new Point(0, 55);

            // Збережені рецепти
            label_SavedRecipes.Parent = guna2PictureBox6;
            label_SavedRecipes.BackColor = Color.Transparent;
            label_SavedRecipes.Location = new Point(12, 30);

            label_favorite.Parent = guna2PictureBox6;
            label_favorite.BackColor = Color.Transparent;
            label_favorite.Location = new Point(173, 94);
        }
        public void LoadUserImg()
        {
            string imagePath = GetUserImgPath();
            if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
            {
                using (Image originalImage = Image.FromFile(imagePath))
                {
                    Image resizedImage = new Bitmap(originalImage, new Size(32, 32));
                    miniImg.Image = resizedImage;
                    originalImg = resizedImage;
                }                
            }
            else
            {
                miniImg.Image = null;
                originalImg = null;
                miniImg.FillColor = Color.FromArgb(67, 55, 110);
            }
        }
        private void LoadDataNutrition()
        {
            int countPeriod = 0;
            dailyRate = GetDailyRateUser();

            string selectQuery = @"SELECT un.idUserNutrition, un.mealPeriod, un.createdAt,
                                          unp.quantity,
                                          p.measure, p.productName, p.calories
                                   FROM UserNutrition un
                                   LEFT JOIN UserNutrition_Products unp ON unp.id_UserNutrition = un.idUserNutrition
                                   LEFT JOIN Products p ON unp.id_Product = p.idProduct
                                   WHERE id_User = @userId AND unp.isActive = 1";

            db.openConnection();

            using (SqlCommand cmd = new SqlCommand(selectQuery, db.getConnection()))
            {
                cmd.Parameters.AddWithValue("@userId", CurrentUser.UserId);

                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        string period = r["mealPeriod"].ToString();
                        DateTime date = Convert.ToDateTime(r["createdAt"]).Date;

                        if (date == DateTime.Now.Date)
                        {
                            decimal caloriesProduct = Convert.ToDecimal(r["calories"]);

                            if (!caloriesOfPeriodDay.ContainsKey(period))
                            {
                                caloriesOfPeriodDay[period] = 0;
                            }
                            caloriesOfPeriodDay[period] += (double)caloriesProduct;
                        }
                        countPeriod++;
                    }
                }
            }

            if (countPeriod > 0)
            {
                btnAddNutrition.Text = "Edit";
            }

            foreach (string period in caloriesOfPeriodDay.Keys)
            {
                UpdateCaloriesForPeriod(period, caloriesOfPeriodDay[period]);
            }

            db.closeConnection();
        }
        private void UpdateCaloriesForPeriod(string period, double calories)
        {
            int periodLine;

            if (period == "Сніданок")
            {
                periodLine = (int)((calories / (dailyRate * 0.3)) * maxLineWidth);
            }
            else if (period == "Обід")
            {

                periodLine = (int)((calories / (dailyRate * 0.4)) * maxLineWidth);
            }
            else
            {
                periodLine = (int)((calories / (dailyRate * 0.3)) * maxLineWidth);
            }

            periodLine = Math.Min(Math.Max(periodLine, minWidth), maxLineWidth);

            switch (period)
            {
                case "Сніданок":
                    toolTip_Morning.ToolTipTitle = period;
                    toolTip_Morning.SetToolTip(morningLine, $"Калорій - {calories.ToString("0")} / {((double)(dailyRate * 0.3)).ToString("0")}");
                    morningLine.Size = new Size(periodLine, lineHeight);
                    break;
                case "Обід":
                    toolTip_Lunch.ToolTipTitle = period;
                    toolTip_Lunch.SetToolTip(lunchLine, $"Калорій - {calories.ToString("0")} / {((double)(dailyRate * 0.4)).ToString("0")}");
                    lunchLine.Size = new Size(periodLine, lineHeight);
                    break;
                case "Вечеря":
                    toolTip_Dinner.ToolTipTitle = period;
                    toolTip_Dinner.SetToolTip(dinnerLine, $"Калорій - {calories.ToString("0")} / {((double)(dailyRate * 0.3)).ToString("0")}");
                    dinnerLine.Size = new Size(periodLine, lineHeight);
                    break;
            }
        }
        private int GetDailyRateUser()
        {
            string query = @"SELECT birthDate, height, weight, gender, id_Activity
                             FROM UserHealthMetrics
                             WHERE id_User = @userId";

            using (SqlCommand cmd = new SqlCommand(query, db.getConnection()))
            {
                cmd.Parameters.AddWithValue("@userId", CurrentUser.UserId);
                db.openConnection();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        DateTime birthDate = reader["birthDate"] != DBNull.Value ? Convert.ToDateTime(reader["birthDate"]) : default(DateTime);
                        int age = birthDate != default(DateTime) ? DateTime.Now.Year - birthDate.Year : 0;
                        int height = reader["height"] != DBNull.Value ? Convert.ToInt32(reader["height"]) : 0;
                        decimal weight = reader["weight"] != DBNull.Value ? Convert.ToDecimal(reader["weight"]) : 0m;
                        string gender = reader["gender"] != DBNull.Value ? reader["gender"].ToString() : "?";
                        int physicalActivity = reader["id_Activity"] != DBNull.Value ? Convert.ToInt32(reader["id_Activity"]) : 0;

                        db.closeConnection();

                        if (age == 0 || height == 0 || weight == 0m || physicalActivity == 0)
                        {
                            return 0;
                        }

                        decimal? bmr = (gender == "Чоловік")
                                ? (10m * weight) + (6.25m * height) - (5m * age) + 5m
                                : (10m * weight) + (6.25m * height) - (5m * age) - 161m;

                        decimal activityMultiplier = GetActivityMultiplierById(physicalActivity);
                        return (int)(bmr * activityMultiplier);
                    }
                }
                return 0;
            }
        }
        private decimal GetActivityMultiplierById(int activityId)
        {
            string query = @"SELECT multiplier
                             FROM PhysicalActivityMultipliersStandard
                             WHERE idActivity = @activityId";

            using (SqlCommand cmd = new SqlCommand(query, db.getConnection()))
            {
                cmd.Parameters.AddWithValue("@activityId", activityId);

                db.openConnection();
                var result = cmd.ExecuteScalar();
                db.closeConnection();

                return result != null ? Convert.ToDecimal(result) : 1.0m;
            }
        }
        private void LoadDataGoals()
        {
            int completedGoals = 0, lineCompleted;
            int totalGoals = 0, lineTotal;
            int maxWidth = 149;

            string queryTotal = @"SELECT COUNT(isCompleted)
                                  FROM UserGoals
                                  WHERE id_User = @UserId";
            string queryCom = @"SELECT COUNT(*)
                                FROM UserGoals
                                WHERE id_User = @UserId AND isCompleted = 1";
            
            using (SqlCommand tot = new SqlCommand(queryTotal, db.getConnection()))
            {
                tot.Parameters.AddWithValue("@UserId", CurrentUser.UserId);
                db.openConnection();
                totalGoals = Convert.ToInt32(tot.ExecuteScalar());
                db.closeConnection();
            }
            using (SqlCommand com = new SqlCommand(queryCom, db.getConnection()))
            {
                com.Parameters.AddWithValue("@UserId", CurrentUser.UserId);
                db.openConnection();
                completedGoals = Convert.ToInt32(com.ExecuteScalar());
                db.closeConnection();
            }
            
            lineTotal = Math.Min(Math.Max(totalGoals + minWidth, minWidth), maxWidth);
            lineCompleted = Math.Min(Math.Max(completedGoals + minWidth, minWidth), maxWidth);            

            toolTip_TotalGoals.ToolTipTitle = "Всього цілей";
            toolTip_TotalGoals.SetToolTip(totalGoalsLine, " ");
            if (totalGoals > 0)
            {
                totalGoalsLine.Size = new Size(lineTotal, lineHeight);
                label_GoalsTotal.Text = totalGoals.ToString();
            }
            else
            {
                totalGoalsLine.Size = new Size(minWidth, lineHeight);
                label_GoalsTotal.Text = "0";
            }

            toolTip_Competed.ToolTipTitle = "Виконаних цілей";
            toolTip_Competed.SetToolTip(competedGoalsLine, " ");
            if (completedGoals > 0)
            {
                competedGoalsLine.Size = new Size(lineCompleted, lineHeight);
                label_GoalsCompleted.Text = completedGoals.ToString();
            }
            else
            {
                competedGoalsLine.Size = new Size(minWidth, lineHeight);
                label_GoalsCompleted.Text = "0";
            }
        }
        private void LoadDataSavedRecipes()
        {

        }
        private string GetUserImgPath()
        {
            string imagePath = "";
            string sql = @"SELECT profilePhotoPath
                           FROM UserPhoto
                           WHERE id_User = @UserId";

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
        private void ActivateButton(object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                DisableButton();    // деактивовуємо попередню кнопку

                // активуємо кнопку
                currentBtn = (IconButton)senderBtn;

                currentBtn.BackColor = Color.FromArgb(20, 40, 85);
                currentBtn.ForeColor = Color.Gainsboro;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = pinkBar;
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
                currentBtn.Padding = new Padding(30, 0, 20, 0);

                // рожева смужка
                leftBorderBtn.BackColor = color;
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();

                // поточна сторінка
                iconOfPage.IconChar = currentBtn.IconChar;
                iconOfPage.IconColor = pinkBar;
            }
        }
        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(1, 23, 52);
                currentBtn.ForeColor = Color.Gainsboro;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = Color.AliceBlue;
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;

                currentBtn.Padding = new Padding(10, 0, 20, 0);
            }
        }
        private void CurrentPage (Form form)
        {
            if (currentForm != null)
            {
                currentForm.Close();
            }

            currentForm = form;
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            //
            mainPagePanel.Controls.Add(form);
            mainPagePanel.Tag = form;
            form.BringToFront();
            form.Show();
            nameOfPage.Text = form.Text;
        }
        private void recipesBtn_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, pinkBar);
            CurrentPage(new Recipes());
            iconOfPage.IconColor = Color.FromArgb(132, 24, 211);
        }
        private void productsBtn_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, pinkBar);
            CurrentPage(new Products());
            iconOfPage.IconColor = Color.FromArgb(132, 24, 211);
        }
        private void chatbotBtn_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, pinkBar);
            CurrentPage(new Chatbot());
            iconOfPage.IconColor = Color.FromArgb(132, 24, 211);
        }
        private void categoryBtn_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, pinkBar);
        }
        private void scannerBtn_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, pinkBar);
            CurrentPage(new Scanner());
            iconOfPage.IconColor = Color.FromArgb(132, 24, 211);
        }
        private void calendarBtn_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, pinkBar);
            iconOfPage.IconColor = Color.FromArgb(132, 24, 211);

            Calendar calendar = new Calendar();
            calendar.GoalsUpdated += LoadDataGoals;
            CurrentPage(calendar);
        }
        private void settingsBtn_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, pinkBar);
            iconOfPage.IconColor = Color.FromArgb(132, 24, 211);

            Settings settingsForm = new Settings();
            settingsForm.PhotoUpdated += SettingsForm_PhotoUpdated; // Підписка на подію
            CurrentPage(settingsForm);
        }
        private void myProfile_Click(object sender, EventArgs e)
        {
            CurrentPage(new Profile());
            DisableButton();
            leftBorderBtn.Visible = false;

            iconOfPage.IconChar = IconChar.User;
            iconOfPage.IconColor = Color.FromArgb(132, 24, 211);
        }
        private void miniImg_Click(object sender, EventArgs e)
        {
            CurrentPage(new Profile());
            DisableButton();
            leftBorderBtn.Visible = false;

            iconOfPage.IconChar = IconChar.User;
            iconOfPage.IconColor = Color.FromArgb(132, 24, 211);
        }
        private void btnAddNutrition_Click(object sender, EventArgs e)
        {
            // isDataNull - псевдонім перевірки
            bool isDataNull = CheckDailyRate();

            if (!isDataNull)
            {
                MessageBoxError.Show("Для того щоб додати харчування, спершу введіть свою інформацію в профілі!");
                return;
            }

            Nutrition nut = new Nutrition();
            nut.OnUpdateCalories += UpdateUserNutrition;
            nut.ShowDialog();
        }
        private void btnViewGoals_Click(object sender, EventArgs e)
        {
            ActivateButton(calendarBtn, pinkBar);
            iconOfPage.IconColor = Color.FromArgb(132, 24, 211);

            Calendar calendar = new Calendar();
            calendar.GoalsUpdated += LoadDataGoals;
            CurrentPage(calendar);
        }
        private bool CheckDailyRate()
        {
            string query = @"SELECT
                                CASE
                                    WHEN height IS NOT NULL AND
                                    weight IS NOT NULL AND
                                    gender IS NOT NULL AND
                                    id_Activity IS NOT NULL THEN 1
                                    ELSE 0
                                END AS isDataNull
                             FROM UserHealthMetrics
                             WHERE id_User = @userId";

            db.openConnection();

            using (SqlCommand cmd = new SqlCommand(query, db.getConnection()))
            {
                cmd.Parameters.AddWithValue("@userId", CurrentUser.UserId);

                var result = cmd.ExecuteScalar();

                return result != null && Convert.ToBoolean(result);
            }
        }
        private void UpdateUserNutrition(double totalCaloriesPerDay, double morning, double lunch, double dinner)
        {
            int morLine = (int)((morning / (totalCaloriesPerDay * 0.3)) * maxLineWidth);
            int lunLine = (int)((lunch / (totalCaloriesPerDay * 0.4)) * maxLineWidth);
            int dinLine = (int)((dinner / (totalCaloriesPerDay * 0.3)) * maxLineWidth);

            // обмежуємо мін та макс довжину лінії
            morLine = Math.Min(Math.Max(morLine, minWidth), maxLineWidth);
            lunLine = Math.Min(Math.Max(lunLine, minWidth), maxLineWidth);
            dinLine = Math.Min(Math.Max(dinLine, minWidth), maxLineWidth);

            toolTip_Morning.ToolTipTitle = "Сніданок";
            toolTip_Morning.SetToolTip(morningLine, $"{morning.ToString("0")} / {(totalCaloriesPerDay * 0.3).ToString("0")}");
            toolTip_Lunch.ToolTipTitle = "Обід";
            toolTip_Lunch.SetToolTip(lunchLine, $"{lunch.ToString("0")} / {(totalCaloriesPerDay * 0.4).ToString("0")}");
            toolTip_Dinner.ToolTipTitle = "Вечеря";
            toolTip_Dinner.SetToolTip(dinnerLine, $"{dinner.ToString("0")} / {(totalCaloriesPerDay * 0.3).ToString("0")}");

            morningLine.Size = new Size(morLine, lineHeight);
            lunchLine.Size = new Size(lunLine, lineHeight);
            dinnerLine.Size = new Size(dinLine, lineHeight);

            btnAddNutrition.Text = "Edit";
        }

        private void SetupButtonColorEffects(IconButton btn)
        {
            Color defaultBackColor = btn.BackColor;

            btn.MouseEnter += (s, e) =>
            {
                if (btn != currentBtn) btn.BackColor = Color.FromArgb(20, 40, 85);
            };

            btn.MouseLeave += (s, e) =>
            {
                if (btn != currentBtn) btn.BackColor = defaultBackColor;
            };

            btn.MouseDown += (s, e) =>
            {
                if (btn != currentBtn) btn.BackColor = Color.FromArgb(15, 30, 65);
            };

            btn.MouseUp += (s, e) =>
            {
                if (btn != currentBtn) btn.BackColor = defaultBackColor;
            };
        }
        private void PictureBox_logo_Click(object sender, EventArgs e)
        {
            if (currentForm != null)
            {
                currentForm.Close();
            }
            Reset();
        }
        private void PictureBox_text_Click(object sender, EventArgs e)
        {
            if (currentForm != null)
            {
                currentForm.Close();
            }
            Reset();
        }
        private void Reset()
        {
            DisableButton();
            leftBorderBtn.Visible = false;

            iconOfPage.IconChar = IconChar.HomeLg;
            iconOfPage.IconColor = Color.FromArgb(132, 24, 211);
            nameOfPage.Text = "Home";
        }
        private void exitapp_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void logout_acc_Click(object sender, EventArgs e)
        {
            bool userConfirmed = MessageBoxAttention.Show("Ви дійсно хочете вийти з акаунта?");
            if (userConfirmed)
            {
                Properties.Settings.Default.Username = string.Empty;
                Properties.Settings.Default.Password = string.Empty;
                Properties.Settings.Default.Save();

                this.Hide();
                Login loginForm = new Login();
                loginForm.Show();
            }
        }
        private void InitializeTimer()
        {
            timer = new Timer
            {
                Interval = 1000
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            CultureInfo culture = new CultureInfo("en-US");
            label_dayWeekName.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(DateTime.Now.ToString("dddd", culture));
            label_date.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(DateTime.Now.ToString("MMMM d, yyyy", culture));
            label_time.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void ConfigureLogOut()
        {
            logout_acc.MouseEnter += (sender, e) =>
            {
                logout_acc.IconColor = Color.FromArgb(220, 45, 140);
            };

            logout_acc.MouseLeave += (sender, e) =>
            {
                logout_acc.IconColor = Color.AliceBlue;
            };

            toolTip_logout.SetToolTip(logout_acc, "Вийти з акаунта");
        }
        private void ConfigureProfileImg()
        {
            if (miniImg.Image == null)
            {
                return;
            }
            originalImg = miniImg.Image;

            miniImg.MouseEnter += (sender, e) => AdjustBrightness(1.2f); // яскравість на 20%
            miniImg.MouseLeave += (sender, e) =>
            {
                if (originalImg != null)
                {
                    miniImg.Image = originalImg;
                }
            };
        }
        private void AdjustBrightness(float brightness)
        {
            if (miniImg.Image == null)
            {
                return;
            }

            Bitmap bitmap = new Bitmap(miniImg.Image.Width, miniImg.Image.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                float[][] matrix = {
            new float[] {brightness, 0, 0, 0, 0},
            new float[] {0, brightness, 0, 0, 0},
            new float[] {0, 0, brightness, 0, 0},
            new float[] {0, 0, 0, 1, 0},
            new float[] {0, 0, 0, 0, 1}
        };

                using (ImageAttributes attributes = new ImageAttributes())
                {
                    attributes.SetColorMatrix(new ColorMatrix(matrix));
                    g.DrawImage(miniImg.Image, new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                                0, 0, miniImg.Image.Width, miniImg.Image.Height, GraphicsUnit.Pixel, attributes);
                }
            }

            miniImg.Image = bitmap;
        }
    }
}