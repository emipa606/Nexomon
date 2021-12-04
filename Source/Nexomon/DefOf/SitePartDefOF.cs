using RimWorld;

namespace Nexomon;

[DefOf]
public static class SitePartDefOf
{
    public static SitePartDef NexomonShip;

    static SitePartDefOf()
    {
        DefOfHelper.EnsureInitializedInCtor(typeof(SitePartDefOf));
    }
}