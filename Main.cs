namespace HasteEffects;

[Landfall.Modding.LandfallPlugin]
public class HasteEffects
{
	private static readonly List<UIStats> stats = new();
	private static readonly List<ActiveModifier> activeModifiers = new();

	private static bool initializedThisRun = false;
	private static bool sceneHookInstalled = false;

	static HasteEffects()
	{
		_ = new Config();

		InstallSceneHooks();

		On.Player.Start += (orig, self) =>
		{
			orig(self);

			if (!IsRun || Player.localPlayer == null)
				return;

			try
			{
				if (!initializedThisRun)
				{
					activeModifiers.Clear();
					initializedThisRun = true;
					UnityEngine.Debug.Log("HasteEffects: run initialized");
				}

				AddModifierForFragment();
				ApplyActiveModifiers();
				RebuildUI();
			}
			catch (System.Exception ex)
			{
				UnityEngine.Debug.LogError($"HasteEffects error: {ex}");
			}
		};
	}

	private static void InstallSceneHooks()
	{
		if (sceneHookInstalled)
			return;

		sceneHookInstalled = true;

		UnityEngine.SceneManagement.SceneManager.sceneLoaded += (scene, mode) =>
		{
			UnityEngine.Debug.Log($"HasteEffects: sceneLoaded name='{scene.name}' build={scene.buildIndex}");

			// Clear when returning to places outside a run
			if (scene.name == "FullHub" || scene.name == "MainMenu")
			{
				ClearModifiers();
			}
		};
	}


	public static void ForceAddModifier(Stat stat, float value)
	{
		try
		{
			if (stat == Stat.Dashes)
			{
				UnityEngine.Debug.Log("HasteEffects DEBUG: Dashes modifier has been removed and cannot be added.");
				return;
			}

			var statHolder = Config.statsHolder.FirstOrDefault(s => s.Stat == stat);
			if (statHolder == null)
			{
				UnityEngine.Debug.LogError($"HasteEffects: Stat {stat} not found");
				return;
			}

			activeModifiers.RemoveAll(m => m.Stat == stat);

			activeModifiers.Add(new ActiveModifier(stat, value));

			statHolder.PStat.multiplier = value;

			UnityEngine.Debug.Log($"HasteEffects DEBUG: Forced {stat} = {value:0.00}x");

			UIPopup.Show($"+ {stat}: {value:0.0}x");

			RebuildUI();
		}
		catch (System.Exception ex)
		{
			UnityEngine.Debug.LogError($"HasteEffects DEBUG error: {ex}");
		}
	}

	internal static bool IsRun
	{
		get
		{
			var curScn = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
			return
				(curScn.name.Contains("challenge", System.StringComparison.OrdinalIgnoreCase) && Config.ChallengeLevels.Value) ||
				(curScn.buildIndex == 27 && Config.BossLevels.Value) ||
				curScn.buildIndex == 7;
		}
	}

	private static void AddModifierForFragment()
	{
		var availableStats = Config.statsHolder
			.Where(s => s.Enabled)
			.Where(s => s.Stat != Stat.Dashes)
			.Where(s => activeModifiers.All(a => a.Stat != s.Stat))
			.ToList();

		if (!availableStats.Any())
			return;

		int count = (int)Config.MaxEffects.Value;

		for (int i = 0; i < count && availableStats.Count > 0; i++)
		{
			var stat = availableStats[UnityEngine.Random.Range(0, availableStats.Count)];
			availableStats.Remove(stat);

			if (UnityEngine.Random.Range(0f, 1f) <= Config.FailChance.Value)
				continue;

			var value = stat.RandomVal;
			activeModifiers.Add(new ActiveModifier(stat.Stat, value));
			UnityEngine.Debug.Log($"HasteEffects: added {stat.Stat} = {value:0.00}x");
			UIPopup.Show($"+ {stat.Stat}: {value:0.0}x");
		}
	}

	private static void ApplyActiveModifiers()
	{
		foreach (var mod in activeModifiers)
		{
			var statHolder = Config.statsHolder.FirstOrDefault(s => s.Stat == mod.Stat);
			if (statHolder == null)
				continue;

			statHolder.PStat.multiplier = mod.Multiplier;
			UnityEngine.Debug.Log($"HasteEffects: applied {mod.Stat} = {mod.Multiplier:0.00}x");
		}
	}

	private static void RebuildUI()
	{
		stats.ForEach(s => s.Destroy());
		stats.Clear();

		for (int i = 0; i < activeModifiers.Count; i++)
		{
			var mod = activeModifiers[i];
			stats.Add(new UIStats($"{mod.Stat}: {mod.Multiplier:0.0}x", i));
		}

		UnityEngine.Debug.Log($"HasteEffects: UI rebuilt with {activeModifiers.Count} modifiers");
	}

	private static void ClearModifiers()
	{
		activeModifiers.Clear();
		stats.ForEach(s => s.Destroy());
		stats.Clear();
		initializedThisRun = false;

		UnityEngine.Debug.Log("HasteEffects: cleared modifiers");
	}

	private readonly struct ActiveModifier
	{
		public ActiveModifier(Stat stat, float multiplier)
		{
			Stat = stat;
			Multiplier = multiplier;
		}

		public Stat Stat { get; }
		public float Multiplier { get; }
	}
}