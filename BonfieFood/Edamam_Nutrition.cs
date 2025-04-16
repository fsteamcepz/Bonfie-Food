using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace BonfieFood
{
    public class Edamam_Nutrition
    {
        private const string FILENAME_EDAMAM = "Edamam_Nutrition.json";
        private readonly string idEdamam;
        private readonly string apiKey;
        private const string NUTRITION_URL = "https://api.edamam.com/api/nutrition-details";

        public string NameProduct { get; set; }
        public string Measure { get; set; }
        public double Calories { get; set; }
        public double Weight { get; set; }
        public double Quantity { get; set; }

        public double TotalCalories { get; set; }
        public List<string> Products { get; set; }  // введені продукти користувача
        public List<Edamam_Nutrition> ProductsUser { get; set; } =
                                      new List<Edamam_Nutrition>();  // детальна інформ. про продукти після аналізу

        public Edamam_Nutrition()
        {
            if (File.Exists(FILENAME_EDAMAM))
            {
                var json = File.ReadAllText(FILENAME_EDAMAM);
                dynamic config = JsonConvert.DeserializeObject(json);

                if (config == null || string.IsNullOrEmpty(config.Id?.ToString()) ||
                    string.IsNullOrEmpty(config.ApiKey?.ToString()))
                {
                    MessageBoxError.Show($"ID або API ключ не вказані або некоректні у файлі «{FILENAME_EDAMAM}».");
                }
                idEdamam = config.Id;
                apiKey = config.ApiKey;
            }
            else
            {
                MessageBoxError.Show($"Файл «{FILENAME_EDAMAM}» не знайдено.");
            }
        }
        public async Task<double?> AnalyzeNutritionAsync()
        {
            TotalCalories = 0;
            ProductsUser.Clear();

            string url = $"{NUTRITION_URL}?app_id={idEdamam}&app_key={apiKey}";

            using (HttpClient client = new HttpClient())
            {
                var request = new
                {
                    ingr = Products
                };

                var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    string jResponse = await response.Content.ReadAsStringAsync();
                    dynamic nutrition = JsonConvert.DeserializeObject(jResponse);
                    
                    foreach (var ingredient in nutrition.ingredients)
                    {
                        foreach (var p in ingredient.parsed)
                        {
                            var product = new Edamam_Nutrition
                            {
                                NameProduct = p.food,
                                Measure = p.measure,
                                Calories = p.nutrients.ENERC_KCAL.quantity,
                                Weight = p.weight,
                                Quantity = p.quantity
                            };
                            TotalCalories += product.Calories;

                            ProductsUser.Add(product);
                        }
                    }
                    return TotalCalories;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}