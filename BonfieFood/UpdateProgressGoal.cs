﻿using System;
using System.Data.SqlClient;
using System.Drawing;
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

            UpdateTexts();
            Language.OnLanguageChanged += ChangeLanguage;
        }
        private void ChangeLanguage(string cultureCode)
        {
            UpdateTexts();
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
                             WHERE ug.id_User = @id_User
                                   AND ug.goalDate = @goalDate
                                   AND gp.progressDate = (
                                        SELECT MAX(progressDate)
                                        FROM GoalProgress
                                        WHERE id_UserGoals = ug.idUserGoals)";

            using (SqlCommand cmd = new SqlCommand(query, db.getConnection()))
            {
                cmd.Parameters.AddWithValue("@id_User", CurrentUser.UserId);
                cmd.Parameters.AddWithValue("@goalDate", date.Date);

                db.openConnection();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    bool isFoundActiveGoal = false;
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
                            isFoundActiveGoal = true;
                            break;
                        }
                    }
                    db.closeConnection();

                    if (isFoundActiveGoal)
                    {
                        updateGoal.SelectedItem = activeCategory;
                        InactiveGoal();
                        progressValue.Value = activeProgress;
                    }
                    else
                    {
                        string queryCategory = @"SELECT gc.categoryName
                                                 FROM GoalCategories gc
                                                 JOIN UserGoals ug ON ug.id_Category = gc.idCategory
                                                 WHERE ug.id_User = @id_User
                                                       AND ug.goalDate = @goalDate AND ug.isCompleted = 0";

                        using (SqlCommand cmd2 = new SqlCommand(queryCategory, db.getConnection()))
                        {
                            cmd2.Parameters.AddWithValue("@id_User", CurrentUser.UserId);
                            cmd2.Parameters.AddWithValue("@goalDate", date.Date);

                            db.openConnection();
                            using (SqlDataReader r = cmd2.ExecuteReader())
                            {
                                if (r.Read())
                                {
                                    string categoryName = r["categoryName"] != DBNull.Value
                                        ? r["categoryName"].ToString()
                                        : "";

                                    updateGoal.SelectedItem = categoryName;
                                    InactiveGoal();
                                    progressValue.Value = 0;
                                }
                                else
                                {
                                    updateGoal.Enabled = false;
                                    progressValue.Value = 0;
                                }
                            }
                            db.closeConnection();
                        }
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

        private void UpdateTexts()
        {
            label_UpdateProgress.Text = Properties.Resources.label_UpdateProgress;
            label_Goal.Text = Properties.Resources.label_Goal;
            label_CurrentResult.Text = Properties.Resources.label_CurrentResult;
            label_Notes.Text = Properties.Resources.label_Notes;
            updateProgress.Text = Properties.Resources.updateProgress;
            complete.Text = Properties.Resources.completeGoal;
        }
    }
}