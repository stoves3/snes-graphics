using snes.graphics.demos.hdma.Assets;
using snes.graphics.demos.hdma.Managers;
using System;
using System.Linq;
using Ultraviolet;
using Ultraviolet.Content;
using Ultraviolet.Core;
using Ultraviolet.Graphics;
using Ultraviolet.Graphics.Graphics2D;
using Ultraviolet.OpenGL;

namespace snes.graphics.demos.hdma
{
    public class Game : UltravioletApplication
    {
        private ContentManager _content;
        private SpriteBatch _spriteBatch;
        private ScreenManager _screenManager;

        public Game() : base("stoves3", "Snes Graphics Demos HDMA") 
        { }

        protected override UltravioletContext OnCreatingUltravioletContext()
        {
            var configuration = new OpenGLUltravioletConfiguration();
            PopulateConfiguration(configuration);

#if DEBUG
            configuration.Debug = true;
            configuration.DebugLevels = DebugLevels.Error | DebugLevels.Warning;
            configuration.DebugCallback = (uv, level, message) =>
            {
                System.Diagnostics.Debug.WriteLine(message);
            };
#endif

            return new OpenGLUltravioletContext(this, configuration);
        }

        private ObjectManager _objectManager;

        protected override void OnLoadingContent()
        {
            _content = ContentManager.Create("Content");
            LoadContentManifests(_content);

            _screenManager = new ScreenManager(Ultraviolet);
            _screenManager.UpdateWindow(_screenManager.Current.First());

            _objectManager = new ObjectManager(_content, _screenManager);

            _spriteBatch = SpriteBatch.Create();

            base.OnLoadingContent();
        }
                
        protected override void OnUpdating(UltravioletTime time)
        {
            _objectManager.UpdateObjects(time);

            base.OnUpdating(time);
        }
        
        protected override void OnDrawing(UltravioletTime time)
        {
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            _objectManager.DrawObjects(_spriteBatch);

            _spriteBatch.End();

            base.OnDrawing(time);
        }
        
        protected override void Dispose(Boolean disposing)
        {
            if (disposing)
            {
                SafeDispose.DisposeRef(ref _spriteBatch);
                SafeDispose.DisposeRef(ref _content);
            }
            base.Dispose(disposing);
        }

        protected void LoadContentManifests(ContentManager content)
        {
            Contract.Require(content, nameof(content));

            var uvContent = Ultraviolet.GetContent();

            var contentManifestFiles = content.GetAssetFilePathsInDirectory("Manifests");
            uvContent.Manifests.Load(contentManifestFiles);

            Ultraviolet.GetContent().Manifests["Global"]["Images"].PopulateAssetLibrary(typeof(GlobalImageID));
        }
    }
}