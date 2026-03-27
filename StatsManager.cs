using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace HasteEffects;

/// <summary>
/// Holds a single player stat and its min/max range.
/// </summary>
public class StatHolder
{
    private readonly float _defaultMin;
    private readonly float _defaultMax;

    public StatHolder(Stat stat, float defaultMin, float defaultMax)
    {
        Stat = stat;
        _defaultMin = defaultMin;
        _defaultMax = defaultMax;

        MinValue = defaultMin;
        MaxValue = defaultMax;
    }

    public Stat Stat { get; }

    public string Name => Stat.ToString();

    public float MinValue { get; set; }
    public float MaxValue { get; set; }

    public float Min
    {
        get
        {
            if (MinValue >= MaxValue)
            {
                Debug.LogWarning($"{Stat} min value is greater than or equal to max value. Resetting min.");
                MinValue = _defaultMin;
            }

            return MinValue;
        }
    }

    public float Max
    {
        get
        {
            if (MaxValue <= MinValue)
            {
                Debug.LogWarning($"{Stat} max value is less than or equal to min value. Resetting max.");
                MaxValue = _defaultMax;
            }

            return MaxValue;
        }
    }

    public float RandomVal => UnityEngine.Random.Range(Min, Max);

    public PlayerStat PStat
    {
        get
        {
            return (PlayerStat)Player.localPlayer.stats
                .GetType()
                .GetFields(BindingFlags.Public | BindingFlags.Instance)
                .Where(f => f.FieldType == typeof(PlayerStat))
                .First(f => f.Name.Contains(Stat.ToString(), StringComparison.OrdinalIgnoreCase))
                .GetValue(Player.localPlayer.stats);
        }
    }

    public void Reset()
    {
        MinValue = _defaultMin;
        MaxValue = _defaultMax;
    }
}

/// <summary>
/// All supported player stats.
/// </summary>
public enum Stat
{
    MaxHealth,
    RunSpeed,
    AirSpeed,
    TurnSpeed,
    Drag,
    Gravity,
    FastFall,
    Dashes,
    Boost,
    Luck,
    MaxEnergy,
    SparkMulti,
    EnergyGain,
    DamageMulti,
    PickupRange
}
