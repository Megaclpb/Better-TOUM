using AmongUs.GameOptions;
using Il2CppInterop.Runtime.Attributes;
using MiraAPI.GameOptions;
using MiraAPI.Modifiers;
using MiraAPI.Patches.Stubs;
using MiraAPI.Roles;
using TownOfUs.Modules;
using TownOfUs.Options.Roles.Impostor;
using TownOfUs.Roles.Crewmate;
using UnityEngine;

namespace TownOfUs.Roles.Impostor;

public sealed class HypnotistRole(IntPtr cppPtr)
    : ImpostorRole(cppPtr), ITouImpRole, IWikiDiscoverable, IDoomable, ICrewVariant
{
    private MeetingMenu meetingMenu;

    public void FixedUpdate()
    {
        if (!Player || Player.Data.Role is not HypnotistRole || Player.HasDied() || !Player.AmOwner ||
            MeetingHud.Instance || (!HudManager.Instance.UseButton.isActiveAndEnabled &&
                                    !HudManager.Instance.PetButton.isActiveAndEnabled))
        {
            return;
        }

        HudManager.Instance.KillButton.ToggleVisible(OptionGroupSingleton<HypnotistOptions>.Instance.HypnoKill ||
                                                     (Player != null && Player.GetModifiers<BaseModifier>()
                                                         .Any(x => x is ICachedRole)) ||
                                                     (Player != null && MiscUtils.ImpAliveCount == 1));
    }

    public RoleBehaviour CrewVariant => RoleManager.Instance.GetRole((RoleTypes)RoleId.Get<LookoutRole>());
    public DoomableType DoomHintType => DoomableType.Fearmonger;
    public string LocaleKey => "Hypnotist";
    public string RoleName => TouLocale.Get($"TouRole{LocaleKey}");
    public string RoleDescription => TouLocale.GetParsed($"TouRole{LocaleKey}IntroBlurb");
    public string RoleLongDescription => TouLocale.GetParsed($"TouRole{LocaleKey}TabDescription");

    public string GetAdvancedDescription()
    {
        return
            TouLocale.GetParsed($"TouRole{LocaleKey}WikiDescription").Replace("<symbol>", "<color=#D53F42>@</color>") +
            MiscUtils.AppendOptionsText(GetType());
    }

    public Color RoleColor => TownOfUsColors.Impostor;
    public ModdedRoleTeams Team => ModdedRoleTeams.Impostor;
    public RoleAlignment RoleAlignment => RoleAlignment.ImpostorSupport;

    public CustomRoleConfiguration Configuration => new(this)
    {
        UseVanillaKillButton = true,
        OptionsScreenshot = TouBanners.ImpostorRoleBanner,
        Icon = TouRoleIcons.Hypnotist
    };



    [HideFromIl2Cpp]
    public List<CustomButtonWikiDescription> Abilities
    {
        get
        {
            return new List<CustomButtonWikiDescription>
            {
                new(TouLocale.GetParsed($"TouRole{LocaleKey}Hypnotize", "Hypnotize"),
                    TouLocale.GetParsed($"TouRole{LocaleKey}HypnotizeWikiDescription"),
                    TouImpAssets.HypnotiseButtonSprite),
            };
        }
    }

    public override void OnVotingComplete()
    {
        RoleBehaviourStubs.OnVotingComplete(this);

        if (Player.AmOwner)
        {
            meetingMenu.HideButtons();
        }
    }

    public override void Deinitialize(PlayerControl targetPlayer)
    {
        RoleBehaviourStubs.Deinitialize(this, targetPlayer);
        TouRoleUtils.ClearTaskHeader(Player);

        if (Player.AmOwner)
        {
            meetingMenu?.Dispose();
            meetingMenu = null!;
        }
    }

    public bool IsExempt(PlayerVoteArea voteArea)
    {
        return voteArea?.TargetPlayerId != Player.PlayerId;
    }
}