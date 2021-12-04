using RimWorld;
using Verse;

public class DeathActionWorker_IncredibleExplosion : DeathActionWorker
{
    public override RulePackDef DeathRules => RulePackDefOf.Transition_DiedExplosive;

    public override bool DangerousInMelee => true;

    public override void PawnDied(Corpse corpse)
    {
        GenExplosion.DoExplosion(
            radius: corpse.InnerPawn.ageTracker.CurLifeStageIndex == 0 ? 2.9f :
            corpse.InnerPawn.ageTracker.CurLifeStageIndex != 1 ? 5.9f : 3.9f, center: corpse.Position, map: corpse.Map,
            damType: DamageDefOf.Flame, instigator: corpse.InnerPawn);
    }
}