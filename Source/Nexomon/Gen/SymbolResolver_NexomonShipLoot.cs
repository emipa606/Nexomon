using System.Collections.Generic;
using System.Linq;
using RimWorld.BaseGen;
using Verse;

namespace Nexomon;

public class SymbolResolver_NexomonShipLoot : SymbolResolver
{
    private static NexomonShipLootUtility utility;

    public static NexomonShipLootUtility Utility
    {
        get
        {
            if (utility != null)
            {
                return utility;
            }

            if (DefDatabase<NexomonShipLootUtility>.DefCount == 1)
            {
                utility = DefDatabase<NexomonShipLootUtility>.AllDefsListForReading[0];
            }
            else if (DefDatabase<NexomonShipLootUtility>.DefCount > 1)
            {
                Log.Error("You have more then 1 NexomonShipLootUtility you should only have 1");
            }
            else
            {
                Log.Error("NexomonShipLootUtility not found in DefDatabase");
            }

            return utility;
        }
    }

    public override void Resolve(ResolveParams rp)
    {
        var cells = rp.rect.Cells.ToList().ListFullCopy();
        int i;
        var commonLoot =
            DefDatabase<NexomonShipLootDef>.AllDefsListForReading.FindAll(l =>
                l.rarity == NexomonShipLootRarity.Common);
        var uncommonLoot =
            DefDatabase<NexomonShipLootDef>.AllDefsListForReading.FindAll(l =>
                l.rarity == NexomonShipLootRarity.Uncommon);
        var rareLoot =
            DefDatabase<NexomonShipLootDef>.AllDefsListForReading.FindAll(l => l.rarity == NexomonShipLootRarity.Rare);
        for (i = 0; i < Utility.GetRarityCount(NexomonShipLootRarity.Common); i++)
        {
            SpawnLoot(ref commonLoot, ref cells);
        }

        for (i = 0; i < Utility.GetRarityCount(NexomonShipLootRarity.Uncommon); i++)
        {
            SpawnLoot(ref uncommonLoot, ref cells);
        }

        for (i = 0; i < Utility.GetRarityCount(NexomonShipLootRarity.Rare); i++)
        {
            SpawnLoot(ref rareLoot, ref cells);
        }
    }

    private void SpawnLoot(ref List<NexomonShipLootDef> loots, ref List<IntVec3> cells)
    {
        var loot = loots.RandomElement();
        loots.Remove(loot);
        var cell = cells.RandomElement();
        cells.Remove(cell);
        var thing = ThingMaker.MakeThing(loot.thing);
        thing.stackCount = loot.quantity.RandomInRange;
        GenSpawn.Spawn(thing, cell, BaseGen.globalSettings.map, Rot4.North);
    }
}