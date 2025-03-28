using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Newtonsoft.Json;

namespace BonfieFood
{
    public partial class Settings_PersonInfo : Form
    {
        DataBase db = new DataBase();
        private bool isInitialLoad = true;      // відстежуємо завантаження country і city
        private string saveCity = null;         // збереження city

        private List<Country> countries = new List<Country>();

        public Settings_PersonInfo()
        {
            InitializeComponent();
        }

        private void Settings_PersonInfo_Load(object sender, EventArgs e)
        {
            LoadCountriesAsync();
            switchBirthDate.Checked = false;
            dateOfBirth.Visible = false;
        }
        private async void LoadCountriesAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync(CountryResponse.COUNTRIES_API_URL);
                var countriesData = JsonConvert.DeserializeObject<CountryResponse>(response);

                if (countriesData?.Geonames != null)
                {
                    countries = countriesData.Geonames
                        .Select(countryElement => new Country
                        {
                            Name = countryElement.CountryName,
                            Code = countryElement.CountryCode
                        })
                        .OrderBy(c => c.Name)
                        .ToList();

                    country.DataSource = countries;
                    country.DisplayMember = "Name";
                    country.SelectedIndex = -1;

                    await LoadUserData();
                }
            }
        }
        private async void country_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialLoad && country.SelectedItem is Country selectedCountry)
            {
                await LoadCitiesByCode(selectedCountry.Code, null);
            }
        }
        private async Task LoadCitiesByCode(string countryCode, string savedCity)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync(string.Format(CityResponse.CitiesAPIUrl, countryCode));
                var citiesData = JsonConvert.DeserializeObject<CityResponse>(response);
                var cities = new List<string>();

                if (citiesData?.Geonames != null)
                {
                    cities = citiesData.Geonames
                        .Where(cityElement => !string.IsNullOrEmpty(cityElement.Name))
                        .Select(cityElement => cityElement.Name)
                        .ToList();
                }

                // відключаємо події під час оновлення
                city.BeginUpdate();
                city.Items.Clear();
                city.Items.AddRange(cities.ToArray());

                if (!string.IsNullOrEmpty(savedCity))
                {
                    for (int i = 0; i < city.Items.Count; i++)
                    {
                        if (city.Items[i].ToString().Equals(savedCity, StringComparison.OrdinalIgnoreCase))
                        {
                            city.SelectedIndex = i;
                            break;
                        }
                    }
                }
                else
                {
                    city.SelectedIndex = -1;
                }
                city.EndUpdate();
            }
        }
        private async Task LoadUserData()
        {
            string query = @"
                   SELECT gender, firstName, lastName, country, city, birthDate, favoriteDish
                   FROM UserPersonalInfo
                   JOIN UserHealthMetrics ON UserPersonalInfo.id_User = UserHealthMetrics.id_User
                   WHERE UserPersonalInfo.id_User = @id_User";

            using (SqlCommand cmd = new SqlCommand(query, db.getConnection()))
            {
                cmd.Parameters.AddWithValue("@id_User", CurrentUser.UserId);

                db.openConnection();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string gender = reader["gender"].ToString();
                        if (gender == "Чоловік") male.Checked = true;
                        else if (gender == "Жінка") female.Checked = true;

                        // Ім'я
                        firstName.Text = reader["firstName"].ToString();
                        lastName.Text = reader["lastName"].ToString();

                        // Країна та місто
                        string countryUser = reader["country"].ToString();
                        saveCity = reader["city"].ToString();

                        if (!string.IsNullOrEmpty(countryUser))
                        {
                            var countryItem = countries.FirstOrDefault(c => c.Name.Equals(countryUser, StringComparison.OrdinalIgnoreCase));
                            if (countryItem != null)
                            {
                                country.SelectedItem = countryItem;
                                await LoadCitiesByCode(countryItem.Code, saveCity);
                            }
                        }

                        // Дата народження
                        if (!reader.IsDBNull(reader.GetOrdinal("birthDate")))
                        {
                            dateOfBirth.Value = reader.GetDateTime(reader.GetOrdinal("birthDate"));
                            switchBirthDate.Checked = true;
                            dateOfBirth.Visible = true;
                        }
                        else
                        {
                            dateOfBirth.Value = DateTime.Today;
                            switchBirthDate.Checked = false;
                            dateOfBirth.Visible = false;
                        }

                        // Улюблена страва
                        favoriteDish.Text = reader["favoriteDish"].ToString();
                    }
                }
                db.closeConnection();
            }
            isInitialLoad = false; // початкове завантаження завершено
        }

        private void savePerInfo_Click(object sender, EventArgs e)
        {
            // Стать
            string genderU = null;
            if (male.Checked)
                genderU = "Чоловік";
            else if (female.Checked)
                genderU = "Жінка";

            // Ім'я
            string forbiddenSymbols = "[]{}+=-&^:;|/><$#@!?№%()€₴~-”„«»\"-";
            string firstU = firstName.Text.Trim();

            if (string.IsNullOrWhiteSpace(firstName.Text))
            {
                firstU = null;
            }
            else if (firstU.Any(c => forbiddenSymbols.Contains(c) && c != '\''))
            {
                MessageBoxError.Show("Ім'я не повинно містити спеціальні символи");
                firstName.BorderColor = Color.Red;
                return;
            }
            else if (!firstU.All(c => Char.IsLetter(c) || c == '\''))
            {
                MessageBoxError.Show("Ім'я має містити лише літери");
                firstName.BorderColor = Color.Red;
                return;
            }
            else if ((firstU.Length < 2 || firstU.Length > 50) || firstU.Count(c => Char.IsLetter(c)) < 2)
            {
                MessageBoxError.Show("Ім'я має бути від 2 до 50 літер!");
                firstName.BorderColor = Color.Red;
                return;
            }
            firstName.BorderColor = Color.FromArgb(197, 116, 222);

            // Прізвище
            string lastU = lastName.Text.Trim();

            if (string.IsNullOrWhiteSpace(lastName.Text))
            {
                lastU = null;
            }
            else if (lastU.Any(c => forbiddenSymbols.Contains(c) && c != '\''))
            {
                MessageBoxError.Show("Прізвище не повинно містити спеціальні символи");
                lastName.BorderColor = Color.Red;
                return;
            }
            else if (!lastU.All(c => Char.IsLetter(c) || c == '\''))
            {
                MessageBoxError.Show("Прізвище має містити лише літери");
                lastName.BorderColor = Color.Red;
                return;
            }
            else if ((lastU.Length < 2 || lastU.Length > 50) || lastU.Count(c => Char.IsLetter(c)) < 2)
            {
                MessageBoxError.Show("Прізвище має бути від 2 до 50 літер!");
                lastName.BorderColor = Color.Red;
                return;
            }
            lastName.BorderColor = Color.FromArgb(197, 116, 222);

            // Країна
            string countryU = string.IsNullOrWhiteSpace(country.Text) ? null : country.Text.Trim();

            // Місто
            string cityU = string.IsNullOrWhiteSpace(city.Text) ? null : city.Text.Trim();

            // Дата народження
            DateTime? birthDate = null;

            if (switchBirthDate.Checked)
            {
                if (dateOfBirth.Value == DateTimePicker.MinimumDateTime)
                {
                    MessageBoxError.Show("Будь ласка, оберіть дату народження!");
                    return;
                }

                birthDate = dateOfBirth.Value;
                int ageUser = DateTime.Now.Year - birthDate.Value.Year;

                if (birthDate.Value.Date > DateTime.Now.Date.AddYears(-ageUser))
                {
                    ageUser--;
                }
                if (ageUser < 4 || ageUser > 100)
                {
                    MessageBoxError.Show("Введіть коректну дату народження (вік 4-100 років)!");
                    return;
                }
            }

            // Улюблена страва
            string favDishU = favoriteDish.Text.Trim();
            if (string.IsNullOrWhiteSpace(favDishU))
            {
                favDishU = null;
            }
            else if (!Regex.IsMatch(favDishU, @"^[а-яА-ЯіІїЇєЄёЁa-zA-ZіІїЇєЄёЁ\s\-”„«»`'""-]+$"))
            {
                MessageBoxError.Show("Введіть коректну назву страви!");
                favoriteDish.BorderColor = Color.Red;
                return;
            }
            else if (favDishU.Length < 2 || favDishU.Length > 50)
            {
                MessageBoxError.Show("Назва улюбленої страви має складатися з 2-50 літер!");
                favoriteDish.BorderColor = Color.Red;
                return;
            }
            favoriteDish.BorderColor = Color.FromArgb(197, 116, 222);


            // зберігаємо дані
            string checkHealthMetrics = @"SELECT COUNT(*) FROM UserHealthMetrics WHERE id_User = @id_User";
            string checkPersonalInfo = @"SELECT COUNT(*) FROM UserPersonalInfo WHERE id_User = @id_User";
            bool hasPersonalInfo, hasHealthMetrics;

            db.openConnection();
            using (SqlCommand cmd = new SqlCommand(checkPersonalInfo, db.getConnection()))
            {
                cmd.Parameters.AddWithValue("@id_User", CurrentUser.UserId);
                hasPersonalInfo = (int)cmd.ExecuteScalar() > 0;
            }

            using (SqlCommand cmd = new SqlCommand(checkHealthMetrics, db.getConnection()))
            {
                cmd.Parameters.AddWithValue("@id_User", CurrentUser.UserId);
                hasHealthMetrics = (int)cmd.ExecuteScalar() > 0;
            }

            // зберігаємо дані
            string personalInfoQuery = hasPersonalInfo
                ? "UPDATE UserPersonalInfo SET firstName = @firstName, lastName = @lastName, country = @country, city = @city, favoriteDish = @favoriteDish WHERE id_User = @id_User"
                : "INSERT INTO UserPersonalInfo (id_User, firstName, lastName, country, city, favoriteDish) VALUES (@id_User, @firstName, @lastName, @country, @city, @favoriteDish)";

            string healthMetricsQuery = hasHealthMetrics
                ? "UPDATE UserHealthMetrics SET birthDate = @birthDate, gender = @gender WHERE id_User = @id_User"
                : "INSERT INTO UserHealthMetrics (id_User, birthDate, gender) VALUES (@id_User, @birthDate, @gender)";

            // UserPersonalInfo
            using (SqlCommand cmd = new SqlCommand(personalInfoQuery, db.getConnection()))
            {
                cmd.Parameters.AddWithValue("@id_User", CurrentUser.UserId);
                cmd.Parameters.AddWithValue("@firstName", (object)firstU ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@lastName", (object)lastU ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@country", (object)countryU ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@city", (object)cityU ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@favoriteDish", (object)favDishU ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }

            // UserHealthMetrics
            using (SqlCommand cmd = new SqlCommand(healthMetricsQuery, db.getConnection()))
            {
                cmd.Parameters.AddWithValue("@id_User", CurrentUser.UserId);
                cmd.Parameters.AddWithValue("@birthDate", (object)birthDate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@gender", (object)genderU ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }

            db.closeConnection();

            MessageBoxSuccess.Show("Дані успішно збережено!");
        }

        private void clearGender_Click(object sender, EventArgs e)
        {
            male.Checked = false;
            female.Checked = false;
        }
        private void clearFirstName_Click(object sender, EventArgs e)
        {
            firstName.Text = "";
            firstName.BorderColor = Color.FromArgb(197, 116, 222);
        }
        private void clearLastName_Click(object sender, EventArgs e)
        {
            lastName.Text = "";
            firstName.BorderColor = Color.FromArgb(197, 116, 222);
        }
        private void clearFavoriteDish_Click(object sender, EventArgs e)
        {
            favoriteDish.Text = "";
            firstName.BorderColor = Color.FromArgb(197, 116, 222);
        }
        private void clearCountry_Click(object sender, EventArgs e)
        {
            country.StartIndex = 0;
            country.StartIndex = -1;
        }
        private void clearCity_Click(object sender, EventArgs e)
        {
            city.StartIndex = -1;
        }
        private void switchBirthDate_CheckedChanged(object sender, EventArgs e)
        {
            if (switchBirthDate.Checked)
            {
                dateOfBirth.Enabled = true;
                dateOfBirth.Visible = true;
            }
            else
            {
                dateOfBirth.Enabled = false;
                dateOfBirth.Visible = false;
            }
        }
    }
}