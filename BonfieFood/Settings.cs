using FontAwesome.Sharp;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace BonfieFood
{
    public partial class Settings : Form
    {
        DataBase db = new DataBase();

        private IconButton currentBtn;
        private Form currentForm;

        public event EventHandler PhotoUpdated;

        public Settings()
        {
            InitializeComponent();
        }
        private void Settings_Load(object sender, EventArgs e)
        {
            LoadUserImg();
            UpdateTexts();

            IconButton[] btn = { personalInfo, EmailAndPass };
            foreach (IconButton i in btn)
            {
                SetupButtonColorEffects(i);
            }

            ActivateButton(personalInfo);
            CurrentPage(new Settings_PersonInfo());

            Language.OnLanguageChanged += ChangeLanguage;
        }
        private void ChangeLanguage(string cultureCode)
        {
            UpdateTexts();
        }
        private void ActivateButton(object senderBtn)
        {
            if (senderBtn != null)
            {
                DisableButton();

                currentBtn = (IconButton)senderBtn;

                currentBtn.BackColor = Color.FromArgb(81, 116, 195);
                currentBtn.ForeColor = Color.Gainsboro;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }
        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(35, 20, 85);
                currentBtn.ForeColor = Color.Gainsboro;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = Color.AliceBlue;
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;

                currentBtn.Padding = new Padding(10, 0, 20, 0);
            }
        }
        private void SetupButtonColorEffects(IconButton btn)
        {
            Color defaultBackColor = btn.BackColor;

            btn.MouseEnter += (s, e) =>
            {
                if (btn != currentBtn) btn.BackColor = Color.FromArgb(20, 40, 85);
            };

            btn.MouseLeave += (s, e) =>
            {
                if (btn != currentBtn) btn.BackColor = defaultBackColor;
            };

            btn.MouseDown += (s, e) =>
            {
                if (btn != currentBtn) btn.BackColor = Color.FromArgb(15, 30, 65);
            };

            btn.MouseUp += (s, e) =>
            {
                if (btn != currentBtn) btn.BackColor = defaultBackColor;
            };
        }

        private void upload_img_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Зображення (*.jpg;*.jpeg;*.png;*.webm)|*.jpg;*.jpeg;*.png;*.webm|Всі файли (*.*)|*.*";
                dialog.Title = "Select a photo";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string fileExtension = Path.GetExtension(dialog.FileName).ToLower();

                    // список дозволених розширень
                    string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".webm" };
                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        MessageBoxError.Show("Файл має недопустимий формат! Дозволені формати: jpg, jpeg, png, webm.");
                        return;
                    }

                    // перевірка розміру файлу (не більше 5 МБ)
                    FileInfo fileInfo = new FileInfo(dialog.FileName);

                    if (fileInfo.Length > 5 * 1024 * 1024)
                    {
                        MessageBoxError.Show("Файл занадто великий! Максимальний розмір — 5 МБ.");
                        return;
                    }
                    if (imgBox.Image != null)
                    {
                        imgBox.Image.Dispose();
                    }

                    imgBox.Image = new Bitmap(Image.FromFile(dialog.FileName), new Size(164, 164));

                    // збереження фото в папку "User-photo" та БД
                    SaveUserProfileImg(CurrentUser.UserId, dialog.FileName);
                }
            }
        }
        private void delete_photo_Click(object sender, EventArgs e)
        {
            bool userConfirmed = MessageBoxAttention.Show("Ви впевнені, що хочете видалити фото?");
            if (userConfirmed)
            {
                if (imgBox.Image != null)
                {
                    imgBox.Image.Dispose();  // звільняємо ресурси
                    imgBox.Image = null;     // очищаємо зображення
                    CurrentUser.ProfilePhotoPath = null;
                    DeleteUserProfilePhotoFromDB(CurrentUser.UserId);

                    Home homeForm = Application.OpenForms.OfType<Home>().FirstOrDefault();
                    homeForm?.LoadUserImg();
                    PhotoUpdated?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    MessageBoxError.Show("Фото для видалення не знайдено!");
                }
            }
        }
        private void LoadUserImg()
        {
            string imagePath = GetUserImgPath();
            if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
            {
                using (Image image = Image.FromFile(imagePath))
                {
                    imgBox.Image = new Bitmap(image, new Size(164, 164));
                }
            }
            else
            {
                imgBox.FillColor = Color.FromArgb(67, 55, 110); // Колір, якщо фото немає
            }
        }
        public string GetUserImgPath()
        {
            string imagePath = "";
            string sql = @"SELECT profilePhotoPath
                            FROM UserPhoto
                            WHERE id_User = @UserId";

            using (SqlCommand cmd = new SqlCommand(sql, db.getConnection()))
            {
                cmd.Parameters.AddWithValue("@UserId", CurrentUser.UserId);
                db.openConnection();
                var result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    imagePath = result.ToString();
                }
                db.closeConnection();
            }
            return imagePath;
        }
        private void SaveUserProfileImg(int userId, string filePath)
        {
            string projectDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
            string folderPath = Path.Combine(projectDirectory, "User-photo");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // генеруємо унікальне ім'я файлу
            string fileExtension = Path.GetExtension(filePath);
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string fileName = $"User_{userId}_{timestamp}{fileExtension}";
            string newFilePath = Path.Combine(folderPath, fileName);

            // збереження нового зображення
            using (Image originalImage = Image.FromFile(filePath))
            using (Image convertedImage = new Bitmap(originalImage))
            {
                convertedImage.Save(newFilePath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }

            SaveProfilePhotoPathToDB(userId, newFilePath);
            CurrentUser.ProfilePhotoPath = newFilePath;

            // оновлення даних у програмі
            Home homeForm = Application.OpenForms.OfType<Home>().FirstOrDefault();
            homeForm?.LoadUserImg();
            PhotoUpdated?.Invoke(this, EventArgs.Empty);

        }
        private void SaveProfilePhotoPathToDB(int userId, string filePath)
        {
            string sqlUpdate = "UPDATE UserPhoto SET profilePhotoPath = @FilePath WHERE id_User = @UserId";
            string sqlInsert = "INSERT INTO UserPhoto (id_User, profilePhotoPath) VALUES (@UserId, @FilePath)";

            using (SqlCommand cmd = new SqlCommand(sqlUpdate, db.getConnection()))
            {
                cmd.Parameters.AddWithValue("@FilePath", filePath);
                cmd.Parameters.AddWithValue("@UserId", userId);
                db.openConnection();

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    using (SqlCommand insertCmd = new SqlCommand(sqlInsert, db.getConnection()))
                    {
                        insertCmd.Parameters.AddWithValue("@UserId", userId);
                        insertCmd.Parameters.AddWithValue("@FilePath", filePath);
                        insertCmd.ExecuteNonQuery();
                    }
                }
                db.closeConnection();
            }
        }
        
        private void DeleteUserProfilePhotoFromDB(int userId)
        {
            string sql = "UPDATE UserPhoto SET profilePhotoPath = NULL WHERE id_User = @UserId";

            using (SqlCommand cmd = new SqlCommand(sql, db.getConnection()))
            {
                cmd.Parameters.AddWithValue("@UserId", userId);
                db.openConnection();
                cmd.ExecuteNonQuery();
                db.closeConnection();
            }
        }
        private void CurrentPage(Form form)
        {
            if (currentForm != null)
            {
                currentForm.Close();
            }

            currentForm = form;
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            guna2Panel_main.Controls.Add(form);
            guna2Panel_main.Tag = form;
            form.BringToFront();
            form.Show();
        }
        private void personalInfo_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            CurrentPage(new Settings_PersonInfo());
        }

        private void UsernameAndPass_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            CurrentPage(new Settings_UsernamePass());
        }

        private void UpdateTexts()
        {
            upload_img.Text = Properties.Resources.upload_img;
            personalInfo.Text = Properties.Resources.personalInfo;
            EmailAndPass.Text = Properties.Resources.EmailAndPass;
            toolTip_delete.SetToolTip(delete_photo, Properties.Resources.toolTip_delete);
            toolTip_Version.SetToolTip(versionApp, Properties.Resources.toolTip_Version);
        }
    }
}
