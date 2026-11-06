//  __  _  __    __   ___ __  ___ ___
// |  \| |/__\ /' _/ / _//__\| _ \ __|
// | | ' | \/ |`._`.| \_| \/ | v / _|
// |_|\__|\__/ |___/ \__/\__/|_|_\___|
// -----------------------------------

namespace NosCore.Shared.Enumerations
{
    // The option an armour shell carries, numbered as the client names them.
    public enum ArmorShellOptionType : byte
    {
        EnhancedMeleeDefence = 1,
        EnhancedRangedDefence = 2,
        EnhancedMagicDefence = 3,
        IncreasedOverallDefence = 4,
        ReducedMinorBleeding = 5,
        ReducedBleeding = 6,
        ReducedAllBleeding = 7,
        ReducedBlackout = 8,
        ReducedAllBlackouts = 9,
        ReducedHandOfDeath = 10,
        ReducedFreeze = 11,
        ReducedBlind = 12,
        ReducedBind = 13,
        ReducedWeakenDefence = 14,
        ReducedShock = 15,
        ReducedParalysis = 16,
        ReducedAllNegativeEffects = 17,
        IncreasedHpRecoveryWhileResting = 18,
        IncreasedNaturalHpRecovery = 19,
        IncreasedMpRecoveryWhileResting = 20,
        IncreasedNaturalMpRecovery = 21,
        RecoverDamageTakenAsHp = 22,
        ReducedCriticalChanceReceived = 23,
        IncreasedFireResistance = 24,
        IncreasedWaterResistance = 25,
        IncreasedLightResistance = 26,
        IncreasedShadowResistance = 27,
        IncreasedAllResistances = 28,
        ReducedDignityLoss = 29,
        ReducedProductionPointConsumption = 30,
        IncreasedMinigameRewards = 31,
        IncreasedItemRecovery = 32,
        PvpIncreasedOverallDefence = 33,
        PvpDodgeMelee = 34,
        PvpDodgeRanged = 35,
        PvpDodgeMagic = 36,
        PvpDodgeAll = 37,
        PvpProtectionAgainstManaDamage = 38,
        PvpImmuneToFire = 39,
        PvpImmuneToWater = 40,
        PvpImmuneToLight = 41,
        PvpImmuneToShadow = 42
    }
}
