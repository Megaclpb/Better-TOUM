using MiraAPI.GameOptions;
using MiraAPI.GameOptions.Attributes;
using MiraAPI.GameOptions.OptionTypes;
using MiraAPI.Utilities;
using TownOfUs.Roles.Impostor;

namespace TownOfUs.Options.Roles.Impostor;

public sealed class RelocatorOptions : AbstractOptionGroup<RelocatorRole>
{
    public override string GroupName => TouLocale.Get("TouRoleRelocator", "Relocator");

    [ModdedNumberOption("TouOptionRelocatorTransportCooldown", 5f, 120f, 2.5f, MiraNumberSuffixes.Seconds)]
    public float RelocatorCooldown { get; set; } = 25f;

    [ModdedNumberOption("TouOptionRelocatorMaxUses", 1f, 15f, 1f, MiraNumberSuffixes.None, "0")]
    public float MaxNumTransports { get; set; } = 5f;

    [ModdedToggleOption("TouOptionRelocatorMoveWithMenu")]
    public bool MoveWithMenu { get; set; } = true;

    [ModdedToggleOption("TouOptionRelocatorCanVent")]
    public bool CanVent { get; set; } = true;

    [ModdedToggleOption("TouOptionRelocatorCanKillWithTeammate")]
    public bool RelocatorKill { get; set; } = true;
}