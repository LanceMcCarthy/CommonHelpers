using CommonHelpers.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;
using System.Linq;

namespace CommonHelpers.Tests.Extensions;

[TestClass]
public class ColorExtensionsTests
{
    [TestMethod]
    public void ConvertToHex()
    {
        var white = Color.FromArgb(255, 255, 255, 255);
        const string expectedValue = "#FFFFFFFF";
        var colorString = white.ToHexString();
        Assert.AreEqual(expectedValue, colorString);
    }

    [TestMethod]
    public void ConvertFromHex()
    {
        const string colorString = "#FFFFFFFF";
        var expectedColor = Color.FromArgb(255, 255, 255, 255);
        var colorResult = ColorExtensions.ConvertHexStringToColor(colorString);
        Assert.AreEqual(expectedColor, colorResult);
    }

    [TestMethod]
    public void ConvertToHexAndBack_RoundTrip()
    {
        var original = Color.FromArgb(123, 45, 67, 89);
        var hex = original.ToHexString();
        var result = ColorExtensions.ConvertHexStringToColor(hex);
        Assert.AreEqual(original, result);
    }

    [TestMethod]
    public void ConvertFromHex_Invalid_Throws()
    {
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => ColorExtensions.ConvertHexStringToColor("#FFF"));
        Assert.ThrowsExactly<FormatException>(() => ColorExtensions.ConvertHexStringToColor("#GGGGGGGG"));
    }

    [TestMethod]
    public void GenerateHslColors_DefaultCount()
    {
        var baseColor = Color.CadetBlue;
        var hslColors = baseColor.GenerateHSLGradient();
        Assert.IsNotNull(hslColors);
        Assert.AreEqual(12, hslColors.Count);
        Assert.IsTrue(hslColors.Distinct().Count() > 1);
    }

    [TestMethod]
    public void GenerateHslColors_CustomCount()
    {
        var baseColor = Color.CadetBlue;
        const int count = 5;
        var hslColors = baseColor.GenerateHSLGradient(count);
        Assert.AreEqual(count, hslColors.Count);
    }

    [TestMethod]
    public void GenerateHslColors_ColorsAreDifferent()
    {
        var baseColor = Color.CadetBlue;
        var hslColors = baseColor.GenerateHSLGradient(6);
        Assert.IsTrue(hslColors.Zip(hslColors.Skip(1), (a, b) => a != b).Any(x => x));
    }

    [TestMethod]
    public void ConvertRgbToHsv()
    {
        const double r = 200;
        const double g = 154;
        const double b = 154;
        var hsvColor = ColorExtensions.ConvertRgbToHsv(r, g, b);
        var h = hsvColor.Item1;
        var s = hsvColor.Item2;
        var v = hsvColor.Item3;
        Assert.IsInstanceOfType(hsvColor, typeof(Tuple<double, double, double>));
        Assert.AreNotEqual(r, h);
        Assert.AreNotEqual(g, s);
        Assert.AreNotEqual(v, b);
    }

    [TestMethod]
    public void ConvertRgbToHsv_KnownValue()
    {
        var hsv = ColorExtensions.ConvertRgbToHsv(255, 0, 0); // Red
        Assert.AreEqual(0, hsv.Item1, 1); // Hue
        Assert.AreEqual(1, hsv.Item2, 0.01); // Saturation
        Assert.AreEqual(1, hsv.Item3, 0.01); // Value
    }

    [TestMethod]
    public void ConvertRgbToHsv_Gray()
    {
        var hsv = ColorExtensions.ConvertRgbToHsv(128, 128, 128);
        Assert.AreEqual(0, hsv.Item2, 0.01); // Saturation should be 0 for gray
    }

    [TestMethod]
    public void GenerateContrastColor()
    {
        var expectedContrastColor = Color.White;
        var darkColor = Color.SaddleBrown;
        var contrastColor = darkColor.GetContrastColor();
        Assert.AreEqual(expectedContrastColor, contrastColor);
    }

    [TestMethod]
    public void GenerateContrastColor_Dark()
    {
        var expectedContrastColor = Color.White;
        var darkColor = Color.SaddleBrown;
        var contrastColor = darkColor.GetContrastColor();
        Assert.AreEqual(expectedContrastColor, contrastColor);
    }

    [TestMethod]
    public void GenerateContrastColor_Light()
    {
        var expectedContrastColor = Color.Black;
        var lightColor = Color.WhiteSmoke;
        var contrastColor = lightColor.GetContrastColor();
        Assert.AreEqual(expectedContrastColor, contrastColor);
    }
}