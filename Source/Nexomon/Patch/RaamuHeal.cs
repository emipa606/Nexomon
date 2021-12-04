using HarmonyLib;
using RimWorld;
using Verse;

namespace Nexomon;

[HarmonyPatch(typeof(Hediff_Injury), "Heal")]
internal class RaamuHeal
{
    public static void Prefix(ref Hediff_Injury __instance, ref float amount)
    {
        if (__instance.pawn.def == ThingDefOf.Raamu)
        {
            amount *= __instance.pawn.def.statBases.GetStatValueFromList(StatDefOf.ImmunityGainSpeed, 1);
        }
    }
}