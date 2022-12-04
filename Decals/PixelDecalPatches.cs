using GBC;
using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#pragma warning disable Publicizer001
#nullable enable

namespace GBCDecals.Decals;

[HarmonyPatch]
internal static class PixelDecalPatches
{
    internal static Dictionary<string, Sprite> PixelDecals = new();

    [HarmonyPatch(typeof(PixelCardDisplayer), nameof(PixelCardDisplayer.DisplayInfo))]
    [HarmonyPostfix]
    private static void DecalPatches(PixelCardDisplayer __instance)
    {
        // I have to manage multiple scenes differently.
        // Thus, uh. Yeah.

        switch (SceneManager.GetActiveScene().name)
        {
            case "GBC_CardBattle":
                {
                    if (__instance.gameObject.name == "CardPreviewPanel"
                    || !PixelDecals.ContainsKey(__instance.info.name)) return;

                    AddDecalToCard(in __instance);
                }
                break;
            default:
                {
                    if (__instance.gameObject.name != "PixelSnap"
                    || !PixelDecals.ContainsKey(__instance.info.name)) return;

                    AddDecalToCard(in __instance);
                }
                break;
        }
    }

    private static void AddDecalToCard(in PixelCardDisplayer instance)
    {
        Transform? cardElements = instance
            .gameObject
            .transform
            .Find("CardElements");

        if (cardElements == null) return;

        HideStats(in cardElements);
        // If card already has a PixelDecal, no need to add it again.
        if (cardElements.Find("PixelDecal") != null) return;

        CreateDecal(in cardElements, instance.info.name);
    }

    private static void CreateDecal(in Transform cardElements, string name)
    {
        GameObject decal = new GameObject("PixelDecal");
        decal.transform.SetParent(cardElements, false);
        decal.layer = LayerMask.NameToLayer("GBCUI");

        SpriteRenderer sr = decal.AddComponent<SpriteRenderer>();
        sr.sprite = PixelDecals[name];

        // Find sorting group values
        SpriteRenderer? sortingReference = cardElements.Find("Portrait")
            .gameObject
            .GetComponent<SpriteRenderer>();

        sr.sortingLayerID = sortingReference?.sortingLayerID ?? 0;
        sr.sortingOrder = sortingReference?.sortingOrder + 100 ?? 0;
    }

    private static void HideStats(in Transform cardElements)
    {
        Transform? pixelStats = cardElements.Find("PixelCardStats");
        if (pixelStats == null) return;
        foreach (Transform stat in pixelStats)
        {
            Canvas? canvas = stat.gameObject.GetComponent<Canvas>();
            if (canvas != null) canvas.enabled = false;
        }
    }
}
