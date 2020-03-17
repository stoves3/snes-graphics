using System;
using Ultraviolet;
using Ultraviolet.Graphics;
using Ultraviolet.Graphics.Graphics2D;

namespace snes.graphics.demos.hdma.Objects
{
    public class Fire : BaseObject
    {
        public Fire(Texture2D image, Vector2 screenSize) : base(image, screenSize) { }

        public Fire(Sprite sprite, string animation, Vector2 screenSize) : base(sprite, animation, screenSize)
        { }

        protected override void Init()
        {
            PercentageHeightOfScreen = 1.0f;
            var effectDuration = new TimeSpan(0, 0, 0, 2);
            Effect = new EllipseShift(30, EffectOrientation.Vertical, effectDuration);

            base.Init();
        }

        public override void Update(UltravioletTime time)
        {
            base.Update(time);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
