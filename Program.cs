// This application takes a command line input as the name of a city,
// and provides information about the weather for that city. This is a client
// app which consumes the RESTful OpenWeather API.

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace WeatherREST {
	class Program {

		// Receives a string city name and performs an HTTP GET request with the
		// passed in city name as a query parameter.
		static void Main(string[] args) {
			if (args.Length < 1) {
				Console.WriteLine("Incorrect Usage: .exe cityName");
			} else {
				// Must combine all parameters to be a single city name.
				string result = "";
				for (int i = 0; i < args.Length; i++) {
					if (i > 0) {
						result = result + " " + args[i];
					} else {
						result = args[i];
					}
				}
				GetWeather(result);
			}
		}

		// Performs an HTTP GET request to OpenWeatherAPI using the string cityName as a 
		// query parameter. Outputs an error message to the console if the HTTP Get was
		// not successful. Otherwise, displays the fetched weather data to console.
		static void GetWeather(string cityName) {
			Console.WriteLine("Making OpenWeather API Call by cityName: " + cityName);
			try {
				using (var client = new HttpClient()) {
					client.BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/");
					string key = "REDACTED";
					HttpResponseMessage response = client.GetAsync("weather?q=" + cityName + "&APPID=" + key).Result;
					response.EnsureSuccessStatusCode();
					string result = response.Content.ReadAsStringAsync().Result;

					RootObject weatherDetails = JsonConvert.DeserializeObject<RootObject>(result);
					DisplayWeatherInfo(weatherDetails);
				}
			} catch (Exception e) {
				Console.WriteLine(e.Message);
			}
		}

		// Receives a deserialized JSON object from the OpenWeatherAPI.
		// The passed in RootObject contains all the information returned from the GET.
		static void DisplayWeatherInfo(RootObject weatherDetails) {
			// Resulting Weather Info
			Console.WriteLine("Weather in: " + weatherDetails.name + ", " + weatherDetails.sys.country);
			// Temperature
			int temp = GetTemp(weatherDetails.main.temp);
			int minTemp = GetTemp(weatherDetails.main.temp_min);
			int maxTemp = GetTemp(weatherDetails.main.temp_max);
			Console.WriteLine(" -- Temperature: " + temp + "F : min(" + minTemp + "F),max(" + maxTemp + "F)");
			// Quick Weather Description
			Console.WriteLine(" -- " + weatherDetails.weather[0].description);
			// Humidity
			Console.WriteLine(" -- Humidity: " + weatherDetails.main.humidity + "%");
			// Wind
			Console.WriteLine(" -- Wind: " + weatherDetails.wind.speed + " meter/sec " 
							  +  "(" + DegreeToDirection(weatherDetails.wind.deg) + ")");
			// Time, prints DateTime objects in localtime to the screen.
			DateTime currentDateTime = EpochToDateTime(weatherDetails.dt).ToLocalTime();
			DateTime sunriseDateTime = EpochToDateTime(weatherDetails.sys.sunrise).ToLocalTime();
			DateTime sunsetDateTime = EpochToDateTime(weatherDetails.sys.sunset).ToLocalTime();
			Console.WriteLine(" -- Date (local): " + currentDateTime.ToLongDateString());
			Console.WriteLine(" -- Time (local): " + currentDateTime.ToShortTimeString());
			Console.WriteLine(" -- Sunrise (local): " + sunriseDateTime.ToShortTimeString());
			Console.WriteLine(" -- Sunset (local): " + sunsetDateTime.ToShortTimeString());
			Console.WriteLine("---------------------------------");
		}

		// Recieves a long epoch (unix time) and outputs a DateTime relative to your local time.
		private static DateTime EpochToDateTime(long epoch) {
			return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(epoch);
		}

		// Receives a kevlinTemp as a float and returns it as an integer in farenheit.
		private static int GetTemp(float kelvinTemp) {
			return (int)((kelvinTemp - 273.15) * 1.8 + 32);
		}

		// Receives a float degree and returns a string cardinal direction.
		// Returns a string error if degree cannot map to a direction.
		private static string DegreeToDirection(float degree) {
			if (degree >= 348.75 || degree <= 11.25) {
				return "N";
			} else if (degree >= 11.25 && degree <= 33.75) {
				return "NNE";
			} else if (degree >= 33.75 && degree <= 56.25) {
				return "NE";
			} else if (degree >= 56.25 && degree <= 78.75) {
				return "ENE";
			} else if (degree >= 78.75 && degree <= 101.25) {
				return "E";
			} else if (degree >= 101.25 && degree <= 123.75) {
				return "ESE";
			} else if (degree >= 123.75 && degree <= 146.25) {
				return "SE";
			} else if (degree >= 146.25 && degree <= 168.75) {
				return "SSE";
			} else if (degree >= 168.75 && degree <= 191.25) {
				return "S";
			} else if (degree >= 191.25 && degree <= 213.75) {
				return "SSW";
			} else if (degree >= 213.75 && degree <= 236.25) {
				return "SW";
			} else if (degree >= 236.25 && degree <= 258.75) {
				return "WSW";
			} else if (degree >= 258.75 && degree <= 281.25) {
				return "W";
			} else if (degree >= 281.25 && degree <= 303.75) {
				return "WNW";
			} else if (degree >= 303.75 && degree <= 326.25) {
				return "NW";
			} else if (degree >= 326.25 && degree <= 348.75) {
				return "NNW";
			} else {
				return degree + " does not map to a direction.";
			}
		}
	}
}
