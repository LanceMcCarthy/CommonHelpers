using System;
using CommonHelpers.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkiaSharp;
using ZXing;

namespace CommonHelpers.Tests.Services
{
    [TestClass]
    public class BarcodeGeneratorServiceTests
    {
        private readonly BarcodeGeneratorService service = new();

        [TestMethod]
        public void GenerateQrCodeReturnsDecodablePng()
        {
            const string value = "https://github.com/LanceMcCarthy/CommonHelpers";

            var png = service.GenerateQrCode(value);
            var result = DecodePng(png);

            CollectionAssert.AreEqual(new byte[] { 137, 80, 78, 71, 13, 10, 26, 10 }, png[..8]);
            Assert.AreEqual(512, result.Width);
            Assert.AreEqual(512, result.Height);
            Assert.AreEqual(BarcodeFormat.QR_CODE, result.BarcodeResult.BarcodeFormat);
            Assert.AreEqual(value, result.BarcodeResult.Text);
        }

        [TestMethod]
        public void GenerateBarcodeWithUpcAReturnsDecodablePng()
        {
            const string valueWithoutCheckDigit = "03600029145";

            var png = service.GenerateBarcode(BarcodeType.UpcA, valueWithoutCheckDigit);
            var result = DecodePng(png);

            Assert.AreEqual(600, result.Width);
            Assert.AreEqual(300, result.Height);
            Assert.AreEqual(BarcodeFormat.UPC_A, result.BarcodeResult.BarcodeFormat);
            Assert.AreEqual("036000291452", result.BarcodeResult.Text);
        }

        [TestMethod]
        [DataRow(BarcodeType.Code128, "CommonHelpers-128", BarcodeFormat.CODE_128, "CommonHelpers-128", 600, 300)]
        [DataRow(BarcodeType.Ean13, "590123412345", BarcodeFormat.EAN_13, "5901234123457", 600, 300)]
        [DataRow(BarcodeType.Ean8, "9638507", BarcodeFormat.EAN_8, "96385074", 600, 300)]
        [DataRow(BarcodeType.DataMatrix, "CommonHelpers Data Matrix", BarcodeFormat.DATA_MATRIX, "CommonHelpers Data Matrix", 512, 512)]
        [DataRow(BarcodeType.Pdf417, "CommonHelpers PDF417", BarcodeFormat.PDF_417, "CommonHelpers PDF417", 800, 400)]
        [DataRow(BarcodeType.Aztec, "CommonHelpers Aztec", BarcodeFormat.AZTEC, "CommonHelpers Aztec", 512, 512)]
        [DataRow(BarcodeType.Code39, "ABC-123", BarcodeFormat.CODE_39, "ABC-123", 600, 300)]
        [DataRow(BarcodeType.Itf14, "1234567890123", BarcodeFormat.ITF, "12345678901231", 600, 300)]
        public void GenerateBarcodeReturnsDecodablePng(
            BarcodeType barcodeType,
            string value,
            BarcodeFormat expectedFormat,
            string expectedValue,
            int expectedWidth,
            int expectedHeight)
        {
            var png = service.GenerateBarcode(barcodeType, value);
            var result = DecodePng(png);

            Assert.AreEqual(expectedWidth, result.Width);
            Assert.AreEqual(expectedHeight, result.Height);
            Assert.AreEqual(expectedFormat, result.BarcodeResult.BarcodeFormat);
            Assert.AreEqual(expectedValue, result.BarcodeResult.Text);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow("123")]
        [DataRow("0360002914A")]
        [DataRow("036000291453")]
        public void GenerateUpcARejectsInvalidValues(string value)
        {
            Assert.ThrowsExactly<ArgumentException>(() => service.GenerateUpcA(value));
        }

        [TestMethod]
        [DataRow(BarcodeType.Ean13, "5901234123458")]
        [DataRow(BarcodeType.Ean13, "59012341234A")]
        [DataRow(BarcodeType.Ean8, "96385075")]
        [DataRow(BarcodeType.Itf14, "12345678901232")]
        [DataRow(BarcodeType.Itf14, "123456789012")]
        [DataRow(BarcodeType.Code39, "lowercase")]
        [DataRow(BarcodeType.Code128, "Café")]
        public void GenerateBarcodeRejectsInvalidFormatValues(BarcodeType barcodeType, string value)
        {
            Assert.ThrowsExactly<ArgumentException>(() => service.GenerateBarcode(barcodeType, value));
        }

        [TestMethod]
        public void GenerateBarcodeRejectsUnsupportedType()
        {
            Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => service.GenerateBarcode((BarcodeType)int.MaxValue, "value"));
        }

        [TestMethod]
        public void GenerateQrCodeRejectsInvalidSize()
        {
            Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => service.GenerateQrCode("value", 0));
        }

        private static DecodedBarcode DecodePng(byte[] png)
        {
            using var bitmap = SKBitmap.Decode(png);
            Assert.IsNotNull(bitmap);

            var pixels = bitmap.Pixels;
            var rgb = new byte[pixels.Length * 3];

            for (var i = 0; i < pixels.Length; i++)
            {
                rgb[i * 3] = pixels[i].Red;
                rgb[i * 3 + 1] = pixels[i].Green;
                rgb[i * 3 + 2] = pixels[i].Blue;
            }

            var luminanceSource = new RGBLuminanceSource(rgb, bitmap.Width, bitmap.Height, RGBLuminanceSource.BitmapFormat.RGB24);
            var barcodeResult = new BarcodeReaderGeneric().Decode(luminanceSource);

            Assert.IsNotNull(barcodeResult);
            return new DecodedBarcode(bitmap.Width, bitmap.Height, barcodeResult);
        }

        private sealed record DecodedBarcode(int Width, int Height, Result BarcodeResult);
    }
}
