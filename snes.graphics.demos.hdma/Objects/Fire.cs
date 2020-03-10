using snes.graphics.demos.formulas;
using Ultraviolet;
using Ultraviolet.Graphics.Graphics2D;

namespace snes.graphics.demos.hdma.Objects
{
    internal class Fire : BaseObject
    {
        public Fire(Sprite sprite, string animation, Vector2d screenSize) : base(sprite, animation, screenSize)
        { }

        protected override void Init()
        {
            //var imgHeight = Controller.Height;
            //this.StartPos = new Vector2d(0, this.ScreenSize.Y - imgHeight);
                        
            base.Init();
        }

        public override void Update(UltravioletTime time)
        {
            base.Update(time);
        }
    }
}
