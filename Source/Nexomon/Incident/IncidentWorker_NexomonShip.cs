using System.Collections.Generic;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace Nexomon;

public class IncidentWorker_NexomonShip : IncidentWorker
{
    private const int minSiteRange = 5;
    private const int maxSiteRange = 12;

    private static readonly FloatRange siteTimeRange = new FloatRange(13, 16);
    private int tile;

    protected override bool CanFireNowSub(IncidentParms parms)
    {
        return base.CanFireNowSub(parms) && TileFinder.TryFindNewSiteTile(out tile, minSiteRange, maxSiteRange);
    }

    protected override bool TryExecuteWorker(IncidentParms parms)
    {
        if (!TileFinder.TryFindNewSiteTile(out tile, minSiteRange, maxSiteRange))
        {
            return false;
        }

        var siteParts = new List<SitePartDef> { SitePartDefOf.NexomonShip };
        var site = SiteMaker.MakeSite(siteParts, tile, Faction.OfAncientsHostile);
        site.GetComponent<TimeoutComp>().StartTimeout((int)(siteTimeRange.RandomInRange * GenDate.TicksPerDay));
        Find.WorldObjects.Add(site);
        Find.LetterStack.ReceiveLetter(def.letterLabel, def.letterText, LetterDefOf.PositiveEvent, site);

        return true;
    }
}