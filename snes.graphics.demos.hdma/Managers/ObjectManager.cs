using snes.graphics.demos.hdma.Assets;
using snes.graphics.demos.hdma.Objects;
using System.Collections.Generic;
using Ultraviolet;
using Ultraviolet.Content;
using Ultraviolet.Graphics.Graphics2D;

namespace snes.graphics.demos.hdma.Managers
{
    public class ObjectManager
    {
        private readonly ContentManager _content;
        private readonly ScreenManager _screenManager;

        private readonly List<IObject> _objects = new List<IObject>();

        public ObjectManager(ContentManager content, ScreenManager screenManager)
        {
            _content = content;
            _screenManager = screenManager;

            LoadObjects();

            _screenManager.ScreenChangedHandler += newSize => ResizeObjects();
        }

        private void LoadObjects()
        {
            new[] { GlobalImageID.RocketTown, GlobalImageID.Fire, GlobalImageID.Sephiroth }.LoadImages(_content);

            _objects.AddRange(new[] {
                (IObject)new Town(GlobalImageID.RocketTown.Image(), _screenManager.ScreenSize),
                (IObject)new Fire(GlobalImageID.Fire.Image(), _screenManager.ScreenSize),
                (IObject)new Sephiroth(GlobalImageID.Sephiroth.Image(), _screenManager.ScreenSize),                
            });
        }

        private void ResizeObjects()
        {
            _objects.ForEach(o => o.ResetSize(_screenManager.ScreenSize));
        }

        public void UpdateObjects(UltravioletTime time)
        {
            _objects.ForEach(o => o.Update(time));
        }

        public void DrawObjects(SpriteBatch spriteBatch)
        {
            _objects.ForEach(o => o.Draw(spriteBatch));
        }
    }
}
