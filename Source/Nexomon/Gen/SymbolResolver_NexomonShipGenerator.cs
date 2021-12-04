using RimWorld;
using RimWorld.BaseGen;
using Verse;

namespace Nexomon;

public class SymbolResolver_NexomonShipGenerator : SymbolResolver
{
    public override void Resolve(ResolveParams rp)
    {
        var generator = ThingMaker.MakeThing(DefDatabase<ThingDef>.GetNamed("ChemfuelPoweredGenerator"));
        generator.TryGetComp<CompRefuelable>().Refuel(500);
        var rot = rp.thingRot ?? Rot4.East;
        var pos = rp.rect.BottomLeft;
        GenSpawn.Spawn(generator, pos, BaseGen.globalSettings.map, rot);
    }
}