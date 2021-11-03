/*Jacob Boyd
 * Mr T, Nov 3, 2021
 * Basic weather app that uses open weather api to pull the 
 * current and forecast tempuratures as well as weather conditions and maxs and mins
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace WeatherApp
{
    public partial class Form1 : Form
    {
        //forecast list
        public static List<Day> days = new List<Day>();
        //location
        public static string city = "Stratford";

        public Form1()
        {
            ExtractForecast();
            ExtractCurrent();
            InitializeComponent();

            MainScreen ms = new MainScreen();
            this.Controls.Add(ms);
        }

        //Extract the forecast for the week
        public static bool ExtractForecast()
        {
            try
            {
                XmlReader reader = XmlReader.Create("http://api.openweathermap.org/data/2.5/forecast/daily?q=" + city + ",CA&mode=xml&units=metric&cnt=7&appid=3f2e224b815c0ed45524322e145149f0");

                while (reader.Read())
                {
                    Day day = new Day();

                    reader.ReadToFollowing("time");
                    day.date = reader.GetAttribute("day");
                    reader.ReadToFollowing("symbol");
                    day.icon = reader.GetAttribute("var");
                    reader.ReadToFollowing("temperature");
                    day.tempLow = Round(reader.GetAttribute("min"));
                    day.tempHigh = Round(reader.GetAttribute("max"));
                    if (day.date != null) { days.Add(day); }
                }
                return true;
            }
            catch { }
            return false;
        }
        //Extract today's information
        public static void ExtractCurrent()
        {
            XmlReader reader = XmlReader.Create("http://api.openweathermap.org/data/2.5/weather?q=" + city + ",CA&mode=xml&units=metric&appid=3f2e224b815c0ed45524322e145149f0");

            reader.ReadToFollowing("city");
            days[0].location = reader.GetAttribute("name");
            reader.ReadToFollowing("temperature");
            days[0].currentTemp = Round(reader.GetAttribute("value"));
            reader.ReadToFollowing("weather");
            days[0].code = Convert.ToDecimal(reader.GetAttribute("number"));
            days[0].condition = reader.GetAttribute("value").ToUpper();
            days[0].icon = reader.GetAttribute("icon");
        }
        //method that rounds the numbers pulled from the xml
        public static decimal Round(string number)
        {
            decimal d = decimal.Round(Convert.ToDecimal(number), 0);
            return d;
        }
    }
}
