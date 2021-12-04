using System.Collections.Generic;
using RimWorld;
using RimWorld.BaseGen;
using Verse;

namespace Nexomon;

public class SymbolResolver_NexomonCryptosleep : SymbolResolver
{
    private readonly List<string> nexomons = new List<string>
    {
        "Abaddog",
        "Arctivore",
        "Boltusk",
        "Carnagrius",
        "Draclone",
        "Fuzztino",
        "Jeeta",
        "Raamu",
        "Mystogen",
        "Voltaic"
    };

    public override void Resolve(ResolveParams rp)
    {
        var nexomon =
            PawnGenerator.GeneratePawn(new PawnGenerationRequest(PawnKindDef.Named(nexomons.RandomElement())));
        nexomon.Name = PawnBioAndNameGenerator.GeneratePawnName(nexomon);
        //HealthUtility.DamageUntilDowned(nexomon);
        nexomon.health.AddHediff(HediffDefOf.JoinHealed);
        nexomon.health.AddHediff(RimWorld.HediffDefOf.Anesthetic);
        //nexomon.SetFaction(Faction.OfPlayer);
        var csc = (Building_CryptosleepCasket)ThingMaker.MakeThing(RimWorld.ThingDefOf.AncientCryptosleepCasket);
        if (!csc.TryAcceptThing(nexomon))
        {
            Log.Error("nexomon not accepted in cryptosleep");
        }

        var bottomLeft = rp.rect.BottomLeft;
        var rect = new CellRect(bottomLeft.x + (rp.rect.Width / 2) - 1, bottomLeft.z + (rp.rect.Height / 2), 2, 1);
        GenSpawn.Spawn(csc, CellRect.SingleCell(rect.BottomLeft).RandomCell, BaseGen.globalSettings.map, Rot4.East);
    }
}