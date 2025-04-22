using FontAwesome.Sharp;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BonfieFood
{
    public partial class Recipes: Form
    {
        DataBase db = new DataBase();
        private Edamam_Recipe edamamRecipe = new Edamam_Recipe();
        private bool isMouseOverSend = false;
        private bool isBtnSearchClicked = false;
        private bool isEnterPressed = false;
        private bool isDataLoaded = false;
        public event Action RecipesUpdated;

        // зберігаємо поточні продукти
        private List<Edamam_Recipe> currentRecipes = new List<Edamam_Recipe>();

        public Recipes()
        {
            InitializeComponent();
        }

        private void Recipes_Load(object sender, EventArgs e)
        {
            btnSearchRecipe.Image = Properties.Resources.search_recipes;
            mainPanel.Controls.Clear();
            labelQuantity.Visible = false;
            recipesQuantity.Visible = false;

            search.KeyDown += OnMessageFromUserKeyDown;
        }
        private async void btnSearchRecipe_Click(object sender, EventArgs e)
        {
            isMouseOverSend = false;
            isBtnSearchClicked = true;
            btnSearchRecipe.Image = Properties.Resources.search_recipes;

            try
            {
                string nameOfRecipe = search.Text.Trim();

                if (string.IsNullOrEmpty(nameOfRecipe))
                {
                    MessageBoxError.Show("Будь ласка, введіть назву рецепта для пошуку!");
                    return;
                }

                isDataLoaded = false;
                var recipes = await edamamRecipe.SearchRecipesAsync(nameOfRecipe);

                if (recipes == null || recipes.Count == 0)
                {
                    search.Text = "";
                    return;
                }

                search.Text = "";
                currentRecipes = recipes;

                isDataLoaded = true;
                ShowRecipes(currentRecipes);

                ActionHistory.SaveActionHistoryToDB(db, "Пошук рецептів");
            }
            catch (Exception ex)
            {
                MessageBoxError.Show($"Помилка: {ex.Message}");
            }
        }
        private void ShowRecipes(List<Edamam_Recipe> recipes)
        {
            if (!isDataLoaded)
            {
                return;
            }

            mainPanel.Controls.Clear();
            mainPanel.Location = new Point(19, 101);
            mainPanel.Size = new Size(682, 355);

            int yOffset = 0;
            int countResult = 2;
            Guna2Panel activePanel = null;

            foreach (var rec in recipes)
            {
                Guna2Panel miniPanel = new Guna2Panel
                {
                    Size = new Size(682, 166),
                    BackColor = Color.Transparent,
                    Margin = new Padding(0),
                    Location = new Point(0, yOffset),
                    Tag = rec.Name
                };

                Guna2PictureBox photoRecipe = new Guna2PictureBox()
                {
                    Size = new Size(100, 94),
                    Location = new Point(15, 9),
                    ImageLocation = rec.Photo,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    BorderRadius = 30,
                    FillColor = Color.Gray,
                };

                IconPictureBox saveRecipe = new IconPictureBox()
                {
                    Size = new Size(28, 28),
                    Location = new Point(10, 6),
                    ForeColor = Color.AliceBlue,
                    IconChar = IconChar.Heart,
                    IconFont = IconFont.Regular,
                    BackColor = Color.Transparent
                };
                Guna2HtmlToolTip toolTip_save = new Guna2HtmlToolTip()
                {
                    BackColor = Color.FromArgb(14, 15, 40),
                    BorderColor = Color.FromArgb(70, 67, 83),
                    ForeColor = Color.FromArgb(253, 235,250),
                    InitialDelay = 500,
                    ReshowDelay = 100,
                    Font = new Font("Segoe UI", 9),
                    AutomaticDelay = 500,
                    AutoPopDelay = 5000
                };
                bool isRecipeSaved = IsRecipeSavedByUser(rec.Name, rec.Calories);
                saveRecipe.IconFont = isRecipeSaved ? IconFont.Solid : IconFont.Regular;

                toolTip_save.SetToolTip(saveRecipe, isRecipeSaved ? "Рецепт збережено!" : "Зберегти рецепт");

                saveRecipe.MouseEnter += (sender, e) =>
                {
                    if (!isRecipeSaved)
                    {
                        saveRecipe.IconFont = IconFont.Solid;
                    }
                };
                saveRecipe.MouseLeave += (sender, e) =>
                {
                    if (!isRecipeSaved)
                    {
                        saveRecipe.IconFont = IconFont.Regular;
                    }
                };

                saveRecipe.Click += async (sender, e) =>
                {
                    if (!isRecipeSaved)
                    {
                        await SaveRecipe(rec);
                        saveRecipe.IconFont = IconFont.Solid;
                        isRecipeSaved = true;
                        toolTip_save.SetToolTip(saveRecipe, "Рецепт збережено!");
                        RecipesUpdated?.Invoke();
                    }
                    else
                    {
                        await RemoveRecipe(rec.Name, rec.Calories);
                        saveRecipe.IconFont = IconFont.Regular;
                        isRecipeSaved = false;
                        toolTip_save.SetToolTip(saveRecipe, "Зберегти рецепт");
                        RecipesUpdated?.Invoke();
                    }
                };

                Label category = new Label()
                {
                    Text = rec.Category,
                    Size = new Size(121, 48),
                    Location = new Point(5, 113),
                    Font = new Font("Arial Black", 10, FontStyle.Bold),
                    ForeColor = Color.FromArgb(253, 235, 250),
                    AutoSize = false,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                
                Label nameOfRecipe = new Label()
                {
                    Text = rec.Name,
                    Size = new Size(534, 29),
                    Location = new Point(137, 9),
                    Font = new Font("Constantia", 18, FontStyle.Bold),
                    ForeColor = Color.FromArgb(253, 235, 250),
                    AutoSize = false,
                    TextAlign = ContentAlignment.MiddleLeft
                };
                
                Label calories = new Label()
                {
                    Text = "Calories:",
                    Size = new Size(114, 29),
                    Location = new Point(137, 51),
                    Font = new Font("Constantia", 18, FontStyle.Bold | FontStyle.Italic),
                    ForeColor = Color.FromArgb(253, 235, 250),
                    AutoSize = true
                };
                Label valueCalories = new Label()
                {
                    Text = (rec.Calories / rec.Yield).ToString("0"),
                    Size = new Size(90, 28),
                    Location = new Point(258, 51),
                    Font = new Font("Rockwell Extra Bold", 18),
                    ForeColor = Color.FromArgb(76, 151, 254),
                    AutoSize = true,
                };
                
                Label valueProtein = new Label()
                {
                    Text = rec.Protein.ToString("0.0") + "g",
                    Size = new Size(82, 29),
                    Location = new Point(387, 44),
                    Font = new Font("Segoe UI", 16),
                    ForeColor = Color.FromArgb(253, 235, 250),
                    AutoSize = false,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                Guna2Button p_circle = new Guna2Button()
                {
                    Size = new Size(12, 12),
                    Location = new Point(423, 78),
                    BorderRadius = 5,
                    BorderThickness = 2,
                    BorderColor = Color.FromArgb(252, 175, 62),
                    FillColor = Color.FromArgb(252, 175, 62),
                    Animated = true,
                };
                p_circle.HoverState.FillColor = Color.FromArgb(252, 175, 62);
                p_circle.HoverState.BorderColor = Color.FromArgb(252, 175, 62);

                Label valueFat = new Label()
                {
                    Text = rec.Fat.ToString("0.0") + "g",
                    Size = new Size(82, 29),
                    Location = new Point(483, 44),
                    Font = new Font("Segoe UI", 16),
                    ForeColor = Color.FromArgb(253, 235, 250),
                    AutoSize = false,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                Guna2Button f_circle = new Guna2Button()
                {
                    Size = new Size(12, 12),
                    Location = new Point(518, 78),
                    BorderRadius = 5,
                    BorderThickness = 2,
                    BorderColor = Color.FromArgb(197, 116, 222),
                    FillColor = Color.FromArgb(197, 116, 222),
                    Animated = true
                };
                f_circle.HoverState.FillColor = Color.FromArgb(197, 116, 222);
                f_circle.HoverState.BorderColor = Color.FromArgb(197, 116, 222);

                Label valueCarbohydrates = new Label()
                {
                    Text = rec.Carbohydrates.ToString("0.0") + "g",
                    Size = new Size(82, 29),
                    Location = new Point(583, 44),
                    Font = new Font("Segoe UI", 16),
                    ForeColor = Color.FromArgb(253, 235, 250),
                    AutoSize = false,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                Guna2Button c_circle = new Guna2Button()
                {
                    Size = new Size(12, 12),
                    Location = new Point(618, 78),
                    BorderRadius = 5,
                    BorderThickness = 2,
                    BorderColor = Color.FromArgb(40, 184, 171),
                    FillColor = Color.FromArgb(40, 184, 171),
                    Animated = true
                };
                c_circle.HoverState.FillColor = Color.FromArgb(40, 184, 171);
                c_circle.HoverState.BorderColor = Color.FromArgb(40, 184, 171);

                Guna2Panel paneIng = new Guna2Panel()
                {
                    Size = new Size(527, 65),
                    Location = new Point(138, 96),
                    BackColor = Color.Transparent,
                    AutoSize = false,
                    AutoScroll = true,
                };
                Label ingredients = new Label()
                {
                    Text = rec.IngredientCount.ToString() + " Ingredients: " +
                           string.Join(", ", rec.Ingredients),
                    Location = new Point(0, 0),
                    Font = new Font("Segoe UI", 12),
                    ForeColor = SystemColors.ControlDark,
                    AutoSize = true,
                    MaximumSize = new Size(508, 0)
                };

                Guna2HtmlToolTip protein = new Guna2HtmlToolTip()
                {
                    BackColor = Color.FromArgb(14, 15, 40),
                    BorderColor = Color.FromArgb(70, 67, 83),
                    ForeColor = Color.FromArgb(253, 235, 250),
                    InitialDelay = 500,
                    ReshowDelay = 100,
                    Font = new Font("Segoe UI", 9),
                    AutomaticDelay = 500,
                    AutoPopDelay = 5000
                };
                Guna2HtmlToolTip fat = new Guna2HtmlToolTip()
                {
                    BackColor = Color.FromArgb(14, 15, 40),
                    BorderColor = Color.FromArgb(70, 67, 83),
                    ForeColor = Color.FromArgb(253, 235, 250),
                    InitialDelay = 500,
                    ReshowDelay = 100,
                    Font = new Font("Segoe UI", 9),
                    AutomaticDelay = 500,
                    AutoPopDelay = 5000
                };
                Guna2HtmlToolTip carbohydrates = new Guna2HtmlToolTip()
                {
                    BackColor = Color.FromArgb(14, 15, 40),
                    BorderColor = Color.FromArgb(70, 67, 83),
                    ForeColor = Color.FromArgb(253, 235, 250),
                    InitialDelay = 500,
                    ReshowDelay = 100,
                    Font = new Font("Segoe UI", 9),
                    AutomaticDelay = 500,
                    AutoPopDelay = 5000
                };
                protein.SetToolTip(p_circle, "Білки");
                fat.SetToolTip(f_circle, "Жири");
                carbohydrates.SetToolTip(c_circle, "Вуглеводи");

                miniPanel.Controls.Add(saveRecipe);
                miniPanel.Controls.Add(photoRecipe);
                miniPanel.Controls.Add(category);
                miniPanel.Controls.Add(nameOfRecipe);
                miniPanel.Controls.Add(calories);
                miniPanel.Controls.Add(valueCalories);
                miniPanel.Controls.Add(valueProtein);
                miniPanel.Controls.Add(p_circle);
                miniPanel.Controls.Add(valueFat);
                miniPanel.Controls.Add(f_circle);
                miniPanel.Controls.Add(valueCarbohydrates);
                miniPanel.Controls.Add(c_circle);
                miniPanel.Controls.Add(paneIng);
                paneIng.Controls.Add(ingredients);

                miniPanel.MouseEnter += (sender, e) =>
                {
                    if (activePanel != null && activePanel != sender)
                    {
                        activePanel.FillColor = Color.Transparent;
                    }

                    Guna2Panel panel = sender as Guna2Panel;
                    panel.FillColor = Color.FromArgb(64, 64, 64);
                    activePanel = panel;
                };
                miniPanel.MouseLeave += (sender, e) =>
                {
                    Guna2Panel panel = sender as Guna2Panel;
                    if (panel != activePanel)
                    {
                        panel.FillColor = Color.Transparent;
                    }
                };

                mainPanel.Controls.Add(miniPanel);
                yOffset += 166;
            }

            labelQuantity.Visible = true;
            recipesQuantity.Visible = true;
            recipesQuantity.Text = recipes.Count.ToString();

            if (recipes.Count > countResult)
            {
                mainPanel.Size = new Size(699, 355);
            }
            else
            {
                mainPanel.Size = new Size(682, 355);
            }
        }
        private async Task SaveRecipe(Edamam_Recipe recipe)
        {
            string selectRecipe = @"SELECT idRecipe
                                   FROM Recipes
                                   WHERE recipeName = @name";
            int? recipeId = null;       // зберігаємо id

            string insertRecipe = @"INSERT INTO Recipes (recipeName, category, yield, calories)
                                    VALUES (@recipeName, @category, @yield, @calories);
                                    SELECT SCOPE_IDENTITY();";      // отримуємо idRecipe останнього доданого запису

            string checkSavedQuery = @"SELECT isSaved
                                       FROM UserSavedRecipes
                                       WHERE id_User = @userId AND id_Recipe = @recipeId";
            bool isSaved = false;

            string updateQuery = @"UPDATE UserSavedRecipes
                                   SET isSaved = 1
                                   WHERE id_User = @userId AND id_Recipe = @recipeId";

            string insertSavedQuery = @"INSERT INTO UserSavedRecipes (id_User, id_Recipe, isSaved)
                                        VALUES (@userId, @recipeId, 1)";

            db.openConnection();

            // 1. Перевіряємо чи існує рецепт в таблиці Recipes
            using (SqlCommand checkCmd = new SqlCommand(selectRecipe, db.getConnection()))
            {
                checkCmd.Parameters.AddWithValue("@name", recipe.Name);
                var result = await checkCmd.ExecuteScalarAsync();

                if (result != null)
                {
                    recipeId = (int)result;
                }
            }
                        
            if (recipeId == null)
            {                
                using (SqlCommand insertCmd = new SqlCommand(insertRecipe, db.getConnection()))
                {
                    insertCmd.Parameters.AddWithValue("@recipeName", recipe.Name);
                    insertCmd.Parameters.AddWithValue("@category", recipe.Category);
                    insertCmd.Parameters.AddWithValue("@yield", (int)recipe.Yield);
                    insertCmd.Parameters.AddWithValue("@calories", Math.Round(recipe.Calories, 2));

                    recipeId = Convert.ToInt32(await insertCmd.ExecuteScalarAsync());
                }
            }            

            // 2. Перевіряємо чи рецепт вже збережений
            using (SqlCommand checkSavedCmd = new SqlCommand(checkSavedQuery, db.getConnection()))
            {
                checkSavedCmd.Parameters.AddWithValue("@userId", CurrentUser.UserId);
                checkSavedCmd.Parameters.AddWithValue("@recipeId", recipeId);

                var result = await checkSavedCmd.ExecuteScalarAsync();

                if (result != null)
                {
                    isSaved = (bool)result;
                }
            }

            // якщо рецепт був збережений (але isSaved = 0), то оновлюємо на 1
            if (isSaved)
            {
                using (SqlCommand updCmd = new SqlCommand(updateQuery, db.getConnection()))
                {
                    updCmd.Parameters.AddWithValue("@userId", CurrentUser.UserId);
                    updCmd.Parameters.AddWithValue("@recipeId", recipeId);

                    await updCmd.ExecuteNonQueryAsync();
                }
            }
            else
            {
                using (SqlCommand insertSavedCmd = new SqlCommand(insertSavedQuery, db.getConnection()))
                {
                    insertSavedCmd.Parameters.AddWithValue("@userId", CurrentUser.UserId);
                    insertSavedCmd.Parameters.AddWithValue("@recipeId", recipeId);

                    await insertSavedCmd.ExecuteNonQueryAsync();
                }
            }
            db.closeConnection();
        }
        private async Task RemoveRecipe(string recipeName, double calories)
        {
            string queryUpd = @"UPDATE UserSavedRecipes
                                SET isSaved = 0
                                FROM UserSavedRecipes s
                                JOIN Recipes r ON s.id_Recipe = r.idRecipe
                                WHERE s.id_User = @userId AND r.recipeName = @name AND r.calories = @calories";

            using (SqlCommand cmd = new SqlCommand(queryUpd, db.getConnection()))
            {
                cmd.Parameters.AddWithValue("@userId", CurrentUser.UserId);
                cmd.Parameters.AddWithValue("@name", recipeName);
                cmd.Parameters.AddWithValue("@calories", Math.Round(calories, 2));

                db.openConnection();
                await cmd.ExecuteNonQueryAsync();
                db.closeConnection();
            }
        }
        private bool IsRecipeSavedByUser(string recipeName, double calories)
        {
            string query = @"SELECT s.isSaved
                             FROM UserSavedRecipes s
                             JOIN Recipes r ON s.id_Recipe = r.idRecipe
                             WHERE s.id_User = @userId AND r.recipeName = @name AND r.calories = @calories;";

            using (SqlCommand cmd = new SqlCommand(query, db.getConnection()))
            {
                cmd.Parameters.AddWithValue("@userId", CurrentUser.UserId);
                cmd.Parameters.AddWithValue("@name", recipeName);
                cmd.Parameters.AddWithValue("@calories", Math.Round(calories, 2));

                db.openConnection();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["isSaved"] != null && (bool)reader["isSaved"])
                        {
                            db.closeConnection();
                            return true;
                        }
                    }
                }
                db.closeConnection();
            }
            return false;
        }

        private void OnMessageFromUserKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                isEnterPressed = true;

                btnSearchRecipe_Click(sender, e);

                isEnterPressed = false;
            }
        }
        private void btnSearchRecipe_MouseEnter(object sender, EventArgs e)
        {
            isMouseOverSend = true;
            if (!isBtnSearchClicked)
            {
                btnSearchRecipe.Image = Properties.Resources.search_recipes_hover;
            }
        }
        private void btnSearchRecipe_MouseHover(object sender, EventArgs e)
        {
            isMouseOverSend = true;
            if (!isBtnSearchClicked)
            {
                btnSearchRecipe.Image = Properties.Resources.search_recipes_hover;
            }
        }
        private void btnSearchRecipe_MouseLeave(object sender, EventArgs e)
        {
            isMouseOverSend = false;
            if (!isBtnSearchClicked)
            {
                btnSearchRecipe.Image = Properties.Resources.search_recipes;
            }
        }
    }
}