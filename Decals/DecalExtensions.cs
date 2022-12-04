using DiskCardGame;
using UnityEngine;

namespace GBCDecals.Decals;

public static class DecalExtensions
{
    public static CardInfo AddPixelDecal(this CardInfo cardInfo, Sprite sprite)
    {
        PixelDecalPatches.PixelDecals.Add(cardInfo.name, sprite);
        return cardInfo;
    }

    public static CardInfo AddPixelDecal(this CardInfo cardInfo, Texture2D texture)
    {
        Sprite sprite = TextureHandler.SpriteFromTexture(texture);
        PixelDecalPatches.PixelDecals.Add(cardInfo.name, sprite);
        return cardInfo;
    }

    public static CardInfo AddPixelDecal(this CardInfo cardInfo, string texturePath)
    {
        Sprite sprite = TextureHandler.SpriteFromPath(texturePath);
        PixelDecalPatches.PixelDecals.Add(cardInfo.name, sprite);
        return cardInfo;
    }
}
