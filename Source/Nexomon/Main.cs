using HarmonyLib;
using Verse;

namespace Nexomon;

[StaticConstructorOnStartup]
internal static class Main
{
    public const string Id = "Sielfyr.Nexomon";
    public const string ModName = "Nexomon";
    public const string Version = "0.001";

    static Main()
    {
        var harmony = new Harmony(Id);
        harmony.PatchAll();
        /*
        var original = AccessTools.Method( typeof(Caravan_PathFollower), "CostToMove", new Type[] { typeof(int), typeof(int), typeof(int), typeof(int?), typeof(bool), typeof(StringBuilder), typeof(string) });
        var postfix = typeof(Patch_Caravan).GetMethod("Postfix");
        harmony.Patch(original, postfix: new HarmonyMethod(postfix));
        */
        Log.Message("Initialized " + ModName + " v" + Version);
    }
}