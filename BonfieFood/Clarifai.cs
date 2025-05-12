using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace BonfieFood
{
    public class Clarifai : ApiKeyFileChecker
    {
        private const string FILENAME_CLARIFAI = "Clarifai.json";
        private readonly string apiKey;
        private const string URL_MODEL = "https://api.clarifai.com/v2/users/clarifai/apps/main/models/food-item-v1-recognition/versions/dfebc169854e429086aceb8368662641/outputs";

        public Clarifai()
        {
            if (File.Exists(FILENAME_CLARIFAI))
            {
                var json = File.ReadAllText(FILENAME_CLARIFAI);
                dynamic config = JsonConvert.DeserializeObject(json);

                if (config == null || string.IsNullOrEmpty(config.ApiKey?.ToString()))
                {
                    MessageBoxError.Show($"API ключ не вказано або некоректний у файлі «{FILENAME_CLARIFAI}».");
                }

                apiKey = config.ApiKey;
                isFileAvailable = true;
            }
            else
            {
                isFileAvailable = false;
            }
        }
        public async Task<(string dishName, List<string> products)> AnalyzePhoto(string imagePath)
        {
            try
            {
                // зчитуємо зображення та кодуємо його у формат Base64
                var imageBytes = File.ReadAllBytes(imagePath);
                var base64Image = Convert.ToBase64String(imageBytes);

                // формуємо JSON-запит
                var requestData = new
                {
                    inputs = new[]
                    {
                        new
                        {
                            data = new
                            {
                                image = new
                                {
                                    base64 = base64Image
                                }
                            }
                        }
                    }
                };

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Key {apiKey}");
                    var content = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(URL_MODEL, content);

                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBoxError.Show($"Помилка авторизації Clarifai API: {response.StatusCode}");
                    }

                    var responseBody = await response.Content.ReadAsStringAsync();
                    dynamic jsonResponse = JsonConvert.DeserializeObject(responseBody);

                    var concepts = jsonResponse.outputs[0].data.concepts;
                    // перетворюємо concepts на список для обробки
                    var conceptsEnumerable = ((IEnumerable<dynamic>)concepts).ToList();

                    // фільтруємо інгредієнти з value > 0.2
                    var filteredProducts = conceptsEnumerable
                        .Where(c => (double)c.value > 0.2)
                        .Select(c => c.name.ToString())
                        .Cast<string>()     // перетворюємо на List<string>
                        .ToList();

                    if (filteredProducts.Count == 0)
                    {
                        MessageBoxError.Show("Не вдалося виявити продукти!");
                    }

                    string dishName;
                    List<string> selectedProducts;

                    // декілька value = 0.999
                    var highProducts = conceptsEnumerable
                        .Where(c => (double)c.value > 0.999)
                        .Select(c => c.name.ToString())
                        .Cast<string>()     // перетворюємо на List<string>
                        .ToList();

                    if (highProducts.Count >= 2)
                    {
                        dishName = string.Join(" ", highProducts);
                        selectedProducts = new List<string> { dishName };
                    }
                    else if (highProducts.Count == 1)
                    {
                        dishName = highProducts[0];
                        selectedProducts = new List<string> { dishName };
                    }
                    else
                    {
                        var topConcepts = conceptsEnumerable
                            .Where(c => (double)c.value > 0.5)
                            .OrderByDescending(c => (double)c.value)
                            .Take(2)
                            .ToList();

                        if (topConcepts.Count >= 2)
                        {
                            dishName = $"{topConcepts[0].name} and {topConcepts[1].name}";
                            selectedProducts = topConcepts.Select(c => c.name.ToString()).Cast<string>().ToList();
                        }
                        else
                        {
                            dishName = "Невідома страва";
                            selectedProducts = filteredProducts;
                        }
                    }

                    return (dishName, selectedProducts);
                }
            }
            catch
            {
                throw new Exception($"Помилка під час запиту до Clarifai API!");
            }
        }
    }
}