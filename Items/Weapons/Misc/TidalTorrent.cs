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

namespace Dayrise.Items.Weapons.Misc
{
    public class TidalTorrent : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tidal Torrent");
            Tooltip.SetDefault("Occasionally unleashes a storm of water");
        }

        public override void SetDefaults()
        {
            item.damage = 45;
            item.melee = true;
            item.width = 34;
            item.height = 34;
            item.useTime = 24;
            item.useAnimation = 24;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 4f;
            item.shoot = mod.ProjectileType("TidalTeardrop");
            item.shootSpeed = 8;
            item.value = Item.buyPrice(0, 60, 0, 0);
            item.rare = 7;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            for (int i = 0; i < Main.rand.Next(3, 6); i++)
            {
                Projectile.NewProjectile(position, new Vector2(speedX + (Main.rand.NextFloat(-6, 7) / 3), speedY + (Main.rand.NextFloat(-12, 6) / 3)), mod.ProjectileType("TidalTeardrop"), 24, 1f, player.whoAmI);
            }
            for (int i = 0; i < 4; i++)
            {
                Projectile proj = Projectile.NewProjectileDirect(position, new Vector2((speedX * 2.5f) + (Main.rand.NextFloat(-6, 7) / 6f), (speedY * 2.5f) + (Main.rand.NextFloat(-6, 7) / 6f)), mod.ProjectileType("TidalTorrentProj"), 24, 1f, player.whoAmI);
                proj.ai[1] = Main.rand.Next(-15, 15);
            }
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 12);
            recipe.AddIngredient(ItemID.BottledWater, 10);
            recipe.AddIngredient(ItemID.Coral, 9);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
