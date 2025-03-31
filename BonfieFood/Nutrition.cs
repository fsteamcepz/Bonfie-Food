using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BonfieFood
{
    public partial class Nutrition : Form
    {
        DataBase db = new DataBase();
        private Edamam_Nutrition edamamNutrition = new Edamam_Nutrition();
        private string selectedMealPeriod;
        Control toolBtn = null;    // прив'язка підказки до клавіші
        private double? caloriesPeriod;
        private int dailyRate;
        private double morningCalories = 0;
        private double lunchCalories = 0;
        private double dinnerCalories = 0;
        private Dictionary<string, List<string>> productsOfPeriodDay = new Dictionary<string, List<string>>();
        public event Action<double, double, double, double> OnUpdateCalories;

        public Nutrition()
        {
            InitializeComponent();
        }
        private void Nutrition_Load(object sender, EventArgs e)
        {
            dailyRate = GetDailyRateUser();
            nutritionGoal.Text = "0 / " + dailyRate;
            LoadUserData();
            LoadToolTips();
        }
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            SystemSounds.Hand.Play();
        }
        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string productsList = nutritionUser.Text.Trim();
                if (string.IsNullOrWhiteSpace(productsList))
                {
                    MessageBoxError.Show("Введіть список продуктів для підрахунку калорій!");
                    return;
                }
                if (string.IsNullOrEmpty(selectedMealPeriod))
                {
                    MessageBoxError.Show("Оберіть період дня (сніданок, обід, вечеря)!");
                    return;
                }
                // розбиваємо список продуктів на окремі рядки
                edamamNutrition.Products = productsList.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
                               .Select(p => p.Trim())
                               .ToList();

                caloriesPeriod = await edamamNutrition.AnalyzeNutritionAsync();

                if (caloriesPeriod == null)
                {
                    MessageBoxError.Show("Помилка під час запиту до Nutrition Analysis API!");
                    return;
                }

                await SaveNutritionToDB();
                                
                UpdateCaloriesForPeriod(selectedMealPeriod, (double)caloriesPeriod);
                UpdateBtns(selectedMealPeriod);
                SaveProductsForPeriod(selectedMealPeriod);

                nutritionUser.Text = "";
                btnSave.Text = "Save";

                OnUpdateCalories?.Invoke(dailyRate, morningCalories, lunchCalories, dinnerCalories);
            }
            catch (Exception ex)
            {
                MessageBoxError.Show($"Помилка: {ex.Message}");
            }
        }
        private async Task SaveNutritionToDB()
        {
            string select_U_N = @"SELECT idUserNutrition
                                  FROM UserNutrition
                                  WHERE id_User = @userId AND mealPeriod = @mealPeriod";
            
            string update_U_N = @"UPDATE UserNutrition
                                  SET updatedAt = GETDATE()
                                  WHERE idUserNutrition = @nutritionId";
            
            string insert_U_N = @"INSERT INTO UserNutrition (id_User, mealPeriod) 
                                  VALUES (@userId, @mealPeriod); 
                                  SELECT SCOPE_IDENTITY();";    // отримуємо idUserNutrition останнього доданого запису
            int? nutritionId = null;        // зберігаємо id

            db.openConnection();

            // 1. Додаємо/оновлюємо таблицю UserNutrition
            using (SqlCommand cmd = new SqlCommand(select_U_N, db.getConnection()))
            {
                cmd.Parameters.AddWithValue("@userId", CurrentUser.UserId);
                cmd.Parameters.AddWithValue("@mealPeriod", selectedMealPeriod);
                
                var result = await cmd.ExecuteScalarAsync();
                
                if (result != null)
                {
                    nutritionId = Convert.ToInt32(result);

                    using (SqlCommand updCmd = new SqlCommand(update_U_N, db.getConnection()))
                    {
                        updCmd.Parameters.AddWithValue("@nutritionId", nutritionId);
                        
                        await updCmd.ExecuteNonQueryAsync();
                    }

                    // деактивуємо старі продукти
                    string deactivateOldProducts = @"UPDATE UserNutrition_Products
                                                     SET isActive = 0
                                                     WHERE id_UserNutrition = @nutritionId";
                    using (SqlCommand dCmd = new SqlCommand(deactivateOldProducts, db.getConnection()))
                    {
                        dCmd.Parameters.AddWithValue("@nutritionId", nutritionId);
                        
                        await dCmd.ExecuteNonQueryAsync();
                    }
                }
                else
                {
                    using (SqlCommand insertCmd = new SqlCommand(insert_U_N, db.getConnection()))
                    {
                        insertCmd.Parameters.AddWithValue("@userId", CurrentUser.UserId);
                        insertCmd.Parameters.AddWithValue("@mealPeriod", selectedMealPeriod);

                        nutritionId = Convert.ToInt32(await insertCmd.ExecuteScalarAsync());
                    }
                }
            }

            foreach (var p in edamamNutrition.ProductsUser)
            {
                string select_P = @"SELECT idProduct 
                                    FROM Products 
                                    WHERE productName = @name AND measure = @measure AND weightProduct = @weight";
                
                string insert_P = @"INSERT INTO Products (productName, measure, calories, weightProduct) 
                                    VALUES (@name, @measure, @calories, @weight); 
                                    SELECT SCOPE_IDENTITY();";
                int? productId = null;

                // 2. Перевіряємо чи існує продукт в таблиці Products
                using (SqlCommand checkCmd = new SqlCommand(select_P, db.getConnection()))
                {
                    checkCmd.Parameters.AddWithValue("@name", p.NameProduct);
                    checkCmd.Parameters.AddWithValue("@measure", p.Measure);
                    checkCmd.Parameters.AddWithValue("@weight", Math.Round(p.Weight, 2));
                    
                    var result = await checkCmd.ExecuteScalarAsync();
                    
                    if (result != null)
                    {
                        productId = (int)result;
                    }
                    else
                    {
                        using (SqlCommand insCmd = new SqlCommand(insert_P, db.getConnection()))
                        {
                            insCmd.Parameters.AddWithValue("@name", p.NameProduct);
                            insCmd.Parameters.AddWithValue("@measure", p.Measure);
                            insCmd.Parameters.AddWithValue("@calories", p.Calories);
                            insCmd.Parameters.AddWithValue("@weight", p.Weight);
                            
                            productId = Convert.ToInt32(await insCmd.ExecuteScalarAsync());
                        }
                    }
                }
                
                string insert_U_N_P = @"INSERT INTO UserNutrition_Products (id_UserNutrition, id_Product, quantity, isActive) 
                                        VALUES (@nutritionId, @productId, @quantity, 1)";

                // 3. Додаємо запис у таблицю UserNutrition_Products
                using (SqlCommand insert = new SqlCommand(insert_U_N_P, db.getConnection()))
                {
                    insert.Parameters.AddWithValue("@nutritionId", nutritionId);
                    insert.Parameters.AddWithValue("@productId", productId);
                    insert.Parameters.AddWithValue("@quantity", Math.Round(p.Quantity, 2));
                    
                    await insert.ExecuteNonQueryAsync();
                }
            }
            db.closeConnection();
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
                        DateTime? birthDate = (DateTime?)reader["birthDate"];
                        int? height = (int?)reader["height"];
                        decimal? weight = (decimal?)reader["weight"];
                        string gender = reader["gender"].ToString();
                        int? physicalActivityId = (int?)reader["id_Activity"];

                        db.closeConnection();

                        int age = DateTime.Now.Year - birthDate.Value.Year;
                        if (birthDate.Value.Date > DateTime.Now.Date.AddYears(-age))
                        {
                            age--;
                        }                            

                        decimal? bmr = (gender == "Чоловік")
                                ? (10m * weight) + (6.25m * height) - (5m * age) + 5m
                                : (10m * weight) + (6.25m * height) - (5m * age) - 161m;

                        decimal activityMultiplier = GetActivityMultiplierById(physicalActivityId.Value);
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
        private void LoadUserData()
        {
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
                            decimal quantity = Convert.ToDecimal(r["quantity"]);
                            string measure = r["measure"].ToString();
                            string productName = r["productName"].ToString();
                            decimal caloriesProduct = Convert.ToDecimal(r["calories"]);

                            string quantityStr = quantity.ToString("0");
                            string info = $"{quantityStr} {measure} {productName}";
                            
                            if (!productsOfPeriodDay.ContainsKey(period))
                            {
                                productsOfPeriodDay[period] = new List<string>();
                            }
                            productsOfPeriodDay[period].Add(info);

                            UpdateCaloriesForPeriod(period, (double)caloriesProduct);
                        }
                    }
                }
            }

            foreach (string period in productsOfPeriodDay.Keys)
            {
                UpdateBtns(period);
            }

            db.closeConnection();
        }
        private void UpdateBtns(string period)
        {
            switch (period)
            {
                case "Сніданок":
                    toolBtn = morning;
                    added_morning.Visible = true;
                    morning.BorderColor = Color.FromArgb(211, 121, 77);
                    morning.FillColor = Color.FromArgb(59, 66, 159);
                    toolTip_InfoMorning.ToolTipTitle = period;
                    toolTip_InfoMorning.SetToolTip(toolBtn, $"Калорій - {morningCalories.ToString("0")}");
                    break;
                case "Обід":
                    toolBtn = lunch;
                    added_lunch.Visible = true;
                    lunch.BorderColor = Color.FromArgb(211, 121, 77);
                    lunch.FillColor = Color.FromArgb(59, 66, 159);
                    toolTip_InfoLunch.ToolTipTitle = period;
                    toolTip_InfoLunch.SetToolTip(toolBtn, $"Калорій - {lunchCalories.ToString("0")}");
                    break;
                case "Вечеря":
                    toolBtn = dinner;
                    added_dinner.Visible = true;
                    dinner.BorderColor = Color.FromArgb(211, 121, 77);
                    dinner.FillColor = Color.FromArgb(59, 66, 159);
                    toolTip_InfoDinner.ToolTipTitle = period;
                    toolTip_InfoDinner.SetToolTip(toolBtn, $"Калорій - {dinnerCalories.ToString("0")}");
                    break;
            }

            double totalDailyCalories = morningCalories + lunchCalories + dinnerCalories;
            nutritionGoal.Text = totalDailyCalories.ToString("0") + " / " + dailyRate;
        }
        private void UpdateCaloriesForPeriod(string period, double calories)
        {
            switch (period)
            {
                case "Сніданок":
                    morningCalories += calories;
                    toolTip_InfoMorning.ToolTipTitle = period;
                    toolTip_InfoMorning.SetToolTip(morning, $"Калорій - {calories.ToString("0")}");
                    break;
                case "Обід":
                    lunchCalories += calories;
                    toolTip_InfoLunch.ToolTipTitle = period;
                    toolTip_InfoLunch.SetToolTip(lunch, $"Калорій - {calories.ToString("0")}");
                    break;
                case "Вечеря":
                    dinnerCalories += calories;
                    toolTip_InfoDinner.ToolTipTitle = period;
                    toolTip_InfoDinner.SetToolTip(dinner, $"Калорій - {calories.ToString("0")}");
                    break;
            }

            double totalCalories = morningCalories + lunchCalories + dinnerCalories;
            nutritionGoal.Text = totalCalories.ToString("0") + " / " + dailyRate;
        }
        private void SaveProductsForPeriod(string period)
        {
            if (productsOfPeriodDay.ContainsKey(period))
            {
                productsOfPeriodDay[period].Clear();
            }
            else
            {
                productsOfPeriodDay[period] = new List<string>();
            }
            productsOfPeriodDay[period].AddRange(edamamNutrition.Products);
        }
        private void LoadProductsForPeriod(string period)
        {
            if (productsOfPeriodDay.ContainsKey(period) && productsOfPeriodDay[period].Count > 0)
            {
                nutritionUser.Text = string.Join(Environment.NewLine, productsOfPeriodDay[period]);
                btnSave.Text = "Update";

                if (period == "Сніданок") added_morning.Visible = true;
                if (period == "Обід") added_lunch.Visible = true;
                if (period == "Вечеря") added_dinner.Visible = true;
            }
            else
            {
                nutritionUser.Text = "";
                btnSave.Text = "Save";
            }
        }

        private void morning_Click(object sender, EventArgs e)
        {
            selectedMealPeriod = "Сніданок";
            toolBtn = morning;
            UpdateButton(morning, lunch, dinner);

            LoadProductsForPeriod(selectedMealPeriod);
        }
        private void lunch_Click(object sender, EventArgs e)
        {
            selectedMealPeriod = "Обід";
            toolBtn = lunch;
            UpdateButton(lunch, morning, dinner);

            LoadProductsForPeriod(selectedMealPeriod);
        }
        private void dinner_Click(object sender, EventArgs e)
        {
            selectedMealPeriod = "Вечеря";
            toolBtn = dinner;
            UpdateButton(dinner, morning, lunch);

            LoadProductsForPeriod(selectedMealPeriod);
        }
        private void UpdateButton(Guna2Button a, Guna2Button in1, Guna2Button in2)
        {
            a.FillColor = Color.FromArgb(62, 57, 123);
            a.BorderColor = Color.FromArgb(62, 57, 123);

            in1.FillColor = Color.FromArgb(59, 66, 159);
            in1.BorderColor = Color.FromArgb(211, 121, 77);
            in2.FillColor = Color.FromArgb(59, 66, 159);
            in2.BorderColor = Color.FromArgb(211, 121, 77);
        }

        private void LoadToolTips()
        {
            toolTip_GoaNutrition.SetToolTip(nutritionGoal, "Ціль харчування на день");

            toolTip_NutritionTip.ToolTipTitle = "Додавання харчування";
            toolTip_NutritionTip.SetToolTip(nutrition_tip, "Щоб додати харчування, введіть список продуктів англійською мовою у форматі:<br>" +
                                                           "«кількість + одиниця вимірювання + назва». Наприклад:<br>" +
                                                           "1 chicken cutlets,<br>" +
                                                           "368 ml coca cola<br><br>" +
                                                           "Ви можете додавати кілька продуктів, розділяючи їх комою або новим рядком.<br>" +
                                                           "Обов'язково вкажіть період дня (сніданок, обід, вечеря) перед збереженням.<br>" +
                                                           "Система автоматично розрахує калорійність продуктів та оновить лічильник.<br>" +
                                                           "Якщо продукт не розпізнається, перевірте правильність написання.");
        }
    }
}
