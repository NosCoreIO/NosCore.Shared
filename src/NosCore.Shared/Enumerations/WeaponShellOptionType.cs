//  __  _  __    __   ___ __  ___ ___
// |  \| |/__\ /' _/ / _//__\| _ \ __|
// | | ' | \/ |`._`.| \_| \/ | v / _|
// |_|\__|\__/ |___/ \__/\__/|_|_\___|
// -----------------------------------

namespace NosCore.Shared.Enumerations
{
    // The option a weapon shell carries, numbered as the client names them.
    public enum WeaponShellOptionType : byte
    {
        EnhancedDamage = 1,
        IncreasedDamage = 2,
        MinorBleeding = 3,
        Bleeding = 4,
        HeavyBleeding = 5,
        Blackout = 6,
        Freeze = 7,
        DeadlyBlackout = 8,
        IncreasedDamageToPlants = 9,
        IncreasedDamageToAnimals = 10,
        IncreasedDamageToMonsters = 11,
        IncreasedDamageToUndead = 12,
        IncreasedDamageToSmallAnimals = 13,
        IncreasedDamageToMapBosses = 14,
        IncreasedCriticalChance = 15,
        IncreasedCriticalDamage = 16,
        UndisturbedWhenCasting = 17,
        IncreasedFireElement = 18,
        IncreasedWaterElement = 19,
        IncreasedLightElement = 20,
        IncreasedShadowElement = 21,
        IncreasedAllElements = 22,
        ReducedMpConsumption = 23,
        HpRecoveryPerKill = 24,
        MpRecoveryPerKill = 25,
        IncreasedSlDamage = 26,
        IncreasedSlDefence = 27,
        IncreasedSlElement = 28,
        IncreasedSlEnergy = 29,
        IncreasedSlAll = 30,
        IncreasedGold = 31,
        IncreasedCombatExperience = 32,
        IncreasedJobExperience = 33,
        PvpIncreasedDamage = 34,
        PvpReducedEnemyDefence = 35,
        PvpReducedEnemyFireResistance = 36,
        PvpReducedEnemyWaterResistance = 37,
        PvpReducedEnemyLightResistance = 38,
        PvpReducedEnemyShadowResistance = 39,
        PvpReducedEnemyAllResistances = 40,
        PvpAlwaysHit = 41,
        PvpDamageAtFifteenPercent = 42,
        PvpDrainMp = 43,
        PvpIgnoreFireResistance = 44,
        PvpIgnoreWaterResistance = 45,
        PvpIgnoreLightResistance = 46,
        PvpIgnoreShadowResistance = 47,
        SpRecoveryPerKill = 48
    }
}
