namespace HasteEffects;

using SettingsLib;

/// <summary>
/// Holds a single player stat, including UI and settings
/// </summary>
public class StatHolder
{
	private readonly HastyCollapsible _hastyCol;

	public StatHolder(Stat stat, HastySetting cfg, Unity.Mathematics.float2 defaultMinMax, bool enabled = true)
	{
		Stat = stat;

		_hastyCol = new HastyCollapsible(cfg, stat.ToString(), $"Settings for <b>{stat}</b>");

		HastyFloatMin = new HastyFloat(_hastyCol, stat.ToString(), "Minimum", new(0, 5, defaultMinMax.x));
		HastyFloatMax = new HastyFloat(_hastyCol, stat.ToString(), "Maximum", new(0, 5, defaultMinMax.y));
		HastyBoolEnabled = new HastyBool(_hastyCol, stat.ToString(), "Enable or disable this stat", new("Disabled", "Enabled", enabled));
		HastyButtonReset = new HastyButton(_hastyCol, "Reset", $"Reset <b>{stat}</b>'s stats", new() { ButtonText = "Reset", OnClicked = Reset });
	}

	public HastyBool HastyBoolEnabled { get; private set; }
	public HastyButton HastyButtonReset { get; private set; }
	public HastyCollapsible HastyCol => _hastyCol;
	public HastyFloat HastyFloatMax { get; private set; }
	public HastyFloat HastyFloatMin { get; private set; }

	public float Max
	{
		get
		{
			if (HastyFloatMax.Value <= HastyFloatMin.Value)
			{
				HastyFloatMax.Reset();
				return HastyFloatMax.Value;
			}
			return HastyFloatMax.Value;
		}
	}

	public float Min
	{
		get
		{
			if (HastyFloatMin.Value >= HastyFloatMax.Value)
			{
				HastyFloatMin.Reset();
				return HastyFloatMin.Value;
			}
			return HastyFloatMin.Value;
		}
	}

	public string Name => Stat.ToString();

	public PlayerStat PStat
	{
		get
		{
			return (PlayerStat)Player.localPlayer.stats.GetType()
				.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
				.Where(f => f.FieldType == typeof(PlayerStat))
				.FirstOrDefault(f => f.Name.Contains(Stat.ToString(), StringComparison.OrdinalIgnoreCase))
				.GetValue(Player.localPlayer.stats);
		}
	}

	public float RandomVal => UnityEngine.Random.Range(Min, Max);

	public Stat Stat { get; private set; }

	public void Reset()
	{
		HastyFloatMax.Reset();
		HastyFloatMin.Reset();
		HastyBoolEnabled.Reset();
	}
}

public enum Stat
{
	MaxHealth,
	RunSpeed,
	AirSpeed,
	TurnSpeed,
	Drag,
	Gravity,
	FastFall,
	Boost,
	Luck,
	MaxEnergy,
	SparkMulti,
	EnergyGain,
	DamageMulti,
	PickupRange
}
