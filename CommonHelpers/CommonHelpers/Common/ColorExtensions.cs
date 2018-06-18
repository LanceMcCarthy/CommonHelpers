using System;
using System.Drawing;
using System.Globalization;
using System.Linq;

namespace CommonHelpers.Common
{
    public static class ColorExtensions
    {
        public static Color ConvertHexToColor(string hex)
        {
            hex = hex.Remove(0, 1);

            return Color.FromArgb(
                hex.Length == 8 ? byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber) : 255, 
                byte.Parse(hex.Substring(hex.Length - 6, 2), NumberStyles.HexNumber), 
                byte.Parse(hex.Substring(hex.Length - 4, 2), NumberStyles.HexNumber), 
                byte.Parse(hex.Substring(hex.Length - 2), NumberStyles.HexNumber));
        }

        public static string ConvertColorToHex(Color color)
        {
            return $"#{color.A}{color.R}{color.G}{color.B}"; 
        }

        public static Tuple<double, double, double> RgbToHsv(double r, double g, double b)
        {
            var hsv = new double[3]; 

            r = r / 255.0;
            g = g / 255.0;
            b = b / 255.0;

            var max = new[] { r, g, b }.Max();
            var min = new[] { r, g, b }.Min(); 
            var delta = max - min;

            hsv[1] = Math.Abs(max) > .1 ? delta / max : 0;
            hsv[2] = max;

            if (Math.Abs(hsv[1]) < .1) 
            {
                return new Tuple<double, double, double>(hsv[0], hsv[1], hsv[2]);
            }

            if (Math.Abs(r - max) < .1)
            {
                hsv[0] = ((g - b) / delta);
            }
            else if (Math.Abs(g - max) < .1)
            {
                hsv[0] = ((b - r) / delta) + 2.0;
            }
            else if (Math.Abs(b - max) < .1)
            {
                hsv[0] = ((r - g) / delta) + 4.0;
            }

            hsv[0] *= 60.0;

            if (hsv[0] < 0)
            {
                hsv[0] += 360.0;
            }

            return new Tuple<double, double, double>(hsv[0], hsv[1], hsv[2]);
        }
        
        public static Color GetContrastingForegroundColor(this Color c) =>
            c.R * 0.3 + c.G * 0.59 + c.B * 0.11 > 127 ? Color.Black : Color.White;
    }
}