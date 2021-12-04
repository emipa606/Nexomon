using RimWorld;
using Verse;

namespace Nexomon;

[DefOf]
public static class HediffDefOf
{
    public static HediffDef JeetaAura;

    public static HediffDef JoinHealed;

    static HediffDefOf()
    {
        DefOfHelper.EnsureInitializedInCtor(typeof(HediffDefOf));
    }
}