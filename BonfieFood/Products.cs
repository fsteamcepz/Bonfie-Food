using FontAwesome.Sharp;
using Guna.UI2.WinForms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BonfieFood
{
    public partial class Products : Form
    {
        DataBase db = new DataBase();

        private Edamam_Products edamamProducts = new Edamam_Products();
        private bool isMouseOverSend = false;
        private bool isBtnSearchClicked = false;
        private bool isEnterPressed = false;
        private bool isSortByName = true;
        private bool isSortByCategory = true;
        private bool isSortByCalories = true;

        // зберігаємо поточні продукти
        private List<Edamam_Products> currentProducts = new List<Edamam_Products>();

        public Products()
        {
            InitializeComponent();
        }
        private void Products_Load(object sender, EventArgs e)
        {
            btnSearchProducts.Image = Properties.Resources.search_product;
            mainPanel.Controls.Clear();
            HideResultForm();
            informationMenu.Visible = true;

            searchProducts.KeyDown += OnMessageFromUserKeyDown;
        }
        private async void btnSearchProducts_Click(object sender, EventArgs e)
        {
            await HandleSearch();
        }
        private async Task HandleSearch()
        {
            isMouseOverSend = false;
            isBtnSearchClicked = true;
            btnSearchProducts.Image = Properties.Resources.search_product;

            string productName = searchProducts.Text.Trim();
            if (string.IsNullOrEmpty(productName))
            {
                MessageBoxError.Show("Будь ласка, введіть назву продукту.");
                return;
            }

            var productInfo = await GetInfoFromEdamam(productName);
            if (productInfo == null || productInfo.Count == 0)
            {
                searchProducts.Text = "";
                return;
            }
            searchProducts.Text = "";
            currentProducts = productInfo;
            ShowResultForm();
            ShowProducts(currentProducts);

            ActionHistory.SaveActionHistoryToDB(db, "Пошук продуктів");
        }
        private async Task<List<Edamam_Products>> GetInfoFromEdamam(string productName)
        {
            try
            {
                // 1. Пошук продукту (Food Database API)
                string searchUrl = $"{edamamProducts.EDAMAM_SEARCH_URL}?app_id={edamamProducts.idEdamam}&app_key={edamamProducts.apiKey}&ingr={Uri.EscapeDataString(productName)}";
                using (var client = new HttpClient())
                {
                    var searchResponse = await client.GetAsync(searchUrl);
                    if (!searchResponse.IsSuccessStatusCode)
                    {
                        MessageBoxError.Show("Помилка відповіді Food Database API!");
                        return null;
                    }

                    var searchResponseBody = await searchResponse.Content.ReadAsStringAsync();
                    //string filePath = "response_search_Edamam.json";
                    //File.WriteAllText(filePath, searchResponseBody);
                    //MessageBoxError.Show($"Відповідь від Edamam API збережена у файлі: {filePath}");
                    dynamic searchJsonResponse = JsonConvert.DeserializeObject(searchResponseBody);

                    if (searchJsonResponse?.hints == null || searchJsonResponse.hints.Count == 0)
                    {
                        MessageBoxError.Show("Продукт не знайдено.");
                        return null;
                    }

                    var products = new List<Edamam_Products>();

                    // 2. Пошук інформації про продукт (Nutrition Analysis API)
                    foreach (var hint in searchJsonResponse.hints)
                    {
                        var food = hint.food;

                        // знаходимо одиницю вимірювання для грамів
                        foreach (var measure in hint.measures)
                        {
                            if (measure.uri.ToString().Contains("Measure_gram"))
                            {
                                edamamProducts.MEASURE_URI = measure.uri;
                                break;
                            }
                        }

                        var foodItem = new
                        {
                            quantity = 100,         // 100 г
                            measureURI = edamamProducts.MEASURE_URI,
                            foodId = food.foodId
                        };
                        var nutrientsRequest = new
                        {
                            ingredients = new[] { foodItem }
                        };

                        var nutrientsContent = new StringContent(JsonConvert.SerializeObject(nutrientsRequest), Encoding.UTF8, "application/json");
                        string nutrientsUrl = $"{edamamProducts.EDAMAM_NUTRIENTS_URL}?app_id={edamamProducts.idEdamam}&app_key={edamamProducts.apiKey}";

                        var nutrientsResponse = await client.PostAsync(nutrientsUrl, nutrientsContent);
                        if (!nutrientsResponse.IsSuccessStatusCode)
                        {
                            MessageBoxError.Show("Помилка відповіді Nutrition Analysis API!");
                            continue;
                        }

                        var nutrientsResponseBody = await nutrientsResponse.Content.ReadAsStringAsync();
                        //string filePath_product = "response_Product.json";
                        //File.WriteAllText(filePath_product, searchResponseBody);
                        //MessageBoxError.Show($"Відповідь про Product збережена у файлі: {filePath_product}");   
                        dynamic nutrientsJsonResponse = JsonConvert.DeserializeObject(nutrientsResponseBody);

                        if (nutrientsJsonResponse == null || nutrientsJsonResponse.totalNutrients == null)
                        {
                            continue;
                        }

                        var totalNutrients = nutrientsJsonResponse.totalNutrients;

                        var product = new Edamam_Products
                        {
                            FoodName = food.label,
                            Calories = totalNutrients.ENERC_KCAL?.quantity,
                            Protein = totalNutrients.PROCNT?.quantity,
                            Fat = totalNutrients.FAT?.quantity,
                            SaturatedFat = totalNutrients.FASAT?.quantity,
                            TransFat = totalNutrients.FATRN?.quantity,
                            PolyunsaturatedFat = totalNutrients.FAPU?.quantity,
                            MonounsaturatedFat = totalNutrients.FAMS?.quantity,
                            Carbohydrates = totalNutrients.CHOCDF?.quantity,
                            DietaryFiber = totalNutrients.FIBTG?.quantity,
                            Sugars = totalNutrients.SUGAR?.quantity,
                            Cholesterol = totalNutrients.CHOLE?.quantity,
                            Sodium = totalNutrients.NA?.quantity,
                            Calcium = totalNutrients.CA?.quantity,
                            Iron = totalNutrients.FE?.quantity,
                            Potassium = totalNutrients.K?.quantity,
                            VitaminA = totalNutrients.VITA_RAE?.quantity,
                            VitaminC = totalNutrients.VITC?.quantity,
                            Thiamin = totalNutrients.THIA?.quantity,
                            Riboflavin = totalNutrients.RIBF?.quantity,
                            Niacin = totalNutrients.NIA?.quantity,
                            VitaminB6 = totalNutrients.VITB6A?.quantity,
                            Folate = totalNutrients.FOLDFE?.quantity,
                            VitaminB12 = totalNutrients.VITB12?.quantity,
                            VitaminD = totalNutrients.VITD?.quantity,
                            VitaminE = totalNutrients.TOCPHA?.quantity,
                            VitaminK = totalNutrients.VITK1?.quantity,
                            Photo = food.image ?? null,
                            Category = food.category ?? "Unknown"
                        };
                        products.Add(product);
                    }
                    return products;
                }
            }
            catch (Exception ex)
            {
                MessageBoxError.Show($"Помилка: {ex.Message}");
                return null;
            }
        }
        private void ShowProducts(List<Edamam_Products> products)
        {
            mainPanel.Controls.Clear();
            mainPanel.Location = new Point(19, 132);
            mainPanel.Size = new Size(682, 321);

            int yOffset = 0;
            int countResult = 6;
            Guna2Panel activePanel = null;

            foreach (var product in products)
            {
                Guna2Panel miniPanel = new Guna2Panel
                {
                    Size = new Size(682, 46),
                    BackColor = Color.Transparent,
                    Margin = new Padding(0),
                    Location = new Point(0, yOffset),
                    Cursor = Cursors.Hand,
                    Tag = product.FoodName
                };

                Label resultName = new Label
                {
                    Text = product.FoodName,
                    Size = new Size(291, 29),
                    BackColor = Color.Transparent,
                    ForeColor = Color.FromArgb(253, 235, 250),
                    Font = new Font("Constantia", 14.25f),
                    AutoSize = false,
                    Location = new Point(28, 12),
                    Cursor = Cursors.Hand,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Tag = product.FoodName
                };
                Label resultCategory = new Label
                {
                    Text = product.Category,
                    Size = new Size(214, 29),
                    Location = new Point(325, 9),
                    BackColor = Color.Transparent,
                    ForeColor = Color.FromArgb(253, 235, 250),
                    Font = new Font("Arial Black", 10),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Cursor = Cursors.Hand,
                    Tag = product.FoodName
                };

                Label resultCalories = new Label
                {
                    Text = product.Calories.HasValue ? $"{product.Calories.Value:0}" : "0",
                    Size = new Size(101, 29),
                    BackColor = Color.Transparent,
                    ForeColor = Color.FromArgb(253, 235, 250),
                    Font = new Font("Arial Black", 15.75f),
                    AutoSize = false,
                    Location = new Point(545, 9),
                    Cursor = Cursors.Hand,
                    TextAlign = ContentAlignment.TopCenter,
                    Tag = product.FoodName
                };

                resultName.MouseEnter += (sender, e) =>
                {
                    Label label = sender as Label;
                    if (label != null)
                    {
                        label.Font = new Font(label.Font, FontStyle.Underline);
                    }
                };
                resultName.MouseLeave += (sender, e) =>
                {
                    Label label = sender as Label;
                    if (label != null)
                    {
                        label.Font = new Font(label.Font, FontStyle.Regular);
                    }
                };

                void HandleClick(object sender, EventArgs e)
                {
                    var control = sender as Control;
                    if (control != null && control.Tag is string productName)
                    {
                        SearchReadOnly();
                        
                        var selectedProduct = products.FirstOrDefault(p => p.FoodName == productName);
                        if (selectedProduct != null)
                        {
                            ShowProductDetails(selectedProduct);
                        }
                    }
                }

                miniPanel.Click += HandleClick;
                resultName.Click += HandleClick;
                resultCategory.Click += HandleClick;
                resultCalories.Click += HandleClick;

                miniPanel.Controls.Add(resultName);
                miniPanel.Controls.Add(resultCategory);
                miniPanel.Controls.Add(resultCalories);

                miniPanel.MouseEnter += (sender, e) =>
                {
                    if (activePanel != null && activePanel != sender)
                    {
                        activePanel.FillColor = Color.Transparent;
                    }

                    Guna2Panel panel = sender as Guna2Panel;
                    panel.FillColor = Color.FromArgb(54, 54, 54);
                    activePanel = panel;        // оновлюємо посилання на активну панель
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
                yOffset += 46;
            }

            labelQuantity.Visible = true;
            productQuantity.Visible = true;
            productQuantity.Text = products.Count.ToString();

            if (products.Count > countResult)
            {
                mainPanel.Size = new Size(699, 321);
            }
            else
            {
                mainPanel.Size = new Size(682, 321);
            }
        }
        private void ShowProductDetails(Edamam_Products product)
        {
            mainPanel.Controls.Clear();

            IconPictureBox infoProduct = new IconPictureBox()
            {
                IconChar = IconChar.CircleExclamation,
                ForeColor = Color.AliceBlue,
                BackColor = Color.FromArgb(121, 85, 193),
                SizeMode = PictureBoxSizeMode.StretchImage,
                IconSize = 28,
                Location = new Point(200, 8),
                Size = new Size(28, 28)
            };
            toolTip_infoProduct.ToolTipTitle = "Стандартна порція (100г).";
            toolTip_infoProduct.SetToolTip(infoProduct, "Інформація стосується стандартної порції продукту<br>" +
                                                   "(залежно від типу їжі). Харчовий склад, включаючи КБЖВ,<br>" +
                                                   "розрахований на основі даних про типовий розмір порції.<br>" +
                                                   "Якщо ви споживаєте інший обсяг, врахуйте, що значення<br>" +
                                                   "будуть пропорційно змінюватися.");

            Guna2PictureBox productPhoto = new Guna2PictureBox
            {
                Size = new Size(120, 120),
                Location = new Point(66, 12),
                Image = string.IsNullOrEmpty(product?.Photo)
                        ? Properties.Resources.product_not_found
                        : LoadPhotoFromUrl(product.Photo),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BorderRadius = 30,
            };
            toolTip_Category.TitleForeColor = Color.DeepSkyBlue;
            toolTip_Category.ToolTipTitle = "Категорія";
            toolTip_Category.SetToolTip(productPhoto, product.Category);

            Label nameProduct = new Label
            {
                Text = product.FoodName,
                Size = new Size(223, 58),
                Location = new Point(16, 144),
                Font = new Font("Constantia", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(253, 235, 250),
                AutoSize = false,
                TextAlign = ContentAlignment.TopCenter
            };

            Label calories = new Label
            {
                Text = "Calories:",
                Size = new Size(113, 29),
                Font = new Font("Constantia", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(253, 235, 250),
                AutoSize = true,
                Location = new Point(16, 212),
            };
            Label caloriesValue = new Label
            {
                Text = product.Calories.HasValue ? $"{product.Calories.Value:0}" : "0",
                Font = new Font("Arial Black", 16),
                ForeColor = Color.FromArgb(255, 128, 0),
                AutoSize = false,
                Size = new Size(92, 30),
                Location = new Point(147, 212),
                TextAlign = ContentAlignment.MiddleRight
            };

            Guna2Button btnBack = new Guna2Button
            {
                Text = "Back",
                Font = new Font("Constantia", 12, FontStyle.Bold),
                BackColor = Color.Transparent,
                ForeColor = Color.FromArgb(253, 235, 250),
                FillColor = Color.FromArgb(54, 54, 54),
                Location = new Point(75, 281),
                Size = new Size(101, 40),
                Animated = true,
                BorderRadius = 17,
                BorderThickness = 1,
                BorderColor = Color.FromArgb(253, 235, 250),
            };
            btnBack.Click += (sender, e) =>
            {
                SearchNotReadOnly();
                ShowProducts(currentProducts);
            };

            Guna2Panel lineLeft = new Guna2Panel
            {
                Size = new Size(1, 309),
                BackColor = Color.Silver,
                Location = new Point(251, 12)
            };

            Guna2Panel gunaPanel_Information = new Guna2Panel
            {
                Size = new Size(421, 321),
                BackColor = Color.Transparent,
                Location = new Point(261, 0)
            };
            Label nutrientInfoTitle = new Label
            {
                Text = "Nutrient information",
                Font = new Font("Constantia", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(253, 235, 250),
                AutoSize = true,
                Location = new Point(90, 5)
            };

            int yLocName = 52;
            int yLocValue = 47;
            int yLocLineBetween = 77;

            // cловник з вітамінами
            var vitamins = new Dictionary<string, double?>
            {
                { "Vitamin A", product.VitaminA },
                { "Vitamin C", product.VitaminC },
                { "Thiamin (B1)", product.Thiamin },
                { "Riboflavin (B2)", product.Riboflavin },
                { "Niacin (B3)", product.Niacin },
                { "Vitamin B6", product.VitaminB6 },
                { "Folate (B9)", product.Folate },
                { "Vitamin B12", product.VitaminB12 },
                { "Vitamin D", product.VitaminD },
                { "Vitamin E", product.VitaminE },
                { "Vitamin K", product.VitaminK }
            };

            var maxVitamin = vitamins.OrderByDescending(v => v.Value).FirstOrDefault();
            product.MaxVitaminName = maxVitamin.Key;
            product.MaxVitaminValue = maxVitamin.Value;

            // cловник з інформацією
            var nutrientInfo = new Dictionary<string, string>
            {
                { "Protein", (product.Protein?.ToString("0.0") ?? "0") + " g" },
                { "Total fat", (product.Fat?.ToString("0.0") ?? "0") + " g" },
                { "Cholesterol", (product.Cholesterol?.ToString("0.0") ?? "0") + " mg" },
                { "Total carbohydrates", (product.Carbohydrates?.ToString("0.0") ?? "0") + " g" },
                { "Sodium", (product.Sodium?.ToString("0.0") ?? "0") + " mg" },
                { "Calcium", (product.Calcium?.ToString("0.0") ?? "0") + " mg" },
                { "Iron", (product.Iron?.ToString("0.0") ?? "0") + " mg" },
                { "Potassium", (product.Potassium?.ToString("0.0") ?? "0") + " mg" },
                { product.MaxVitaminName, (product.MaxVitaminValue?.ToString("0.0") ?? "0") + " µg/mg" }
            };

            foreach (var nutrient in nutrientInfo)
            {
                Label nutrientName = new Label
                {
                    Text = nutrient.Key,
                    Font = new Font("Constantia", 14, FontStyle.Italic),
                    ForeColor = Color.FromArgb(253, 235, 250),
                    AutoSize = true,
                    Location = new Point(5, yLocName)
                };
                Label nutrientValue = new Label
                {
                    Text = nutrient.Value,
                    Font = new Font("Arial Black", 14),
                    ForeColor = Color.FromArgb(253, 235, 250),
                    AutoSize = false,
                    Size = new Size(118, 30),
                    Location = new Point(282, yLocValue),
                    TextAlign = ContentAlignment.TopRight
                };
                Guna2Panel lineBetween = new Guna2Panel
                {
                    Size = new Size(391, 1),
                    BackColor = Color.Silver,
                    Location = new Point(9, yLocLineBetween)
                };

                if(nutrient.Key == "Total fat")
                {
                    toolTip_totalFat.TitleForeColor = Color.FromArgb(240, 171, 0);
                    toolTip_totalFat.ToolTipTitle = "Total fat: " + product.Fat?.ToString("0.0");
                    string totalFatText = $"Saturated Fat - {product.SaturatedFat?.ToString("0.0") ?? "0"} g<br>" +
                                         $"Trans Fat - {product.TransFat?.ToString("0.0") ?? "0"} g<br>" +
                                         $"Polyunsaturated Fat - {product.PolyunsaturatedFat?.ToString("0.0") ?? "0"} g<br>" +
                                         $"Monounsaturated Fat - {product.MonounsaturatedFat?.ToString("0.0") ?? "0"} g";
                    toolTip_totalFat.SetToolTip(nutrientName, totalFatText);
                }
                if (nutrient.Key == "Total carbohydrates")
                {
                    toolTip_totalCarbohydrates.TitleForeColor = Color.LimeGreen;
                    toolTip_totalCarbohydrates.ToolTipTitle = "Total Carbohydrates: " + product.Carbohydrates?.ToString("0.0");
                    string totalCarbohydratesText = $"Dietary Fiber - {product.DietaryFiber?.ToString("0.0") ?? "0"} g<br>" +
                                                    $"Sugars - {product.Sugars?.ToString("0.0") ?? "0"} g";
                    toolTip_totalCarbohydrates.SetToolTip(nutrientName, totalCarbohydratesText);
                }
                if (nutrient.Key == product.MaxVitaminName)
                {
                    toolTip_Vitamins.TitleForeColor = Color.FromArgb(94, 148, 255);
                    toolTip_Vitamins.ToolTipTitle = "Vitamins";
                    string vitaminsSummary = $"Vitamin A: {product.VitaminA?.ToString("0.0") ?? "0"} µg<br>" +
                                             $"Vitamin C: {product.VitaminC?.ToString("0.0") ?? "0"} mg<br>" +
                                             $"Thiamin (B1): {product.Thiamin?.ToString("0.0") ?? "0"} mg<br>" +
                                             $"Riboflavin (B2): {product.Riboflavin?.ToString("0.0") ?? "0"} mg<br>" +
                                             $"Niacin (B3): {product.Niacin?.ToString("0.0") ?? "0"} mg<br>" +
                                             $"Vitamin B6: {product.VitaminB6?.ToString("0.0") ?? "0"} mg<br>" +
                                             $"Folate (B9): {product.Folate?.ToString("0.0") ?? "0"} µg<br>" +
                                             $"Vitamin B12: {product.VitaminB12?.ToString("0.0") ?? "0"} µg<br>" +
                                             $"Vitamin D: {product.VitaminD?.ToString("0.0") ?? "0"} µg<br>" +
                                             $"Vitamin E: {product.VitaminE?.ToString("0.0") ?? "0"} mg<br>" +
                                             $"Vitamin K: {product.VitaminK?.ToString("0.0") ?? "0"} µg";
                    toolTip_Vitamins.SetToolTip(nutrientName, vitaminsSummary);
                }

                gunaPanel_Information.Controls.Add(nutrientName);
                gunaPanel_Information.Controls.Add(nutrientValue);
                gunaPanel_Information.Controls.Add(lineBetween);

                yLocName += 30;
                yLocValue += 30;
                yLocLineBetween += 30;
            }

            mainPanel.Controls.Add(infoProduct);
            mainPanel.Controls.Add(productPhoto);
            mainPanel.Controls.Add(nameProduct);
            mainPanel.Controls.Add(calories);
            mainPanel.Controls.Add(caloriesValue);
            mainPanel.Controls.Add(btnBack);
            mainPanel.Controls.Add(lineLeft);
            mainPanel.Controls.Add(gunaPanel_Information);
            gunaPanel_Information.Controls.Add(nutrientInfoTitle);

            mainPanel.Size = new Size(682, 321);
        }
        private System.Drawing.Image LoadPhotoFromUrl(string url)
        {
            try
            {
                using (var webClient = new WebClient())
                {
                    byte[] data = webClient.DownloadData(url);
                    using (var stream = new MemoryStream(data))
                    {
                        return System.Drawing.Image.FromStream(stream);
                    }
                }
            }
            catch
            {
                return Properties.Resources.product_not_found;
            }
        }

        private async void OnMessageFromUserKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Повністю блокуємо обробку клавіші Enter
                e.SuppressKeyPress = true;
                e.Handled = true;
                isEnterPressed = true;  // Встановлюємо прапорець

                await HandleSearch(); // Викликаємо новий метод

                isEnterPressed = false;
            }
        }
        private void btnSearchProducts_MouseEnter(object sender, EventArgs e)
        {
            isMouseOverSend = true;
            if (!isBtnSearchClicked)
            {
                btnSearchProducts.Image = Properties.Resources.search_product_hover;
            }
        }
        private void btnSearchProducts_MouseHover(object sender, EventArgs e)
        {
            isMouseOverSend = true;
            if (!isBtnSearchClicked)
            {
                btnSearchProducts.Image = Properties.Resources.search_product_hover;
            }
        }
        private void btnSearchProducts_MouseLeave(object sender, EventArgs e)
        {
            isMouseOverSend = false;
            if (!isBtnSearchClicked)
            {
                btnSearchProducts.Image = Properties.Resources.search_product;
            }
        }
        private void HideResultForm()
        {
            //informationMenu.Visible = false;
            sortingName.Visible = false;
            sortingCategory.Visible = false;
            sortingCalories.Visible = false;
            mainPanel.Visible = false;
            label_NameProduct.Visible = false;
            label_Calories.Visible = false;
            label_Category.Visible = false;

            labelQuantity.Visible = false;
            productQuantity.Visible = false;

            line.Visible = false;
        }
        private void ShowResultForm()
        {
            sortingName.Visible = true;
            sortingCategory.Visible = true;
            sortingCalories.Visible = true;
            //informationMenu.Visible = true;
            mainPanel.Visible = true;
            label_NameProduct.Visible = true;
            label_Calories.Visible = true;
            label_Category.Visible = true;
            line.Visible = true;
        }
        private void SearchReadOnly()
        {
            sortingName.Visible = false;
            sortingCategory.Visible = false;
            sortingCalories.Visible = false;
            label_NameProduct.Visible = false;
            label_Category.Visible = false;
            label_Calories.Visible = false;
            labelQuantity.Visible = false;
            productQuantity.Visible = false;

            searchProducts.ReadOnly = true;
            searchProducts.Enabled = false;
            searchProducts.DisabledState.FillColor = Color.FromArgb(90, 63, 145);
            searchProducts.DisabledState.BorderColor = Color.FromArgb(90, 63, 145);

            btnSearchProducts.Enabled = false;
            searchProducts.Refresh();
        }
        private void SearchNotReadOnly()
        {
            sortingName.Visible = true;
            sortingCategory.Visible = true;
            sortingCalories.Visible = true;
            label_NameProduct.Visible = true;
            label_Category.Visible = true;
            label_Calories.Visible = true;
            labelQuantity.Visible = true;
            productQuantity.Visible = true;

            searchProducts.ReadOnly = false;
            searchProducts.Enabled = true;
            searchProducts.DisabledState.FillColor = Color.FromArgb(121, 85, 193);
            searchProducts.DisabledState.BorderColor = Color.FromArgb(121, 85, 193);

            btnSearchProducts.Enabled = true;
            searchProducts.Refresh();
        }

        private void sortingName_Click(object sender, EventArgs e)
        {
            isSortByName = !isSortByName;

            sortingName.IconChar = isSortByName ? IconChar.AngleUp : IconChar.AngleDown;

            if (isSortByName)
            {
                currentProducts = currentProducts.OrderBy(f => f.FoodName).ToList();
            }
            else
            {
                currentProducts = currentProducts.OrderByDescending(f => f.FoodName).ToList();
            }
            ShowProducts(currentProducts);
        }
        private void sortingCategory_Click(object sender, EventArgs e)
        {
            isSortByCategory = !isSortByCategory;

            sortingCategory.IconChar = isSortByCategory ? IconChar.AngleUp : IconChar.AngleDown;

            if (isSortByCategory)
            {
                currentProducts = currentProducts.OrderBy(f => f.Category).ToList();
            }
            else
            {
                currentProducts = currentProducts.OrderByDescending(f => f.Category).ToList();
            }
            ShowProducts(currentProducts);
        }
        private void sortingCalories_Click(object sender, EventArgs e)
        {
            isSortByCalories = !isSortByCalories;

            sortingCalories.IconChar = isSortByCalories ? IconChar.AngleUp : IconChar.AngleDown;

            if (isSortByCalories)
            {
                currentProducts = currentProducts.OrderBy(f => f.Calories).ToList();
            }
            else
            {
                currentProducts = currentProducts.OrderByDescending(f => f.Calories).ToList();
            }
            ShowProducts(currentProducts);
        }
    }
}