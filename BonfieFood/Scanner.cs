using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Drawing;

namespace BonfieFood
{
    public partial class Scanner : Form
    {
        private bool isBtnClicked = false;
        private string currentPhotoPath;
        private CloudVision cloudVision = new CloudVision();
        private Clarifai clarifai = new Clarifai();
        private Edamam_Products edamam = new Edamam_Products();

        public Scanner()
        {
            InitializeComponent();
        }
        private void Scanner_Load(object sender, EventArgs e)
        {
            ConfigurateLabels();
            UpdateBtns();

            HidePhotoAnalysisResults();
            UpdateTexts();
            Language.OnLanguageChanged += ChangeLanguage;
        }
        private void ChangeLanguage(string cultureCode)
        {
            UpdateTexts();
        }
        private void uploadPhotoHover_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Зображення (*.jpg;*.jpeg;*.png;*.webm)|*.jpg;*.jpeg;*.png;*.webm|Всі файли (*.*)|*.*";
                dialog.Title = "Select a photo";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string fileExtension = Path.GetExtension(dialog.FileName).ToLower();
                    string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".webm" };
                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        MessageBoxError.Show("Файл має недопустимий формат! Дозволені формати: jpg, jpeg, png, webm.");
                        return;
                    }
                    FileInfo fileInfo = new FileInfo(dialog.FileName);
                    if (fileInfo.Length > 10 * 1024 * 1024)
                    {
                        MessageBoxError.Show("Файл занадто великий! Максимальний розмір — 10 МБ.");
                        return;
                    }
                    if (uploadedUserPoto.Image != null)
                    {
                        uploadedUserPoto.Image.Dispose();
                    }
                    currentPhotoPath = dialog.FileName;
                    isBtnClicked = false;
                    uploadPhotoHover.Visible = false;
                    uploadedUserPoto.Visible = true;
                    uploadedUserPoto.Image = System.Drawing.Image.FromFile(currentPhotoPath);
                    deletePhoto.Visible = true;
                    btn_ScanPhoto.Enabled = true;
                }
            }
        }
        private async void btn_ScanPhoto_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentPhotoPath) || !File.Exists(currentPhotoPath))
            {
                MessageBoxError.Show("Будь ласка, спочатку завантажте фото для аналізу.");
                return;
            }

            try
            {
                // перевіряємо чи на фото їжа
                bool isFood = await cloudVision.IsFood(currentPhotoPath);

                if (!isFood)
                {
                    MessageBoxError.Show("На зображенні не виявлено їжу!");
                    return;
                }

                // отримуємо назву страви та інгредієнти через Clarifai
                var (dishName, products) = await clarifai.AnalyzePhoto(currentPhotoPath);

                if (dishName == null && products == null)
                {
                    return;
                }

                // отримуємо інформацію та обчислюємо сумарний харчовий склад інгредієнтів
                var totalNutritionInfo = await CalculateTotalNutrition(products);

                if (string.IsNullOrEmpty(dishName) || products == null || products.Count == 0)
                {
                    MessageBoxError.Show("Не вдалося отримати інформацію про харчовий склад!");
                    return;
                }
                deletePhoto.Visible = false;
                uploadedUserPoto.Visible = false;

                ShowPhotoAnalysisResults(dishName, products, totalNutritionInfo);
            }
            catch (Exception error)
            {
                MessageBoxError.Show("Помилка: " + error.Message);
            }
        }
        private async Task<Edamam_Products> GetInfoFromEdamam(string productName)
        {
            try
            {
                // 1. Пошук продукту (Food Database API)
                string searchUrl = $"{edamam.EDAMAM_SEARCH_URL}?app_id={edamam.idEdamam}&app_key={edamam.apiKey}&ingr={Uri.EscapeDataString(productName)}";
                using (var client = new HttpClient())
                {
                    var searchResponse = await client.GetAsync(searchUrl);
                    if (!searchResponse.IsSuccessStatusCode)
                    {
                        MessageBoxError.Show("Помилка відповіді Food Database API!");
                        return null;
                    }

                    var searchResponseBody = await searchResponse.Content.ReadAsStringAsync();
                    //string file_FD = "Scanner_InfoDish_FD.json";
                    //File.WriteAllText(file_FD, searchResponseBody);
                    //MessageBoxError.Show($"Відповідь від Edamam API збережена у файлі: {file_FD}");

                    dynamic searchJsonResponse = JsonConvert.DeserializeObject(searchResponseBody);
                    if (searchJsonResponse?.hints == null || searchJsonResponse.hints.Count == 0)
                    {
                        MessageBoxError.Show("Продукт не знайдено.");
                        return null;
                    }

                    // 2. Вибираємо найрелевантніший продукт
                    Edamam_Products bestMatch = null;
                    double bestScore = 0;

                    foreach (var hint in searchJsonResponse.hints)
                    {
                        var food = hint.food;
                        string foodLabel = food.label.ToString().ToLower();
                        string query = productName.ToLower();

                        double score = 0;
                        if (foodLabel == query) // якщо назви повністю збігаються
                        {
                            score = 1.0;
                        }
                        else if (foodLabel.Contains(query)) // (шукало "картопля", знайшло "картопля фрі")
                        {
                            score = 0.8;
                        }
                        else if (query.Contains(foodLabel)) // навпаки (шукало "картопля фрі", знайшло "картопля")
                        {
                            score = 0.6;
                        }

                        if (score > bestScore)
                        {
                            bestScore = score;

                            edamam.MEASURE_URI = null;
                            foreach (var measure in hint.measures)
                            {
                                if (measure.uri.ToString().Contains("Measure_gram"))
                                {
                                    edamam.MEASURE_URI = measure.uri;
                                    break;
                                }
                            }

                            var foodItem = new
                            {
                                quantity = 100,
                                measureURI = edamam.MEASURE_URI,
                                foodId = food.foodId
                            };

                            var nutrientsRequest = new
                            {
                                ingredients = new[] { foodItem }
                            };

                            var nutrientsContent = new StringContent(JsonConvert.SerializeObject(nutrientsRequest), Encoding.UTF8, "application/json");
                            string nutrientsUrl = $"{edamam.EDAMAM_NUTRIENTS_URL}?app_id={edamam.idEdamam}&app_key={edamam.apiKey}";
                            var nutrientsResponse = await client.PostAsync(nutrientsUrl, nutrientsContent);

                            if (!nutrientsResponse.IsSuccessStatusCode)
                            {
                                MessageBoxError.Show("Помилка відповіді Nutrition Analysis API!");
                                continue;
                            }

                            var nutrientsResponseBody = await nutrientsResponse.Content.ReadAsStringAsync();
                            dynamic nutrientsJsonResponse = JsonConvert.DeserializeObject(nutrientsResponseBody);
                            if (nutrientsJsonResponse == null || nutrientsJsonResponse.totalNutrients == null)
                            {
                                continue;
                            }

                            var totalNutrients = nutrientsJsonResponse.totalNutrients;

                            bestMatch = new Edamam_Products
                            {
                                FoodName = food.label,
                                Calories = totalNutrients.ENERC_KCAL?.quantity,
                                Protein = totalNutrients.PROCNT?.quantity,
                                Fat = totalNutrients.FAT?.quantity,
                                Carbohydrates = totalNutrients.CHOCDF?.quantity,
                                Cholesterol = totalNutrients.CHOLE?.quantity,
                                Calcium = totalNutrients.CA?.quantity,
                                Iron = totalNutrients.FE?.quantity
                            };
                        }
                    }
                    return bestMatch;
                }
            }
            catch
            {
                MessageBoxError.Show("Помилка під час запиту до Edamam API!");
                return null;
            }
        }
        private async Task<Edamam_Products> CalculateTotalNutrition(List<string> products)
        {
            Edamam_Products totalNutrition = new Edamam_Products
            {
                Calories = 0,
                Protein = 0,
                Fat = 0,
                Carbohydrates = 0,
                Cholesterol = 0,
                Calcium = 0,
                Iron = 0
            };

            List<string> productTooltipList = new List<string>();

            foreach (var ing in products)
            {
                var nutritionInfo = await GetInfoFromEdamam(ing);

                if (nutritionInfo != null)
                {
                    totalNutrition.Calories += nutritionInfo.Calories ?? 0;
                    totalNutrition.Protein += nutritionInfo.Protein ?? 0;
                    totalNutrition.Fat += nutritionInfo.Fat ?? 0;
                    totalNutrition.Carbohydrates += nutritionInfo.Carbohydrates ?? 0;
                    totalNutrition.Cholesterol += nutritionInfo.Cholesterol ?? 0;
                    totalNutrition.Calcium += nutritionInfo.Calcium ?? 0;
                    totalNutrition.Iron += nutritionInfo.Iron ?? 0;

                    productTooltipList.Add($"{ing.Substring(0, 1).ToUpper()}{ing.Substring(1)} - " +
                                           $"{nutritionInfo.Calories.Value:0} cal<br>");
                }
            }
            if (productTooltipList.Count > 1)
            {
                toolTip_Products.ToolTipTitle = "Продукти";
                toolTip_Products.SetToolTip(res_totalCaories_value, string.Join(Environment.NewLine, productTooltipList));
            }
            else
            {
                toolTip_Products.SetToolTip(res_totalCaories_value, "");
            }

            return totalNutrition;
        }
        private void ShowPhotoAnalysisResults(string dishName, List<string> products, Edamam_Products nutritionInfo)
        {
            resultsAnalysis.Visible = true;
            ConfigureInfoDish();

            // 1. Асинхронне завантаження зображення
            Task.Run(() =>
            {
                if (res_userPhotoDish.Image != null)
                {
                    res_userPhotoDish.Image.Dispose();
                }
                using (var originalImage = System.Drawing.Image.FromFile(currentPhotoPath))
                {
                    var resizedImage = new Bitmap(res_userPhotoDish.Width, res_userPhotoDish.Height);
                    using (var graphics = Graphics.FromImage(resizedImage))
                    {
                        graphics.DrawImage(originalImage, 0, 0, resizedImage.Width, resizedImage.Height);
                    }
                    Invoke((Action)(() =>
                    {
                        res_userPhotoDish.Image = resizedImage;
                        res_userPhotoDish.Visible = true;
                    }));
                }
            });

            // 2. Назва страви
            res_dishName.Visible = true;
            res_dishName.Text = dishName.Substring(0, 1).ToUpper() + dishName.Substring(1) + ".";

            // 3. Інгредієнти
            res_products.Visible = true;
            res_products.Text = "Products: " + string.Join(", ", products) + ".";

            info_dish.Visible = true;

            // 4. Харчовий склад
            line.Visible = true;

            // Калорії
            res_totalCalories.Visible = true;
            res_totalCaories_value.Visible = true;
            res_totalCaories_value.Text = nutritionInfo.Calories.HasValue
                ? $"{nutritionInfo.Calories.Value:0} cal"
                : "–";

            // Білки
            res_protein.Visible = true;
            res_protein_value.Visible = true;
            res_protein_value.Text = nutritionInfo.Protein.HasValue
                ? $"{nutritionInfo.Protein.Value:0.0} g"
                : "–";

            // Жири
            res_fat.Visible = true;
            res_fat_value.Visible = true;
            res_fat_value.Text = nutritionInfo.Fat.HasValue
                ? $"{nutritionInfo.Fat.Value:0.0} g"
                : "–";

            // Вуглеводи
            res_carbohydrates.Visible = true;
            res_carbohydrates_value.Visible = true;
            res_carbohydrates_value.Text = nutritionInfo.Carbohydrates.HasValue
                ? $"{nutritionInfo.Carbohydrates.Value:0.0} g"
                : "-";

            // Холестерин
            res_cholesterol.Visible = true;
            res_cholesterol_value.Visible = true;
            res_cholesterol_value.Text = nutritionInfo.Cholesterol.HasValue
                ? $"{nutritionInfo.Cholesterol.Value:0.0} mg"
                : "Н/Д";

            // Кальцій
            res_calcium.Visible = true;
            res_calcium_value.Visible = true;
            res_calcium_value.Text = nutritionInfo.Calcium.HasValue
                ? $"{nutritionInfo.Calcium.Value:0.0} mg"
                : "-";

            // Залізо
            res_iron.Visible = true;
            res_iron_value.Visible = true;
            res_iron_value.Text = nutritionInfo.Iron.HasValue
                ? $"{nutritionInfo.Iron.Value:0.0} mg"
                : "-";

            closeResults.Visible = true;
            currentPhotoPath = null;
        }

        private void ConfigurateLabels()
        {
            label_Clarifai.Parent = homePageImg;
            label_Clarifai.BackColor = Color.Transparent;
        }
        private void UpdateBtns()
        {
            deletePhoto.Visible = false;
            uploadedUserPoto.Image = null;
            uploadedUserPoto.Visible = false;
            uploadPhotoHover.Visible = true;
        }
        private void deletePhoto_MouseEnter(object sender, EventArgs e)
        {
            if (!isBtnClicked)
            {
                deletePhoto.Image = Properties.Resources.remove_scanner_hover;
            }
        }
        private void deletePhoto_MouseHover(object sender, EventArgs e)
        {
            deletePhoto.Image = Properties.Resources.remove_scanner_hover;
        }
        private void deletePhoto_MouseLeave(object sender, EventArgs e)
        {
            if (!isBtnClicked)
            {
                deletePhoto.Image = Properties.Resources.remove_scanner;
            }
        }
        private void deletePhoto_Click(object sender, EventArgs e)
        {
            isBtnClicked = true;
            deletePhoto.Image = Properties.Resources.remove_scanner;
            deletePhoto.Visible = false;
            uploadedUserPoto.Image.Dispose();
            uploadedUserPoto.Image = null;
            uploadedUserPoto.Visible = false;
            uploadPhotoHover.Visible = true;
            currentPhotoPath = null;
        }

        private void HidePhotoAnalysisResults()
        {
            resultsAnalysis.Visible = false;
            if (res_userPhotoDish.Image != null)
            {
                res_userPhotoDish.Image.Dispose();
                res_userPhotoDish.Image = null;
            }
            if (uploadedUserPoto.Image != null)
            {
                uploadedUserPoto.Image.Dispose();
                uploadedUserPoto.Image = null;
            }

            res_userPhotoDish.Visible = false;
            res_userPhotoDish.Image = null;

            res_dishName.Visible = false;

            res_products.Visible = false;

            info_dish.Visible = false;

            line.Visible = false;

            res_totalCalories.Visible = false;
            res_totalCaories_value.Visible = false;
            res_protein.Visible = false;
            res_protein_value.Visible = false;

            res_fat.Visible = false;
            res_fat_value.Visible = false;

            res_carbohydrates.Visible = false;
            res_carbohydrates_value.Visible = false;

            res_cholesterol.Visible = false;
            res_cholesterol_value.Visible = false;

            res_calcium.Visible = false;
            res_calcium_value.Visible = false;

            res_iron.Visible = false;
            res_iron_value.Visible = false;

            closeResults.Visible = false;
        }
        private void closeResults_Click(object sender, EventArgs e)
        {
            HidePhotoAnalysisResults();

            uploadPhotoHover.Visible = true;
            btn_ScanPhoto.Visible = true;
        }
        private void ConfigureInfoDish()
        {
            toolTip_infoDish.ToolTipTitle = "Стандартна порція (100г).";
            toolTip_infoDish.SetToolTip(info_dish, "Інформація стосується стандартної порції продукту<br>" +
                                                   "(залежно від типу їжі). Харчовий склад, включаючи КБЖВ,<br>" +
                                                   "розрахований на основі даних про типовий розмір порції.<br>" +
                                                   "Якщо ви споживаєте інший обсяг, врахуйте, що значення<br>" +
                                                   "будуть пропорційно змінюватися.");
        }

        private void UpdateTexts()
        {
            label_Clarifai.Text = Properties.Resources.label_Clarifai;
            btn_ScanPhoto.Text = Properties.Resources.btn_ScanPhoto;
            toolTip_uploadPhoto.SetToolTip(uploadPhotoHover, Properties.Resources.toolTip_uploadPhoto);
        }
    }
}