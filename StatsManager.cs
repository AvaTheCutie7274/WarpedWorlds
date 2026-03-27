namespace HasteEffects;

/// <summary>
/// Holds a single player stat and its min/max/enabled settings.
/// </summary>
public class StatHolder
{
	private readonly float _defaultMin;
	private readonly float _defaultMax;
	private readonly bool _defaultEnabled;

	public StatHolder(Stat stat, float defaultMin, float defaultMax, bool enabled = true)
	{
		Stat = stat;
		_defaultMin = defaultMin;
		_defaultMax = defaultMax;
		_defaultEnabled = enabled;

		MinValue = defaultMin;
		MaxValue = defaultMax;
		Enabled = enabled;
	}

	public bool Enabled { get; set; }

	public float Max
	{
		get
		{
			if (MaxValue <= MinValue)
			{
				UnityEngine.Debug.LogWarning($"{Stat} max value is less than or equal to min value. Resetting max.");
				MaxValue = _defaultMax;
			}
			return MaxValue;
		}
	}

	public float MaxValue { get; set; }

	public float Min
	{
		get
		{
			if (MinValue >= MaxValue)
			{
				UnityEngine.Debug.LogWarning($"{Stat} min value is greater than or equal to max value. Resetting min.");
				MinValue = _defaultMin;
			}
			return MinValue;
		}
	}

	public float MinValue { get; set; }

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

	public Stat Stat { get; }

	public void Reset()
	{
		MinValue = _defaultMin;
		MaxValue = _defaultMax;
		Enabled = _defaultEnabled;
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