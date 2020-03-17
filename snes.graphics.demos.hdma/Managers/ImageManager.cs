using System.Collections.Generic;
using System.Linq;
using Ultraviolet.Content;
using Ultraviolet.Graphics;

namespace snes.graphics.demos.hdma.Managers
{
    public static class ImageManager
    {
        public static Dictionary<AssetID, Texture2D> Images = new Dictionary<AssetID, Texture2D>();

        public static void LoadImages(this IEnumerable<AssetID> imageAssets, ContentManager content)
        {
            imageAssets.ToList().ForEach(assetID => assetID.GetImage(content));
        }

        public static Texture2D GetImage(this AssetID assetID, ContentManager content)
        {
            if (!Images.ContainsKey(assetID)) Images.Add(assetID, content.Load<Texture2D>(assetID));

            return Images[assetID];
        }

        public static Texture2D Image(this AssetID assetID)
        {
            if (!Images.ContainsKey(assetID)) return null;

            return Images[assetID];
        }
    }
}
