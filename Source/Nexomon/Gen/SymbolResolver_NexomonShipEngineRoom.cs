using RimWorld;
using RimWorld.BaseGen;
using Verse;

namespace Nexomon;

public class SymbolResolver_NexomonShipEngineRoom : SymbolResolver
{
    private const int cryptoNeededCount = 8;
    private const int cryptoEnemyCount = 4;

    private const int offsetCryptoX = 1;
    private const int offsetCryptoZ = 1;

    private const int LWDoorFromTopOffset = 5;
    private const int RWDoorFromTopOffset = 8;

    public override void Resolve(ResolveParams rp)
    {
        var doorRp = rp;
        doorRp.rect = new CellRect(rp.rect.TopRight.x, rp.rect.TopRight.z - LWDoorFromTopOffset, 1, 1);
        BaseGen.symbolStack.Push("NexomonShipDoor", doorRp);
        doorRp.rect = new CellRect(rp.rect.TopRight.x, rp.rect.TopRight.z - RWDoorFromTopOffset, 1, 1);
        BaseGen.symbolStack.Push("NexomonShipDoor", doorRp);

        var width = rp.rect.Width / SymbolResolver_AncientShrinesGroup.StandardAncientShrineSize.x;
        var height = rp.rect.Height / SymbolResolver_AncientShrinesGroup.StandardAncientShrineSize.z;
        var bottomLeft = rp.rect.BottomLeft;
        var count = 0;
        var enemyCount = 0;
        for (var i = 0; i < height; i++)
        {
            for (var j = 0; j < width; j++)
            {
                if (count >= cryptoNeededCount)
                {
                    break;
                }

                var rect = new CellRect(
                    bottomLeft.x + offsetCryptoX + (j * SymbolResolver_AncientShrinesGroup.StandardAncientShrineSize.x),
                    bottomLeft.z + offsetCryptoZ + (i * SymbolResolver_AncientShrinesGroup.StandardAncientShrineSize.z),
                    SymbolResolver_AncientShrinesGroup.StandardAncientShrineSize.x,
                    SymbolResolver_AncientShrinesGroup.StandardAncientShrineSize.z);
                if (!rect.FullyContainedWithin(rp.rect))
                {
                    continue;
                }

                var resolveParams = rp;
                resolveParams.rect = rect;
                if (enemyCount >= cryptoEnemyCount)
                {
                    BaseGen.symbolStack.Push("NexomonCryptosleep", resolveParams);
                }
                else if (count - enemyCount >= cryptoNeededCount - cryptoEnemyCount)
                {
                    resolveParams.podContentsType = PodContentsType.AncientHostile;
                    BaseGen.symbolStack.Push("ancientShrine", resolveParams);
                    enemyCount++;
                }
                else if (Rand.Value < 0.5f)
                {
                    resolveParams.podContentsType = PodContentsType.AncientHostile;
                    BaseGen.symbolStack.Push("ancientShrine", resolveParams);
                    enemyCount++;
                }
                else
                {
                    BaseGen.symbolStack.Push("NexomonCryptosleep", resolveParams);
                }

                count++;
            }
        }

        BaseGen.symbolStack.Push("emptyRoom", rp);
        BaseGen.symbolStack.Push("clear", rp);
    }
}