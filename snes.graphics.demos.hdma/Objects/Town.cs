using System;
using Ultraviolet;
using Ultraviolet.Graphics;
using Ultraviolet.Graphics.Graphics2D;

namespace snes.graphics.demos.hdma.Objects
{
    public class Town : BaseObject
    {
        public Town(Texture2D image, Vector2 screenSize) : base(image, screenSize) { }

        public Town(Sprite sprite, string animation, Vector2 screenSize) : base(sprite, animation, screenSize)
        { }

        protected override void Init()
        {
            PercentageHeightOfScreen = 1.0f;
            var effectDuration = new TimeSpan(0, 0, 0, 1);
            Effect = new EllipseShift(20, EffectOrientation.Horizontal, effectDuration);

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
