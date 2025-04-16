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
    public class Edamam_Recipe
    {
        private const string FILENAME_EDAMAM = "Edamam_Recipes.json";
        private readonly string idEdamam;
        private readonly string apiKey;
        private readonly string idUser;
        private const string RECIPE_SEARCH_URL = "https://api.edamam.com/api/recipes/v2";

        public string Name { get; set; }
        public string Photo { get; set; }
        public string Category { get; set; }
        public List<string> Ingredients { get; set; }
        public double Calories { get; set; }
        public double Yield { get; set; }
        public double Protein { get; set; }
        public double Fat { get; set; }
        public double Carbohydrates { get; set; }
        public int IngredientCount { get; set; }

        public Edamam_Recipe()
        {
            if (File.Exists(FILENAME_EDAMAM))
            {
                var json = File.ReadAllText(FILENAME_EDAMAM);
                dynamic config = JsonConvert.DeserializeObject(json);

                if (config == null || string.IsNullOrEmpty(config.Id?.ToString()) ||
                    string.IsNullOrEmpty(config.ApiKey?.ToString()) ||
                    string.IsNullOrEmpty(config.IdUser?.ToString()))
                {
                    MessageBoxError.Show($"ID/IdUser або API ключ не вказані або некоректні у файлі «{FILENAME_EDAMAM}».");
                }
                idEdamam = config.Id;
                apiKey = config.ApiKey;
                idUser = config.IdUser;
            }
            else
            {
                MessageBoxError.Show($"Файл «{FILENAME_EDAMAM}» не знайдено.");
            }
        }

        public async Task<List<Edamam_Recipe>> SearchRecipesAsync(string nameOfRecipe)
        {
            string url = $"{RECIPE_SEARCH_URL}?type=public&q={Uri.EscapeDataString(nameOfRecipe)}&app_id={idEdamam}&app_key={apiKey}";

            using (var client = new HttpClient())
            {
                // додавання заголовків
                client.DefaultRequestHeaders.Add("Edamam-Account-User", idUser);

                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string jResponse = await response.Content.ReadAsStringAsync();
                    dynamic recipeResponse = JsonConvert.DeserializeObject(jResponse);

                    if (recipeResponse?.hits == null || recipeResponse.hits.Count == 0)
                    {
                        MessageBoxError.Show("Рецепт не знайдено.");
                        return null;
                    }

                    var recipes = new List<Edamam_Recipe>();

                    foreach (var hit in recipeResponse.hits)
                    {
                        var recipe = new Edamam_Recipe
                        {
                            Name = hit.recipe.label,
                            Photo = hit.recipe.image,
                            Category = hit.recipe.source,
                            Yield = hit.recipe.yield,
                            Ingredients = hit.recipe.ingredientLines.ToObject<List<string>>(),
                            Calories = hit.recipe.calories,
                            Protein = hit.recipe.totalNutrients.PROCNT.quantity,
                            Fat = hit.recipe.totalNutrients.FAT.quantity,
                            Carbohydrates = hit.recipe.totalNutrients.CHOCDF.quantity,
                            IngredientCount = hit.recipe.ingredientLines.Count
                        };
                        recipes.Add(recipe);
                    }
                    return recipes;
                }
                else
                {
                    MessageBoxError.Show("Помилка при запиті до Recipe Search API!");
                    return null;
                }
            }
        }
    }
}