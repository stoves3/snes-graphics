using System;
using System.Collections.Generic;
using Ultraviolet;

namespace snes.graphics.demos.hdma.Utils
{
    public enum AspectRatio
    {
        Unknown,
        ThreeTwo,
        FourThree,
        SixteenNine
    }

    public class Scaler
    {
        public static readonly Dictionary<AspectRatio, float> ScaleValues = new Dictionary<AspectRatio, float>
        {
            { AspectRatio.ThreeTwo, (float)3/2 },
            { AspectRatio.FourThree, (float)4/3 },
            { AspectRatio.SixteenNine, (float)16/9 }
        };

        public Vector2 ScreenSize { get; private set; }
        public Vector2 BaseScreenSize { get; private set; }
        public Vector2 Ratios { get; private set; }

        public bool RetinaOverride { get; set; }

        public const int StandardScreenWidth = 1024;
        public const int StandardScreenHeight = 768;

        public const int RetinaWidth = 2048;
        public const int RetinaHeight = 1536;
        public bool Retina => ScreenSize.X >= RetinaWidth && ScreenSize.Y >= RetinaHeight;

        public bool WidthStandardOrLess => ScreenSize.X <= StandardScreenWidth;

        private readonly Vector2 _centerScreen;
        public Vector2 CenterScreen => _centerScreen;

        private bool _calc = true;

        private readonly Dictionary<Vector2, AspectRatio> _aspectRatios = new Dictionary<Vector2, AspectRatio>
        {
            {new Vector2(3,2), AspectRatio.ThreeTwo},
            {new Vector2(4, 3), AspectRatio.FourThree},
            {new Vector2(16, 9), AspectRatio.SixteenNine},
            {new Vector2(71,40), AspectRatio.SixteenNine}
        };

        private readonly List<AspectRatio> _widescreenRatios = new List<AspectRatio> { AspectRatio.SixteenNine };

        public Scaler(Vector2 screenSize, Vector2 baseScreenSize, bool retinaOverride = false)
        {
            ScreenSize = screenSize;
            BaseScreenSize = baseScreenSize;
            RetinaOverride = retinaOverride;

            SetCalc();
            SetRatios();
            _centerScreen = GetCenterScreen();
        }

        public bool Widescreen()
        {
            var ratio = GetAspectRatio();
            var aspectRatio = GetAspectRatio(ratio);
            var widescreen = _widescreenRatios.Contains(aspectRatio);
            return widescreen;
        }

        private void SetCalc()
        {
            _calc = (ScreenSize != BaseScreenSize);
        }

        private void SetRatios()
        {
            Ratios = !_calc ? new Vector2(1, 1) : new Vector2(ScreenSize.X / BaseScreenSize.X, ScreenSize.Y / BaseScreenSize.Y);
        }

        public Vector2 Scale(Vector2 baseVector)
        {
            return Scale(baseVector.X, baseVector.Y);
        }

        public Vector3 Scale(Vector3 baseVector, bool vertical = false)
        {
            var scaledX = ScaleX(baseVector.X);
            var scaledY = ScaleY(baseVector.Y);
            var scaledZ = vertical ? ScaleY(baseVector.Z) : ScaleX(baseVector.Z);

            return new Vector3(scaledX, scaledY, scaledZ);
        }

        public Vector2 Scale(int x, int y)
        {
            return new Vector2(ScaleX(x), ScaleY(y));
        }

        public Vector2 Scale(float x, float y)
        {
            return new Vector2(ScaleX(x), ScaleY(y));
        }

        public RectangleF Scale(RectangleF baseRectangle)
        {
            return Scale(baseRectangle.X, baseRectangle.Y, baseRectangle.Width, baseRectangle.Height);
        }

        public RectangleF Scale(float x, float y, float width, float height)
        {
            return new RectangleF(ScaleX(x), ScaleY(y), ScaleX(width), ScaleY(height));
        }

        public RectangleF Scale(int x, int y, int width, int height)
        {
            return new RectangleF(ScaleX(x), ScaleY(y), ScaleX(width), ScaleY(height));
        }

        public Vector2 ReverseScale(Vector2 vector)
        {
            return ReverseScale(vector.X, vector.Y);
        }

        public Vector2 ReverseScale(float x, float y)
        {
            return new Vector2(ReverseScaleX(x), ReverseScaleY(y));
        }

        public Vector2 ReverseScale(int x, int y)
        {
            return new Vector2(ReverseScaleX(x), ReverseScaleY(y));
        }

        public Rectangle ReverseScale(int x, int y, int width, int height)
        {
            return new Rectangle(ReverseScaleX(x), ReverseScaleY(y), ReverseScaleX(width), ReverseScaleY(height));
        }

        public int ScaleX(int x)
        {
            if (RetinaOverride && Retina) return x;

            return Convert.ToInt32(x * Ratios.X);
        }

        public float ScaleX(float x)
        {
            if (RetinaOverride && Retina) return x;

            return Convert.ToInt32(x * Ratios.X);
        }

        public int ScaleY(int y)
        {
            if (RetinaOverride && Retina) return y;

            return Convert.ToInt32(y * Ratios.Y);
        }

        public float ScaleY(float y)
        {
            if (RetinaOverride && Retina) return y;

            return Convert.ToInt32(y * Ratios.Y);
        }

        public int ReverseScaleX(int x)
        {
            if (RetinaOverride && Retina) return x;

            return Convert.ToInt32(x / Ratios.X);
        }

        public float ReverseScaleX(float x)
        {
            if (RetinaOverride && Retina) return x;

            return Convert.ToInt32(x / Ratios.X);
        }

        public int ReverseScaleY(int y)
        {
            if (RetinaOverride && Retina) return y;

            return Convert.ToInt32(y / Ratios.Y);
        }

        public float ReverseScaleY(float y)
        {
            if (RetinaOverride && Retina) return y;

            return Convert.ToInt32(y / Ratios.Y);
        }

        private Vector2 GetCenterScreen()
        {
            return new Vector2(ScreenSize.X / 2, ScreenSize.Y / 2);
        }

        private Vector2 GetAspectRatio()
        {
            var a = (int)ScreenSize.X / GCD((int)ScreenSize.X, (int)ScreenSize.Y);
            var b = (int)ScreenSize.Y / GCD((int)ScreenSize.X, (int)ScreenSize.Y);
            return new Vector2(a, b);
        }

        private AspectRatio GetAspectRatio(Vector2 ratio)
        {
            if (!_aspectRatios.ContainsKey(ratio)) return AspectRatio.Unknown;

            var aspectRatio = _aspectRatios[ratio];
            return aspectRatio;
        }
        private static int GCD(int a, int b)
        {
            int remainder;

            while (b != 0)
            {
                remainder = a % b;
                a = b;
                b = remainder;
            }

            return a;
        }
    }
}
