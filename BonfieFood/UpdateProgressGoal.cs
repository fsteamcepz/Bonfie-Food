using Guna.UI2.WinForms.Suite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace BonfieFood
{
    public partial class UpdateProgressGoal : Form
    {
        private DataBase db = new DataBase();
        private DateTime date;
        public event Action<DateTime, bool> OnGoalUpdate;

        public UpdateProgressGoal(DateTime selectedDate)
        {
            InitializeComponent();
            date = selectedDate.Date;
        }
        private void UpdateProgressGoal_Load(object sender, EventArgs e)
        {
            LoadUserData();
            selectedDate.Text = date.ToString("dd.MM");
        }
        private void updateProgress_Click(object sender, EventArgs e)
        {
            // Ціль
            string updateGoalUser = updateGoal.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(updateGoalUser))
            {
                MessageBoxError.Show("Оберіть ціль!");
                return;
            }

            // Поточний результат
            decimal updateTargetValueUser = progressValue.Value;
            if (updateTargetValueUser < 0 || updateTargetValueUser > 999999.99m)
            {
                MessageBoxError.Show("Введіть коректне значення для обраної категорії!");
                progressValue.Value = 0;
                return;
            }

            // Нотатки
            string updateNotes = notes.Text;
            if (updateNotes.Length > 500)
            {
                MessageBoxError.Show("Опис не повинен перевищувати 500 символів!");
                return;
            }

            // Дата, коли був зафіксований прогрес
            DateTime dateProgess = DateTime.Now;

            int userGoalId = GetUserGoalId(updateGoalUser);

            // зберігаємо дані
            string queryInsertProgress = @"INSERT INTO GoalProgress(id_UserGoals, progressDate, progressValue, notes)
                                           VALUES (@id_UserGoals, @progressDate, @progressValue, @notes)";

            using (SqlCommand cmd = new SqlCommand(queryInsertProgress, db.getConnection()))
            {
                cmd.Parameters.AddWithValue("@id_UserGoals", userGoalId);
                cmd.Parameters.AddWithValue("@progressDate", dateProgess);
                cmd.Parameters.AddWithValue("@progressValue", updateTargetValueUser);
                cmd.Parameters.AddWithValue("@notes", updateNotes);

                db.openConnection();
                cmd.ExecuteScalar();
                db.closeConnection();
            }

            // отримуємо цільове значення
            decimal targetValue = GetTargetValue(userGoalId);

            if (updateTargetValueUser >= targetValue)
            {
                TargetCompleted(userGoalId);
                MessageBoxSuccess.Show("Ціль досягнута!");

                OnGoalUpdate?.Invoke(date, true);
            }
            else
            {
                MessageBoxSuccess.Show("Прогрес успішно оновлено!");
                OnGoalUpdate?.Invoke(date, false);
            }
            this.Hide();
        }
        private void compete_Click(object sender, EventArgs e)
        {
            string updateGoalUser = updateGoal.SelectedItem?.ToString();
            int userGoalId = GetUserGoalId(updateGoalUser);

            TargetCompleted(userGoalId);

            MessageBoxSuccess.Show("Ціль досягнута!");
            OnGoalUpdate?.Invoke(date, true);
            this.Hide();
        }
        private void LoadUserData()
        {
            string query = @"SELECT gc.categoryName, gp.progressValue, ug.isCompleted
                     FROM GoalCategories gc
                     JOIN UserGoals ug ON ug.id_Category = gc.idCategory
                     LEFT JOIN GoalProgress gp ON gp.id_UserGoals = ug.idUserGoals
                     WHERE ug.id_User = @id_User AND ug.goalDate = @goalDate";

            using (SqlCommand cmd = new SqlCommand(query, db.getConnection()))
            {
                cmd.Parameters.AddWithValue("@id_User", CurrentUser.UserId);
                cmd.Parameters.AddWithValue("@goalDate", date.Date);

                db.openConnection();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    bool foundActiveGoal = false;
                    string activeCategory = "";
                    decimal activeProgress = 0m;

                    while (reader.Read())
                    {
                        string categoryName = reader["categoryName"] != DBNull.Value
                            ? reader["categoryName"].ToString()
                            : "";
                        decimal progress = reader["progressValue"] != DBNull.Value
                            ? Convert.ToDecimal(reader["progressValue"])
                            : 0m;
                        bool isCompleted = reader["isCompleted"] != DBNull.Value && Convert.ToBoolean(reader["isCompleted"]);

                        if (!isCompleted)
                        {
                            activeCategory = categoryName;
                            activeProgress = progress;
                            foundActiveGoal = true;
                            break;
                        }
                    }
                    db.closeConnection();

                    if (foundActiveGoal)
                    {
                        updateGoal.SelectedItem = activeCategory;
                        InactiveGoal();
                        progressValue.Value = activeProgress;
                    }
                    else
                    {
                        updateGoal.Enabled = true;
                        progressValue.Value = 0;
                    }
                }
            }
        }

        private void TargetCompleted(int userGoalId)
        {
            string query = @"UPDATE UserGoals
                             SET isCompleted = 1
                             WHERE idUserGoals = @id_UserGoals";

            using (SqlCommand cmd = new SqlCommand(query, db.getConnection()))
            {
                cmd.Parameters.AddWithValue("@id_UserGoals", userGoalId);
                db.openConnection();
                cmd.ExecuteNonQuery();
                db.closeConnection();
            }
        }

        private int GetUserGoalId(string goalUser)
        {
            string query = @"SELECT ug.idUserGoals
                             FROM UserGoals ug
                             JOIN GoalCategories gc ON ug.id_Category = gc.idCategory
                             WHERE gc.categoryName = @category AND ug.goalDate = @goalDate AND ug.id_User = @id_User";

            using (SqlCommand cmd = new SqlCommand(query, db.getConnection()))
            {
                cmd.Parameters.AddWithValue("@category", goalUser);
                cmd.Parameters.AddWithValue("@goalDate", date.Date);
                cmd.Parameters.AddWithValue("@id_User", CurrentUser.UserId);
                db.openConnection();
                var result = cmd.ExecuteScalar();
                db.closeConnection();

                return result != null ? (int)result : 0;
            }
        }
        private decimal GetTargetValue(int userGoalId)
        {
            string query = @"SELECT targetValue
                             FROM UserGoals
                             WHERE idUserGoals = @idUserGoals";

            using (SqlCommand cmd = new SqlCommand(query, db.getConnection()))
            {
                cmd.Parameters.AddWithValue("@idUserGoals", userGoalId);
                db.openConnection();
                var result = cmd.ExecuteScalar();
                db.closeConnection();

                return result != null ? Convert.ToDecimal(result) : 0;
            }
        }
        private void InactiveGoal()
        {
            updateGoal.Enabled = false;
            updateGoal.FillColor = Color.FromArgb(35, 20, 85);
            updateGoal.BorderColor = Color.FromArgb(76, 151, 254);
            updateGoal.DisabledState.FillColor = Color.FromArgb(35, 20, 85);
            updateGoal.DisabledState.BorderColor = Color.FromArgb(76, 151, 254);
            updateGoal.DisabledState.ForeColor = Color.Gray;
            updateGoal.Cursor = Cursors.Default;
        }
    }
}
