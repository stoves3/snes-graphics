using snes.graphics.demos.formulas;
using System;
using Ultraviolet;
using Ultraviolet.Graphics.Graphics2D;

namespace snes.graphics.demos.hdma.Objects
{
    public class BaseObject : IObject
    {
        public Sprite Sprite { get; set; }
        public Vector2d Pos { get; set; }

        protected Vector2 DrawPosition { get; set; }
        protected Vector2d StartPos { get; set; }
        protected SpriteAnimationController Controller { get; set; }
        protected Vector2d ScreenSize { get; set; }

        string _animation;

        public BaseObject(Sprite sprite, string animation, Vector2d screenSize)
        {
            Sprite = sprite;
            _animation = animation;
            ScreenSize = screenSize;

            Controller = new SpriteAnimationController();
            
            StartPos = new Vector2d(0, 0);
            Init();
        }

        protected virtual void Init()
        {
            UpdateAnimation(new UltravioletTime(TimeSpan.Zero, TimeSpan.Zero));

            Pos = StartPos;

            DrawPosition = new Vector2(Pos.X, Pos.Y);
        }

        public virtual void Update(UltravioletTime time)
        {
            var moved = false;

            if (moved) DrawPosition = new Vector2(Pos.X, Pos.Y);

            UpdateAnimation(time);
        }

        private void UpdateAnimation(UltravioletTime time)
        {
            Sprite.Update(time);
            if (!Controller.IsPlaying) Controller.PlayAnimation(Sprite[_animation]);
            Controller.Update(time);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawSprite(Controller, DrawPosition);
        }
    }
}
