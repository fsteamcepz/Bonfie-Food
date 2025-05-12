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
    public class Huggingface : ApiKeyFileChecker
    {
        private const string HUGGINGFACE_KEY = "Huggingface.json";
        private readonly string apiKey;
        private const string MODEL = "HuggingFaceH4/zephyr-7b-beta"; // google/gemma-2-27b-it
        private const string HUGGINGFACE_URL = "https://api-inference.huggingface.co/models/" + MODEL;

        public Huggingface()
        {
            if (File.Exists(HUGGINGFACE_KEY))
            {
                var json = File.ReadAllText(HUGGINGFACE_KEY);
                dynamic config = JsonConvert.DeserializeObject(json);

                if (config == null || string.IsNullOrEmpty(config.ApiKey?.ToString()))
                {
                    MessageBoxError.Show($"API ключ не вказано або некоректний у файлі «{HUGGINGFACE_KEY}».");
                }
                apiKey = config.ApiKey;
                isFileAvailable = true;
            }
            else
            {
                isFileAvailable = false;
            }
        }
        public async Task<string> GetResponse(string userPrompt)
        {
            // Роль бота
            string systemPrompt = @"Ти - інтелектуальний помічник 'Bonfie' у сфері харчування 
                    з досвідом понад 50 років. Ти впевнений у своїх відповідях, використовуєш гумор, і даєш неперевершено 
                    цікаві та корисні поради. Також, ти неймовірний вчитель (вчився на стипендії та закінчив з відзнакою 
                    Массачусетський технологічний інститут (MIT)), який із задоволенням відповідає на питання, що стосуються 
                    здоров'я, життя людини та харчування. Твоя особливість — багатий досвід, цікаві історії та вміння якісно 
                    навчати та легкий в спілкуванні. Ти повинен менше хизуватися своїм досвідом і більше акцентувати увагу на питанні";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                var requestBody = new
                {
                    inputs = $"{systemPrompt}\n\nUser: {userPrompt}\n\nAssistant:",
                    parameters = new
                    {
                        temperature = 0.7,
                        max_tokens = 2000,
                        top_p = 0.9,
                        return_full_text = false
                    }
                };

                string jsonBody = JsonConvert.SerializeObject(requestBody);
                HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(HUGGINGFACE_URL, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    var resultArray = JsonConvert.DeserializeObject<dynamic>(responseContent);

                    if (resultArray != null && resultArray.Count > 0)
                    {
                        string generatedText = resultArray[0]["generated_text"]?.ToString();

                        // видаляємо "роль бота" та "запитання користувача" з відповіді, якщо вони там є
                        if (!string.IsNullOrEmpty(generatedText))
                        {
                            int assistantIndex = generatedText.LastIndexOf("Assistant:");
                            if (assistantIndex != -1)
                            {
                                generatedText = generatedText.Substring(assistantIndex + "Assistant:".Length).Trim();
                            }

                            return generatedText;
                        }
                    }
                    return "Немає відповіді від моделі.";
                }
                else
                {
                    return $"Упс. Помилка: {response.StatusCode} {response.ReasonPhrase}";
                }
            }
        }
    }
}
