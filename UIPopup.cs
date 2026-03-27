namespace HasteEffects;

internal class UIPopup
{
	public static void Show(string text)
	{
		var parent = FindUiParent();
		if (parent == null)
		{
			UnityEngine.Debug.LogError("HasteEffects UI Popup: no parent found");
			return;
		}

		var popup = new UnityEngine.GameObject("HasteEffects_Popup");
		popup.transform.SetParent(parent, false);

		var rect = popup.AddComponent<UnityEngine.RectTransform>();
		rect.anchorMin = new UnityEngine.Vector2(0.5f, 1f);
		rect.anchorMax = new UnityEngine.Vector2(0.5f, 1f);
		rect.pivot = new UnityEngine.Vector2(0.5f, 1f);
		rect.anchoredPosition = new UnityEngine.Vector2(0f, -80f);
		rect.sizeDelta = new UnityEngine.Vector2(520f, 56f);

		var bg = popup.AddComponent<UnityEngine.UI.Image>();
		bg.color = new UnityEngine.Color(0f, 0f, 0f, 0.28f);

		AddBorder(popup);

		var textObj = new UnityEngine.GameObject("Text");
		textObj.transform.SetParent(popup.transform, false);

		var textRect = textObj.AddComponent<UnityEngine.RectTransform>();
		textRect.anchorMin = new UnityEngine.Vector2(0f, 0f);
		textRect.anchorMax = new UnityEngine.Vector2(1f, 1f);
		textRect.offsetMin = new UnityEngine.Vector2(12f, 4f);
		textRect.offsetMax = new UnityEngine.Vector2(-12f, -4f);

		var uiText = textObj.AddComponent<UnityEngine.UI.Text>();
		uiText.font = UnityEngine.Resources.GetBuiltinResource<UnityEngine.Font>("Arial.ttf");
		uiText.fontSize = 28;
		uiText.alignment = UnityEngine.TextAnchor.MiddleCenter;
		uiText.color = new UnityEngine.Color(0.3f, 1f, 0.7f, 0.75f);
		uiText.text = text;

		var shadow = textObj.AddComponent<UnityEngine.UI.Shadow>();
		shadow.effectColor = new UnityEngine.Color(0f, 0f, 0f, 0.8f);
		shadow.effectDistance = new UnityEngine.Vector2(1f, -1f);

		var fader = popup.AddComponent<UIPopupFader>();
		fader.Setup(bg, uiText, 1f, 1.5f);
	}

	private static UnityEngine.Transform? FindUiParent()
	{
		var canvas = UnityEngine.Object.FindFirstObjectByType<UnityEngine.Canvas>();
		if (canvas != null)
		{
			UnityEngine.Debug.Log($"HasteEffects UI: using canvas '{canvas.name}'");
			return canvas.transform;
		}

		return null;
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
}

internal class UIPopupFader : UnityEngine.MonoBehaviour
{
	private UnityEngine.UI.Image? _bg;
	private UnityEngine.UI.Text? _text;
	private float _holdTime;
	private float _fadeTime;
	private float _timer;

	private float _bgStartAlpha;
	private float _textStartAlpha;

	public void Setup(UnityEngine.UI.Image bg, UnityEngine.UI.Text text, float holdTime, float fadeTime)
	{
		_bg = bg;
		_text = text;
		_holdTime = holdTime;
		_fadeTime = fadeTime;
		_timer = 0f;

		_bgStartAlpha = bg.color.a;
		_textStartAlpha = text.color.a;
	}

	private void Update()
	{
		_timer += UnityEngine.Time.deltaTime;

		if (_bg == null || _text == null)
		{
			Destroy(gameObject);
			return;
		}

		if (_timer <= _holdTime)
		{
			return;
		}

		float fadeProgress = (_timer - _holdTime) / _fadeTime;
		fadeProgress = UnityEngine.Mathf.Clamp01(fadeProgress);

		var bgColor = _bg.color;
		bgColor.a = UnityEngine.Mathf.Lerp(_bgStartAlpha, 0f, fadeProgress);
		_bg.color = bgColor;

		var textColor = _text.color;
		textColor.a = UnityEngine.Mathf.Lerp(_textStartAlpha, 0f, fadeProgress);
		_text.color = textColor;

		if (fadeProgress >= 1f)
		{
			Destroy(gameObject);
		}
	}
}