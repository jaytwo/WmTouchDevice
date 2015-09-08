using System;
using System.Windows;

namespace WmTouchDevice
{
    class GraphicsHelper
    {
        public static double DpiX { get; private set; }
        public static double DpiY { get; private set; }

        public static Point DivideByDpi(Point point)
        {
            return new Point(point.X * 96.0 / DpiX, point.Y * 96.0 / DpiY);
        }

        static GraphicsHelper()
        {
            using (var graphics = System.Drawing.Graphics.FromHwnd(IntPtr.Zero))
            {
                DpiX = graphics.DpiX;
                DpiY = graphics.DpiY;
            }
        }
    }
}
