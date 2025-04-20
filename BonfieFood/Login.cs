using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace BonfieFood
{
    public partial class Login : Form
    {
        DataBase db = new DataBase();
        private bool isPasswordVisible = false;

        public Login()
        {
            InitializeComponent();
        }
        private void sign_up_Click(object sender, EventArgs e)
        {
            this.Hide();
            SignUp signUpForm = new SignUp();
            signUpForm.Show();
        }
        private void forgot_password_Click(object sender, EventArgs e)
        {
            this.Hide();
            ForgotPassword forgotPassForm = new ForgotPassword();
            forgotPassForm.Show();
        }
        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            // вихід з програми
            Application.Exit();
        }
        private void btn_login_Click(object sender, EventArgs e)
        {
            string userName = username_login.Text;
            string password = password_login.Text;
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
            {
                MessageBoxError.Show("Будь ласка, введіть Username та Password!");
                return;
            }

            if (ValidateUserCredentials(userName, password))
            {
                // отримуємо ID користувача
                int userId = GetUserId(userName);

                if (userId > 0)
                {
                    // зберігаємо поточного користувача
                    CurrentUser.UserId = userId;
                    CurrentUser.UserName = userName;

                    // запам'ятовуємо користувача
                    SaveRememberMeState(userName, password, rememberMe.Checked);

                    //MessageBoxSuccess.Show("Вхід успішний! Ласкаво просимо, " + CurrentUser.UserName + "!");
                    username_login.Text = "";
                    password_login.Text = "";

                    this.Hide();
                    Home homeForm = new Home();
                    homeForm.Show();
                }
                else
                {
                    MessageBoxError.Show("Помилка отримання даних користувача!");
                }
            }
            else
            {
                MessageBoxError.Show("Неправильний Username або Password!");
                return;
            }
        }
        private bool ValidateUserCredentials(string userName, string password)
        {
            // SQL-запит для отримання хешу пароля
            string querySelect = @"SELECT passwordUser
                                   FROM Users
                                   WHERE userName = @userName";

            using (SqlCommand command = new SqlCommand(querySelect, db.getConnection()))
            {
                command.Parameters.Add("@userName", SqlDbType.VarChar).Value = userName;

                db.openConnection();

                // виконуємо запит і отримуємо результат
                object result = command.ExecuteScalar();

                db.closeConnection();

                if (result == null || result == DBNull.Value)
                {
                    return false;
                }

                // перетворюємо результат у рядок
                string hashedPassword = result.ToString();

                // перевіряємо пароль
                return PasswordHashing.VerifyPassword(password, hashedPassword);
            }
        }
        private int GetUserId(string userName)
        {
            string sql = "SELECT idUser FROM Users WHERE userName = @userName";

            using (SqlCommand cmd = new SqlCommand(sql, db.getConnection()))
            {
                cmd.Parameters.Add("@userName", SqlDbType.VarChar).Value = userName;
                db.openConnection();

                object result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    return Convert.ToInt32(result);
                }
                db.closeConnection();
            }
            return -1;
        }

        private void btnShowPassword_Click(object sender, EventArgs e)
        {
            if (isPasswordVisible)
            {
                isPasswordVisible = false;
                password_login.PasswordChar = '●';
                btnShowPassword.Image = Properties.Resources.password_hide;
            }
            else
            {
                isPasswordVisible = true;
                password_login.PasswordChar = '\0';
                btnShowPassword.Image = Properties.Resources.password_show;
            }
        }
        public bool AutomaticLogin()
        {
            string savedUsername = Properties.Settings.Default.Username;
            string savedPassword = Properties.Settings.Default.Password;

            if (!string.IsNullOrEmpty(savedUsername) && !string.IsNullOrEmpty(savedPassword))
            {
                // виконуємо автоматичний вхід
                if (ValidateUserCredentials(savedUsername, savedPassword))
                {
                    int userId = GetUserId(savedUsername);
                    if (userId > 0)
                    {
                        // зберігаємо поточного користувача
                        CurrentUser.UserId = userId;
                        CurrentUser.UserName = savedUsername;

                        this.Hide();
                        return true;
                    }
                }
            }
            return false;
        }
        private void SaveRememberMeState(string userName, string password, bool remember)
        {
            if (remember)
            {
                Properties.Settings.Default.Username = userName;
                Properties.Settings.Default.Password = password;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.Username = string.Empty;
                Properties.Settings.Default.Password = string.Empty;
                Properties.Settings.Default.Save();
            }
        }
    }
}