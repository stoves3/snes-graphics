using snes.graphics.demos.formulas;
using Ultraviolet;
using Ultraviolet.Graphics.Graphics2D;

namespace snes.graphics.demos.hdma.Objects
{
    internal class Sephiroth : BaseObject
    {
        public Sephiroth(Sprite sprite, string animation, Vector2d screenSize) : base(sprite, animation, screenSize)
        { }

        public override void Update(UltravioletTime time)
        {
            base.Update(time);
        }
    }
}
