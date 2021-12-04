using RimWorld;
using Verse;

namespace Nexomon;

[DefOf]
public static class ThingDefOf
{
    public static ThingDef Carnagrius;
    public static ThingDef Omnicron;
    public static ThingDef Raamu;
    public static ThingDef Fuzztino;
    public static ThingDef Draclone;
    public static ThingDef Jeeta;

    static ThingDefOf()
    {
        DefOfHelper.EnsureInitializedInCtor(typeof(ThingDefOf));
    }
}