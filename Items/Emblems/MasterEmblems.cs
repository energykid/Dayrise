using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Dayrise.Items.Emblems
{
    #region warrior
    public class MasterWarriorEmblem : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 46;
            item.rare = ItemRarityID.Red;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.accessory = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Advanced Warrior Emblem");
            Tooltip.SetDefault("20% increased melee damage\n15% increased melee speed\nSwords create a giant slash");
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.meleeDamage *= 1.2f;
            player.meleeSpeed *= 1.15f;
        }
    }
    #endregion warrior

    #region ranger
    public class MasterRangerEmblem : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 46;
            item.rare = ItemRarityID.Red;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.accessory = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Advanced Ranger Emblem");
            Tooltip.SetDefault("20% increased ranged damage\n35% increased ranged velocity\nCrits create a powerful explosion");
        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<MasterPlayer>().ranger = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.rangedDamage *= 1.2f;
            player.GetModPlayer<DayrisePlayer>().rangedVelocity = 1.35f;
        }
    }
    #endregion

    #region sorcerer
    public class MasterSorcererEmblem : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 46;
            item.rare = ItemRarityID.Red;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.accessory = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Advanced Sorcerer Emblem");
            Tooltip.SetDefault("20% increased magic damage\nMana regeneration does not decrease when moving\nAll magic attacks have 1% lifesteal");
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.magicDamage *= 1.2f;
            player.manaRegen *= (int)1.5;
            player.manaRegenBuff = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<MasterPlayer>().sorcerer = true;
        }
    }
    #endregion

    #region summoner
    public class MasterSummonerEmblem : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 46;
            item.rare = ItemRarityID.Red;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.accessory = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Advanced Summoner Emblem");
            Tooltip.SetDefault("20% increased summon damage\n<ana regeneration does not decrease when moving\nAll attacks have 1% lifesteal");
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.magicDamage *= 1.2f;
            player.manaRegen *= (int)1.5;
            player.manaRegenBuff = true;
        }
    }
    #endregion

    #region modplayer & projectiles
    public class MasterPlayer : ModPlayer
    {
        public bool warrior = false;
        public bool ranger = false;
        public bool sorcerer = false;
        public bool summoner = false;

        public override void ResetEffects()
        {
            warrior = false;
            ranger = false;
            sorcerer = false;
            summoner = false;
        }
         
        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (ranger && crit && !target.friendly && !target.immortal && item.ranged)
            {
                CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width / 2, player.height / 2), new Color(169, 248, 255, 100), "Ranged Crit Effect");
            }

            if (sorcerer && !target.friendly && !target.immortal && item.magic)
            {
                player.statLife += (int)(item.damage * 0.01f);
                player.HealEffect((int)(item.damage * 0.01f));
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (ranger && crit && !target.friendly && !target.immortal && proj.ranged)
            {
                Projectile.NewProjectile(target.Center, Vector2.Zero, mod.ProjectileType("Vortexplosion"), 125, 0f, player.whoAmI);
                //CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width / 2, player.height / 2), new Color(169, 248, 255, 100), "Ranged Crit Effect");
            }

            if (sorcerer && !target.friendly && !target.immortal && proj.magic)
            {
                player.statLife += (int)(proj.damage * 0.01f);
                player.HealEffect((int)(proj.damage * 0.01f));
            }
        }
    }
    #endregion
}
