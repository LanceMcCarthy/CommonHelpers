using System;
using System.Collections.Generic;
using System.Drawing;
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
            Color expectedColor = Color.FromArgb(255,255,255,255);

            // Act
            var colorResult = ColorExtensions.ConvertHexStringToColor(colorString);

            // Assert
            Assert.AreEqual(expectedColor, colorResult);
        }

        [TestMethod]
        public void GenerateHslColors()
        {
            // Arrange
            var baseColor = Color.CadetBlue;
            List<Color> hslColors;

            // Act
            hslColors = baseColor.GenerateHSLGradient();

            // Assert
            Assert.IsNotNull(hslColors);
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
    }
}
