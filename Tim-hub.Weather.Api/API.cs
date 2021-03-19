using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace Tim_hub.Weather
{
    public static class API
    {
        public static string key;

        public class Weather
        {
            public class Cond
            {
                /// <summary>
                /// Weather condition id. Detail https://openweathermap.org/weather-conditions
                /// </summary>
                [JsonProperty("id")]
                public int ID;


                /// <summary>
                /// Group of weather parameters (Rain, Snow, Extreme etc.)
                /// </summary>
                [JsonProperty("main")]
                public string state;


                /// <summary>
                /// Weather condition within the group
                /// </summary>
                [JsonProperty("description")]
                public string Description;


                /// <summary>
                /// Weather icon id. Detail https://openweathermap.org/weather-conditions
                /// </summary>
                [JsonProperty("icon")]
                public string IconID;
            }

            public class Coord
            {
                /// <summary>
                /// City geo location, longitude
                /// </summary>
                [JsonProperty("lat")]
                public float Latitude;
                /// <summary>
                /// City geo location, latitude
                /// </summary>
                [JsonProperty("lon")]
                public float Longitude;

                public Coord() { }
                public Coord(float latitude, float longitude)
                {
                    this.Longitude = longitude;
                    this.Latitude = latitude;
                }
            }

            public class Measurem
            {
                /// <summary>
                /// Temperature. Unit Default: Kelvin, Metric: Celsius, Imperial: Fahrenheit.
                /// </summary>
                [JsonProperty("temp")]
                public float Temperatur;


                /// <summary>
                /// Temperature. This temperature parameter accounts for the human perception of weather. Unit Default: Kelvin, Metric: Celsius, Imperial: Fahrenheit.
                /// </summary>
                [JsonProperty("feels_like")]
                public float Temperatur_FeelsLike;


                /// <summary>
                /// Atmospheric pressure (on the sea level, if there is no sea_level or grnd_level data), hPa
                /// </summary>
                [JsonProperty("pressure")]
                public float Pressure;


                /// <summary>
                /// Humidity, %
                /// </summary>
                [JsonProperty("humidity")]
                public float Humidity;


                /// <summary>
                /// Minimum temperature at the moment. This is minimal currently observed temperature (within large megalopolises and urban areas). Unit Default: Kelvin, Metric: Celsius, Imperial: Fahrenheit.
                /// </summary>
                [JsonProperty("temp_min")]
                public float TemperaturMin;



                /// <summary>
                /// Maximum temperature at the moment. This is maximal currently observed temperature (within large megalopolises and urban areas). Unit Default: Kelvin, Metric: Celsius, Imperial: Fahrenheit.
                /// </summary>
                [JsonProperty("temp_max")]
                public float TemperaturMax;


                /// <summary>
                ///  Atmospheric pressure on the sea level, hPa
                /// </summary>
                [JsonProperty("sea_level")]
                public float PressureSeaLevel;


                /// <summary>
                /// Atmospheric pressure on the ground level, hPa
                /// </summary>
                [JsonProperty("grnd_level")]
                public float GroundSeaLevel;
            }

            public class wind
            {
                /// <summary>
                ///  Wind speed. Unit Default: meter/sec, Metric: meter/sec, Imperial: miles/hour.
                /// </summary>
                [JsonProperty("speed")]
                public float Speed;


                /// <summary>
                ///  Wind direction, degrees (meteorological)
                /// </summary>
                [JsonProperty("deg")]
                public float Degrees;


                /// <summary>
                /// Wind gust. Unit Default: meter/sec, Metric: meter/sec, Imperial: miles/hour
                /// </summary>
                [JsonProperty("gust")]
                public float Gust;
            }

            public class clouds
            {
                /// <summary>
                /// Cloudiness, %
                /// </summary>
                [JsonProperty("all")]
                public float All;
            }

            public class fall
            {
                /// <summary>
                /// fall volume for the last 1 hour, mm
                /// </summary>
                [JsonProperty("1h")]
                public float h1;

                /// <summary>
                /// fall volume for the last 3 hours, mm
                /// </summary>
                [JsonProperty("3h")]
                public float h3;
            }

            /// <summary>
            /// coordinate in a number of spherical coordinate systems
            /// </summary>
            [JsonProperty("coord")]
            public Coord Coordinate;

            /// <summary>
            /// Weather state
            /// </summary>
            [JsonProperty("weather")]
            public Cond[] Condition;

            /// <summary>
            /// Measurements : Temperatur, pressure another
            /// </summary>
            [JsonProperty("main")]
            public Measurem Measurements;


            [JsonProperty("wind")]
            public wind Wind;

            [JsonProperty("clouds")]
            public clouds Clouds;

            [JsonProperty("rain")]
            public fall Rain;

            [JsonProperty("snow")]
            public fall Snow;

            /// <summary>
            /// Time of data calculation, unix, UTC
            /// </summary>
            [JsonProperty("dt")]
            public float Date;

            [JsonProperty("name")]
            public string CityName;
        }

        public enum WetherUnits
        {
            metric,
            imperial
        }
        /// <summary>
        /// You can call by city name or city name, state code and country code. Please note that searching by states available only for the USA locations.
        /// </summary>
        /// <param name="city">name of city</param>
        /// <param name="units"></param>
        /// <param name="lang">language. Detail https://openweathermap.org/current#multi </param>
        /// <returns></returns>
        public static Weather WeatherByCityName(string city, WetherUnits units = WetherUnits.metric, string lang = "en")
        {
            string responce = new HttpClient().GetAsync("https://api.openweathermap.org/data/2.5/weather?" +
                                             "q=" + city +
                                             "&appid=" + key +
                                             "&units=" + units.ToString() +
                                             "&lang=" + lang)
                .Result.Content.ReadAsStringAsync().Result;

            var weather = JsonConvert.DeserializeObject<Weather>(responce);

            return weather;
        }


        /// <summary>
        /// You can make an API call by city ID. List of city ID 'city.list.json.gz' can be downloaded here.
        ///We recommend to call API by city ID to get unambiguous result for your city.
        /// </summary>
        /// <param name="cityid">City ID. List of city ID 'city.list.json.gz' can be downloaded http://bulk.openweathermap.org/sample/ </param>
        /// <param name="units"></param>
        /// <param name="lang">language. Detail https://openweathermap.org/current#multi </param>
        /// <returns></returns>
        public static Weather WeatherByCityID(string cityid, WetherUnits units = WetherUnits.metric, string lang = "en")
        {
            string responce = new HttpClient().GetAsync("https://api.openweathermap.org/data/2.5/weather?" +
                                 "id=" + cityid +
                                 "&appid=" + key +
                                 "&units=" + units.ToString() +
                                 "&lang=" + lang)
    .Result.Content.ReadAsStringAsync().Result;

            var weather = JsonConvert.DeserializeObject<Weather>(responce);

            return weather;
        }

        /// <summary>
        /// Weather by geographic coordinates
        /// </summary>
        /// <param name="coord">Geographical coordinates (latitude, longitude)</param>
        /// <param name="units"></param>
        /// <param name="lang">language. Detail https://openweathermap.org/current#multi </param>
        /// <returns></returns>
        public static Weather WeatherByCoordinates(Weather.Coord coord, WetherUnits units = WetherUnits.metric, string lang = "en")
        {
            string responce = new HttpClient().GetAsync("https://api.openweathermap.org/data/2.5/weather?" +
                     "lat=" + coord.Latitude +
                     "&lon=" + coord.Longitude +
                     "&appid="+ key +
                     "&units=" + units.ToString() +
                     "&lang=" + lang)
    .Result.Content.ReadAsStringAsync().Result;

            var weather = JsonConvert.DeserializeObject<Weather>(responce);

            return weather;
        }

        /// <summary>
        /// Please note if country is not specified then the search works for USA as a default.
        /// </summary>
        /// <param name="zip">Zip code</param>
        /// <param name="countryCode"></param>
        /// <param name="units"></param>
        /// <param name="lang">language. Detail https://openweathermap.org/current#multi </param>
        /// <returns></returns>
        public static Weather WeatherByZipCode(string zip, string countryCode = "1", WetherUnits units = WetherUnits.metric, string lang = "en")
        {
            string responce = new HttpClient().GetAsync("https://api.openweathermap.org/data/2.5/weather?" +
                     "zip=" + zip +
                     "," + countryCode +
                     "&appid=" + key +
                     "&units=" + units.ToString() +
                     "&lang=" + lang)
    .Result.Content.ReadAsStringAsync().Result;

            var weather = JsonConvert.DeserializeObject<Weather>(responce);

            return weather;
        }
    }
}
