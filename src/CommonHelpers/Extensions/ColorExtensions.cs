using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using CommonHelpers.Common;

namespace CommonHelpers.Extensions
{
    public static class ColorExtensions
    {
        /// <summary>
        /// Converts Hex string value to a Color object
        /// </summary>
        /// <param name="hex">Hex, supports alpha channel.</param>
        /// <returns></returns>
        public static Color ConvertHexStringToColor(string hex)
        {
            hex = hex.Remove(0, 1);

            return Color.FromArgb(
                hex.Length == 8 ? byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber) : 255, 
                byte.Parse(hex.Substring(hex.Length - 6, 2), NumberStyles.HexNumber), 
                byte.Parse(hex.Substring(hex.Length - 4, 2), NumberStyles.HexNumber), 
                byte.Parse(hex.Substring(hex.Length - 2), NumberStyles.HexNumber));
        }

        /// <summary>
        /// Converts Color object to HEX string equivalent
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string ToHexString(this Color color)
        {
            return $"#{color.A:X}{color.R:X}{color.G:X}{color.B:X}"; 
        }

        /// <summary>
        /// Converts an RGB values to HSV values
        /// </summary>
        /// <param name="r">Red value</param>
        /// <param name="g">Green value</param>
        /// <param name="b">Blue value</param>
        /// <returns>An HSV value equivalent of the input RGB</returns>
        public static Tuple<double, double, double> ConvertRgbToHsv(double r, double g, double b)
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

        /// <summary>
        /// Generates a list of colors along the HSL gradient for the base color.
        /// This is good for controls that need contrast between colors, for example a Pie Chart
        /// </summary>
        /// <param name="baseColor">Starting base color</param>
        /// <param name="numberOfColors">List of colors</param>
        /// <returns>List of colors</returns>
        // ReSharper disable once InconsistentNaming
        public static List<Color> GenerateHSLGradient(this Color baseColor, int numberOfColors = 12)
        {
            var baseHue = new HSLColor(baseColor).Hue;

            var colors = new List<Color> { baseColor };

            var step = 240.0 / (double)numberOfColors;

            for (int i = 1; i < numberOfColors; ++i)
            {
                var nextColor = new HSLColor(baseColor)
                {
                    Hue = (baseHue + step * i) % 240.0
                };

                colors.Add((System.Drawing.Color)nextColor);
            }

            return colors;
        }

        /// <summary>
        /// Returns contrasting color. If contrast is not strong enough, White or Black will be returned instead.
        /// </summary>
        /// <param name="c">Base Color</param>
        /// <returns>Contrast color</returns>
        public static Color GetContrastColor(this Color c)
        {
            return c.R * 0.3 + c.G * 0.59 + c.B * 0.11 > 127 ? Color.Black : Color.White;
        }
    }
}