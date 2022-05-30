using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using System.Globalization;

namespace Note.Extensions;
public static class ColorExtensions
{
    private static Color HexToColor(ReadOnlySpan<char> Hex)
    {
        var cuint = int.Parse(Hex, NumberStyles.HexNumber);
        byte a = 255;
        byte r = 0;
        byte g = 0;
        byte b = 0;

        switch (Hex.Length)
        {
            case 8:
                a = (byte)(cuint >> 24);
                r = (byte)((cuint >> 16) & 0xff);
                g = (byte)((cuint >> 8) & 0xff);
                b = (byte)(cuint & 0xff);
                break;

            case 6:
                r = (byte)((cuint >> 16) & 0xff);
                g = (byte)((cuint >> 8) & 0xff);
                b = (byte)(cuint & 0xff);
                break;

            case 4:
                a = (byte)(cuint >> 12);
                r = (byte)((cuint >> 8) & 0xf);
                g = (byte)((cuint >> 4) & 0xf);
                b = (byte)(cuint & 0xf);
                a = (byte)(a << 4 | a);
                r = (byte)(r << 4 | r);
                g = (byte)(g << 4 | g);
                b = (byte)(b << 4 | b);
                break;

            case 3:
                r = (byte)((cuint >> 8) & 0xf);
                g = (byte)((cuint >> 4) & 0xf);
                b = (byte)(cuint & 0xf);
                r = (byte)(r << 4 | r);
                g = (byte)(g << 4 | g);
                b = (byte)(b << 4 | b);
                break;
        }
        return Color.FromArgb(a, r, g, b);
    }
    public static Color ToColor(this string ColorString)
    {
        if (string.IsNullOrEmpty(ColorString))
            throw new Exception("Color string is empty");

        if (ColorString[0] is not '#')
            throw new Exception("Color is not in correct format");

        return HexToColor(ColorString.AsSpan(1));
    }
}
