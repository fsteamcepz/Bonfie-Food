using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BonfieFood
{
    public partial class Settings_UsernamePass : Form
    {
        DataBase db = new DataBase();
        private bool isPasswordVisible = false;

        public Settings_UsernamePass()
        {
            InitializeComponent();
        }
        private void Settings_UsernamePass_Load(object sender, EventArgs e)
        {
            GetUserEmail();
            ReadOnlyCurrentEmail();
            UpdateTexts();

            Language.OnLanguageChanged += ChangeLanguage;
        }
        private void ChangeLanguage(string cultureCode)
        {
            UpdateTexts();
        }
        private void ReadOnlyCurrentEmail()
        {
            currentEmail.ReadOnly = true;
            currentEmail.Enabled = false;
            currentEmail.FillColor = Color.FromArgb(35, 20, 85);
            currentEmail.BorderColor = Color.FromArgb(197, 116, 222);
            currentEmail.DisabledState.FillColor = Color.FromArgb(35, 20, 85);
            currentEmail.DisabledState.BorderColor = Color.FromArgb(197, 116, 222);
            currentEmail.DisabledState.ForeColor = Color.Gray;
            currentEmail.Cursor = Cursors.Default;

            currentEmail.Refresh();
        }
        private void GetUserEmail()
        {
            string query = @"SELECT emailUser
                             FROM Users
                             WHERE idUser = @userId";
            string email;

            using (SqlCommand command = new SqlCommand(query, db.getConnection()))
            {
                command.Parameters.AddWithValue("@userId", CurrentUser.UserId);

                db.openConnection();
                email = command.ExecuteScalar()?.ToString();
                db.closeConnection();
            }
            currentEmail.Text = email;
        }
        private void saveEmPass_Click(object sender, EventArgs e)
        {
            // Email
            string curr_Email = currentEmail.Text;
            string new_Email = newEmail.Text;

            // Password
            string curr_Password = currentPass.Text;
            string new_Password = newPass.Text;
            string confirm_Password = confirmPass.Text;

            bool emailUpdated = false;
            bool passwordUpdated = false;

            if (string.IsNullOrWhiteSpace(new_Email) && string.IsNullOrWhiteSpace(new_Password))
            {
                MessageBoxError.Show("Будь ласка, введіть новий Email або Password!");
                return;
            }

            if (!string.IsNullOrWhiteSpace(new_Email))
            {
                string correct_email = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                                       @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";

                if (!Regex.IsMatch(new_Email, correct_email, RegexOptions.IgnoreCase))
                {
                    MessageBoxError.Show("Новий Email введено невірно!");
                    newEmail.BorderColor = Color.Red;
                    return;
                }
                if (new_Email.Equals(curr_Email, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBoxError.Show("Email «" + curr_Email + "» вже використовується! Будь ласка, введіть інший!");
                    newEmail.BorderColor = Color.Red;
                    return;
                }
                if (IsEmailAlreadyExistsDB(new_Email))
                {
                    MessageBoxError.Show("Ця електронна адреса вже зареєстрована іншим користувачем!");
                    newEmail.BorderColor = Color.Red;
                    return;
                }
                newEmail.BorderColor = Color.FromArgb(197, 116, 222);

                string queryUpdateEmail = @"UPDATE Users
                                          SET emailUser = @newEmail
                                          WHERE idUser = @userId";

                using (SqlCommand command = new SqlCommand(queryUpdateEmail, db.getConnection()))
                {
                    command.Parameters.AddWithValue("@newEmail", new_Email);
                    command.Parameters.AddWithValue("@userId", CurrentUser.UserId);

                    db.openConnection();
                    command.ExecuteNonQuery();
                    db.closeConnection();
                }

                emailUpdated = true;
            }

            if (!string.IsNullOrWhiteSpace(new_Password))
            {
                if (!PasswordHashing.VerifyPassword(curr_Password, GetUserPasswordHash()))
                {
                    MessageBoxError.Show("Неправильний поточний пароль!");
                    currentPass.BorderColor = Color.Red;
                    return;
                }
                currentPass.BorderColor = Color.FromArgb(197, 116, 222);

                if (new_Password != confirm_Password)
                {
                    MessageBoxError.Show("Новий пароль не співпадає з підтвердженим паролем!");
                    newPass.BorderColor = Color.Red;
                    confirmPass.BorderColor = Color.Red;
                    return;
                }
                newPass.BorderColor = Color.FromArgb(197, 116, 222);
                confirmPass.BorderColor = Color.FromArgb(197, 116, 222);

                string hashedPassword = PasswordHashing.HashPassword(new_Password);
                string queryUpdatePassword = "UPDATE Users SET passwordUser = @newPassword WHERE idUser = @userId";

                using (SqlCommand command = new SqlCommand(queryUpdatePassword, db.getConnection()))
                {
                    command.Parameters.AddWithValue("@newPassword", hashedPassword);
                    command.Parameters.AddWithValue("@userId", SqlDbType.Int).Value = CurrentUser.UserId;

                    db.openConnection();
                    command.ExecuteNonQuery();
                    db.closeConnection();
                }

                passwordUpdated = true;
            }

            if (emailUpdated && passwordUpdated)
            {
                MessageBoxSuccess.Show("Джекпот! Email та пароль успішно оновлено!");
            }
            else if (emailUpdated)
            {
                MessageBoxSuccess.Show("Чудово! Email успішно оновлено!");
            }
            else if (passwordUpdated)
            {
                MessageBoxSuccess.Show("Юху! Пароль успішно оновлено!");
            }

            newEmail.Text = "";
            currentPass.Text = "";
            newPass.Text = "";
            confirmPass.Text = "";
            GetUserEmail();
        }
        private bool IsEmailAlreadyExistsDB(string email)
        {
            string query = "SELECT COUNT(*) FROM Users WHERE emailUser = @email AND idUser != @userId";
            using (SqlCommand command = new SqlCommand(query, db.getConnection()))
            {
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@userId", CurrentUser.UserId);
                db.openConnection();
                int count = (int)command.ExecuteScalar();
                db.closeConnection();

                return count > 0;
            }
        }
        private string GetUserPasswordHash()
        {
            string query = "SELECT passwordUser FROM Users WHERE idUser = @userId";
            using (SqlCommand command = new SqlCommand(query, db.getConnection()))
            {
                command.Parameters.AddWithValue("@userId", CurrentUser.UserId);

                db.openConnection();
                object result = command.ExecuteScalar();
                db.closeConnection();

                return result?.ToString() ?? string.Empty;
            }
        }
        private void btnShowPassword_Click(object sender, EventArgs e)
        {
            if (isPasswordVisible)
            {
                isPasswordVisible = false;
                newPass.PasswordChar = '●';
                iconNewPass.Image = Properties.Resources.password_hide;
            }
            else
            {
                isPasswordVisible = true;
                newPass.PasswordChar = '\0';
                iconNewPass.Image = Properties.Resources.password_show;
            }
        }
        private void btnShowConfirmPassword_Click(object sender, EventArgs e)
        {
            if (isPasswordVisible)
            {
                isPasswordVisible = false;
                confirmPass.PasswordChar = '●';
                iconConfirmPass.Image = Properties.Resources.password_hide;
            }
            else
            {
                isPasswordVisible = true;
                confirmPass.PasswordChar = '\0';
                iconConfirmPass.Image = Properties.Resources.password_show;
            }
        }

        private void UpdateTexts()
        {
            label_EmailPassword.Text = Properties.Resources.label_EmailPassword;
            label_Message_h2.Text = Properties.Resources.label_Message_h2;
            label_Email.Text = Properties.Resources.label_Email;
            label_NewEmail.Text = Properties.Resources.label_NewEmail;
            label_CurrentPassword.Text = Properties.Resources.label_CurrentPassword;
            label_NewPassword.Text = Properties.Resources.label_NewPassword;
            label_ConfirmPassword.Text = Properties.Resources.label_ConfirmPassword;
            saveEmPass.Text = Properties.Resources.saveEmPass;
        }
    }
}