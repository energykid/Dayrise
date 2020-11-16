using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
            player.GetModPlayer<MasterPlayer>().warrior = true;
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
            player.GetModPlayer<MasterPlayer>().summoner = true;
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
            if (sorcerer && !target.friendly && !target.immortal && item.magic)
            {
                player.statLife += (int)(item.damage * 0.01f);
                player.HealEffect((int)(item.damage * 0.01f));
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            //CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width / 2, player.height / 2), new Color(169, 248, 255, 100), "Ranged Crit Effect");
            if (sorcerer && !target.friendly && !target.immortal && proj.magic)
            {
                 Projectile.NewProjectile(target.Center, Vector2.Zero, mod.ProjectileType("NebulaHealingBolt"), 0, 0f, player.whoAmI, proj.damage * 0.01f);
            }
            if (summoner && !target.friendly && proj.minion)
            {
                target.AddBuff(mod.BuffType("ElectrifiedV2"), 180, true);
            }
        }
    }
    #endregion

    #region global classses
    public class MasterItem : GlobalItem
    {
        public override bool CanUseItem(Item item, Player player)
        {
             if (player.GetModPlayer<MasterPlayer>().warrior && item.melee && !item.noMelee && item.useStyle == ItemUseStyleID.SwingThrow && !item.noUseGraphic)
            {
                Vector2 mouse = new Vector2(Main.mouseX, Main.mouseY) + Main.screenPosition;
                Vector2 direction = mouse - player.Center;
                direction.Normalize();
                direction *= 90;
                Projectile.NewProjectile(player.Center, Vector2.Zero, ModContent.ProjectileType<SolarSlash>(), 125, 0f, player.whoAmI, direction.X, direction.Y);
            }
            return base.CanUseItem(item,player);
        }
    }
    public class MasterProj : GlobalProjectile
    {
        float alphaCounter = 0;
        public override bool InstancePerEntity => true;
        public override void PostDraw(Projectile projectile, SpriteBatch spriteBatch, Color lightColor)
        {
            Player player = Main.player[projectile.owner];
            if (player.GetModPlayer<MasterPlayer>().summoner && projectile.minion)
            {
                alphaCounter += 0.04f;
                float sineAdd = (float)Math.Sin(alphaCounter) + 3;
			    Main.spriteBatch.Draw(mod.GetTexture("Effects/Masks/Extra_49"), (projectile.Center - Main.screenPosition), null, new Color((int)(7.5f * sineAdd), (int)(16.5f * sineAdd), (int)(18f * sineAdd), 0), 0f, new Vector2(50, 50), 0.25f * (sineAdd + 1), SpriteEffects.None, 0f);
            }
        }

        public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[projectile.owner];
            if (projectile.ranged && crit && player.GetModPlayer<MasterPlayer>().ranger && !target.friendly && !target.immortal)
            {
                Main.PlaySound(SoundID.Item14, projectile.Center);
                Projectile.NewProjectile(target.Center, Vector2.Zero, mod.ProjectileType("Vortexplosion"), 125, 0f, player.whoAmI);
            }
        }
    }
    #endregion
}
