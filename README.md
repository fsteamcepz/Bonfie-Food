# Bonfie Food

# Login
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
