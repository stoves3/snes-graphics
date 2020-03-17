 using Ultraviolet;
using Ultraviolet.Graphics;
using Ultraviolet.Graphics.Graphics2D;

namespace snes.graphics.demos.hdma.Objects
{
    public class Sephiroth : BaseObject
    {
        public Sephiroth(Texture2D image, Vector2 screenSize) : base(image, screenSize) { }

        public Sephiroth(Sprite sprite, string animation, Vector2 screenSize) : base(sprite, animation, screenSize)
        { }

        protected override void Init()
        {
            PercentageHeightOfScreen = 0.72f;

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
