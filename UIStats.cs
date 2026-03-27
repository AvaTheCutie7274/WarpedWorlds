namespace HasteEffects;

internal class UIStats
{
	public UnityEngine.GameObject? InfoObject;

	public UIStats(string text, int shift)
	{
		var parent = FindUiParent();
		if (parent == null)
		{
			UnityEngine.Debug.LogError("HasteEffects UIStats: no parent found");
			return;
		}

		// Root object
		InfoObject = new UnityEngine.GameObject($"HasteEffects_{shift}");
		InfoObject.transform.SetParent(parent, false);

		var rect = InfoObject.AddComponent<UnityEngine.RectTransform>();
		rect.anchorMin = new UnityEngine.Vector2(0f, 1f);
		rect.anchorMax = new UnityEngine.Vector2(0f, 1f);
		rect.pivot = new UnityEngine.Vector2(0f, 1f);

		// Position (left side list)
		rect.anchoredPosition = new UnityEngine.Vector2(20f, -220f - (38f * shift));
		rect.sizeDelta = new UnityEngine.Vector2(320f, 32f);

		// Background
		var bg = InfoObject.AddComponent<UnityEngine.UI.Image>();
		bg.color = new UnityEngine.Color(0f, 0f, 0f, 0.45f);

		AddBorder(InfoObject);

		// Text object
		var textObj = new UnityEngine.GameObject("Text");
		textObj.transform.SetParent(InfoObject.transform, false);

		var textRect = textObj.AddComponent<UnityEngine.RectTransform>();
		textRect.anchorMin = new UnityEngine.Vector2(0f, 0f);
		textRect.anchorMax = new UnityEngine.Vector2(1f, 1f);
		textRect.offsetMin = new UnityEngine.Vector2(10f, 2f);
		textRect.offsetMax = new UnityEngine.Vector2(-10f, -2f);

		var uiText = textObj.AddComponent<UnityEngine.UI.Text>();
		uiText.font = UnityEngine.Resources.GetBuiltinResource<UnityEngine.Font>("Arial.ttf");
		uiText.fontSize = 20;
		uiText.alignment = UnityEngine.TextAnchor.MiddleLeft;
		uiText.color = new UnityEngine.Color(0.3f, 1f, 0.7f);
		uiText.text = text;

		var shadow = textObj.AddComponent<UnityEngine.UI.Shadow>();
		shadow.effectColor = new UnityEngine.Color(0f, 0f, 0f, 0.8f);
		shadow.effectDistance = new UnityEngine.Vector2(1f, -1f);
	}

	private static UnityEngine.Transform? FindUiParent()
	{
		string[] paths =
		{
			"GAME/UI_Gameplay/LeftCorner",
			"GAME/UI_Gameplay",
			"UI_Gameplay",
			"GAME/UI",
			"UI"
		};

		foreach (var path in paths)
		{
			var go = UnityEngine.GameObject.Find(path);
			if (go != null)
				return go.transform;
		}

		var canvas = UnityEngine.Object.FindFirstObjectByType<UnityEngine.Canvas>();
		return canvas != null ? canvas.transform : null;
	}

	private static void AddBorder(UnityEngine.GameObject parent)
	{
		var borderObj = new UnityEngine.GameObject("Border");
		borderObj.transform.SetParent(parent.transform, false);

		var rect = borderObj.AddComponent<UnityEngine.RectTransform>();
		rect.anchorMin = new UnityEngine.Vector2(0f, 0f);
		rect.anchorMax = new UnityEngine.Vector2(1f, 1f);
		rect.offsetMin = new UnityEngine.Vector2(0f, 0f);
		rect.offsetMax = new UnityEngine.Vector2(0f, 0f);

		var outline = borderObj.AddComponent<UnityEngine.UI.Outline>();
		outline.effectColor = new UnityEngine.Color(0.3f, 1f, 0.7f, 0.9f);
		outline.effectDistance = new UnityEngine.Vector2(1.5f, -1.5f);
	}

	public void Destroy()
	{
		if (InfoObject != null)
		{
			UnityEngine.GameObject.Destroy(InfoObject);
		}
	}
}