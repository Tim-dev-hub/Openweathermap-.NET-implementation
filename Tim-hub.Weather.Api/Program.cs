using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tim_hub.Weather
{
    class Program
    {
        static void Main(string[] args)
        {
            //47.69192049478249, 35.57939870119467
            //API.key = "Your openweathermap.org api key";

            var obj = API.WeatherByCoordinates(new API.Weather.Coord(47.69192049478249f, 35.57939870119467f));
            
        }
    }
}
