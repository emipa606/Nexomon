using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using Verse;

namespace Nexomon;

[HarmonyPatch(typeof(Pawn), "TickRare")]
public class Patch_PawnTick
{
    public static void Postfix(Pawn __instance)
    {
        List<Pawn> pawns;
        if (__instance.def == ThingDefOf.Fuzztino && __instance.Faction != null && __instance.Spawned)
        {
            pawns = GenRadial.RadialDistinctThingsAround(__instance.Position, __instance.Map, 15, true)
                .Where(t => t is Pawn && t.Spawned && t.Faction == __instance.Faction)
                .Select(t => t as Pawn)
                .Where(p => p?.needs?.mood?.thoughts?.memories != null).ToList();
            if (!pawns.NullOrEmpty())
            {
                for (var index = 0; index < pawns.Count; index++)
                {
                    var pawn = pawns[index];
                    pawn.needs.mood.thoughts.memories.TryGainMemory(ThoughtDefOf.FuzztinoMood);
                }
            }
        }

        if (__instance.def != ThingDefOf.Jeeta || __instance.Faction == null || !__instance.Spawned)
        {
            return;
        }

        pawns = GenRadial.RadialDistinctThingsAround(__instance.Position, __instance.Map, 15, true)
            .Where(t => t is Pawn && t.Spawned && t.Faction == __instance.Faction)
            .Select(t => t as Pawn).ToList();
        if (pawns.NullOrEmpty())
        {
            return;
        }

        for (var index = 0; index < pawns.Count; index++)
        {
            var pawn = pawns[index];
            var hediff = HediffMaker.MakeHediff(HediffDefOf.JeetaAura, pawn);
            pawn.health.AddHediff(hediff);
        }
    }
}