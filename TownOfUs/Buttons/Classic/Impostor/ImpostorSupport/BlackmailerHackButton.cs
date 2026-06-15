using MiraAPI.GameOptions;
using MiraAPI.Hud;
using MiraAPI.Modifiers;
using MiraAPI.Utilities;
using MiraAPI.Utilities.Assets;
using TownOfUs.Modifiers.Neutral;
using TownOfUs.Options.Roles.Impostor;
using TownOfUs.Roles.Impostor;
using UnityEngine;

namespace TownOfUs.Buttons.Impostor;

public sealed class BlackmailerHackButton : TownOfUsRoleButton<BlackmailerRole, PlayerControl>, IAftermathablePlayerButton
{
    public override string Name => TouLocale.GetParsed("TouRoleBlackmailerHack", "Hack");
    public override BaseKeybind Keybind => Keybinds.TertiaryAction;
    public override Color TextOutlineColor => TownOfUsColors.Impostor;
    public override float Cooldown => Math.Clamp(OptionGroupSingleton<BlackmailerOptions>.Instance.HackCooldown + MapCooldown, 5f, 120f);
    public override LoadableAsset<Sprite> Sprite => TouNeutAssets.HackSprite;
    public override ButtonLocation Location => ButtonLocation.BottomRight;
    public override bool ShouldPauseInVent => false;

    public override PlayerControl? GetTarget()
    {
        return PlayerControl.LocalPlayer.GetClosestLivingPlayer(true, Distance);
    }

    public void AftermathHandler()
    {
        TouAudio.PlaySound(TouAudio.HackedSound);
        PlayerControl.LocalPlayer.RpcAddModifier<GlitchHackedModifier>(PlayerControl.LocalPlayer.PlayerId);
    }
    protected override void OnClick()
    {
        if (Target == null)
        {
            Error("Blackmailer Hack: Target is null");
            return;
        }

        var notif1 = Helpers.CreateAndShowNotification(
            TouLocale.GetParsed("TouRoleBlackmailerHackNotif").Replace("<player>", $"{TownOfUsColors.Impostor.ToTextColor()}{Target.Data.PlayerName}</color>"),
            Color.white, new Vector3(0f, 1f, -20f), spr: TouRoleIcons.RandomAny.LoadAsset());
        notif1.AdjustNotification();

        TouAudio.PlaySound(TouAudio.HackedSound);
        Target.RpcAddModifier<GlitchHackedModifier>(PlayerControl.LocalPlayer.PlayerId);
    }
}