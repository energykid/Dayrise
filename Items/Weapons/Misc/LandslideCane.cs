using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;

namespace Dayrise.Items.Weapons.Misc
{
    public class LandslideCane : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Landslide Cane");
            Tooltip.SetDefault("Conjures a miniature landslide above the cursor");
            Item.staff[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.damage = 23;
            item.magic = true;
            item.mana = 11;
            item.width = 34;
            item.height = 34;
            item.useTime = 25;
            item.useAnimation = 25;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 4f;
            item.value = Item.buyPrice(0, 60, 0, 0);
            item.rare = 5;
            item.UseSound = SoundID.Item20;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("LandslideProj");
            item.shootSpeed = 9f;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            position = Main.MouseWorld + new Vector2(0, -120);
            speedX = 0;
            speedY = 0;
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Obsidian, 25);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
