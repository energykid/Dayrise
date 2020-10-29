using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;

namespace Dayrise.Items.Weapons.Frostbite
{
    public class SnowstormStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Snowstorm Staff");
            Tooltip.SetDefault("Summons a frigid barrage of snowballs");
            Item.staff[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.damage = 5;
            item.magic = true;
            item.mana = 5;
            item.width = 34;
            item.height = 34;
            item.useTime = 5;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 4f;
            item.value = Item.buyPrice(0, 60, 0, 0);
            item.rare = 2;
            item.UseSound = SoundID.Item20;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("SnowstormProj");
            item.shootSpeed = 9f;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            for (int i = 0; i < 2; i++)
            {
                float rot = Main.rand.Next(-15, 0);
                if (i == 1) rot = -rot;
                Vector2 newSpd = new Vector2(speedX, speedY).RotatedBy(MathHelper.ToRadians(rot * 3));

                Projectile proj = Projectile.NewProjectileDirect(position, newSpd * Main.rand.NextFloat(0.8f, 1.3f), type, damage, knockBack, player.whoAmI);
                proj.ai[0] = -rot / 3;
                proj.scale = Main.rand.NextFloat(0.3f, 0.8f);
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SilverBar, 8);
            recipe.AddIngredient(ItemID.IceBlock, 20);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
            ModRecipe recipe2 = new ModRecipe(mod);
            recipe2.AddIngredient(ItemID.TungstenBar, 8);
            recipe2.AddIngredient(ItemID.IceBlock, 20);
            recipe2.AddTile(TileID.Anvils);
            recipe2.SetResult(this);
            recipe2.AddRecipe();
        }
    }
}
