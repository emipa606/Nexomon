using RimWorld.BaseGen;
using Verse;

namespace Nexomon;

public class GenStep_NexomonShip : GenStep
{
    public override int SeedPart => 54867541;

    public override void Generate(Map map, GenStepParams parms)
    {
        var minX = map.Size.x / 10;
        var width = 8 * map.Size.x / 10;
        var minZ = map.Size.z / 10;
        var height = 8 * map.Size.z / 10;
        var adventureRegion = new CellRect(minX, minZ, width, height);
        adventureRegion.ClipInsideMap(map);
        BaseGen.globalSettings.map = map;
        CellFinder.TryFindRandomEdgeCellWith(v => v.Standable(map), map, 0f, out var result);
        MapGenerator.PlayerStartSpot = result;
        var resolveParams = new ResolveParams();
        var rect = new CellRect(Rand.RangeInclusive(adventureRegion.minX + 10, adventureRegion.maxX - 50),
            Rand.RangeInclusive(adventureRegion.minZ + 10, adventureRegion.maxZ - 35), 40, 25);
        rect.ClipInsideMap(map);
        resolveParams.rect = rect;
        BaseGen.symbolStack.Push("NexomonShip", resolveParams);
        BaseGen.Generate();
    }
}