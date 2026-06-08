using MiraAPI.GameOptions;
using MiraAPI.GameOptions.Attributes;
using MiraAPI.Utilities;
using TownOfUs.Roles.Impostor;

namespace TownOfUs.Options.Roles.Impostor;

public sealed class HypnotistOptions : AbstractOptionGroup<HypnotistRole>
{
    public override string GroupName => TouLocale.Get("TouRoleHypnotist", "Hypnotist");

    [ModdedNumberOption("Hypnotize Cooldown", 5f, 120f, 2.5f, MiraNumberSuffixes.Seconds)]
    public float HypnotiseCooldown { get; set; } = 25f;

    [ModdedNumberOption("Hypnotize Delay", 0.5f, 5f, 0.5f, MiraNumberSuffixes.Seconds)]
    public float HypnotiseDelay{ get; set; } = 3f;

    [ModdedNumberOption("Hypnotize Duration", 5f, 30f, 2.5f, MiraNumberSuffixes.Seconds)]
    public float HypnotiseDuration { get; set; } = 15f;

    [ModdedToggleOption("Hypnotist Can Kill With Teammate")]
    public bool HypnoKill { get; set; } = true;
}