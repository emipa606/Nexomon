using RimWorld;
using RimWorld.BaseGen;
using Verse;
using Verse.AI.Group;

namespace Nexomon;

public class SymbolResolver_NexomonShip : SymbolResolver
{
    private const int base_offset_x = 7;
    private const int base_offset_z = 0;

    private const int ship_heigth = 14;

    private const int engine_width = 10;

    private const int engines_offset_x = -3;
    private const int engines_offset_y = 2;

    private const int wing_width = 12;
    private const int wing_heigth = ship_heigth / 2;

    private const int cockpit_width = 9;
    private const int cockpit_heigth = 6;

    private static readonly IntRange enemiesCount = new IntRange(4, 6);
    private static readonly IntRange enemiesStayLength = new IntRange(GenDate.TicksPerDay * 5, GenDate.TicksPerDay * 7);
    private static readonly FloatRange enemiesPoints = new FloatRange(1700f, 2100f);

    public override void Resolve(ResolveParams rp)
    {
        if (rp.wallStuff == null)
        {
            rp.wallStuff = RimWorld.ThingDefOf.Steel;
        }

        if (rp.floorDef == null)
        {
            rp.floorDef = BaseGenUtility.CorrespondingTerrainDef(rp.wallStuff, true);
        }

        var faction = Find.FactionManager.RandomEnemyFaction(false, true, false, TechLevel.Spacer);
        var map = BaseGen.globalSettings.map;

        rp.faction = faction;

        var genX = rp.rect.minX + base_offset_x;
        var genZ = rp.rect.minZ + base_offset_z;

        var engineOffsetX = genX + engines_offset_x;
        var thing = ThingMaker.MakeThing(RimWorld.ThingDefOf.Ship_Engine);
        GenSpawn.Spawn(thing, new IntVec3(engineOffsetX, 0, genZ + engines_offset_y), map, Rot4.East);
        thing = ThingMaker.MakeThing(RimWorld.ThingDefOf.Ship_Engine);
        GenSpawn.Spawn(thing, new IntVec3(engineOffsetX, 0, genZ + ship_heigth - 1 - engines_offset_y), map, Rot4.East);

        var resolveParams = rp;

        var cockpitOffsetX = genX + engine_width + wing_width - 2;
        var cockpitOffsetY = genZ + (ship_heigth / 2) - (cockpit_heigth / 2);

        var wingOffsetX = genX + engine_width - 1;

        var ship_width = cockpitOffsetX + cockpit_width - genX;

        var turretOffsetX = cockpitOffsetX + 1;
        var turretOffsetZ1 = genZ - 1;
        var turretOffsetZ2 = genZ + ship_heigth;
        thing = ThingMaker.MakeThing(RimWorld.ThingDefOf.Turret_MiniTurret, RimWorld.ThingDefOf.Steel);
        GenSpawn.Spawn(thing, new IntVec3(turretOffsetX, 0, turretOffsetZ1), map, Rot4.North);
        thing.SetFaction(faction);
        thing = ThingMaker.MakeThing(RimWorld.ThingDefOf.Turret_MiniTurret, RimWorld.ThingDefOf.Steel);
        GenSpawn.Spawn(thing, new IntVec3(turretOffsetX, 0, turretOffsetZ2), map, Rot4.North);
        thing.SetFaction(faction);

        var enemyRP = rp;
        enemyRP.pawnGroupKindDef = PawnGroupKindDefOf.Combat;

        enemyRP.singlePawnLord =
            LordMaker.MakeNewLord(faction, new LordJob_DefendBase(faction, rp.rect.CenterCell), map);
        enemyRP.pawnGroupMakerParams = new PawnGroupMakerParms();
        enemyRP.pawnGroupMakerParams.tile = map.Tile;
        enemyRP.pawnGroupMakerParams.faction = faction;
        enemyRP.pawnGroupMakerParams.points = rp.settlementPawnGroupPoints ?? enemiesPoints.RandomInRange;
        enemyRP.pawnGroupMakerParams.inhabitants = false;
        //enemyRP.pawnGroupMakerParams.seed = rp.settlementPawnGroupSeed;
        BaseGen.symbolStack.Push("pawnGroup", enemyRP);
        /*int enemyNumber = enemiesCount.RandomInRange;
        for (int i = 0; i < enemyNumber; i++)
        {
            enemyRP = rp;
            enemyRP.singlePawnToSpawn = null;
            enemyRP.singlePawnKindDef = RimWorld.PawnKindDefOf.AncientSoldier;
            enemyRP.postThingSpawn = delegate (Thing t)
            {
                Pawn pawn = (t as Pawn);
                if (pawn == null) Log.Error("Nexomon ship enemy pawn is null");
                pawn.mindState.exitMapAfterTick = Find.TickManager.TicksGame + enemiesStayLength.RandomInRange;
            };
            BaseGen.symbolStack.Push("pawn", enemyRP);
        }*/

        resolveParams.rect = new CellRect(genX - 6, genZ - 4, ship_width + 7, ship_heigth + 8);
        BaseGen.symbolStack.Push("NexomonShipDebris", resolveParams);

        resolveParams.rect = new CellRect(genX, genZ, engine_width, ship_heigth);
        BaseGen.symbolStack.Push("NexomonShipEngineRoom", resolveParams);

        resolveParams.rect = new CellRect(cockpitOffsetX, cockpitOffsetY, cockpit_width, cockpit_heigth);
        BaseGen.symbolStack.Push("NexomonShipCockpit", resolveParams);

        resolveParams.rect = new CellRect(wingOffsetX, genZ, wing_width, wing_heigth);
        BaseGen.symbolStack.Push("NexomonShipRightWing", resolveParams);
        resolveParams.rect = new CellRect(wingOffsetX, genZ + wing_heigth, wing_width, wing_heigth);
        BaseGen.symbolStack.Push("NexomonShipLeftWing", resolveParams);
    }
}