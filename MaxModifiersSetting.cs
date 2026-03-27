using UnityEngine;
using UnityEngine.Localization;
using Zorro.Settings;

namespace HasteEffects;

[HasteSetting]
public class MaxModifiersSetting : IntSetting, IExposedSetting
{
    public string GetCategory() => "HasteEffects";

    public LocalizedString GetDisplayName() =>
        new UnlocalizedString("Max Modifiers");

    protected override int GetDefaultValue() => 4;

    public override void ApplyValue()
    {
        Debug.Log($"HasteEffects: Max Modifiers = {Value}");
    }
}
