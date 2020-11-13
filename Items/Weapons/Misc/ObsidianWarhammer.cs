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
    public class ObsidianWarhammer : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Obsidian Warhammer");
            Tooltip.SetDefault("Swings in a 360-degree arc, crushing enemies");
        }

        public override void SetDefaults()
        {
            item.damage = 20;
            item.melee = true;
            item.width = 34;
            item.height = 34;
            item.useTime = 17;
            item.useAnimation = 17;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 4f;
            item.value = Item.buyPrice(0, 60, 0, 0);
            item.rare = 5;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }

        public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
        {
            if (!target.boss)
            {
                target.velocity.X = 0;
                if (target.noGravity || target.velocity.Y < 0) target.velocity.Y = 0;
            }
        }

        public override void UseStyle(Player player)
        {
            player.itemRotation = MathHelper.Lerp(-player.direction * MathHelper.ToRadians(0), -player.direction * MathHelper.ToRadians(200), (float)player.itemAnimation / ((float)item.useAnimation / 2f));
        }
        public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox)
        {
            hitbox = new Rectangle((int)player.Center.X - 56, (int)player.Center.Y - 56, 112, 112);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Obsidian, 32);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
