using RimWorld;
using Verse;

namespace Nexomon;

public class HeddifComp_JoinHealed : HediffComp
{
    public HediffCompProperties Props => (HeddifCompPropreties_JoinHealed)props;

    public override void CompPostTick(ref float severityAdjustment)
    {
        base.CompPostTick(ref severityAdjustment);
        if (!Pawn.InBed())
        {
            return;
        }

        Pawn.SetFaction(Faction.OfPlayer);
        Messages.Message("NexomonJoin".Translate(), new LookTargets(Pawn), MessageTypeDefOf.PositiveEvent);
        Pawn.health.RemoveHediff(parent);
    }
}