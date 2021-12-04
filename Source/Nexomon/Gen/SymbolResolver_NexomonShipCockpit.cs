using RimWorld.BaseGen;
using Verse;

namespace Nexomon;

public class SymbolResolver_NexomonShipCockpit : SymbolResolver
{
    private const int LWDoorFromBotOffset = 4;
    private const int RWDoorFromBotOffset = 1;

    public override void Resolve(ResolveParams rp)
    {
        var doorRp = rp;
        doorRp.rect = new CellRect(rp.rect.BottomLeft.x, rp.rect.BottomLeft.z + LWDoorFromBotOffset, 1, 1);
        BaseGen.symbolStack.Push("NexomonShipDoor", doorRp);
        doorRp.rect = new CellRect(rp.rect.BottomLeft.x, rp.rect.BottomLeft.z + RWDoorFromBotOffset, 1, 1);
        BaseGen.symbolStack.Push("NexomonShipDoor", doorRp);

        var resolveParams = rp;

        resolveParams.thingRot = Rot4.West;
        resolveParams.rect = new CellRect(rp.rect.TopRight.x - 1, rp.rect.TopRight.z - 1, 1, 1);
        BaseGen.symbolStack.Push("NexomonShipBattery", resolveParams);
        resolveParams.rect = new CellRect(rp.rect.TopRight.x - 3, rp.rect.TopRight.z - 1, 1, 1);
        BaseGen.symbolStack.Push("NexomonShipBattery", resolveParams);
        resolveParams.rect = new CellRect(rp.rect.TopRight.x - 1, rp.rect.BottomLeft.z + 1, 1, 1);
        BaseGen.symbolStack.Push("NexomonShipBattery", resolveParams);
        resolveParams.rect = new CellRect(rp.rect.TopRight.x - 3, rp.rect.BottomLeft.z + 1, 1, 1);
        BaseGen.symbolStack.Push("NexomonShipBattery", resolveParams);
        resolveParams.rect = new CellRect(rp.rect.TopRight.x - 1, rp.rect.TopRight.z - 2, 1, 1);
        resolveParams.thingRot = Rot4.South;
        BaseGen.symbolStack.Push("NexomonShipBattery", resolveParams);

        resolveParams.thingRot = Rot4.North;
        resolveParams.rect = new CellRect(rp.rect.TopRight.x - 3, rp.rect.BottomLeft.z + 2, 2, 2);
        BaseGen.symbolStack.Push("NexomonShipGenerator", resolveParams);

        BaseGen.symbolStack.Push("emptyRoom", rp);
        BaseGen.symbolStack.Push("clear", rp);
    }
}