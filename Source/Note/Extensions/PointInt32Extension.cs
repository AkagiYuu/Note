using Windows.Graphics;

namespace Note.Extensions;

public static class PointInt32Extension
{
    public static void Deconstruct(this PointInt32 Point, out int X, out int Y)
    {
        X = Point.X;
        Y = Point.Y;
    }
}
