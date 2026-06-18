using MiraAPI.GameEnd;
using MiraAPI.Modifiers;
using MiraAPI.Utilities.Assets;
using UnityEngine;
using AmongUs.GameOptions;
using TownOfUs.GameOver;

namespace TownOfUs.Modifiers.Neutral;

public sealed class SuccubusSeducedModifier(PlayerControl succubus) : BaseModifier
{
    public override string ModifierName => TouLocale.Get("Seduced");
    public static bool DoesTasks => false;
    public static bool CountTowardsTrueFaction => false;
    public static RoleBehaviour UnderlyingRole => RoleManager.Instance.GetRole(RoleTypes.Crewmate);
    public bool ForceDisableTasks;
    public override bool HideOnUi => false;
    public override LoadableAsset<Sprite>? ModifierIcon => TouModifierIcons.Seduced;
    public PlayerControl Succubus { get; set; } = succubus;
    public List<CustomButtonWikiDescription> Abilities { get; } = [];

    public bool? DidWin(GameOverReason reason)
    {
        if (reason == CustomGameOver.GameOverReason<NeutralGameOver>())
        {
            return Succubus.Data.Role.DidWin(reason);
        }

        return false;
    }
}