using RimWorld.BaseGen;
using Verse;

namespace Nexomon;

public class SymbolResolver_NexomonShipDoor : SymbolResolver
{
    public override void Resolve(ResolveParams rp)
    {
        var rot = rp.thingRot ?? Rot4.East;
        var door = ThingMaker.MakeThing(RimWorld.ThingDefOf.Door, rp.wallStuff);
        GenSpawn.Spawn(door, rp.rect.BottomLeft, BaseGen.globalSettings.map, rot);
    }
}