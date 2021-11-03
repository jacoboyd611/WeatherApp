using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp
{
    public class Day
    {
        public decimal currentTemp, currentTime, tempHigh, tempLow,
            code;

        public string location, date, icon, condition;

        public Day()
        {
            currentTemp = currentTime = tempHigh = tempLow =
                code = 0;
            location = date = icon = condition = "";
        }
    }
}
