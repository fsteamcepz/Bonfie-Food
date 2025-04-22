using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Data.SqlClient;
using Guna.UI2.WinForms;
using FontAwesome.Sharp;
using System.Globalization;

namespace BonfieFood
{
    public partial class Profile : Form
    {
        DataBase db = new DataBase();

        public Profile()
        {
            InitializeComponent();
        }

        private void Profile_Load(object sender, EventArgs e)
        {
            // Фото
            LoadUserImg();
            LoadHistory();

            // виводимо інформацію про користувача
            LoadUserData();
            label_username.Text = CurrentUser.UserName;
            ConfigureInfoCPFC();            

            UpdateTexts();
            Language.OnLanguageChanged += ChangeLanguage;
        }
        private void ChangeLanguage(string cultureCode)
        {
            UpdateTexts();
        }
        private void LoadHistory()
        {
            CultureInfo culture = System.Threading.Thread.CurrentThread.CurrentCulture;

            guna2Panel_miniHistory.Controls.Clear();

            string select = @"SELECT nameAction, date
                              FROM UserActionHistory
                              WHERE @UserId = id_User
                              ORDER BY date DESC";

            using (SqlCommand cmd = new SqlCommand(select, db.getConnection()))
            {
                cmd.Parameters.AddWithValue("@UserId", CurrentUser.UserId);

                db.openConnection();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    int locIcon = 10;
                    int locName = 12;
                    int top = 40;
                    int count = 0;

                    Label history = new Label
                    {
                        Text = Properties.Resources.label_HistoryNotFound,
                        Size = new Size(142, 49),
                        Location = new Point(26, 44),
                        Font = new Font("Constantia", 12, FontStyle.Bold),
                        ForeColor = Color.FromArgb(192, 192, 255),
                        BackColor = Color.Transparent,
                        Padding = new Padding(3, 0, 3, 0),
                        TextAlign = ContentAlignment.MiddleCenter,
                        AutoSize = false,
                        Visible = false,
                        Cursor = Cursors.Default
                    };
                    guna2Panel_miniHistory.Controls.Add(history);

                    while (reader.Read())
                    {
                        if (count == 4)
                            break;

                        string actionName = reader["nameAction"].ToString();
                        DateTime searchDate = Convert.ToDateTime(reader["date"]);

                        IconPictureBox icon = new IconPictureBox
                        {
                            Size = new Size(32, 32),
                            Location = new Point(0, locIcon),
                            ForeColor = Color.Gold,
                            IconChar = IconChar.RectangleList,
                            IconFont = IconFont.Auto,
                            Cursor = Cursors.Default
                        };

                        if (culture.ToString() == "en")
                        {
                            if (actionName == "Пошук рецептів")
                            {
                                icon.ForeColor = Color.FromArgb(85, 163, 0);
                                actionName = Properties.Resources.label_SearchRecipes;
                            }
                            else if (actionName == "Пошук продуктів")
                            {
                                icon.ForeColor = Color.FromArgb(121, 85, 193);
                                actionName = Properties.Resources.label_SearchProducts;
                            }
                            else if (actionName == "Діалог з чат-ботом")
                            {
                                icon.ForeColor = Color.Gold;
                                actionName = Properties.Resources.label_Chatbot;
                            }
                            else if (actionName == "Аналіз їжі")
                            {
                                icon.ForeColor = Color.FromArgb(1, 75, 254);
                                actionName = Properties.Resources.label_FoodAnalysis;
                            }
                            else if (actionName == "Додавання цілі")
                            {
                                icon.ForeColor = Color.DeepPink;
                                actionName = Properties.Resources.label_CreationGoal;
                            }
                        }

                        Guna2HtmlToolTip toolTip_History = new Guna2HtmlToolTip()
                        {
                            BackColor = Color.FromArgb(14, 15, 40),
                            BorderColor = Color.FromArgb(70, 67, 83),
                            ForeColor = Color.FromArgb(253, 235, 250),
                            InitialDelay = 100,
                            ReshowDelay = 100,
                            Font = new Font("Segoe UI", 9),
                            AutomaticDelay = 500,
                            AutoPopDelay = 5000
                        };
                        toolTip_History.SetToolTip(icon, searchDate.ToString("dd.MM.yyyy (HH:mm)"));

                        Label labelName = new Label
                        {
                            Text = actionName,
                            Font = new Font("Segoe UI", 11.25F),
                            ForeColor = Color.FromArgb(253, 235, 250),
                            AutoSize = true,
                            Location = new Point(34, locName),
                            Cursor = Cursors.Default
                        };

                        guna2Panel_miniHistory.Controls.Add(icon);
                        guna2Panel_miniHistory.Controls.Add(labelName);

                        locIcon += top;
                        locName += top;
                        count++;
                    }

                    if (count == 0)
                        history.Visible = true;
                    else
                        history.Visible = false;
                }
                db.closeConnection();
            }
        }
        private void LoadUserData()
        {
            string querySelect = @"SELECT birthDate, height, weight, gender, id_Activity, id_BMICategory
                                   FROM UserHealthMetrics
                                   WHERE id_User = @id_User";

            using (SqlCommand cmd = new SqlCommand(querySelect, db.getConnection()))
            {
                cmd.Parameters.AddWithValue("@id_User", CurrentUser.UserId);
                db.openConnection();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // перевірка на null для кожного поля
                        DateTime birthDate = reader["birthDate"] != DBNull.Value ? Convert.ToDateTime(reader["birthDate"]) : default(DateTime);
                        int age = birthDate != default(DateTime) ? DateTime.Now.Year - birthDate.Year : 0;
                        if (birthDate != default(DateTime) && birthDate.Date > DateTime.Now.Date.AddYears(-age)) age--;
                        UpdateAge(age);

                        int height = reader["height"] != DBNull.Value ? Convert.ToInt32(reader["height"]) : 0;
                        UpdateHeight(height);

                        decimal weight = reader["weight"] != DBNull.Value ? Convert.ToDecimal(reader["weight"]) : 0m;
                        UpdateWeight(weight);

                        string gender = reader["gender"] != DBNull.Value ? reader["gender"].ToString() : "?";
                        UpdateGender(gender);

                        int physicalActivity = reader["id_Activity"] != DBNull.Value ? Convert.ToInt32(reader["id_Activity"]) : 0;
                        int bmiCategoryId = reader["id_BMICategory"] != DBNull.Value ? Convert.ToInt32(reader["id_BMICategory"]) : 0;

                        decimal bmi = CalculateBMI(weight, height);
                        UpdateBMI(bmi, bmiCategoryId);

                        decimal bmr = 0;
                        // якщо недостатньо даних для обчислення КБЖВ, вивести 0
                        if (weight > 0 && height > 0 && age > 0 && !string.IsNullOrEmpty(gender))
                        {
                            bmr = (gender == "Чоловік")
                                ? (10m * weight) + (6.25m * height) - (5m * age) + 5m
                                : (10m * weight) + (6.25m * height) - (5m * age) - 161m;
                        }

                        db.closeConnection();

                        label_TDEE.Text = Properties.Resources.label_TDEE;
                        calculateTheTDEE.Text = Properties.Resources.updateTheTDEE;

                        if (bmr == 0)
                        {
                            myCal.Text = "0" + "\ncal";
                            myProtein.Text = "0" + "\nprotein";
                            myFat.Text = "0" + "\nfat";
                            myCarbs.Text = "0" + "\ncarbs";
                        }
                        else
                        {
                            decimal activityValue = GetActivityMultiplierById(physicalActivity);
                            int dailyBMR = (int)(bmr * activityValue);
                            UpdateDailyCalories(dailyBMR);
                        }
                    }
                    else
                    {
                        myAge.Text = "?";
                        myAge.BorderColor = Color.LimeGreen;
                        myHeight.Text = "?";
                        myHeight.BorderColor = Color.Gold;
                        myWeight.Text = "?";
                        myWeight.BorderColor = Color.Crimson;
                        myGender.Text = "?";
                        myGender.BorderColor = Color.AliceBlue;

                        Guna2Button[] el = { BMI_1, BMI_2, BMI_3 };
                        for (int i = 0; i < el.Length; i++)
                        {
                            el[i].Visible = false;
                        }

                        // КБЖВ
                        myCal.Text = "0" + "\ncal";
                        myProtein.Text = "0" + "\nprotein";
                        myFat.Text = "0" + "\nfat";
                        myCarbs.Text = "0" + "\ncarbs";

                        label_TDEE.Text = Properties.Resources.label_TDEE;
                        calculateTheTDEE.Text = Properties.Resources.calculateTheTDEE;
                    }
                }
            }
        }
        private void LoadUserImg()
        {
            string imagePath = GetUserImgPath();
            if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
            {
                using (Image image = Image.FromFile(imagePath))
                {
                    imgBox_profile.Image = new Bitmap(image, new Size(164, 164));
                }
            }
            else
            {
                imgBox_profile.FillColor = Color.FromArgb(67, 55, 110); // якщо фото немає
            }
        }
        public string GetUserImgPath()
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
        
        private void calculateTheCPFC_Click(object sender, EventArgs e)
        {
            PanelTheCPFC calCPFC = new PanelTheCPFC();
            calCPFC.OnCalculationComplete += UpdateUserInfoAfterCal;
            calCPFC.ShowDialog();
        }
        private void UpdateUserInfoAfterCal(int age, int height, decimal weight, string gender, int dailyCalories, decimal bmi, int bmiCategoryId)
        {
            UpdateAge(age);
            UpdateHeight(height);
            UpdateWeight(weight);
            UpdateGender(gender);

            UpdateBMI(bmi, bmiCategoryId);

            // Добова норма КБЖВ
            UpdateDailyCalories(dailyCalories);

            label_TDEE.Text = Properties.Resources.label_TDEE;
            calculateTheTDEE.Text = Properties.Resources.updateTheTDEE;
        }
        private void UpdateAge(int currentAge)
        {
            if (currentAge == 0)
            {
                myAge.BorderColor = Color.LimeGreen;
                myAge.Text = "0";
                return;
            }

            int minAge = 1, maxAge = 100;
            currentAge = Math.Max(minAge, Math.Min(maxAge, currentAge));

            double agePercentage = (double)(currentAge - minAge) / (maxAge - minAge);

            // обчислення компонентів RGB
            int red, green;
            if (agePercentage <= 0.5)
            {
                red = (int)(255 * (agePercentage / 0.5));
                green = 255;
            }
            else
            {
                red = 255;
                green = (int)(255 * (1 - (agePercentage - 0.5) / 0.5));
            }

            // встановлюємо новий колір обвідки
            myAge.BorderColor = Color.FromArgb(red, green, 0);
            myAge.Text = currentAge.ToString();
        }
        private void UpdateHeight(int currentHeight)
        {
            myHeight.BorderColor = Color.Gold;
            myHeight.Text = currentHeight.ToString();
        }
        private void UpdateWeight(decimal currentWeight)
        {
            myWeight.BorderColor = Color.Crimson;
            myWeight.Text = currentWeight.ToString("0.0");
        }
        private void UpdateGender(string myGen)
        {
            if (myGen == "Чоловік")
            {
                myGender.BorderColor = Color.FromArgb(177, 177, 177);
                myGender.Text = "Чол";
            }
            else if (myGen == "Жінка")
            {
                myGender.BorderColor = Color.Magenta;
                myGender.Text = "Жін";
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
        private void UpdateBMI(decimal bmiUser, int bmiCategoryId)
        {
            Guna2Button[] el = { BMI_1, BMI_2, BMI_3 };
            for (int i = 0; i < el.Length; i++)
            {
                el[i].Visible = true;
                el[i].FillColor = Color.FromArgb(35, 20, 85);
                el[i].BorderColor = Color.FromArgb(253, 235, 250);
            }
            

            if (bmiCategoryId == 1)
            {
                BMI_1.FillColor = Color.FromArgb(169, 20, 9);
                BMI_1.BorderColor = Color.FromArgb(169, 20, 9);
                BMI_1.Animated = true;
                toolTip_BMI.TitleForeColor = Color.FromArgb(169, 20, 9);
                toolTip_BMI.ToolTipTitle = "BMI: " + bmiUser.ToString("F2"); ;
                toolTip_BMI.SetToolTip(BMI_1, "Низька вага.");
            }
            else if (bmiCategoryId == 2)
            {
                BMI_2.FillColor = Color.FromArgb(0, 150, 0);
                BMI_2.BorderColor = Color.FromArgb(0, 150, 0);
                BMI_2.Animated = true;
                toolTip_BMI.TitleForeColor = Color.FromArgb(0, 150, 0);
                toolTip_BMI.ToolTipTitle = "BMI: " + bmiUser.ToString("F2"); ;
                toolTip_BMI.SetToolTip(BMI_2, "Нормальна вага.");
            }
            else if (bmiCategoryId == 3)
            {
                BMI_3.FillColor = Color.FromArgb(150, 112, 0);
                BMI_3.BorderColor = Color.FromArgb(150, 112, 0);
                BMI_3.Animated = true;
                toolTip_BMI.TitleForeColor = Color.FromArgb(150, 112, 0);
                toolTip_BMI.ToolTipTitle = "BMI: " + bmiUser.ToString("F2"); ;
                toolTip_BMI.SetToolTip(BMI_3, "Надлишкова вага.");
            }
            else if (bmiCategoryId == 4)
            {
                BMI_3.FillColor = Color.FromArgb(190, 119, 9);
                BMI_3.BorderColor = Color.FromArgb(190, 119, 9);
                BMI_3.Animated = true;
                toolTip_BMI.TitleForeColor = Color.FromArgb(190, 119, 9);
                toolTip_BMI.ToolTipTitle = "BMI: " + bmiUser.ToString("F2"); ;
                toolTip_BMI.SetToolTip(BMI_3, "Ожиріння I ступеня.");
            }
            else if (bmiCategoryId == 5)
            {
                BMI_3.FillColor = Color.FromArgb(169, 20, 9);
                BMI_3.BorderColor = Color.FromArgb(169, 20, 9);
                BMI_3.Animated = true;
                toolTip_BMI.TitleForeColor = Color.FromArgb(169, 20, 9);
                toolTip_BMI.ToolTipTitle = "BMI: " + bmiUser.ToString("F2"); ;
                toolTip_BMI.SetToolTip(BMI_3, "Ожиріння II ступеня.");
            }
            else if (bmiCategoryId == 6)
            {
                BMI_3.FillColor = Color.FromArgb(169, 20, 9);
                BMI_3.BorderColor = Color.FromArgb(169, 20, 9);
                BMI_3.Animated = true;
                toolTip_BMI.TitleForeColor = Color.FromArgb(169, 20, 9);
                toolTip_BMI.ToolTipTitle = "BMI: " + bmiUser.ToString("F2"); ;
                toolTip_BMI.SetToolTip(BMI_3, "Ожиріння III ступеня.");
            }
        }
        private decimal CalculateBMI(decimal weight, int height)
        {
            if (height == 0)
            {
                return 0;
            }
            decimal bmi = weight / (decimal)Math.Pow((double)height / 100, 2);

            return bmi;
        }
        private void UpdateDailyCalories(int dailyCalories)
        {
            myCal.Text = dailyCalories + "\ncal";
            myProtein.Text = Math.Round(dailyCalories * 0.2m / 4, 1) + "\nprotein"; // 20% калорій на білки (4 ккал/г)
            myFat.Text = Math.Round(dailyCalories * 0.3m / 9, 1) + "\nfat";         // 30% калорій на жири (9 ккал/г)
            myCarbs.Text = Math.Round(dailyCalories * 0.5m / 4, 1) + "\ncarbs";     // 50% калорій на вуглеводи (4 ккал/г)
        }

        private void ConfigureInfoCPFC()
        {
            toolTip_formula.ToolTipTitle = "Формула «Міффлін-Сан-Джеора»";
            toolTip_formula.SetToolTip(info_dish, "TDEE розраховується за формулою «Міффлін-Сан-Джеора»,<br>" +
                                                  "яка враховує зріст, вагу, вік і стать для визначення<br>" +
                                                  "базового метаболізму (BMR). Потім загальна добова<br>" +
                                                  "потреба в калоріях розраховується за формулою:<br>" +
                                                  "TDEE = BMR × коефіцієнт активності.<br><br>" +
                                                  "Розрахунок BMR:<br>" +
                                                  "Чоловіки: (10 × ВАГА) + (6.25 × ЗРІСТ) - (5 × ВІК) + 4<br>" +
                                                  "Жінки: (10 × ВАГА) + (6.25 × ЗРІСТ) - (5 × ВІК)");

            info_dish.MouseEnter += (sender, e) =>
            {
                info_dish.IconColor = Color.FromArgb(180, 195, 230);
            };

            info_dish.MouseLeave += (sender, e) =>
            {
                info_dish.IconColor = Color.AliceBlue;
            };
        }

        private void UpdateTexts()
        {
            label_GeneralInfo.Text = Properties.Resources.label_GeneralInfo;
            label_TDEE.Text = Properties.Resources.label_TDEE;
            label_History.Text = Properties.Resources.label_History;

            toolTip_Username.SetToolTip(label_username, Properties.Resources.toolTip_Username);
        }
    }
}