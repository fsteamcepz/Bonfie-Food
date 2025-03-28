using Google.Apis.Auth.OAuth2;
using Google.Cloud.Vision.V1;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace BonfieFood
{
    public class CloudVision
    {
        private const string CLOUDVISION_API = "client_secret_food_analyzer.json";
        private readonly ImageAnnotatorClient client;

        public CloudVision()
        {
            if (File.Exists(CLOUDVISION_API))
            {


                using (var stream = new FileStream(CLOUDVISION_API, FileMode.Open, FileAccess.Read))
                {
                    var credential = GoogleCredential.FromStream(stream)
                        .CreateScoped(ImageAnnotatorClient.DefaultScopes);

                    client = new ImageAnnotatorClientBuilder
                    {
                        Credential = credential
                    }.Build();
                }
            }
            else
            {
                MessageBoxError.Show("Помилка аутентифікації. Файл «" + CLOUDVISION_API + "» не знайдено.");
            }
        }
        public async Task<bool> IsFood(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath) || !File.Exists(imagePath))
            {
                MessageBoxError.Show($"Файл «{CLOUDVISION_API}» не знайдено!");
                return false;
            }

            var image = Google.Cloud.Vision.V1.Image.FromFile(imagePath);
            var labels = await client.DetectLabelsAsync(image);

            return labels.Any(label => label.Description.ToLower().Contains("food"));

        }
    }
}
