using System.IO;
using Newtonsoft.Json;

namespace BonfieFood
{
    public class Edamam_Products : ApiKeyFileChecker
    {
        private const string FILENAME_EDAMAM = "Edamam_Products.json";
        public readonly string idEdamam;
        public readonly string apiKey;
        public readonly string EDAMAM_SEARCH_URL = "https://api.edamam.com/api/food-database/v2/parser";
        public readonly string EDAMAM_NUTRIENTS_URL = "https://api.edamam.com/api/food-database/v2/nutrients";
        public string MEASURE_URI = "http://www.edamam.com/ontologies/edamam.owl#Measure_gram";

        public string FoodName { get; set; }
        public double? Calories { get; set; }
        public double? Protein { get; set; }
        public double? Fat { get; set; }
        public double? SaturatedFat { get; set; }
        public double? TransFat { get; set; }
        public double? PolyunsaturatedFat { get; set; }
        public double? MonounsaturatedFat { get; set; }
        public double? Carbohydrates { get; set; }
        public double? DietaryFiber { get; set; }
        public double? Sugars { get; set; }
        public double? Cholesterol { get; set; }
        public double? Sodium { get; set; }
        public double? Calcium { get; set; }
        public double? Iron { get; set; }
        public double? Potassium { get; set; }
        public double? VitaminA { get; set; }
        public double? VitaminC { get; set; }
        public double? Thiamin { get; set; }
        public double? Riboflavin { get; set; }
        public double? Niacin { get; set; }
        public double? VitaminB6 { get; set; }
        public double? Folate { get; set; }
        public double? VitaminB12 { get; set; }
        public double? VitaminD { get; set; }
        public double? VitaminE { get; set; }
        public double? VitaminK { get; set; }
        public string MaxVitaminName { get; set; }
        public double? MaxVitaminValue { get; set; }
        public string Photo { get; set; }
        public string Category { get; set; }

        public Edamam_Products()
        {
            if (File.Exists(FILENAME_EDAMAM))
            {
                var json = File.ReadAllText(FILENAME_EDAMAM);
                dynamic config = JsonConvert.DeserializeObject(json);

                if (config == null || string.IsNullOrEmpty(config.ApiKey?.ToString()) || string.IsNullOrEmpty(config.Id?.ToString()))
                {
                    MessageBoxError.Show($"ID та API ключ не вказані або некоректні у файлі «{FILENAME_EDAMAM}».");
                }
                idEdamam = config.Id;
                apiKey = config.ApiKey;
                isFileAvailable = true;
            }
            else
            {
                isFileAvailable = false;
            }
        }
    }
}