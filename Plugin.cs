using BepInEx;
using BepInEx.Logging;
using DiskCardGame;
using GBCDecals.Decals;
using HarmonyLib;
using UnityEngine;

namespace GBCDecals;

// Plugin base:
[BepInPlugin(PluginGuid, PluginName, PluginVersion)]
[BepInDependency("cyantist.inscryption.api", BepInDependency.DependencyFlags.HardDependency)]
public class Plugin : BaseUnityPlugin
{   
    private const string PluginGuid = "kel.inscryption.gbcportrait";
    private const string PluginName = "GBCPortrait";
    private const string PluginVersion = "1.0.0";

    internal static ManualLogSource myLogger; // Log source.
    private void Awake() {

        myLogger = Logger; // Make log source.

        Harmony harmony = new Harmony("kel.harmony.dummyname");
        harmony.PatchAll();

        AddDebugDecals();
    }

    private static void AddDebugDecals()
    {
        Sprite sprite = TextureHandler.SpriteFromBytes(Properties.Resources.decal_debug);
        CardLoader.GetCardByName("Skeleton").AddPixelDecal(sprite);
        CardLoader.GetCardByName("Zombie")?.AddPixelDecal(sprite);
        CardLoader.GetCardByName("Gravedigger").AddPixelDecal(sprite);
        CardLoader.GetCardByName("Draugr")?.AddPixelDecal(sprite);
    }
}