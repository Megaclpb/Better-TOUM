using MiraAPI.GameOptions;
using MiraAPI.Modifiers;
using MiraAPI.Utilities.Assets;
using TownOfUs.Modifiers.Impostor;
using TownOfUs.Options;
using TownOfUs.Options.Roles.Impostor;
using TownOfUs.Roles.Impostor;
using UnityEngine;

namespace TownOfUs.Buttons.Impostor;

public sealed class HypnotistHypnotizeButton : TownOfUsRoleButton<HypnotistRole, PlayerControl>,
    IAftermathablePlayerButton
{
    public override string Name => TouLocale.GetParsed("TouRoleHypnotistHypnotize", "Hypnotize");
    public override BaseKeybind Keybind => Keybinds.SecondaryAction;
    public override Color TextOutlineColor => TownOfUsColors.Impostor;
    public override float Cooldown => Math.Clamp(OptionGroupSingleton<HypnotistOptions>.Instance.HypnotiseCooldown + MapCooldown, 5f, 120f);
    public override float EffectDuration => Math.Clamp(OptionGroupSingleton<HypnotistOptions>.Instance.HypnotiseDelay, 0f, 10f);
    public override LoadableAsset<Sprite> Sprite => TouImpAssets.HypnotiseButtonSprite;

    public override bool ZeroIsInfinite { get; set; } = true;
    private PlayerControl? _selectedTarget;

    public override bool Enabled(RoleBehaviour? role)
    {
        return base.Enabled(role) && role is HypnotistRole;
    }

    public void AftermathHandler()
    {
        PlayerControl.LocalPlayer.RpcAddModifier<HypnotisedModifier>(PlayerControl.LocalPlayer);
    }

    protected override void OnClick()
    {
        if (Target == null)
        {
            return;
        }

        _selectedTarget = Target;
    }

    public override void OnEffectEnd()
    {
        if (_selectedTarget == null)
        {
            return;
        }

        _selectedTarget.RpcAddModifier<HypnotisedModifier>(PlayerControl.LocalPlayer);

        _selectedTarget = null;
    }

    public override PlayerControl? GetTarget()
    {
        var isFfa = OptionGroupSingleton<GeneralOptions>.Instance.FFAImpostorMode;
        return PlayerControl.LocalPlayer.GetClosestLivingPlayer(true, Distance, false, x => (isFfa || !x.IsImpostorAligned()) && !x.HasModifier<HypnotisedModifier>(msModifier => msModifier.Hypnotist.AmOwner));
    }
}