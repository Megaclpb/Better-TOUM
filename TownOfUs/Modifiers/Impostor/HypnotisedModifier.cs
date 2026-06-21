using MiraAPI.GameOptions;
using MiraAPI.Modifiers.Types;
using MiraAPI.Utilities;
using TownOfUs.Options.Roles.Impostor;
using UnityEngine;
using Reactor.Utilities.Extensions;
using TownOfUs.Patches;
using TownOfUs.Utilities.Appearances;

namespace TownOfUs.Modifiers.Impostor;

public sealed class HypnotisedModifier(PlayerControl hypnotist) : TimedModifier
{
    public override string ModifierName => "Hypnotised";
    public override float Duration => OptionGroupSingleton<HypnotistOptions>.Instance.HypnotiseDuration;
    public override bool AutoStart => true;
    public override bool HideOnUi => true;
    public PlayerControl Hypnotist { get; } = hypnotist;

    public override void OnActivate()
    {
        if (Player.AmOwner/* || Herbalist.AmOwner*/)
        {
            List<string> hats = new();
            List<string> skins = new();
            List<string> visors = new();
            List<string> pets = new();
            List<int> colors = new();
            foreach (var plr in Helpers.GetAlivePlayers())
            {
                hats.Add(plr.Data.DefaultOutfit.HatId);
                skins.Add(plr.Data.DefaultOutfit.SkinId);
                visors.Add(plr.Data.DefaultOutfit.VisorId);
                pets.Add(plr.Data.DefaultOutfit.PetId);
                colors.Add(plr.Data.DefaultOutfit.ColorId);
            }

            foreach (var plr in Helpers.GetAlivePlayers())
            {
                var randomSize = UnityEngine.Random.RandomRangeInt(3, 5) * 0.2f;
                var morph = new VisualAppearance(Player.GetDefaultAppearance(), TownOfUsAppearances.Morph)
                {
                    HatId = hats.Random(),
                    SkinId = skins.Random(),
                    VisorId = visors.Random(),
                    PetId = pets.Random(),
                    ColorId = colors.Random(),
                    NameColor = Color.clear,
                    ColorBlindTextColor = Color.clear,
                    Size = new Vector3(randomSize, randomSize, 1f)
                };

                plr.RawSetAppearance(morph);

                plr.cosmetics.ToggleNameVisible(false);
            }
        }
    }

    public override void OnDeath(DeathReason reason)
    {
        ModifierComponent?.RemoveModifier(this);
    }

    public override void OnMeetingStart()
    {
        ModifierComponent?.RemoveModifier(this);
    }

    public override void OnDeactivate()
    {
        foreach (var player in Helpers.GetAlivePlayers())
        {
            player.ResetAppearance();
            player.cosmetics.ToggleNameVisible(true);

            if (HudManagerPatches.CamouflageCommsEnabled)
            {
                player.cosmetics.ToggleNameVisible(false);
            }

            if (VanillaSystemCheckPatches.ShroomSabotageSystem && VanillaSystemCheckPatches.ShroomSabotageSystem.IsActive)
            {
                MushroomMixUp(VanillaSystemCheckPatches.ShroomSabotageSystem, player);
            }
        }
    }

    public static void MushroomMixUp(MushroomMixupSabotageSystem instance, PlayerControl player)
    {
        if (player != null && !player.Data.IsDead && instance.currentMixups.ContainsKey(player.PlayerId))
        {
            var condensedOutfit = instance.currentMixups[player.PlayerId];
            var playerOutfit = instance.ConvertToPlayerOutfit(condensedOutfit);
            playerOutfit.NamePlateId = player.Data.DefaultOutfit.NamePlateId;

            player.MixUpOutfit(playerOutfit);
        }
    }
}