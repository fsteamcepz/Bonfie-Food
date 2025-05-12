using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BonfieFood
{
    public class OAuthEmail : ApiKeyFileChecker
    {
        private const string OAUTH2_API = "client_secret_email.json";   // авторизація до OAuth 2.0
        private const string TOKEN_USER = "token.json";                 // токен доступу (уникаємо повторної авторизації)

        public async Task<bool> SendEmail(string emailUser, string subject, string body)
        {
            try
            {
                UserCredential credential = await Authenticate().ConfigureAwait(false);
                if (credential == null)
                {
                    return false;
                }

                var service = new GmailService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "BonfieFood",
                });

                string mimeMessage = $"To: {emailUser}\r\n" +
                                     $"Subject: {subject}\r\n" +
                                     "Content-Type: text/plain; charset=utf-8\r\n" +
                                     "\r\n" +
                                     $"{body}";

                string encodedMessage = Base64UrlEncode(mimeMessage);
                var email = new Google.Apis.Gmail.v1.Data.Message { Raw = encodedMessage };

                // надсилаємо листа від імені 'me'
                await service.Users.Messages.Send(email, "me").ExecuteAsync().ConfigureAwait(false);
                return true;
            }
            catch (Exception ex)
            {
                MessageBoxError.Show($"Помилка надсилання листа: {ex.Message}");
                return false;
            }
        }
        private async Task<UserCredential> Authenticate()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, OAUTH2_API);
            if (!File.Exists(path))
            {
                isFileAvailable = false;
                return null;
            }
            isFileAvailable = true;

            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                return await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    new[] { GmailService.Scope.GmailSend },
                    "user",
                    CancellationToken.None,
                    new FileDataStore(TOKEN_USER, true));
            }
        }
        private string Base64UrlEncode(string input)
        {
            var inputBytes = Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(inputBytes)
                .Replace('+', '-')
                .Replace('/', '_')
                .Replace("=", "");
        }
    }
}
