using System;
using System.Collections.Generic;
using SkiaSharp;
using ZXing;
using ZXing.Common;

namespace CommonHelpers.Services;

public enum BarcodeType
{
    QrCode,
    UpcA,
    Code128,
    Ean13,
    Ean8,
    DataMatrix,
    Pdf417,
    Aztec,
    Code39,
    Itf14
}

public class BarcodeGeneratorService
{
    private const int DefaultQrCodeSize = 512;
    private const int DefaultTwoDimensionalSize = 512;
    private const int DefaultLinearWidth = 600;
    private const int DefaultLinearHeight = 300;
    private const int DefaultPdf417Width = 800;
    private const int DefaultPdf417Height = 400;

    public byte[] GenerateBarcode(BarcodeType barcodeType, string value, int? width = null, int? height = null)
    {
        switch (barcodeType)
        {
            case BarcodeType.QrCode:
                return GenerateQrCode(value, GetDimension(width, DefaultQrCodeSize, nameof(width)), GetDimension(height, DefaultQrCodeSize, nameof(height)));
            case BarcodeType.UpcA:
                return GenerateUpcA(value, GetDimension(width, DefaultLinearWidth, nameof(width)), GetDimension(height, DefaultLinearHeight, nameof(height)));
            case BarcodeType.Code128:
                return GenerateCode128(value, GetDimension(width, DefaultLinearWidth, nameof(width)), GetDimension(height, DefaultLinearHeight, nameof(height)));
            case BarcodeType.Ean13:
                return GenerateEan13(value, GetDimension(width, DefaultLinearWidth, nameof(width)), GetDimension(height, DefaultLinearHeight, nameof(height)));
            case BarcodeType.Ean8:
                return GenerateEan8(value, GetDimension(width, DefaultLinearWidth, nameof(width)), GetDimension(height, DefaultLinearHeight, nameof(height)));
            case BarcodeType.DataMatrix:
                return GenerateDataMatrix(value, GetDimension(width, DefaultTwoDimensionalSize, nameof(width)), GetDimension(height, DefaultTwoDimensionalSize, nameof(height)));
            case BarcodeType.Pdf417:
                return GeneratePdf417(value, GetDimension(width, DefaultPdf417Width, nameof(width)), GetDimension(height, DefaultPdf417Height, nameof(height)));
            case BarcodeType.Aztec:
                return GenerateAztec(value, GetDimension(width, DefaultTwoDimensionalSize, nameof(width)), GetDimension(height, DefaultTwoDimensionalSize, nameof(height)));
            case BarcodeType.Code39:
                return GenerateCode39(value, GetDimension(width, DefaultLinearWidth, nameof(width)), GetDimension(height, DefaultLinearHeight, nameof(height)));
            case BarcodeType.Itf14:
                return GenerateItf14(value, GetDimension(width, DefaultLinearWidth, nameof(width)), GetDimension(height, DefaultLinearHeight, nameof(height)));
            default:
                throw new ArgumentOutOfRangeException(nameof(barcodeType), barcodeType, "Unsupported barcode type.");
        }
    }

    public byte[] GenerateQrCode(string value, int size = DefaultQrCodeSize)
    {
        return GenerateQrCode(value, size, size);
    }

    public byte[] GenerateUpcA(string value, int width = DefaultLinearWidth, int height = DefaultLinearHeight)
    {
        ValidateValue(value);
        ValidateGtinValue(value, 11, "UPC-A");

        return GeneratePng(
            BarcodeFormat.UPC_A,
            value,
            GetDimension(width, DefaultLinearWidth, nameof(width)),
            GetDimension(height, DefaultLinearHeight, nameof(height)),
            10);
    }

    public byte[] GenerateCode128(string value, int width = DefaultLinearWidth, int height = DefaultLinearHeight)
    {
        ValidateCode128Value(value);
        return GenerateLinearBarcode(BarcodeFormat.CODE_128, value, width, height);
    }

    public byte[] GenerateEan13(string value, int width = DefaultLinearWidth, int height = DefaultLinearHeight)
    {
        ValidateValue(value);
        ValidateGtinValue(value, 12, "EAN-13");
        return GenerateLinearBarcode(BarcodeFormat.EAN_13, value, width, height);
    }

    public byte[] GenerateEan8(string value, int width = DefaultLinearWidth, int height = DefaultLinearHeight)
    {
        ValidateValue(value);
        ValidateGtinValue(value, 7, "EAN-8");
        return GenerateLinearBarcode(BarcodeFormat.EAN_8, value, width, height);
    }

    public byte[] GenerateDataMatrix(string value, int width = DefaultTwoDimensionalSize, int height = DefaultTwoDimensionalSize)
    {
        return GenerateTwoDimensionalBarcode(BarcodeFormat.DATA_MATRIX, value, width, height, 1);
    }

    public byte[] GeneratePdf417(string value, int width = DefaultPdf417Width, int height = DefaultPdf417Height)
    {
        return GenerateTwoDimensionalBarcode(BarcodeFormat.PDF_417, value, width, height, 2);
    }

    public byte[] GenerateAztec(string value, int width = DefaultTwoDimensionalSize, int height = DefaultTwoDimensionalSize)
    {
        return GenerateTwoDimensionalBarcode(BarcodeFormat.AZTEC, value, width, height, 2);
    }

    public byte[] GenerateCode39(string value, int width = DefaultLinearWidth, int height = DefaultLinearHeight)
    {
        ValidateCode39Value(value);
        return GenerateLinearBarcode(BarcodeFormat.CODE_39, value, width, height);
    }

    public byte[] GenerateItf14(string value, int width = DefaultLinearWidth, int height = DefaultLinearHeight)
    {
        ValidateValue(value);
        ValidateGtinValue(value, 13, "ITF-14");

        var normalizedValue = value.Length == 13
            ? value + CalculateGtinCheckDigit(value, 13)
            : value;

        return GenerateLinearBarcode(BarcodeFormat.ITF, normalizedValue, width, height);
    }

    private static byte[] GenerateLinearBarcode(BarcodeFormat format, string value, int width, int height)
    {
        ValidateValue(value);
        return GeneratePng(format, value, GetDimension(width, DefaultLinearWidth, nameof(width)), GetDimension(height, DefaultLinearHeight, nameof(height)), 10);
    }

    private static byte[] GenerateTwoDimensionalBarcode(BarcodeFormat format, string value, int width, int height, int margin)
    {
        ValidateValue(value);
        return GeneratePng(format, value, GetDimension(width, DefaultTwoDimensionalSize, nameof(width)), GetDimension(height, DefaultTwoDimensionalSize, nameof(height)), margin);
    }

    private static byte[] GenerateQrCode(string value, int width, int height)
    {
        ValidateValue(value);

        return GeneratePng(
            BarcodeFormat.QR_CODE,
            value,
            GetDimension(width, DefaultQrCodeSize, nameof(width)),
            GetDimension(height, DefaultQrCodeSize, nameof(height)),
            4);
    }

    private static byte[] GeneratePng(BarcodeFormat format, string value, int width, int height, int margin)
    {
        var hints = new Dictionary<EncodeHintType, object>
        {
            [EncodeHintType.MARGIN] = margin
        };

        if (format == BarcodeFormat.QR_CODE ||
            format == BarcodeFormat.DATA_MATRIX ||
            format == BarcodeFormat.PDF_417 ||
            format == BarcodeFormat.AZTEC)
        {
            hints[EncodeHintType.CHARACTER_SET] = "UTF-8";
        }

        var matrix = new MultiFormatWriter().encode(value, format, width, height, hints);
        var bitmapWidth = Math.Max(width, matrix.Width);
        var bitmapHeight = Math.Max(height, matrix.Height);
        var offsetX = (bitmapWidth - matrix.Width) / 2;
        var offsetY = (bitmapHeight - matrix.Height) / 2;

        using (var bitmap = new SKBitmap(bitmapWidth, bitmapHeight, SKColorType.Bgra8888, SKAlphaType.Opaque))
        using (var canvas = new SKCanvas(bitmap))
        using (var paint = new SKPaint { Color = SKColors.Black, Style = SKPaintStyle.Fill, IsAntialias = false })
        {
            canvas.Clear(SKColors.White);

            for (var y = 0; y < matrix.Height; y++)
            {
                var x = 0;

                while (x < matrix.Width)
                {
                    if (!matrix[x, y])
                    {
                        x++;
                        continue;
                    }

                    var start = x;

                    while (x < matrix.Width && matrix[x, y])
                    {
                        x++;
                    }

                    canvas.DrawRect(offsetX + start, offsetY + y, x - start, 1, paint);
                }
            }

            using (var image = SKImage.FromBitmap(bitmap))
            using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
            {
                return data.ToArray();
            }
        }
    }

    private static int GetDimension(int? dimension, int defaultValue, string parameterName)
    {
        var result = dimension ?? defaultValue;

        if (result <= 0)
        {
            throw new ArgumentOutOfRangeException(parameterName, result, "Image dimensions must be greater than zero.");
        }

        return result;
    }

    private static void ValidateValue(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("A barcode value is required.", nameof(value));
        }
    }

    private static void ValidateGtinValue(string value, int payloadLength, string formatName)
    {
        if (value.Length != payloadLength && value.Length != payloadLength + 1)
        {
            throw new ArgumentException($"A {formatName} value must contain {payloadLength} or {payloadLength + 1} digits.", nameof(value));
        }

        for (var i = 0; i < value.Length; i++)
        {
            if (value[i] < '0' || value[i] > '9')
            {
                throw new ArgumentException($"A {formatName} value can contain only digits.", nameof(value));
            }
        }

        if (value.Length == payloadLength + 1 && value[payloadLength] - '0' != CalculateGtinCheckDigit(value, payloadLength))
        {
            throw new ArgumentException($"The {formatName} check digit is invalid.", nameof(value));
        }
    }

    private static int CalculateGtinCheckDigit(string value, int payloadLength)
    {
        var sum = 0;
        var multiplier = 3;

        for (var i = payloadLength - 1; i >= 0; i--)
        {
            var digit = value[i] - '0';
            sum += digit * multiplier;
            multiplier = multiplier == 3 ? 1 : 3;
        }

        return (10 - sum % 10) % 10;
    }

    private static void ValidateCode128Value(string value)
    {
        ValidateValue(value);

        if (value.Length > 80)
        {
            throw new ArgumentException("A Code 128 value cannot exceed 80 characters.", nameof(value));
        }

        for (var i = 0; i < value.Length; i++)
        {
            if (value[i] > 127)
            {
                throw new ArgumentException("A Code 128 value can contain only ASCII characters.", nameof(value));
            }
        }
    }

    private static void ValidateCode39Value(string value)
    {
        const string validCharacters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ-. $/+%";

        ValidateValue(value);

        if (value.Length > 80)
        {
            throw new ArgumentException("A Code 39 value cannot exceed 80 characters.", nameof(value));
        }

        for (var i = 0; i < value.Length; i++)
        {
            if (validCharacters.IndexOf(value[i]) < 0)
            {
                throw new ArgumentException("A Code 39 value contains an unsupported character.", nameof(value));
            }
        }
    }
}
