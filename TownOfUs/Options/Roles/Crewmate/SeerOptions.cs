using MiraAPI.GameOptions;
using MiraAPI.GameOptions.Attributes;
using MiraAPI.GameOptions.OptionTypes;
using MiraAPI.Utilities;
using TownOfUs.Roles.Crewmate;

namespace TownOfUs.Options.Roles.Crewmate;

public sealed class SeerOptions : AbstractOptionGroup<SeerRole>
{
    public override string GroupName => TouLocale.Get("TouRoleSeer", "Seer");

    public ModdedToggleOption SalemSeer { get; set; } = new("TouOptionSeerSalemMode", true);
    
    [ModdedNumberOption("TouOptionSeerCooldown", 5f, 120f, 2.5f, MiraNumberSuffixes.Seconds)]
    public float SeerCooldown { get; set; } = 20f;

    [ModdedNumberOption("TouOptionSeerUses", 0f, 15f, 1f, MiraNumberSuffixes.None, "0", true)]
    public float MaxCompares { get; set; } = 5f;

    public ModdedToggleOption SwapTraitorColors { get; set; } = new("TouOptionSeerTraitorSwapsColors", true)
    {
        Visible = () => !OptionGroupSingleton<SeerOptions>.Instance.SalemSeer
    };
}