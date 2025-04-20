using Google.Type;
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
using DateTime = System.DateTime;

namespace BonfieFood
{
    public partial class CreateGoal : Form
    {
        DataBase db = new DataBase();
        // Подія для передачі даних з CreateGoal в Calendar
        public event Action<string, DateTime, decimal, string, bool> OnCreateGoalComplete;

        public CreateGoal(DateTime selectedDate)
        {
            InitializeComponent();
            goalDate.Value = selectedDate;
        }
        private void CreateGoal_Load(object sender, EventArgs e)
        {
            UpdateTexts();
            Language.OnLanguageChanged += ChangeLanguage;
        }
        private void ChangeLanguage(string cultureCode)
        {
            UpdateTexts();
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
        private void saveGoal_Click(object sender, EventArgs e)
        {
            // Ціль
            string goalUser = goal.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(goalUser))
            {
                MessageBoxError.Show("Оберіть ціль!");
                return;
            }

            // Дата завершення
            DateTime goalDateUser = goalDate.Value;
            if (goalDateUser == DateTimePicker.MinimumDateTime)
            {
                MessageBoxError.Show("Будь ласка, оберіть дату завершення!");
                return;
            }

            // Очікуваний результат
            decimal targetValueUser = targetValue.Value;
            if (targetValueUser < 0 || targetValueUser > 999999.99m)
            {
                MessageBoxError.Show("Введіть коректне значення для обраної категорії!");
                targetValue.Value = 0;
                return;
            }

            // Опис
            string description = descriptionOfGoal.Text;
            if (description.Length > 500)
            {
                MessageBoxError.Show("Опис не повинен перевищувати 500 символів!");
                return;
            }

            // зберігаємо дані
            int goalId = GetGoalId(goalUser);

            string queryInsert = @"
            INSERT INTO UserGoals (id_User, id_Category, goalDate, goalDescription, targetValue)
            VALUES (@id_User, @id_Category, @goalDate, @goalDescription, @targetValue)";

            using (SqlCommand cmd = new SqlCommand(queryInsert, db.getConnection()))
            {
                cmd.Parameters.AddWithValue("@id_User", CurrentUser.UserId);
                cmd.Parameters.AddWithValue("@id_Category", goalId);
                cmd.Parameters.AddWithValue("@goalDate", goalDateUser);
                cmd.Parameters.AddWithValue("@goalDescription", description);
                cmd.Parameters.AddWithValue("@targetValue", targetValueUser);

                db.openConnection();
                cmd.ExecuteScalar();
                db.getConnection();
            }

            // передаємо результати (ціль, дата завершення, результат, опис)
            OnCreateGoalComplete?.Invoke(goalUser, goalDateUser, targetValueUser, description, false);
            this.Hide();
        }
        private int GetGoalId(string goalUser)
        {
            string query = @"SELECT idCategory
                             FROM GoalCategories
                             WHERE categoryName = @category";

            using (SqlCommand cmd = new SqlCommand(query, db.getConnection()))
            {
                cmd.Parameters.AddWithValue("@category", goalUser);
                db.openConnection();
                var result = cmd.ExecuteScalar();
                db.closeConnection();

                return result != null ? (int)result : 0;
            }
        }

        private void UpdateTexts()
        {
            label_CreateGoal_h1.Text = Properties.Resources.label_CreateGoal_h1;
            label_Goal.Text = Properties.Resources.label_Goal;
            label_Date.Text = Properties.Resources.label_DateCompletion;
            label_TargetValue.Text = Properties.Resources.label_TargetValue;
            label_GoalDescription.Text = Properties.Resources.label_GoalDescription;
            saveGoal.Text = Properties.Resources.saveGoal;
        }
    }
}
