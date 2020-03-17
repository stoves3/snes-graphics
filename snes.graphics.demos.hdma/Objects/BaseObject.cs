using System;
using Ultraviolet;
using Ultraviolet.Graphics;
using Ultraviolet.Graphics.Graphics2D;

namespace snes.graphics.demos.hdma.Objects
{
    public enum GraphicType
    {
        Unknown,
        Image,
        Sprite
    }

    public class BaseObject : IObject
    {
        public GraphicType GraphicType { get; private set; }

        public Texture2D Image { get; set; }

        public IEffect Effect { get; set; }

        private float _drawWidth;
        private float _drawHeight;

        private float ImageWidth { get; set; }

        private float ImageHeight { get; set; }

        public Sprite Sprite { get; set; }

        public RectangleF TargetRect { get; set; }
        public Vector2 Pos { get; set; }

        public float PercentageHeightOfScreen { get; protected set; }

        protected Vector2 DrawPosition { get; set; }
        protected Vector2 RenderSize { get; set; }
        protected Color RenderColor { get; set; }

        protected Vector2 StartPos { get; set; }
        protected SpriteAnimationController Controller { get; set; }
        protected Vector2 ScreenSize { get; set; }

        string _animation;
        private Action<SpriteBatch> _drawAction;
        private Action<UltravioletTime> _updateAction;

        public BaseObject(Texture2D image, Vector2 screenSize)
        {
            GraphicType = GraphicType.Image;
            _drawAction = DrawImage;
            _updateAction = UpdateImage;

            ScreenSize = screenSize;

            Image = image;
            
            StartPos = new Vector2(0, 0);
            Pos = StartPos;
            Init();
        }

        public BaseObject(Sprite sprite, string animation, Vector2 screenSize)
        {
            GraphicType = GraphicType.Sprite;
            _drawAction = DrawSprite;
            _updateAction = UpdateSprite;

            Sprite = sprite;
            _animation = animation;
            ScreenSize = screenSize;

            Controller = new SpriteAnimationController();
            
            StartPos = new Vector2(0, 0);
            Pos = StartPos;
            Init();
        }

        protected virtual void Init()
        {
            var imageHeight = Image.Height;
            var imageWidth = Image.Width;

            var heightPercentage = PercentageHeightOfScreen;
            float widthPercentage = 0;

            var scaled = heightPercentage > 0;
            if (scaled)
            {
                imageHeight = (int)(PercentageHeightOfScreen * ScreenSize.Y);

                widthPercentage = imageHeight / (float)Image.Height;
                imageWidth = (int)(widthPercentage * imageWidth);
            }

            _drawHeight = imageHeight;
            _drawWidth = imageWidth;

            DrawPosition = new Vector2(Pos.X, Pos.Y);
            TargetRect = GetTargetRect();
        }

        private RectangleF GetTargetRect()
        {
            return new RectangleF(DrawPosition.X, DrawPosition.Y, _drawWidth, _drawHeight);
        }

        public virtual void Update(UltravioletTime time)
        {
            var moved = false;
            if (Effect != null)
            {
                Effect.Animate(this, time);
                moved = true;
            }

            if (moved)
            {
                DrawPosition = new Vector2(Pos.X, Pos.Y);
                TargetRect = GetTargetRect();
            }

            UpdateAnimation(time);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            _drawAction(spriteBatch);
        }

        public void ResetSize(Vector2 newScreenSize)
        {
            var scaled = PercentageHeightOfScreen > 0;
            if (!scaled) return;

            var imageHeight = (int)(PercentageHeightOfScreen * newScreenSize.Y);
            var widthPercentage = imageHeight / (float)Image.Height;
            var imageWidth = (int)(widthPercentage * Image.Width);
            
            _drawHeight = imageHeight;
            _drawWidth = imageWidth;
            TargetRect = GetTargetRect();
        }

        private void UpdateAnimation(UltravioletTime time)
        {
            _updateAction(time);
        }

        private void UpdateImage(UltravioletTime time)
        {
        }

        private void UpdateSprite(UltravioletTime time)
        {
            Sprite.Update(time);
            if (!Controller.IsPlaying) Controller.PlayAnimation(Sprite[_animation]);
            Controller.Update(time);
        }

        private void DrawImage(SpriteBatch spriteBatch)
        {
            var color = RenderColor == default ? Color.White : RenderColor;

            spriteBatch.Draw(Image, TargetRect, color); //, DrawPosition, RenderSize.X, RenderSize.Y, color);
        }

        private void DrawSprite(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawSprite(Controller, DrawPosition);
        }
    }
}
