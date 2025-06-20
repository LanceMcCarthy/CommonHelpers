using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonHelpers.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonHelpers.Tests.Extensions
{
    [TestClass]
    public class ColorExtensionsTests
    {
        [TestMethod]
        public void ConvertToHex()
        {
            // Arrange
            Color white = Color.FromArgb(255, 255, 255, 255);
            string expectedValue = "#FFFFFFFF";

            // Act
            var colorString = white.ToHexString();

            // Assert
            Assert.AreEqual(expectedValue, colorString);
        }

        [TestMethod]
        public void ConvertFromHex()
        {
            // Arrange
            string colorString = "#FFFFFFFF";
            Color expectedColor = Color.FromArgb(255, 255, 255, 255);

            // Act
            var colorResult = ColorExtensions.ConvertHexStringToColor(colorString);

            // Assert
            Assert.AreEqual(expectedColor, colorResult);
        }

        [TestMethod]
        public void ConvertToHexAndBack_RoundTrip()
        {
            // Arrange
            Color original = Color.FromArgb(123, 45, 67, 89);

            // Act
            var hex = original.ToHexString();
            var result = ColorExtensions.ConvertHexStringToColor(hex);

            // Assert
            Assert.AreEqual(original, result);
        }

        [TestMethod]
        public void ConvertFromHex_Invalid_Throws()
        {
            // Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => ColorExtensions.ConvertHexStringToColor("#FFF"));
            Assert.ThrowsException<FormatException>(() => ColorExtensions.ConvertHexStringToColor("#GGGGGGGG"));
        }

        [TestMethod]
        public void GenerateHslColors_DefaultCount()
        {
            // Arrange
            var baseColor = Color.CadetBlue;

            // Act
            var hslColors = baseColor.GenerateHSLGradient();

            // Assert
            Assert.IsNotNull(hslColors);
            Assert.AreEqual(12, hslColors.Count);
            Assert.IsTrue(hslColors.Distinct().Count() > 1);
        }

        [TestMethod]
        public void GenerateHslColors_CustomCount()
        {
            // Arrange
            var baseColor = Color.CadetBlue;
            int count = 5;

            // Act
            var hslColors = baseColor.GenerateHSLGradient(count);

            // Assert
            Assert.AreEqual(count, hslColors.Count);
        }

        [TestMethod]
        public void GenerateHslColors_ColorsAreDifferent()
        {
            // Arrange
            var baseColor = Color.CadetBlue;

            // Act
            var hslColors = baseColor.GenerateHSLGradient(6);

            // Assert
            // At least two colors should be different
            Assert.IsTrue(hslColors.Zip(hslColors.Skip(1), (a, b) => a != b).Any(x => x));
        }

        [TestMethod]
        public void ConvertRgbToHsv()
        {
            // Arrange
            double r = 200;
            double g = 154;
            double b = 154;
            double h;
            double s;
            double v;

            // Act
            var hsvColor = ColorExtensions.ConvertRgbToHsv(r, g, b);
            h = hsvColor.Item1;
            s = hsvColor.Item2;
            v = hsvColor.Item3;

            // Assert
            Assert.IsInstanceOfType(hsvColor, typeof(Tuple<double, double, double>));
            Assert.AreNotEqual(r, h);
            Assert.AreNotEqual(g, s);
            Assert.AreNotEqual(v, b);
        }

        [TestMethod]
        public void ConvertRgbToHsv_KnownValue()
        {
            // Act
            var hsv = ColorExtensions.ConvertRgbToHsv(255, 0, 0); // Red

            // Assert
            Assert.AreEqual(0, hsv.Item1, 1); // Hue
            Assert.AreEqual(1, hsv.Item2, 0.01); // Saturation
            Assert.AreEqual(1, hsv.Item3, 0.01); // Value
        }

        [TestMethod]
        public void ConvertRgbToHsv_Gray()
        {
            // Act
            var hsv = ColorExtensions.ConvertRgbToHsv(128, 128, 128);

            // Assert
            Assert.AreEqual(0, hsv.Item2, 0.01); // Saturation should be 0 for gray
        }

        [TestMethod]
        public void GenerateContrastColor()
        {
            // Arrange
            var expectedContrastColor = Color.White;
            var darkColor = Color.SaddleBrown;

            // Act
            var contrastColor = darkColor.GetContrastColor();

            // Assert
            Assert.AreEqual(expectedContrastColor, contrastColor);
        }

        [TestMethod]
        public void GenerateContrastColor_Dark()
        {
            // Arrange
            var expectedContrastColor = Color.White;
            var darkColor = Color.SaddleBrown;

            // Act
            var contrastColor = darkColor.GetContrastColor();

            // Assert
            Assert.AreEqual(expectedContrastColor, contrastColor);
        }

        [TestMethod]
        public void GenerateContrastColor_Light()
        {
            // Arrange
            var expectedContrastColor = Color.Black;
            var lightColor = Color.WhiteSmoke;

            // Act
            var contrastColor = lightColor.GetContrastColor();

            // Assert
            Assert.AreEqual(expectedContrastColor, contrastColor);
        }
    }
}
