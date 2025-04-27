# Bonfie Food

# Authentication
<p align="center">
  <img src="https://github.com/fsteamcepz/Bonfie-Food/blob/master/screens/login.jpg?raw=true" alt="login form" />
</p>

# Main menu
<p align="center">
  <img src="https://github.com/fsteamcepz/Bonfie-Food/blob/master/screens/home_page.jpg?raw=true" alt="main menu" />
</p>

# Food Analysis
## Technologies Used
### [Google Cloud Vision API](https://cloud.google.com/vision)
- Used for basic image analysis and determining whether food is present in an image.
### [Clarifai API](https://www.clarifai.com/)
- Used for accurate recognition of food items and dishes in images.
- The [model](https://clarifai.com/clarifai/main/models/food-item-v1-recognition) `food-item-v1-recognition` classifies over 500 types of food products.
### [Edamam API](https://developer.edamam.com/)
Used for retrieving nutritional information about food products.
Edamam provides a comprehensive, ready-to-use food database and nutrition analysis service:

- **Food Database API**: Free access to a database with close to 900,000 foods and over 680,000 unique UPC codes.
- **Recipe Search API**: Over 2.3 million recipes by diets, calories and nutrient ranges.
- **Nutrition Analysis API**: Instantly analyzes the nutrition of any recipe from plain text input.

<p align="center">
  <img src="https://github.com/fsteamcepz/Bonfie-Food/blob/master/screens/analysis_food_1.jpg?raw=true" alt="photo upload" />
  <br/>
  <img src="https://github.com/fsteamcepz/Bonfie-Food/blob/master/screens/analysis_food_2.jpg?raw=true" alt="result" />
</p>

# Chatbot
## Technologies Used
### [Hugging Face API](https://huggingface.co/)
- The chatbot uses the `HuggingFaceH4/zephyr-7b-beta` model for generating human-like, context-aware responses.

<p align="center">
  <img src="https://github.com/fsteamcepz/Bonfie-Food/blob/master/screens/chatbot.jpg?raw=true" alt="history with chatbot" />
</p>

# Reset Password
## Technologies Used
### [Gmail API](https://console.cloud.google.com/marketplace/product/google/gmail.googleapis.com)
- The «Forgot Password» feature uses the Gmail API to send password reset emails securely.
- It leverages «OAuth 2.0» for authentication, ensuring secure access to the user's email account without exposing sensitive credentials.
### [OAuth 2.0](https://developers.google.com/identity/protocols/oauth2)
- OAuth 2.0 is used to authenticate and authorize the application to interact with the Gmail API.
- A JSON key file is utilized to store client secrets, and tokens are securely stored using `FileDataStore`:
```csharp
using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
{
      return await GoogleWebAuthorizationBroker.AuthorizeAsync(
                   GoogleClientSecrets.FromStream(stream).Secrets,
                   new[] { GmailService.Scope.GmailSend },
                   "user",
                   CancellationToken.None,
                   new FileDataStore(TOKEN_USER, true));
}
```
### MIME (Multipurpose Internet Mail Extensions)
- The email content is formatted using the MIME standard, which structures the email headers (To, Subject, Content-Type) and body for proper delivery and rendering by email clients:
```csharp
string mimeMessage = $"To: {emailUser}\r\n" +
                     $"Subject: {subject}\r\n" +
                     "Content-Type: text/plain; charset=utf-8\r\n" +
                     "\r\n" +
                     $"{body}";
```
### Base64 Encoding
- The email content is encoded in Base64 format before being sent via the Gmail API. This encoding is a [requirement](https://developers.google.com/workspace/gmail/api/guides/sending#:~:text=The%20Gmail%20API%20requires%20MIME) of the Gmail API to ensure the safe transmission of email data over the network:
```csharp
private string Base64UrlEncode(string input)
{
      var inputBytes = Encoding.UTF8.GetBytes(input);
      return Convert.ToBase64String(inputBytes)
                    .Replace('+', '-')
                    .Replace('/', '_')
                    .Replace("=", "");
}
```
### Random Password Generation
- A temporary password is generated to ensure the user can regain access to their account securely:
```csharp
private string GenerateRandomPassword(int length)
{
      string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*";
      Random random = new Random();

      char[] pass = new char[length];

      for (int i = 0; i < length; i++)
      {
            pass[i] = chars[random.Next(chars.Length)];
      }
      return new string(pass);
}
```
<p align="center">
  <img src="#" alt="forgot password form" />
</p>
