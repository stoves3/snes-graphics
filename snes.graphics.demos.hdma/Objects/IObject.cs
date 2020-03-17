using Ultraviolet;
using Ultraviolet.Graphics;
using Ultraviolet.Graphics.Graphics2D;

namespace snes.graphics.demos.hdma.Objects
{
    public interface IObject
    {
        GraphicType GraphicType { get; }
        Texture2D Image { get; set; }
        Sprite Sprite { get; set; }
        RectangleF TargetRect { get; set; }
        Vector2 Pos { get; set; }
        float PercentageHeightOfScreen { get; }
        void Update(UltravioletTime time);
        void Draw(SpriteBatch spriteBatch);
        void ResetSize(Vector2 screenSize);
    }
}
