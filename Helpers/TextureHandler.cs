using BepInEx;
using System.IO;
using System.Linq;
using UnityEngine;

namespace GBCDecals;
internal static class TextureHandler
{
    public static Sprite SpriteFromPath(string path)
    {
        string file = Directory.GetFiles(Paths.PluginPath, path, SearchOption.AllDirectories).FirstOrDefault();
        return SpriteFromBytes(File.ReadAllBytes(file));
    }

    public static Sprite SpriteFromBytes(byte[] array)
    {
        Texture2D tex = new Texture2D(1, 1);
        ImageConversion.LoadImage(tex, array);
        tex.filterMode = FilterMode.Point; // Pixel-perfect filter.
        return SpriteFromTexture(tex);
    }

    public static Sprite SpriteFromTexture(Texture2D tex)
    {
        Rect texRect = new Rect(0, 0, tex.width, tex.height);
        Vector2 pivot = new Vector2(0.5f, 0.5f);
        return Sprite.Create(tex, texRect, pivot);
    }
}