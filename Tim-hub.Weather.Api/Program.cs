using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tim_hub.Weather.Api
{
    class Program
    {
        static void Main(string[] args)
        {

            
            API.key = "474d1400037bd7fae6326cf2389258bc";
            API.DownloadIcon("11n").Save("D:/some.png");
            /*
            //47.68805536281123, 35.576733955604986
            var weather = API.WeatherByCoordinates(new API.Weather.Coord(47.68805536281123f, 35.576733955604986f));*/
        }
    }
}
