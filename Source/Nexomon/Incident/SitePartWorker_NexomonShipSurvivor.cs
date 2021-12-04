using System.Linq;
using RimWorld;
using Verse;

namespace Nexomon;

public class SitePartWorker_NexomonShipSurvivor : SitePartWorker
{
    public override void PostMapGenerate(Map map)
    {
        base.PostMapGenerate(map);
        var incidentParms = StorytellerUtility.DefaultParmsNow(IncidentCategoryDefOf.ThreatBig, map);
        incidentParms.forced = true;
        RCellFinder.TryFindRandomPawnEntryCell(out var entry, map, 0f, false, vec => vec.Standable(map));
        Find.FactionManager.AllFactions
            .Where(faction => faction.def.hidden && faction.HostileTo(Faction.OfPlayer))
            .TryRandomElement(out var hostileFaction);

        incidentParms.faction = hostileFaction;
        incidentParms.raidStrategy = RaidStrategyDefOf.ImmediateAttack;
        incidentParms.spawnCenter = entry;
        incidentParms.points = Rand.Range(500f, 800f);
    }
}