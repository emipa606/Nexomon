using HarmonyLib;
using RimWorld;
using Verse;

namespace Nexomon;

[HarmonyPatch(typeof(RelationsUtility), "TryDevelopBondRelation")]
internal class FuzztinoBond
{
    public static void Prefix(Pawn animal, ref float baseChance)
    {
        if (animal.def == ThingDefOf.Fuzztino)
        {
            baseChance *= 5;
        }
    }
}