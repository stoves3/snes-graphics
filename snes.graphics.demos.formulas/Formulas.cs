using System;

namespace snes.graphics.demos.formulas
{
    public static class Formulas
    {
        private static readonly double MaxRads = 2 * Math.PI;

        public static double ToRads(this double percent)
        {
            return (MaxRads * percent);
        }

        public static int EllipticalPosition(this int majorAxisLength, double rads)
        {
            return (int)(majorAxisLength * Math.Cos(rads));
        }
    }
}
