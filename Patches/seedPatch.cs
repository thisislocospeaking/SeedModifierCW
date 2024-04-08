using HarmonyLib;

namespace SeedModifier.Patches
{
    [HarmonyPatch(typeof(RoomStatsHolder))]
    internal class SeedPatch
    {
        internal static int CFGSeed
        {
            get { return (int)Plugin.SeedCFG?.Bind("Settings", "Seed", 0, "Set the game seed. Only valid values between 1-2147483647. Setting the value to 0 keeps the random seed.").Value; }
        }

        [HarmonyPatch("RegenerateSeed", MethodType.Normal)]
        [HarmonyPostfix]
        static void RSHSeedPatch()
        {
            Plugin.SLogger.LogMessage($"Preset game seed: {GameAPI.seed}");
            
            if (CFGSeed > 0)
            {
                Plugin.SLogger.LogMessage($"User set game seed: {CFGSeed}");
                GameAPI.seed = CFGSeed;
            } else
            {
                Plugin.SLogger.LogMessage("No seed specified, keeping random.");
            }
            
        }
    }
}