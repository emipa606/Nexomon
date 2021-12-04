using RimWorld.BaseGen;
using Verse;

namespace Nexomon;

public class SymbolResolver_NexomonShipRightWing : SymbolResolver
{
    public override void Resolve(ResolveParams rp)
    {
        var resolveParams = rp;
        resolveParams.rect = new CellRect(rp.rect.minX + 1, rp.rect.minZ + 1, rp.rect.Width - 2, rp.rect.Height - 2);
        resolveParams.hivesCount = Rand.RangeInclusive(2, 6);
        BaseGen.symbolStack.Push("hives", resolveParams);

        BaseGen.symbolStack.Push("emptyRoom", rp);
        BaseGen.symbolStack.Push("clear", rp);
    }
}