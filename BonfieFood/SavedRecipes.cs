using FontAwesome.Sharp;
using Guna.UI2.WinForms;
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

namespace BonfieFood
{
    public partial class SavedRecipes : Form
    {
        DataBase db = new DataBase();
        public SavedRecipes()
        {
            InitializeComponent();
        }

        private void SavedRecipes_Load(object sender, EventArgs e)
        {
            LoadSavedRecipes();
        }
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            SystemSounds.Hand.Play();
        }
        private void LoadSavedRecipes()
        {
            string query = @"SELECT r.recipeName, r.calories, r.category, r.yield
                             FROM Recipes r
                             JOIN UserSavedRecipes u ON u.id_Recipe = r.idRecipe
                             WHERE id_User = @UserId AND isSaved = 1";

            db.openConnection();

            using (SqlCommand cmd = new SqlCommand(query, db.getConnection()))
            {
                cmd.Parameters.AddWithValue("@UserId", CurrentUser.UserId);
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    mainPanelOfRecipes.Controls.Clear();
                    mainPanelOfRecipes.Location = new Point(0, 30);
                    mainPanelOfRecipes.Size = new Size(630, 325);

                    int yOffset = 0;
                    int count = 0;
                    while (r.Read())
                    {
                        string nameRecipe = r["recipeName"].ToString();
                        decimal caloriesRecipe = Convert.ToDecimal(r["calories"]);
                        string categoryRecipe = r["category"].ToString();
                        int yieldRecipe = Convert.ToInt32(r["yield"]);

                        Guna2Panel miniPanel = new Guna2Panel
                        {
                            Size = new Size(630, 125),
                            BackColor = Color.DarkSlateBlue,
                            Margin = new Padding(0),
                            Location = new Point(0, yOffset)
                        };

                        IconPictureBox savedRecipe = new IconPictureBox()
                        {
                            Size = new Size(28, 28),
                            Location = new Point(10, 12),
                            ForeColor = Color.Violet,
                            IconChar = IconChar.Heart,
                            IconFont = IconFont.Solid,
                            BackColor = Color.Transparent
                        };

                        Label nameOfRecipe = new Label()
                        {
                            Text = nameRecipe,
                            Size = new Size(574, 29),
                            Location = new Point(44, 9),
                            Font = new Font("Constantia", 18, FontStyle.Bold),
                            ForeColor = Color.FromArgb(253, 235, 250),
                            AutoSize = false,
                            TextAlign = ContentAlignment.MiddleLeft
                        };

                        Label calories = new Label()
                        {
                            Text = "Calories:",
                            Size = new Size(114, 29),
                            Location = new Point(44, 52),
                            Font = new Font("Constantia", 18, FontStyle.Bold | FontStyle.Italic),
                            ForeColor = Color.FromArgb(253, 235, 250),
                            AutoSize = true
                        };

                        Label valueCalories = new Label()
                        {
                            Text = (caloriesRecipe / yieldRecipe).ToString("0"),
                            Size = new Size(90, 28),
                            Location = new Point(166, 53),
                            Font = new Font("Rockwell Extra Bold", 18),
                            ForeColor = Color.FromArgb(76, 151, 254),
                            AutoSize = true,
                        };

                        Label category = new Label()
                        {
                            Text = categoryRecipe,
                            Location = new Point(46, 90),
                            Font = new Font("Arial Black", 10, FontStyle.Bold),
                            ForeColor = Color.FromArgb(253, 235, 250),
                            AutoSize = true
                        };

                        Label ingredients = new Label()
                        {
                            Text = yieldRecipe + " Ingredients",
                            Location = new Point(496, 88),
                            Font = new Font("Segoe UI", 12),
                            ForeColor = SystemColors.ControlDark,
                            AutoSize = true
                        };

                        miniPanel.Controls.Add(savedRecipe);
                        miniPanel.Controls.Add(nameOfRecipe);
                        miniPanel.Controls.Add(calories);
                        miniPanel.Controls.Add(valueCalories);
                        miniPanel.Controls.Add(category);
                        miniPanel.Controls.Add(ingredients);

                        mainPanelOfRecipes.Controls.Add(miniPanel);

                        yOffset += 135;
                        count++;
                    }

                    if (count > 2)
                    {
                        mainPanelOfRecipes.Size = new Size(630, 345);
                    }
                    else
                    {
                        mainPanelOfRecipes.Size = new Size(630, 355);
                    }
                    db.closeConnection();
                }
            }
        }
    }
}