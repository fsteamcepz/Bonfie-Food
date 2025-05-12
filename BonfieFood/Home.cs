using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;
using System.IO;
using System.Globalization;
using System.Drawing.Imaging;
using System.Collections.Generic;

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
        private int minWidth = 13;

        // об'єкти класів з API
        Huggingface _hugg = new Huggingface();
        CloudVision _cloud = new CloudVision();
        Clarifai _clarifai = new Clarifai();
        Edamam_Nutrition _nutrition = new Edamam_Nutrition();
        Edamam_Products _products = new Edamam_Products();
        Edamam_Recipe _recipe = new Edamam_Recipe();
        OAuthEmail _email = new OAuthEmail();

        public Home()
        {
            InitializeComponent();
            InitializeTimer();

            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MinimumSize = new Size(916, 609);
        }
        private void Home_Load(object sender, EventArgs e)
        {
            UpdateTexts();
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
            LoadDataSavedRecipes();

            TransparentLabels();
            ConfigureLogOut();
            ConfigureProfileImg();

            Language.OnLanguageChanged += ChangeLanguage;
        }
        private void UpdateTexts()
        {
            label_QuickAction_h1.Text = Properties.Resources.label_QuickAction_h1;
            label_HealthMessage_h2.Text = Properties.Resources.label_HealthMessage_h2;
            label_Nutrition_h2.Text = Properties.Resources.label_Nutrition_h2;
            btnAddNutrition.Text = Properties.Resources.btnAddNutrition;
            label_IntegratedDB_h2.Text = Properties.Resources.label_IntegratedDB_h2;
            label_Products_h3.Text = Properties.Resources.label_Products_h3;
            label_Recipes_h3.Text = Properties.Resources.label_Recipes_h3;
            btnProducts.Text = Properties.Resources.btnProducts;
            btnRecipes.Text = Properties.Resources.btnRecipes;
            label_Goals_h1.Text = Properties.Resources.label_Goals_h1;
            label_GoalProgress_h2.Text = Properties.Resources.label_GoalProgress_h2;
            btnViewGoals.Text = Properties.Resources.btnViewGoals;
            label_SavedRecipes_h2.Text = Properties.Resources.label_SavedRecipes_h2;
            btnViewSavedRecipes.Text = Properties.Resources.btnViewSavedRecipes;
            myProfile.Text = Properties.Resources.myProfile;

            toolTip_logout.SetToolTip(logout_acc, Properties.Resources.toolTip_logout);
            toolTip_DBProduct.ToolTipTitle = Properties.Resources.DBProduct_Title;
            toolTip_DBProduct.SetToolTip(DBProductsLine, Properties.Resources.DBProduct_SetToolTip);
            toolTip_DBRecipe.ToolTipTitle = Properties.Resources.DBRecipe_Title;
            toolTip_DBRecipe.SetToolTip(DBRecipeLine, Properties.Resources.DBRecipe_SetToolTip);
            toolTip_TotalGoals.ToolTipTitle = Properties.Resources.toolTip_TotalGoals_Title;
            toolTip_TotalGoals.SetToolTip(totalGoalsLine, Properties.Resources.toolTip_Goals_SetToolTip);
            toolTip_Competed.ToolTipTitle = Properties.Resources.toolTip_Competed_Title;
            toolTip_Competed.SetToolTip(competedGoalsLine, Properties.Resources.toolTip_Goals_SetToolTip);
        }
        private void ChangeLanguage(string cultureCode)
        {
            UpdateTexts();
        }
        private void SettingsForm_PhotoUpdated(object sender, EventArgs e)
        {
            LoadUserImg();
            //MessageBox.Show("miniImg оновилося на формі Home!");
        }
        private void TransparentLabels()
        {
            // Харчування
            label_Nutrition_h2.Parent = guna2PictureBox1;
            label_Nutrition_h2.BackColor = Color.Transparent;
            label_Nutrition_h2.Location = new Point(12, 15);

            // Назва тижня
            label_dayWeekName.Parent = guna2PictureBox2;
            label_dayWeekName.BackColor = Color.Transparent;
            label_dayWeekName.Location = new Point(0, 50);

            // Дата (September 22, 2024)
            label_date.Parent = guna2PictureBox2;
            label_date.BackColor = Color.Transparent;
            label_date.Location = new Point(-7, 75);

            // БД рецепти та продукти
            label_IntegratedDB_h2.Parent = guna2PictureBox3;
            label_IntegratedDB_h2.BackColor = Color.Transparent;
            label_IntegratedDB_h2.Location = new Point(12, 15);

            label_Products_h3.Parent = guna2PictureBox3;
            label_Products_h3.BackColor = Color.Transparent;
            label_Products_h3.Location = new Point(118, 71);
            label_Recipes_h3.Parent = guna2PictureBox3;
            label_Recipes_h3.BackColor = Color.Transparent;
            label_Recipes_h3.Location = new Point(118, 93);

            // Цілі
            label_GoalProgress_h2.Parent = guna2PictureBox4;
            label_GoalProgress_h2.BackColor = Color.Transparent;
            label_GoalProgress_h2.Location = new Point(12, 30);

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
            label_SavedRecipes_h2.Parent = guna2PictureBox6;
            label_SavedRecipes_h2.BackColor = Color.Transparent;
            label_SavedRecipes_h2.Location = new Point(12, 30);

            label_TotalSaved.Parent = guna2PictureBox6;
            label_TotalSaved.BackColor = Color.Transparent;
            label_TotalSaved.Location = new Point(173, 94);
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
                btnAddNutrition.Text = Properties.Resources.textEdit;
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
            int totalSaved = 0, lineTotal;
            int maxWidth = 150;
            int maxSavedRecipes = 50;

            string query = @"SELECT COUNT(isSaved)
                             FROM UserSavedRecipes
                             WHERE id_User = @UserId AND isSaved = 1";
            db.openConnection();
            using (SqlCommand cmd = new SqlCommand(query, db.getConnection()))
            {
                cmd.Parameters.AddWithValue("@UserId", CurrentUser.UserId);
                totalSaved = Convert.ToInt32(cmd.ExecuteScalar());
                db.closeConnection();
            }

            lineTotal = Math.Min(Math.Max(totalSaved * (maxWidth / maxSavedRecipes) + minWidth, minWidth), maxWidth);

            if (totalSaved > 0)
            {
                savedRecipesLine.Size = new Size(lineTotal, lineHeight);
                label_TotalSaved.Text = totalSaved.ToString();
            }
            else
            {
                savedRecipesLine.Size = new Size(minWidth, lineHeight);
                label_TotalSaved.Text = "0";
            }
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

            if (_recipe.isFileAvailable)
            {
                ActivateButton(sender, pinkBar);
                iconOfPage.IconColor = Color.FromArgb(132, 24, 211);

                Recipes recipes = new Recipes();
                recipes.RecipesUpdated += LoadDataSavedRecipes;
                CurrentPage(recipes);
            }
            else
            {
                MessageBoxError.Show($"Функція «Пошук рецептів» недоступна через відсутність API ключів!");
            }
        }
        private void productsBtn_Click(object sender, EventArgs e)
        {
            if (_products.isFileAvailable)
            {
                ActivateButton(sender, pinkBar);
                CurrentPage(new Products());
                iconOfPage.IconColor = Color.FromArgb(132, 24, 211);
            }
            else
            {
                MessageBoxError.Show($"Функція «Пошук продуктів» недоступна через відсутність API ключів!");
            }
        }
        private void chatbotBtn_Click(object sender, EventArgs e)
        {
            if (_hugg.isFileAvailable)
            {
                ActivateButton(sender, pinkBar);
                CurrentPage(new Chatbot());
                iconOfPage.IconColor = Color.FromArgb(132, 24, 211);
            }
            else
            {
                MessageBoxError.Show($"Функція Chatbot недоступна через відсутність API ключів!");
            }
        }
        private void scannerBtn_Click(object sender, EventArgs e)
        {
            if (_cloud.isFileAvailable || _clarifai.isFileAvailable || _products.isFileAvailable)
            {
                ActivateButton(sender, pinkBar);
                CurrentPage(new Scanner());
                nameOfPage.Text = "Food Analyser";
                iconOfPage.IconColor = Color.FromArgb(132, 24, 211);
            }
            else
            {
                MessageBoxError.Show($"Функція «Аналіз їжі» недоступна через відсутність API ключів!");
            }
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
            if (_nutrition.isFileAvailable)
            {
                // isDataNull - псевдонім перевірки
                bool isDataNull = CheckDailyRate();

                if (!isDataNull)
                {
                    MessageBoxError.Show("Для того щоб додати харчування, спершу потрібно розрахувати добову норму калорій в профілі!");
                    return;
                }

                Nutrition nut = new Nutrition();
                nut.OnUpdateCalories += UpdateUserNutrition;
                nut.ShowDialog();
            }
            else
            {
                MessageBoxError.Show($"Функція «Харчування» недоступна через відсутність API ключів!");
            }            
        }
        private void btnViewGoals_Click(object sender, EventArgs e)
        {
            ActivateButton(calendarBtn, pinkBar);
            iconOfPage.IconColor = Color.FromArgb(132, 24, 211);

            Calendar calendar = new Calendar();
            calendar.GoalsUpdated += LoadDataGoals;
            CurrentPage(calendar);
        }
        private void btnViewSavedRecipes_Click(object sender, EventArgs e)
        {
            SavedRecipes saved = new SavedRecipes();
            saved.ShowDialog();
        }
        private void btnProducts_Click(object sender, EventArgs e)
        {
            if (_hugg.isFileAvailable)
            {
                ActivateButton(productsBtn, pinkBar);
                CurrentPage(new Products());
                iconOfPage.IconColor = Color.FromArgb(132, 24, 211);
            }
            else
            {
                MessageBoxError.Show($"Функція «Пошук продуктів» недоступна через відсутність API ключів!");
            }
        }
        private void btnRecipes_Click(object sender, EventArgs e)
        {
            if (_hugg.isFileAvailable)
            {
                ActivateButton(recipesBtn, pinkBar);
                iconOfPage.IconColor = Color.FromArgb(132, 24, 211);

                Recipes recipes = new Recipes();
                recipes.RecipesUpdated += LoadDataSavedRecipes;
                CurrentPage(recipes);
            }
            else
            {
                MessageBoxError.Show($"Функція «Пошук рецептів» недоступна через відсутність API ключів!");
            }
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

            btnAddNutrition.Text = Properties.Resources.textEdit;
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
                string defaultLanguage = Properties.Settings.Default.Language = "en";
                System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo(defaultLanguage);
                System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(defaultLanguage);
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
            CultureInfo culture = System.Threading.Thread.CurrentThread.CurrentCulture;
            if (culture.ToString() == "en")
                label_date.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(DateTime.Now.ToString("MMMM d, yyyy", culture));
            else
                label_date.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(DateTime.Now.ToString("d MMMM, yyyy", culture));
            
            label_dayWeekName.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(DateTime.Now.ToString("dddd", culture));            
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