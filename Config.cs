namespace HasteEffects;

public class Config
{
	public Config(HastySetting cfg)
	{
		BossLevels = new HastyBool(cfg, "General", "Boss Levels", new("Disabled", "Enabled", false));
		ChallengeLevels = new HastyBool(cfg, "General", "Challenge Levels", new("Disabled", "Enabled", true));
		FailChance = new HastyFloat(cfg, "General", "Fail Chance", new(0f, 1f, 0.15f));
		MaxEffects = new HastyFloat(cfg, "General", "Modifiers Per Fragment", new(1f, 10f, 4f));

		if (statsHolder.Count == 0)
		{
			statsHolder.Add(new(Stat.AirSpeed, cfg, new Unity.Mathematics.float2(0.8f, 1.4f)));
			statsHolder.Add(new(Stat.Boost, cfg, new Unity.Mathematics.float2(0.5f, 2.5f)));
			statsHolder.Add(new(Stat.DamageMulti, cfg, new Unity.Mathematics.float2(0.25f, 3.85f)));
			statsHolder.Add(new(Stat.Drag, cfg, new Unity.Mathematics.float2(0.75f, 1.75f)));
			statsHolder.Add(new(Stat.EnergyGain, cfg, new Unity.Mathematics.float2(0.75f, 2.5f)));
			statsHolder.Add(new(Stat.FastFall, cfg, new Unity.Mathematics.float2(0.75f, 3.25f)));
			statsHolder.Add(new(Stat.Gravity, cfg, new Unity.Mathematics.float2(0.5f, 1.5f)));
			statsHolder.Add(new(Stat.Luck, cfg, new Unity.Mathematics.float2(0.15f, 4.85f)));
			statsHolder.Add(new(Stat.MaxEnergy, cfg, new Unity.Mathematics.float2(0.5f, 2.5f)));
			statsHolder.Add(new(Stat.MaxHealth, cfg, new Unity.Mathematics.float2(0.25f, 5f)));
			statsHolder.Add(new(Stat.PickupRange, cfg, new Unity.Mathematics.float2(0.75f, 3.75f)));
			statsHolder.Add(new(Stat.RunSpeed, cfg, new Unity.Mathematics.float2(0.75f, 2.5f)));
			statsHolder.Add(new(Stat.SparkMulti, cfg, new Unity.Mathematics.float2(0.5f, 1.5f)));
			statsHolder.Add(new(Stat.TurnSpeed, cfg, new Unity.Mathematics.float2(0.5f, 2.5f)));
		}
	}

	public static HastyBool BossLevels { get; private set; } = null!;
	public static HastyBool ChallengeLevels { get; private set; } = null!;
	public static HastyFloat FailChance { get; private set; } = null!;
	public static HastyFloat MaxEffects { get; private set; } = null!;
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
