using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using SeedModifier.Patches;

namespace SeedModifier;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    private readonly Harmony harmony = new(MyPluginInfo.PLUGIN_GUID);

    public static ConfigFile? SeedCFG;
    public static ManualLogSource? SLogger;
    private void Awake()
    {
        SLogger = base.Logger;
        SLogger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        SeedCFG = base.Config;
        SeedCFG?.Bind("Settings", "Seed", 0, "Set the game seed. Only valid values between 1-2147483647. Setting the value to 0 keeps the random seed.");

        harmony.PatchAll(typeof(Plugin));
        harmony.PatchAll(typeof(SeedPatch));
        //harmony.PatchAll(typeof(SpawnHandlerPatch));
    }
}
