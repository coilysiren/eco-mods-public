#pragma warning disable IDE0005
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
#pragma warning restore IDE0005

using Eco.Gameplay.DynamicValues;
using Eco.Gameplay.Items;
using Eco.Gameplay.Players;
using Eco.Mods.TechTree;
using Eco.Shared.Localization;

public partial class PlayerDefaults
{
    private static readonly Dictionary<UserStatType, IDynamicValue> dynamicValuesDictionary =
        new()
        {
            {
                UserStatType.MaxCalories,
                new MultiDynamicValue(
                    MultiDynamicOps.Sum,
                    new MultiDynamicValue(
                        MultiDynamicOps.Multiply,
                        CreateSmv(
                            0f,
                            new BonusUnitsDecoratorStrategy(
                                SelfImprovementSkill.AdditiveStrategy,
                                "cal",
                                (float val) => val / 2f
                            ),
                            typeof(SelfImprovementSkill),
                            Localizer.DoStr("stomach capacity"),
                            DynamicValueType.Misc
                        ),
                        new ConstantValue(0.5f)
                    ),
                    new MultiDynamicValue(
                        MultiDynamicOps.Multiply,
                        CreateSmv(
                            0f,
                            new BonusUnitsDecoratorStrategy(
                                ConstructionWorkerSkill.AdditiveStrategy,
                                "cal",
                                (float val) => val / 2f
                            ),
                            typeof(ConstructionWorkerSkill),
                            Localizer.DoStr("stomach capacity"),
                            DynamicValueType.Misc
                        ),
                        new ConstantValue(0.5f)
                    ),
                    new TalentModifiedValue(
                        typeof(UserStatType),
                        typeof(SelfImprovementGluttonTalent),
                        0
                    ),
                    new ConstantValue(3000)
                )
            },
            {
                UserStatType.MaxCarryWeight,
                new MultiDynamicValue(
                    MultiDynamicOps.Sum,
                    CreateSmv(
                        0f,
                        new BonusUnitsDecoratorStrategy(
                            SelfImprovementSkill.AdditiveStrategy,
                            "kg",
                            (float val) => val / 1000f
                        ),
                        typeof(SelfImprovementSkill),
                        Localizer.DoStr("carry weight"),
                        DynamicValueType.Misc
                    ),
                    CreateSmv(
                        0f,
                        new BonusUnitsDecoratorStrategy(
                            ConstructionWorkerSkill.AdditiveStrategy,
                            "kg",
                            (float val) => val / 1000f
                        ),
                        typeof(ConstructionWorkerSkill),
                        Localizer.DoStr("carry weight"),
                        DynamicValueType.Misc
                    ),
                    new TalentModifiedValue(
                        typeof(UserStatType),
                        typeof(SelfImprovementDeeperPocketsTalent),
                        0
                    ),
                    new ConstantValue(ToolbarBackpackInventory.DefaultWeightLimit)
                )
            },
            {
                UserStatType.CalorieRate,
                new MultiDynamicValue(MultiDynamicOps.Sum, new ConstantValue(1))
            },
            {
                UserStatType.DetectionRange,
                new MultiDynamicValue(
                    MultiDynamicOps.Sum,
                    CreateSmv(
                        0f,
                        HuntingSkill.AdditiveStrategy,
                        typeof(HuntingSkill),
                        Localizer.DoStr("how close you can approach animals"),
                        DynamicValueType.Misc
                    ),
                    new ConstantValue(0)
                )
            },
            {
                UserStatType.MovementSpeed,
                new MultiDynamicValue(
                    MultiDynamicOps.Sum,
                    new TalentModifiedValue(
                        typeof(UserStatType),
                        typeof(SelfImprovementNatureAdventurerSpeedTalent),
                        0
                    ),
                    new TalentModifiedValue(
                        typeof(UserStatType),
                        typeof(SelfImprovementUrbanTravellerSpeedTalent),
                        0
                    )
                )
            },
        };

    private static SkillModifiedValue CreateSmv(
        float startValue,
        ModificationStrategy strategy,
        Type skillType,
        LocString benefitsDescription,
        DynamicValueType valueType
    )
    {
        SkillModifiedValue smv =
            new(startValue, strategy, skillType, typeof(Player), benefitsDescription, valueType);
        return smv;
    }

    public static Dictionary<UserStatType, IDynamicValue> GetDefaultDynamicValues()
    {
        return dynamicValuesDictionary;
    }
}
