using RimWorld;

namespace Nexomon;

[DefOf]
public static class ThoughtDefOf
{
    public static ThoughtDef FuzztinoMood;

    static ThoughtDefOf()
    {
        DefOfHelper.EnsureInitializedInCtor(typeof(ThoughtDefOf));
    }
}