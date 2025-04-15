using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Guna.UI2.WinForms.Suite;
using System.Drawing.Drawing2D;
using System.Data.SqlClient;

namespace BonfieFood
{
    public partial class Calendar : Form
    {
        DataBase db = new DataBase();
        private DateTime currentDate;
        private List<Guna2GradientButton> btn = new List<Guna2GradientButton>();
        private Dictionary<DateTime, (string goalName, decimal targetValue, string description, bool isCompleted)> goalsData;
        public event Action GoalsUpdated;

        public Calendar()
        {
            InitializeComponent();
            currentDate = DateTime.Now;
        }
        private void Calendar_Load(object sender, EventArgs e)
        {
            CreateButtons();
            LoadCalendar();
            ConfigureBtns();
            flowLayoutPanel_numbers.Visible = true;
        }
        private void CreateButtons()
        {
            flowLayoutPanel_numbers.Visible = false;
            flowLayoutPanel_numbers.Controls.Clear();
            for (int i = 1; i <= 31; i++)
            {
                Guna2GradientButton btnM = new Guna2GradientButton
                {
                    Font = new Font("Magneto", 16),
                    ForeColor = Color.WhiteSmoke,
                    FillColor = SystemColors.Highlight,
                    FillColor2 = Color.DeepPink,
                    BorderColor = Color.FromArgb(172, 229, 255),
                    Size = new Size(65, 65),
                    Margin = new Padding(3, 3, 26, 10),
                    BorderRadius = 31,
                    BorderThickness = 1,
                    GradientMode = LinearGradientMode.Vertical,
                    Cursor = Cursors.Hand,
                    TextAlign = HorizontalAlignment.Center,
                    UseTransparentBackground = true,
                    Visible = false
                };

                btnM.HoverState.FillColor = Color.Transparent;
                btnM.HoverState.FillColor2 = Color.Transparent;
                btnM.HoverState.BorderColor = Color.WhiteSmoke;

                btnM.Click += DayButton_Click;

                flowLayoutPanel_numbers.Controls.Add(btnM);
                btn.Add(btnM);
            }
        }
        private void LoadCalendar()
        {
            string monthName = CultureInfo.GetCultureInfo("en-US").DateTimeFormat.GetMonthName(currentDate.Month);
            int daysInMonth = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);

            nameOfMonth.Text = char.ToUpper(monthName[0]) + monthName.Substring(1);
            year.Text = currentDate.Year.ToString();

            GetGoalsForMonth(currentDate);

            int count = 0;

            for (int i = 0; i < btn.Count; i++)
            {
                if (i < daysInMonth)
                {
                    btn[i].Visible = true;
                    btn[i].Text = (i + 1).ToString();
                    btn[i].Tag = new DateTime(currentDate.Year, currentDate.Month, i + 1);

                    DateTime btnDate = (DateTime)btn[i].Tag;
                    GoalProgressInactive(btn[i]);

                    if (goalsData.ContainsKey(btnDate))
                    {
                        var (goalName, targetValue, description, isCompleted) = goalsData[btnDate];

                        if (isCompleted)
                        {
                            GoalProgressInactive(btn[i]);
                            toolTip_InfoGoal.SetToolTip(btn[i], null);
                        }
                        else
                        {
                            GoalProgressActive(btn[i]);

                            string text = "";

                            text += $"Ціль: «{goalName}» (не завершена)<br><br>";
                            text += $"Дата завершення: {btnDate.ToShortDateString()}<br>";

                            if (!string.IsNullOrEmpty(description))
                            {
                                text += $"Опис: {description}<br>";
                            }
                            if (targetValue > 0)
                            {
                                text += $"Цільове значення: {targetValue}";
                            }

                            toolTip_InfoGoal.SetToolTip(btn[i], text);
                            count++;
                        }
                    }
                    else
                    {
                        toolTip_InfoGoal.SetToolTip(btn[i], null);

                        btn[i].FillColor = SystemColors.Highlight;
                        btn[i].FillColor2 = Color.DeepPink;
                        btn[i].BorderColor = Color.FromArgb(172, 229, 255);
                    }

                    if ((i + 1) == 1 || (i + 1) == 16 || (i + 1) == 31)
                    {
                        btn[i].Font = new Font("Rockwell Extra Bold", 16, FontStyle.Italic);
                    }
                    else
                    {
                        btn[i].Font = new Font("Magneto", 16);
                    }

                    if ((i + 1) == 7 || (i + 1) == 14 || (i + 1) == 21 || (i + 1) == 28)
                    {
                        btn[i].Margin = new Padding(3, 3, 3, 10);
                    }
                    else if ((i + 1) == 29 || (i + 1) == 30 || (i + 1) == 31)
                    {
                        btn[i].Margin = new Padding(3, 3, 26, 3);
                    }
                    else
                    {
                        btn[i].Margin = new Padding(3, 3, 26, 10);
                    }

                    btn[i].Enabled = true;
                    btn[i].BorderColor = Color.FromArgb(172, 229, 255);
                    btn[i].Cursor = Cursors.Hand;
                }
                else
                {
                    btn[i].Visible = false;
                }
            }
            goals.Text = $"Goals: {count}";
            toolTip_Goals.SetToolTip(goals, $"Кількість цілей в поточному місяці");
        }
        private void DayButton_Click(object sender, EventArgs e)
        {
            Guna2GradientButton button = sender as Guna2GradientButton;
            DateTime selectedDate = (DateTime)button.Tag;

            // перевіряємо, чи існує ціль для обраної дати
            GetGoalsForMonth(currentDate);

            if (goalsData.ContainsKey(selectedDate))
            {
                var (goalUser, targetValue, description, isCompleted) = goalsData[selectedDate];

                UpdateProgressGoal update = new UpdateProgressGoal(selectedDate);
                update.OnGoalUpdate += (date, completed) =>
                {
                    UpdateCalendar(goalUser, date, targetValue, description, completed);
                };
                update.ShowDialog();
                GoalsUpdated?.Invoke();
            }
            else
            {
                CreateGoal createGoal = new CreateGoal(selectedDate);
                createGoal.OnCreateGoalComplete += UpdateCalendar;
                createGoal.ShowDialog();
                GoalsUpdated?.Invoke();
            }
        }
        private void UpdateCalendar(string goalUser, DateTime goalDateUser, decimal targetValueUser, string description, bool isCompleted)
        {
            LoadCalendar();

            foreach (var button in btn)
            {
                if (button.Tag is DateTime buttonDate && buttonDate.Date == goalDateUser.Date)
                {
                    if (isCompleted)
                    {
                        GoalProgressInactive(button);
                        toolTip_InfoGoal.SetToolTip(button, null);
                    }
                    else
                    {
                        GoalProgressActive(button);

                        string text = "";

                        text += $"Ціль: «{goalUser}» (не завершена)<br><br>";
                        text += $"Дата завершення: {goalDateUser.ToShortDateString()}<br>";

                        if (!string.IsNullOrEmpty(description))
                        {
                            text += $"Опис: {description}<br>";
                        }
                        if (targetValueUser > 0)
                        {
                            text += $"Цільове значення: {targetValueUser}";
                        }

                        toolTip_InfoGoal.SetToolTip(button, text);
                    }
                    break;
                }
            }
        }
        private void nextMonth_Click(object sender, EventArgs e)
        {
            currentDate = currentDate.AddMonths(1);

            if (currentDate.Year > 2125)
            {
                currentDate = new DateTime(2125, 12, 1);
            }
            NotReadOnly();
            LoadCalendar();
        }
        private void previousMonth_Click(object sender, EventArgs e)
        {
            currentDate = currentDate.AddMonths(-1);

            if (currentDate.Year < 2025 || (currentDate.Year == 2025 && currentDate.Month == 1))
            {
                ReadOnly();
                currentDate = new DateTime(2025, 1, 1);
            }
            else
            {
                NotReadOnly();
            }
            LoadCalendar();
        }
        private void GetGoalsForMonth(DateTime date)
        {
            goalsData = new Dictionary<DateTime, (string goalName, decimal targetValue, string description, bool isCompleted)>();

            string query = @"
                            SELECT gc.categoryName, ug.goalDate, ug.targetValue, ug.goalDescription, ug.isCompleted
                            FROM UserGoals ug
                            JOIN GoalCategories gc ON ug.id_Category = gc.idCategory
                            WHERE ug.id_User = @UserId AND
                                  MONTH(ug.goalDate) = @Month AND
                                  YEAR(ug.goalDate) = @Year";

            using (SqlCommand cmd = new SqlCommand(query, db.getConnection()))
            {
                cmd.Parameters.AddWithValue("@UserId", CurrentUser.UserId);
                cmd.Parameters.AddWithValue("@Month", date.Month);
                cmd.Parameters.AddWithValue("@Year", date.Year);

                db.openConnection();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string goalName = reader["categoryName"] != DBNull.Value ? reader["categoryName"].ToString() : "";
                        DateTime goalDate = reader["goalDate"] != DBNull.Value ? Convert.ToDateTime(reader["goalDate"]) : DateTime.MinValue;
                        decimal targetValue = reader["targetValue"] != DBNull.Value ? Convert.ToDecimal(reader["targetValue"]) : 0m;
                        string description = reader["goalDescription"] != DBNull.Value ? reader["goalDescription"].ToString() : "";
                        bool isCompleted = reader["isCompleted"] != DBNull.Value && Convert.ToBoolean(reader["isCompleted"]);

                        if (isCompleted)
                        {
                            continue;
                        }
                        // додаємо запис в словник, якщо немає такої дати
                        if (!goalsData.ContainsKey(goalDate))
                        {
                            goalsData[goalDate] = (goalName, targetValue, description, isCompleted);
                        }
                    }
                }
                db.closeConnection();
            }
        }
        private void ReadOnly()
        {
            previousMonth.Enabled = false;
            previousMonth.ForeColor = Color.Gray;
            previousMonth.Cursor = Cursors.Default;
            previousMonth.IconColor = Color.Gray;
        }
        private void NotReadOnly()
        {
            previousMonth.Enabled = true;
            previousMonth.ForeColor = Color.FloralWhite;
            previousMonth.Cursor = Cursors.Hand;
            previousMonth.IconColor = Color.FloralWhite;
        }
        private void ConfigureBtns()
        {
            previousMonth.MouseEnter += (sender, e) =>
            {
                if (previousMonth.Enabled)
                {
                    previousMonth.IconColor = Color.DeepPink;
                }
            };
            previousMonth.MouseLeave += (sender, e) =>
            {
                if (previousMonth.Enabled)
                {
                    previousMonth.IconColor = Color.FloralWhite;
                }
            };

            nextMonth.MouseEnter += (sender, e) =>
            {
                if (nextMonth.Enabled)
                {
                    nextMonth.IconColor = Color.DeepPink;
                }
            };
            nextMonth.MouseLeave += (sender, e) =>
            {
                if (nextMonth.Enabled)
                {
                    nextMonth.IconColor = Color.FloralWhite;
                }
            };

            toolTip_previousMonth.SetToolTip(previousMonth, "Попередній місяць");
            toolTip_nextMonth.SetToolTip(nextMonth, "Наступний місяць");
        }
        private void GoalProgressActive(Guna2GradientButton btnA)
        {
            btnA.ForeColor = Color.WhiteSmoke;
            btnA.FillColor = Color.FromArgb(0, 61, 175);
            btnA.FillColor2 = Color.FromArgb(0, 61, 175);
            btnA.BorderColor = Color.WhiteSmoke;

            btnA.CustomizableEdges = new CustomizableEdges
            {
                BottomLeft = true,
                BottomRight = true,
                TopLeft = true,
                TopRight = false
            };

            btnA.HoverState.FillColor = Color.FromArgb(0, 61, 175);
            btnA.HoverState.FillColor2 = Color.FromArgb(0, 61, 175);
            btnA.HoverState.BorderColor = Color.FromArgb(172, 229, 255);
        }
        private void GoalProgressInactive(Guna2GradientButton btnIn)
        {
            btnIn.FillColor = SystemColors.Highlight;
            btnIn.FillColor2 = Color.DeepPink;
            btnIn.BorderColor = Color.FromArgb(172, 229, 255);
            btnIn.ForeColor = Color.WhiteSmoke;
            btnIn.CustomizableEdges = new CustomizableEdges
            {
                BottomLeft = true,
                BottomRight = true,
                TopLeft = true,
                TopRight = true
            };
            btnIn.HoverState.FillColor = Color.Transparent;
            btnIn.HoverState.FillColor2 = Color.Transparent;
            btnIn.HoverState.BorderColor = Color.WhiteSmoke;
        }
    }
}
