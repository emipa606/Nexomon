using Verse;

namespace Nexomon;

public class NexomonShipLootUtility : Def
{
    private int common;
    private int rare;
    private int uncommon;

    public int GetRarityCount(NexomonShipLootRarity rarity)
    {
        switch (rarity)
        {
            case NexomonShipLootRarity.Common:
                return common;
            case NexomonShipLootRarity.Uncommon:
                return uncommon;
            case NexomonShipLootRarity.Rare:
                return rare;
            default:
                Log.Error("NexomonShipLootUtility try to use non existant rarity");
                break;
        }

        return 0;
    }
}