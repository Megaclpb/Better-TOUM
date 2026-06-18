using MiraAPI.GameOptions;
using MiraAPI.GameOptions.Attributes;
using MiraAPI.Utilities;
using TownOfUs.Roles.Neutral;

namespace TownOfUs.Options.Roles.Neutral;

public sealed class SuccubusOptions : AbstractOptionGroup<SuccubusRole>
{
    public override string GroupName => TouLocale.Get("TouRoleSuccubus", "Succubus");

    [ModdedNumberOption("TouOptionSuccubusKillCooldown", 5f, 120f, 2.5f, MiraNumberSuffixes.Seconds)]
    public float KillCooldown { get; set; } = 25f;

    [ModdedNumberOption("TouOptionSuccubusSeduceUses", 1f, 3f, 1f)]
    public float SeduceUses { get; set; } = 25f;

    [ModdedNumberOption("TouOptionSuccubusSeduceCooldown", 5f, 120f, 2.5f, MiraNumberSuffixes.Seconds)]
    public float SeduceCooldown { get; set; } = 25f;

    [ModdedToggleOption("TouOptionSuccubusCanVent")]
    public bool CanVent { get; set; } = true;
}