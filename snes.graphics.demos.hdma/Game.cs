using snes.graphics.demos.hdma.Assets;
using snes.graphics.demos.formulas;
using snes.graphics.demos.hdma.Objects;
using System.Collections.Generic;
using System;
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

        protected override void OnLoadingContent()
        {
            content = ContentManager.Create("Content");
            LoadContentManifests(content);

            var clientSize = Ultraviolet.GetUI().GetScreens().Window.ClientSize;
            var screenSize = new Vector2d(clientSize.Width, clientSize.Height);

            var fire = new Fire(content.Load<Sprite>(GlobalSpriteID.Fire), "Fire", screenSize);
            var town = new Town(content.Load<Sprite>(GlobalSpriteID.RocketTown), "RocketTown", screenSize);
            var sephiroth = new Fire(content.Load<Sprite>(GlobalSpriteID.Sephiroth), "Sephiroth", screenSize);

            objects.AddRange(new IObject[] { town, fire, sephiroth });

            spriteBatch = SpriteBatch.Create();

            base.OnLoadingContent();
        }

        private readonly List<IObject> objects = new List<IObject>();
        
        protected override void OnUpdating(UltravioletTime time)
        {
            objects.ForEach(o => o.Update(time));

            base.OnUpdating(time);
        }
        
        protected override void OnDrawing(UltravioletTime time)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            objects.ForEach(o => o.Draw(spriteBatch));

            spriteBatch.End();

            base.OnDrawing(time);
        }
        
        protected override void Dispose(Boolean disposing)
        {
            if (disposing)
            {
                SafeDispose.DisposeRef(ref spriteBatch);
                SafeDispose.DisposeRef(ref content);
            }
            base.Dispose(disposing);
        }

        protected void LoadContentManifests(ContentManager content)
        {
            Contract.Require(content, nameof(content));

            var uvContent = Ultraviolet.GetContent();

            var contentManifestFiles = content.GetAssetFilePathsInDirectory("Manifests");
            uvContent.Manifests.Load(contentManifestFiles);

            Ultraviolet.GetContent().Manifests["Global"]["Sprites"].PopulateAssetLibrary(typeof(GlobalSpriteID));
        }

        private ContentManager content;
        private SpriteBatch spriteBatch;
    }
}