using snes.graphics.demos.formulas;
using Ultraviolet;
using Ultraviolet.Graphics.Graphics2D;

namespace snes.graphics.demos.hdma.Objects
{
    internal interface IObject
    {
        Sprite Sprite { get; set; }
        Vector2d Pos { get; set; }
        void Update(UltravioletTime time);
        void Draw(SpriteBatch spriteBatch);
    }
}
