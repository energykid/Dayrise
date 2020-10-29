using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using System;
using System.Collections.Generic;
using Terraria.GameContent.Shaders;
using Terraria.Graphics.Shaders;
using Terraria.GameContent.Achievements;
using Microsoft.Xna.Framework.Graphics;

namespace Dayrise.Items.Weapons.Phosphor
{
    public class Glowsaber : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Glowsaber");
            Tooltip.SetDefault("Slashes with precision and style");
        }

        public override void SetDefaults()
        {
            item.damage = 16;
            item.melee = true;
            item.width = 34;
            item.height = 34;
            item.useTime = 17;
            item.useAnimation = 17;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 4f;
            item.value = Item.buyPrice(0, 60, 0, 0);
            item.rare = 2;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("GlowsaberProj");
            item.shootSpeed = 21f;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 255, 255);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            position.Y -= 10;
            return true;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            Dust dust = Dust.NewDustDirect(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, mod.DustType("LightParticle"));
        }

        /*public override void UseStyle(Player player)
        {
            player.itemRotation = MathHelper.Lerp(-player.direction * player.AngleTo(player.Center + new Vector2(player.direction, 11)), player.itemRotation, MathHelper.Lerp((float)player.itemAnimation, 0.5f, (float)player.itemAnimation / (float)item.useTime) / (float)item.useTime * 7.4f);
            //below would be cool for axes or hammers
            //player.itemRotation = MathHelper.Lerp(player.itemRotation, -player.direction * MathHelper.ToRadians(360), (float)player.itemAnimation / ((float)item.useAnimation / 2f));
        }*/
    }
}
