using RimWorld;
using RimWorld.BaseGen;
using Verse;

namespace Nexomon;

public class SymbolResolver_NexomonShipBattery : SymbolResolver
{
    public override void Resolve(ResolveParams rp)
    {
        var battery = ThingMaker.MakeThing(RimWorld.ThingDefOf.Battery);
        battery.TryGetComp<CompPowerBattery>().AddEnergy(5000);
        var rot = rp.thingRot ?? Rot4.West;
        var pos = rp.rect.RandomCell;
        GenSpawn.Spawn(battery, pos, BaseGen.globalSettings.map, rot);
    }
}