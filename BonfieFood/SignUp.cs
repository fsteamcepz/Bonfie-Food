using System;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Drawing;

namespace BonfieFood
{
    public partial class SignUp : Form
    {
        DataBase db = new DataBase();
        private bool isPasswordVisible = false;

        public SignUp()
        {
            InitializeComponent();
        }

        private void sign_in_Click(object sender, EventArgs e)
        {
            // закриваємо вікно в якому зараз знаходимося
            this.Hide();

            // перехід до вікна входу
            Login loginForm = new Login();
            loginForm.Show();
        }
        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            // вихід з програми
            Application.Exit();
        }
        private void register_Click(object sender, EventArgs e)
        {
            //////////////////////////////////////////////////////
            // 1.    Checking Username
            //////////////////////////////////////////////////////
            string userName = username_register.Text;

            if (string.IsNullOrWhiteSpace(userName))
            {
                MessageBoxError.Show("Введіть Username!");
                username_register.BorderColor = Color.Red;
                return;
            }
            string symbols = "[]{}+=-&^:;|/><$#@!?№%()€₴~";
            int invalid_symbols = 0;

            for (int i = 0; i < userName.Length; i++)
            {
                for (int j = 0; j < symbols.Length; j++)
                {
                    if (userName.Contains(symbols[j]))
                    {
                        invalid_symbols++;
                    }
                }
            }
            if (invalid_symbols > 0)
            {
                MessageBoxError.Show("В Username є недопустимі символи!");
                username_register.BorderColor = Color.Red;
                return;
            }
            if (userName.Contains(" "))
            {
                MessageBoxError.Show("Username не можна вводити через пробіл!");
                username_register.BorderColor = Color.Red;
                return;
            }
            if ((Regex.Match(userName, @"[а-яА-Я]").Success))
            {
                MessageBoxError.Show("Username повинен складатися з латинських символів!");
                username_register.BorderColor = Color.Red;
                return;
            }
            if (userName.Length > 0 && userName.Length < 4)
            {
                MessageBoxError.Show("Username повинен складатися щонайменше з 4 символів!");
                username_register.BorderColor = Color.Red;
                return;
            }
            username_register.BorderColor = ColorTranslator.FromHtml("#CC6EAA");


            //////////////////////////////////////////////////////
            // 2.    Checking Email
            //////////////////////////////////////////////////////
            string emailUser = email_register.Text;
            string correct_email = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                 @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";

            // якщо є пробіл або порожньо
            if (string.IsNullOrWhiteSpace(emailUser))
            {
                MessageBoxError.Show("Введіть Email!");
                email_register.BorderColor = Color.Red;
                return;
            }
            if (!Regex.IsMatch(emailUser, correct_email, RegexOptions.IgnoreCase))
            {
                MessageBoxError.Show("Email введено невірно!");
                email_register.BorderColor = Color.Red;
                return;
            }
            if (CheckEmail(emailUser))
            {
                MessageBoxError.Show("Email вже зареєстровано! Введіть будь ласка інший.");
                email_register.BorderColor = Color.Red;
                return;
            }
            email_register.BorderColor = ColorTranslator.FromHtml("#CC6EAA");


            //////////////////////////////////////////////////////
            // 3.    Checking Password
            //////////////////////////////////////////////////////
            string passwordUser = pass_register.Text;

            // якщо є пробіл або порожньо
            if (string.IsNullOrWhiteSpace(passwordUser))
            {
                MessageBoxError.Show("Введіть Password!");
                pass_register.BorderColor = Color.Red;
                return;
            }
            if (passwordUser.Length > 0 && passwordUser.Length < 4)
            {
                MessageBoxError.Show("Password повинен складатися щонайменше з 4 символів!");
                pass_register.BorderColor = Color.Red;
                return;
            }
            if (Regex.Match(passwordUser, @"[а-яА-Я]").Success)
            {
                MessageBoxError.Show("Password повинен складатися з латинських символів!");
                pass_register.BorderColor = Color.Red;
                return;
            }
            pass_register.BorderColor = ColorTranslator.FromHtml("#CC6EAA");


            //////////////////////////////////////////////////////
            // 4.    Creating sql-query
            //////////////////////////////////////////////////////
                        
            // перевіряємо наявність username у БД
            if (CheckUser(userName, db))
            {
                MessageBoxError.Show("Такий Username вже існує! Будь ласка, введіть інший!");
                return;
            }

            // хешуємо пароль
            string hashedPassword = PasswordHashing.HashPassword(passwordUser);

            string queryInsert = @"
                   INSERT INTO Users (userName, emailUser, passwordUser)
                   VALUES (@userName, @emailUser, @passwordUser)";

            using (SqlCommand command = new SqlCommand(queryInsert, db.getConnection()))
            {
                db.openConnection();

                command.Parameters.AddWithValue("@userName", userName);
                command.Parameters.AddWithValue("@emailUser", emailUser);
                command.Parameters.AddWithValue("@passwordUser", hashedPassword);

                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBoxSuccess.Show("Реєстрація пройшла успішно!");

                    // автоматично зберігаємо дані користувача
                    Properties.Settings.Default.Username = userName;
                    Properties.Settings.Default.Password = passwordUser;
                    Properties.Settings.Default.Save();

                    this.Hide();
                    Login loginForm = new Login();
                    loginForm.Show();

                    db.closeConnection();
                }
                else
                {
                    MessageBoxError.Show("Не вдалося створити акаунт!");
                }
            }
        }
        private bool CheckUser(string userName, DataBase dataBase)
        {
            string querySelect = "SELECT COUNT(*) FROM Users WHERE userName = @userName";
            
            SqlCommand command = new SqlCommand(querySelect, dataBase.getConnection());
            command.Parameters.AddWithValue("@userName", userName);

            dataBase.openConnection();
            int result = (int)command.ExecuteScalar(); // повертає кількість записів
            dataBase.closeConnection();

            return result > 0; // 1 = якщо користувач існує, 0 = неіснує
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
        private void btnShowPass_Click(object sender, EventArgs e)
        {
            if (isPasswordVisible)
            {
                isPasswordVisible = false;
                pass_register.PasswordChar = '●';
                btnShowPass.Image = Properties.Resources.password_hide;
            }
            else
            {
                isPasswordVisible = true;
                pass_register.PasswordChar = '\0';
                btnShowPass.Image = Properties.Resources.password_show;
            }
        }
    }
}