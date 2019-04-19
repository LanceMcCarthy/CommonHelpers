using System;
using System.Drawing;
using CommonHelpers.Extensions;

// Modernized from the original version
// Original Credit - Rich Newman: https://richnewman.wordpress.com/about/code-listings-and-diagrams/hslcolor-class/
namespace CommonHelpers.Common
{
    // ReSharper disable once InconsistentNaming
    public class HSLColor
    {
        private double hue = 1.0;
        private double saturation = 1.0;
        private double luminosity = 1.0;
        private double scale = 240.0;

        public HSLColor() { }

        public HSLColor(Color color)
        {
            SetRGB(color.R, color.G, color.B);
        }

        public HSLColor(int red, int green, int blue)
        {
            SetRGB(red, green, blue);
        }

        public HSLColor(double hue, double saturation, double luminosity)
        {
            Hue = hue;
            Saturation = saturation;
            Luminosity = luminosity;
        }

        public double Hue
        {
            get => hue * scale;
            set => hue = CheckRange(value / scale);
        }

        public double Saturation
        {
            get => saturation * scale;
            set => saturation = CheckRange(value / scale);
        }

        public double Luminosity
        {
            get => luminosity * scale;
            set => luminosity = CheckRange(value / scale);
        }

        private static double CheckRange(double value)
        {
            if (value < 0.0)
            {
                value = 0.0;
            }
            else if (value > 1.0)
            {
                value = 1.0;
            }

            return value;
        }

        public override string ToString()
        {
            return $"H: {Hue:#0.##} S: {Saturation:#0.##} L: {Luminosity:#0.##}";
        }

        // ReSharper disable once InconsistentNaming
        public string ToRGBString()
        {
            return ColorExtensions.ToHexString(this);
        }

        // ReSharper disable once InconsistentNaming
        public void SetRGB(int red, int green, int blue)
        {
            var hslColor = (HSLColor)Color.FromArgb(red, green, blue);

            hue = hslColor.hue;
            saturation = hslColor.saturation;
            luminosity = hslColor.luminosity;
        }

        #region Casts to/from System.Drawing.Color

        public static implicit operator Color(HSLColor hslColor)
        {
            double r = 0, g = 0, b = 0;

            if (Math.Abs(hslColor.luminosity) > 0.01)
            {
                if (Math.Abs(hslColor.saturation) < 0.01)
                {
                    r = g = b = hslColor.luminosity;
                }
                else
                {
                    double temp2 = GetTemp2(hslColor);
                    double temp1 = 2.0 * hslColor.luminosity - temp2;

                    r = GetColorComponent(temp1, temp2, hslColor.hue + 1.0 / 3.0);
                    g = GetColorComponent(temp1, temp2, hslColor.hue);
                    b = GetColorComponent(temp1, temp2, hslColor.hue - 1.0 / 3.0);
                }
            }

            return Color.FromArgb((int)(255 * r), (int)(255 * g), (int)(255 * b));
        }

        private static double GetColorComponent(double temp1, double temp2, double temp3)
        {
            temp3 = MoveIntoRange(temp3);

            if (temp3 < 1.0 / 6.0)
            {
                return temp1 + (temp2 - temp1) * 6.0 * temp3;
            }

            if (temp3 < 0.5)
            {
                return temp2;
            }

            if (temp3 < 2.0 / 3.0)
            {
                return temp1 + (temp2 - temp1) * (2.0 / 3.0 - temp3) * 6.0;
            }

            return temp1;
        }

        private static double MoveIntoRange(double temp3)
        {
            if (temp3 < 0.0)
            {
                temp3 += 1.0;
            }
            else if (temp3 > 1.0)
            {
                temp3 -= 1.0;
            }

            return temp3;
        }

        private static double GetTemp2(HSLColor hslColor)
        {
            double temp2;

            if (hslColor.luminosity < 0.5)
            {
                temp2 = hslColor.luminosity * (1.0 + hslColor.saturation);
            }
            else
            {
                temp2 = hslColor.luminosity + hslColor.saturation - hslColor.luminosity * hslColor.saturation;
            }

            return temp2;
        }

        public static implicit operator HSLColor(Color color)
        {
            return new HSLColor
            {
                hue = color.GetHue() / 360.0,
                luminosity = color.GetBrightness(),
                saturation = color.GetSaturation()
            };
        }

        #endregion
    }
}
