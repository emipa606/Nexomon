using RimWorld.BaseGen;

namespace Nexomon;

public class SymbolResolver_NexomonShipDebris : SymbolResolver
{
    public override void Resolve(ResolveParams rp)
    {
        var resolveParams = rp;
        resolveParams.singleThingDef = RimWorld.ThingDefOf.Filth_RubbleBuilding;
        var num = rp.rect.Width * rp.rect.Height / 5;
        for (var i = 0; i < num; i++)
        {
            BaseGen.symbolStack.Push("thing", resolveParams);
        }

        resolveParams.singleThingDef = RimWorld.ThingDefOf.ChunkSlagSteel;
        var num2 = (int)(rp.rect.Width * rp.rect.Height * 0.04f);
        for (var j = 0; j < num2; j++)
        {
            BaseGen.symbolStack.Push("thing", resolveParams);
        }
    }
}