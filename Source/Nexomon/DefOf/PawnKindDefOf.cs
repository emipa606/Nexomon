using RimWorld;
using Verse;

namespace Nexomon;

[DefOf]
public static class PawnKindDefOf
{
    public static PawnKindDef Carnagrius;
    public static PawnKindDef Omnicron;

    static PawnKindDefOf()
    {
        DefOfHelper.EnsureInitializedInCtor(typeof(PawnKindDefOf));
    }
}