using snes.graphics.demos.formulas;
using System;
using System.Collections.Generic;
using System.Linq;
using Ultraviolet;

namespace snes.graphics.demos.hdma.Objects
{
    public enum EffectType
    {
        Unknown,
        PositionTransform,
        ScaleTransform,
        CombineImages,
        LineScan
    }

    public enum EffectOrientation
    {
        Unknown,
        Horizontal,
        Vertical
    }

    public interface IEffect
    {
        void Animate(IObject objectToAnimate, UltravioletTime time);
        void Animate(IEnumerable<IObject> objects, UltravioletTime time);
    }

    public class EllipseShift : IEffect
    {
        public EffectOrientation Orientation { get; set; }
        public TimeSpan Duration { get; set; }

        public int Length { get; set; }

        private TimeSpan _elapsed;
        private int _lastLengthPosition;

        public EllipseShift(int length, EffectOrientation orientation, TimeSpan duration)
        {
            Length = length;
            Orientation = orientation;
            Duration = duration;

            _elapsed = TimeSpan.Zero;
            _lastLengthPosition = 0;
        }

        public void Animate(IObject objectToAnimate, UltravioletTime time)
        {
            Animate(new[] { objectToAnimate }, time);
        }

        public void Animate(IEnumerable<IObject> objects, UltravioletTime time)
        {
            _elapsed += time.ElapsedTime;

            var timeToReset = _elapsed > Duration;

            var percent = timeToReset ? 1.0f : _elapsed.TotalMilliseconds / Duration.TotalMilliseconds;

            var nextLength = Length.EllipticalPosition(percent.ToRads());

            var adjustment = _lastLengthPosition - nextLength;

            objects.ToList().ForEach(o =>
            {
                Vector2 newPos;
                switch (Orientation)
                {
                    case EffectOrientation.Horizontal:
                        newPos = new Vector2(o.Pos.X + adjustment, o.Pos.Y);
                        break;
                    case EffectOrientation.Vertical:
                    default:
                        newPos = new Vector2(o.Pos.X, o.Pos.Y + adjustment);
                        break;
                }
                o.Pos = newPos;
            });

            _lastLengthPosition = nextLength;

            if (timeToReset) _elapsed = TimeSpan.Zero;
        }
    }
}
