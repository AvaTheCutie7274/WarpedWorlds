namespace HasteEffects;

public class Config
{
	public Config()
	{
		if (statsHolder.Count == 0)
		{
			statsHolder.Add(new(Stat.AirSpeed, 0.8f, 1.4f));
			statsHolder.Add(new(Stat.Boost, 0.5f, 2.5f));
			statsHolder.Add(new(Stat.DamageMulti, 0.25f, 3.85f));
			statsHolder.Add(new(Stat.Drag, 0.75f, 1.75f));
			statsHolder.Add(new(Stat.EnergyGain, 0.75f, 2.5f));
			statsHolder.Add(new(Stat.FastFall, 0.75f, 3.25f));
			statsHolder.Add(new(Stat.Gravity, 0.5f, 1.5f));
			statsHolder.Add(new(Stat.Luck, 0.15f, 4.85f));
			statsHolder.Add(new(Stat.MaxEnergy, 0.5f, 2.5f));
			statsHolder.Add(new(Stat.MaxHealth, 0.25f, 5f));
			statsHolder.Add(new(Stat.PickupRange, 0.75f, 3.75f));
			statsHolder.Add(new(Stat.RunSpeed, 0.75f, 2.5f));
			statsHolder.Add(new(Stat.SparkMulti, 0.5f, 1.5f));
			statsHolder.Add(new(Stat.TurnSpeed, 0.5f, 2.5f));
		}
	}

	public static SimpleBool BossLevels { get; } = new(false);
	public static SimpleBool ChallengeLevels { get; } = new(true);
	public static SimpleFloat FailChance { get; } = new(0.15f);
	public static SimpleFloat MaxEffects { get; } = new(4f);
	public static List<StatHolder> statsHolder { get; } = new();
}

public class SimpleBool
{
	private readonly bool _defaultValue;

	public SimpleBool(bool defaultValue)
	{
		_defaultValue = defaultValue;
		Value = defaultValue;
	}

	public bool Value { get; set; }

	public void Reset() => Value = _defaultValue;
}

public class SimpleFloat
{
	private readonly float _defaultValue;

	public SimpleFloat(float defaultValue)
	{
		_defaultValue = defaultValue;
		Value = defaultValue;
	}

	public float Value { get; set; }

	public void Reset() => Value = _defaultValue;
}