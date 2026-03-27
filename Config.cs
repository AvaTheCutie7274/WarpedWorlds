using System.Collections.Generic;
using Landfall.Haste;
using UnityEngine;
using UnityEngine.Localization;
using Zorro.Settings;

namespace HasteEffects;

public static class Config
{
    public static List<StatHolder> Stats { get; } = new();

    public static void Initialize()
    {
        if (Stats.Count > 0)
            return;

        Stats.Add(new StatHolder(Stat.AirSpeed, 0.8f, 1.4f));
        Stats.Add(new StatHolder(Stat.Boost, 0.5f, 2.5f));
        Stats.Add(new StatHolder(Stat.DamageMulti, 0.25f, 3.85f));
        Stats.Add(new StatHolder(Stat.Drag, 0.75f, 1.75f));
        Stats.Add(new StatHolder(Stat.EnergyGain, 0.75f, 2.5f));
        Stats.Add(new StatHolder(Stat.FastFall, 0.75f, 3.25f));
        Stats.Add(new StatHolder(Stat.Gravity, 0.5f, 1.5f));
        Stats.Add(new StatHolder(Stat.Luck, 0.15f, 4.85f));
        Stats.Add(new StatHolder(Stat.MaxEnergy, 0.5f, 2.5f));
        Stats.Add(new StatHolder(Stat.MaxHealth, 0.25f, 5f));
        Stats.Add(new StatHolder(Stat.PickupRange, 0.75f, 3.75f));
        Stats.Add(new StatHolder(Stat.RunSpeed, 0.75f, 2.5f));
        Stats.Add(new StatHolder(Stat.SparkMulti, 0.5f, 1.5f));
        Stats.Add(new StatHolder(Stat.TurnSpeed, 0.5f, 2.5f));
    }

    public static int GetMaxEffects()
    {
        try
        {
            if (GameHandler.Instance == null || GameHandler.Instance.SettingsHandler == null)
                return 4;

            return GameHandler.Instance.SettingsHandler
                .GetSetting<MaxModifiersSetting>()
                .Value;
        }
        catch
        {
            return 4;
        }
    }
}

[HasteSetting]
public class MaxModifiersSetting : IntSetting, IExposedSetting
{
    public string GetCategory() => "HasteEffects";

    public LocalizedString GetDisplayName() =>
        new UnlocalizedString("Max Modifiers");

    protected override int GetDefaultValue() => 4;

    public override void ApplyValue()
    {
        Debug.Log($"HasteEffects: Max Modifiers set to {Value}");
    }
}
