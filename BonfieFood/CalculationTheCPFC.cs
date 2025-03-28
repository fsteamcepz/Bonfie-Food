using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BonfieFood
{
    public partial class PanelTheCPFC : Form
    {
        // Подія для передачі даних з panelTheCPFC форми до Profile
        public event Action<int, int, decimal, string, int, decimal, int> OnCalculationComplete;
        DataBase db = new DataBase();
        public PanelTheCPFC()
        {
            InitializeComponent();
            LoadUserData();
        }
        
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // чи натиснута клавіша Enter
            if (keyData == Keys.Enter)
            {
                return true; // звук не відтворюється
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            SystemSounds.Hand.Play();   // відтворюємо звук
        }
        private void LoadUserData()
        {
            string query = @"SELECT birthDate, height, weight, gender, id_Activity, id_BMICategory
                             FROM UserHealthMetrics
                             WHERE id_User = @id_User";

            using (SqlCommand cmd = new SqlCommand(query, db.getConnection()))
            {
                cmd.Parameters.AddWithValue("@id_User", CurrentUser.UserId);

                db.openConnection();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Перевірка на null для кожного поля
                        if (reader["birthDate"] != DBNull.Value)
                        {
                            age.Value = Convert.ToDateTime(reader["birthDate"]);
                        }
                        else
                        {
                            age.Value = DateTime.Today;
                        }
                        height.Value = reader["height"] != DBNull.Value ? Convert.ToInt32(reader["height"]) : 0;
                        weight.Value = reader["weight"] != DBNull.Value ? Convert.ToDecimal(reader["weight"]) : 0m;

                        string gender = reader["gender"] != DBNull.Value ? reader["gender"].ToString() : "";
                        if (gender == "Чоловік") male.Checked = true;
                        else if (gender == "Жінка") female.Checked = true;

                        int activityId = reader["id_Activity"] != DBNull.Value ? Convert.ToInt32(reader["id_Activity"]) : 0;
                        int bmiCategoryId = reader["id_BMICategory"] != DBNull.Value ? Convert.ToInt32(reader["id_BMICategory"]) : 0;

                        db.closeConnection();

                        physicalActivity.SelectedItem = activityId > 0 ? GetActivityName(activityId) : null;
                        string bmiCategory = bmiCategoryId > 0 ? GetBMICategoryName(bmiCategoryId) : "Не вказано";
                    }
                    else
                    {
                        db.closeConnection();
                        age.Value = default(DateTime);
                        height.Value = 0;
                        weight.Value = 0m;
                        male.Checked = false;
                        female.Checked = false;
                        physicalActivity.SelectedItem = null;
                    }
                }
            }
        }
        private string GetActivityName(int activityId)
        {
            string query = @"SELECT physicalActivity
                             FROM PhysicalActivityMultipliersStandard
                             WHERE idActivity = @idActivity";

            using (SqlCommand cmd = new SqlCommand(query, db.getConnection()))
            {
                cmd.Parameters.AddWithValue("@idActivity", activityId);

                db.openConnection();
                var result = cmd.ExecuteScalar();
                db.closeConnection();

                return result?.ToString();
            }
        }
        private string GetBMICategoryName(int bmiCategoryId)
        {
            string query = @"SELECT nameCategory
                             FROM BMIStandard
                             WHERE idBMI = @idBMI";

            using (SqlCommand cmd = new SqlCommand(query, db.getConnection()))
            {
                cmd.Parameters.AddWithValue("@idBMI", bmiCategoryId);

                db.openConnection();
                var result = cmd.ExecuteScalar();
                db.closeConnection();

                return result?.ToString();
            }
        }
        private void calculation_Click(object sender, EventArgs e)
        {
            // Вік
            DateTime birthDate = age.Value;
            int ageUser = DateTime.Now.Year - birthDate.Year;

            if (birthDate.Date > DateTime.Now.Date.AddYears(-ageUser))
            {
                ageUser--;
            }
            if (ageUser < 4 || ageUser > 100)
            {
                MessageBoxError.Show("Введіть коректну дату народження (вік 4-100 років)!");
                return;
            }

            // Зріст
            if (height.Value < 50 || height.Value > 200)
            {
                MessageBoxError.Show("Введіть коректний зріст (50-200 см)!");
                return;
            }
            int heightUser = (int)height.Value;

            // Вага
            decimal weightUser = weight.Value;
            if (weightUser < 10 || weightUser > 150)
            {
                MessageBoxError.Show("Введіть коректну вагу (10-150 кг)!");
                return;
            }

            // Стать
            string genderUser;
            if (male.Checked)
            {
                genderUser = "Чоловік";
            }
            else if (female.Checked)
            {
                genderUser = "Жінка";
            }
            else
            {
                MessageBoxError.Show("Оберіть будь ласка стать!");
                return;
            }

            // Фізична активність
            string physicalActivityUser = physicalActivity.SelectedItem.ToString();
            if (string.IsNullOrEmpty(physicalActivityUser))
            {
                MessageBoxError.Show("Оберіть рівень фізичної активності!");
                return;
            }


            // розрахунок індекс маси тіла (BMI)
            decimal bmi = weightUser / (decimal)Math.Pow((double)heightUser / 100, 2);

            string bmiCategory = GetBMICategory(bmi);

            // Отримуємо id для activity та bmiCategory
            int activityId = GetActivityId(physicalActivityUser);
            int bmiCategoryId = GetBMICategoryId(bmiCategory);

            // розрахунок добової норми калорій (BMR). Формула "Гарріса Бенедикта"
            decimal bmr = (genderUser == "Чоловік")
                ? 88.36m + (13.4m * weightUser) + (4.8m * heightUser) - (5.7m * ageUser)
                : 447.6m + (9.2m * weightUser) + (3.1m * heightUser) - (4.3m * ageUser);

            decimal activityMultiplier = GetActivityMultiplier(physicalActivityUser);
            int dailyCalories = (int)(bmr * activityMultiplier);


            // зберігаємо дані
            string querySelect = @"SELECT COUNT(*)
                                    FROM UserHealthMetrics
                                    WHERE id_User = @id_User";

            using (SqlCommand cmdCount = new SqlCommand(querySelect, db.getConnection()))
            {
                cmdCount.Parameters.AddWithValue("@id_User", CurrentUser.UserId);
                db.openConnection();
                int count = (int)cmdCount.ExecuteScalar();
                db.closeConnection();

                string query;
                if (count > 0)
                {
                    query = @"UPDATE UserHealthMetrics
                              SET birthDate = @birthDate, height = @height, weight = @weight, gender = @gender,
                                  id_Activity = @activityId, id_BMICategory = @bmiCategoryId
                              WHERE id_User = @id_User";
                }
                else
                {
                    query = @"INSERT INTO UserHealthMetrics (id_User, birthDate, height, weight, gender,
                                                             id_Activity, id_BMICategory)
                              VALUES (@id_User, @birthDate, @height, @weight, @gender, @activityId, @bmiCategoryId)";
                }
                
                using (SqlCommand cmd = new SqlCommand(query, db.getConnection()))
                {
                    cmd.Parameters.AddWithValue("@id_User", CurrentUser.UserId);
                    cmd.Parameters.AddWithValue("@birthDate", birthDate);
                    cmd.Parameters.AddWithValue("@height", heightUser);
                    cmd.Parameters.AddWithValue("@weight", weightUser);
                    cmd.Parameters.AddWithValue("@gender", genderUser);
                    cmd.Parameters.AddWithValue("@activityId", activityId);
                    cmd.Parameters.AddWithValue("@bmiCategoryId", bmiCategoryId);

                    db.openConnection();
                    cmd.ExecuteNonQuery();
                    db.closeConnection();
                }
            }

            // передаємо результати обчислення
            OnCalculationComplete?.Invoke(ageUser, heightUser, weightUser, genderUser, dailyCalories, bmi, bmiCategoryId);

            //MessageBoxSuccess.Show("Дані успішно збережено!");
            this.Hide();
        }
        
        private decimal GetActivityMultiplier(string physicalActivity)
        {
            string queryMultiplier = @"SELECT multiplier
                                       FROM PhysicalActivityMultipliersStandard
                                       WHERE physicalActivity = @activity";
            decimal multiplier = 1.0m;

            using (SqlCommand cmd = new SqlCommand(queryMultiplier, db.getConnection()))
            {
                cmd.Parameters.AddWithValue("@activity", physicalActivity);
                db.openConnection();
                var result = cmd.ExecuteScalar();
                db.closeConnection();

                if (result != null)
                {
                    multiplier = (decimal)result;
                }
            }
            return multiplier;
        }
        private string GetBMICategory(decimal bmi)
        {
            string queryBMICategory = @"SELECT nameCategory 
                                        FROM BMIStandard 
                                        WHERE (@bmi >= ISNULL(BMI_Min, @bmi) AND @bmi <= ISNULL(BMI_Max, @bmi))";

            using (SqlCommand cmd = new SqlCommand(queryBMICategory, db.getConnection()))
            {
                cmd.Parameters.AddWithValue("@bmi", bmi);
                db.openConnection();
                var result = cmd.ExecuteScalar();
                db.closeConnection();

                return result?.ToString();
            }
        }
        private int GetActivityId(string physicalActivity)
        {
            string queryActivity = @"SELECT idActivity
                                     FROM PhysicalActivityMultipliersStandard
                                     WHERE physicalActivity = @activity";

            using (SqlCommand cmd = new SqlCommand(queryActivity, db.getConnection()))
            {
                cmd.Parameters.AddWithValue("@activity", physicalActivity);
                db.openConnection();
                var result = cmd.ExecuteScalar();
                db.closeConnection();

                return result != null ? (int)result : 0;
            }
        }
        private int GetBMICategoryId(string bmiCategory)
        {
            string queryBMICategory = @"SELECT idBMI
                                        FROM BMIStandard
                                        WHERE nameCategory = @category";

            using (SqlCommand cmd = new SqlCommand(queryBMICategory, db.getConnection()))
            {
                cmd.Parameters.AddWithValue("@category", bmiCategory);
                db.openConnection();
                var result = cmd.ExecuteScalar();
                db.closeConnection();

                return result != null ? (int)result : 0;
            }
        }
    }
}