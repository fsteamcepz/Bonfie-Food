# Bonfie Food

# Login
![login form](https://github.com/fsteamcepz/Bonfie-Food/blob/master/screens/login.jpg?raw=true)

# Main menu
![main menu](https://github.com/fsteamcepz/Bonfie-Food/blob/master/screens/home_page.jpg?raw=true)

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
- **Recipe Licensing**: Over 180,000 full recipes and nutrition data for more than 2.3 million web recipes.
- **Nutrition Analysis API**: Instantly analyzes the nutrition of any recipe from plain text input.

![photo upload](https://github.com/fsteamcepz/Bonfie-Food/blob/master/screens/analysis_food_1.jpg?raw=true)
![result](https://github.com/fsteamcepz/Bonfie-Food/blob/master/screens/analysis_food_2.jpg?raw=true)

# Chatbot
## Technologies Used
### [Hugging Face API](https://huggingface.co/)
- The chatbot uses the `HuggingFaceH4/zephyr-7b-beta` model for generating human-like, context-aware responses.

![history with chatbot](https://github.com/fsteamcepz/Bonfie-Food/blob/master/screens/chatbot.jpg?raw=true)
