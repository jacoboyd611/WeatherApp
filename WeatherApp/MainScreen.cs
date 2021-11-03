using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeatherApp
{
    public partial class MainScreen : UserControl
    {
        PictureBox[] icons;
        Pen blackpen = new Pen(Color.Black);
        Brush greyBrush = new SolidBrush(Color.DimGray);

        public MainScreen()
        {
            InitializeComponent();
            //list of the picture boxes for displaying the icons
            icons = new PictureBox[] {day1Box, day2Box, day3Box, day4Box, day5Box, day6Box, day7Box};

            Background();
            Setup();
            Refresh();
        }
        //display all relevent information; current data, forecast data, icons, location label
        private void Setup()
        {
            currentOutput.Text = $"{Form1.days[0].currentTemp}°C";
            currentPictureBox.Load("http://openweathermap.org/img/wn/" + Form1.days[0].icon + "@2x.png");
            locationOutput.Text = Form1.days[0].location;
            conditionOutput.Text = Form1.days[0].condition;

            Clear();

            for(int i = 0; i<Form1.days.Count; i++)
            {
                maxOutput.Text += $"{Form1.days[i].tempHigh}°\n";
                minOutput.Text += $"{Form1.days[i].tempLow}°\n";
                icons[i].Load("http://openweathermap.org/img/wn/" + Form1.days[i].icon + "@2x.png");
                if (i == 0) { daysOutput.Text += $"Today\n"; }
                else { daysOutput.Text += Convert.ToDateTime(Form1.days[i].date).ToString("dddd") + "\n"; }
            }
        }
        //change the background based on weather 
        private void Background()
        {
            decimal code = Form1.days[0].code;
            if (200<=code && code < 300) { this.BackgroundImage = Properties.Resources.thunderstorm; }
            else if (300 <= code && code < 600) { this.BackgroundImage = Properties.Resources.rain; }
            else if (600 <= code && code < 700) { this.BackgroundImage = Properties.Resources.snow; }
            else if (800 == code) { this.BackgroundImage = Properties.Resources.clearSky; }
            else if (800 < code) { this.BackgroundImage = Properties.Resources.cloudySky; }
        }
        //clear labels
        private void Clear()
        {
            maxOutput.Text = "";
            minOutput.Text = "";
            daysOutput.Text = "";
        }
        //paint some graphics
        private void MainScreen_Paint(object sender, PaintEventArgs e)
        {
            //Allows me to use the designer to draw the background by using the label's coordinates 
            background.Visible = false;
            e.Graphics.FillRectangle(greyBrush, background.Location.X, background.Location.Y,
                background.Size.Width, background.Size.Height);
            e.Graphics.DrawRectangle(blackpen, background.Location.X, background.Location.Y,
                background.Size.Width, background.Size.Height);

            e.Graphics.DrawRectangle(blackpen, forecastLabel.Location.X, forecastLabel.Location.Y,
                forecastLabel.Size.Width, forecastLabel.Size.Height);
            e.Graphics.DrawRectangle(blackpen, firstTopLabel.Location.X, firstTopLabel.Location.Y,
                firstTopLabel.Size.Width, firstTopLabel.Size.Height);
            e.Graphics.DrawRectangle(blackpen, secondTopLabel.Location.X, secondTopLabel.Location.Y,
                secondTopLabel.Size.Width, secondTopLabel.Size.Height);
        }
        //change location and display the new information
        private void button1_Click(object sender, EventArgs e)
        {
            string lastInput = Form1.city;
            Form1.city = textBox1.Text;
            Form1.days.Clear();

            while (!Form1.ExtractForecast()) 
            { 
                Form1.city = lastInput;
                textBox1.Text = "Error";
            }
            Form1.ExtractCurrent();

            Background();
            Setup();
            Refresh();
        }
    }
}
