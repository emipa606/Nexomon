using RimWorld;
using UnityEngine;
using Verse;

namespace Nexomon;

public class IncidentWorker_CarnagriusPasses : IncidentWorker
{
    protected override bool CanFireNowSub(IncidentParms parms)
    {
        var map = (Map)parms.target;
        if (map.gameConditionManager.ConditionIsActive(GameConditionDefOf.ToxicFallout) ||
            map.Biome == BiomeDefOf.SeaIce)
        {
            return false;
        }

        if (!map.mapTemperature.SeasonAndOutdoorTemperatureAcceptableFor(ThingDefOf.Carnagrius))
        {
            return false;
        }

        return TryFindEntryCell(map, out _);
    }

    protected override bool TryExecuteWorker(IncidentParms parms)
    {
        var map = (Map)parms.target;
        if (!TryFindEntryCell(map, out var cell))
        {
            return false;
        }

        var carnagrius = PawnKindDefOf.Carnagrius;
        var value = GenMath.RoundRandom(StorytellerUtility.DefaultThreatPointsNow(map) / carnagrius.combatPower);
        var max = Rand.RangeInclusive(3, 6);
        value = Mathf.Clamp(value, 2, max);
        var num = Rand.RangeInclusive(90000, 150000);
        if (!RCellFinder.TryFindRandomCellOutsideColonyNearTheCenterOfTheMap(cell, map, 10f, out var result))
        {
            result = IntVec3.Invalid;
        }

        Pawn pawn = null;
        for (var i = 0; i < value; i++)
        {
            var loc = CellFinder.RandomClosewalkCellNear(cell, map, 10);
            pawn = PawnGenerator.GeneratePawn(carnagrius);
            GenSpawn.Spawn(pawn, loc, map, Rot4.Random);
            pawn.mindState.exitMapAfterTick = Find.TickManager.TicksGame + num;
            if (result.IsValid)
            {
                pawn.mindState.forcedGotoPosition = CellFinder.RandomClosewalkCellNear(result, map, 10);
            }
        }

        SendStandardLetter("LetterLabelThrumboPasses".Translate(carnagrius.label).CapitalizeFirst(),
            "LetterThrumboPasses".Translate(carnagrius.label), LetterDefOf.PositiveEvent, parms, pawn);
        return true;
    }

    private bool TryFindEntryCell(Map map, out IntVec3 cell)
    {
        return RCellFinder.TryFindRandomPawnEntryCell(out cell, map, CellFinder.EdgeRoadChance_Animal + 0.2f);
    }
}