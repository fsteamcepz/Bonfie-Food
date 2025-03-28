using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BonfieFood
{
    public partial class ForgotPassword : Form
    {
        DataBase db = new DataBase();
        OAuthEmail emailAuth = new OAuthEmail();
        
        private const int passLength = 10;

        public ForgotPassword()
        {
            InitializeComponent();
        }
        private void return_to_sign_in_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login loginForm = new Login();
            loginForm.Show();
        }
        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private async void send_email_Click(object sender, EventArgs e)
        {
            //////////////////////////////////////////////////////
            // Перевірка Email
            //////////////////////////////////////////////////////
            string email = password_reset.Text;
            string correct_email = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                 @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";

            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBoxError.Show("Введіть Email!");
                return;
            }
            if (!Regex.IsMatch(email, correct_email, RegexOptions.IgnoreCase))
            {
                MessageBoxError.Show("Email введено невірно!");
                return;
            }
            if (!CheckEmail(email))
            {
                MessageBoxError.Show("Email не знайдено! Будь ласка, переконайтеся, що ви правильно ввели свою електронну адресу.");
                password_reset.Text = "";
                return;
            }

            // генеруємо новий пароль та відправляємо на пошту
            string newPassword = GenerateRandomPassword(passLength);
            string hashedPassword = PasswordHashing.HashPassword(newPassword);

            if (UpdatePassword(email, hashedPassword))
            {
                bool emailSent = await SendPasswordEmail(email, newPassword);
                if (emailSent)
                {
                    MessageBoxSuccess.Show("На вашу електронну пошту надіслано новий пароль!");

                    this.Hide();
                    Login loginForm = new Login();
                    loginForm.Show();
                }
                else
                {
                    password_reset.Text = "";
                }
            }
            else
            {
                MessageBoxError.Show("Не вдалося оновити пароль. Спробуйте ще раз.");
            }
        }
        private async Task<bool> SendPasswordEmail(string emailUser, string newPassword)
        {
            try
            {
                string name = GetUserName(emailUser);
                string title = "BonfieFood - Password recovery";
                string body = $"Вітаємо {name}.\n\nВаш новий пароль: {newPassword}\n\nРекомендуємо змінити його після входу в систему.\n\nКоманда BonfieFood";

                bool emailSent = await emailAuth.SendEmail(emailUser, title, body);
                return emailSent;
            }
            catch (Exception ex)
            {
                MessageBoxError.Show("Помилка надсилання листа: " + ex.Message);
                return false;
            }
        }
        private bool UpdatePassword(string email, string hashedPassword)
        {
            string queryUpdate = "UPDATE Users SET passwordUser = @NewPassword WHERE emailUser = @EmailUser";

            using (SqlCommand command = new SqlCommand(queryUpdate, db.getConnection()))
            {
                command.Parameters.AddWithValue("@NewPassword", hashedPassword);
                command.Parameters.AddWithValue("@EmailUser", email);

                db.openConnection();
                int result = command.ExecuteNonQuery();
                db.closeConnection();

                return result > 0;
            }
        }
        private string GenerateRandomPassword(int length)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*";
            Random random = new Random();

            char[] pass = new char[length];

            for (int i = 0; i < length; i++)
            {
                pass[i] = chars[random.Next(chars.Length)];
            }
            return new string(pass);
        }
        private bool CheckEmail(string email)
        {
            string querySelect = @"SELECT COUNT(*)
                                   FROM Users
                                   WHERE emailUser = @EmailUser";

            using (SqlCommand command = new SqlCommand(querySelect, db.getConnection()))
            {
                command.Parameters.AddWithValue("@EmailUser", email);
                db.openConnection();
                int result = (int)command.ExecuteScalar();
                db.closeConnection();

                return result > 0;
            }
        }
        private string GetUserName(string emailReset)
        {
            string sql = "SELECT userName FROM Users WHERE emailUser = @emailReset";

            using (SqlCommand cmd = new SqlCommand(sql, db.getConnection()))
            {
                cmd.Parameters.Add("@emailReset", SqlDbType.VarChar).Value = emailReset;
                db.openConnection();
                object result = cmd.ExecuteScalar();
                db.closeConnection();
                if (result != null && result != DBNull.Value)
                {
                    return result.ToString();
                }
            }
            return null;
        }
    }
}