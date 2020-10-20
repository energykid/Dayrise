using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Dayrise;
using Terraria.ID;
using Terraria.ModLoader;

namespace Dayrise.Items.Emblems
{
    public class ApprenticeSorcererEmblem : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 42;
            item.rare = 6;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.accessory = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Apprentice Sorcerer Emblem");
            Tooltip.SetDefault("12% increased magic damage");
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.magicDamage *= 1.12f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "NoviceSorcererEmblem", 1);
            recipe.AddIngredient(RecipeGroupID.IronBar, 10);
            recipe.AddIngredient(ItemID.ShadowScale, 15);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

            ModRecipe recipe2 = new ModRecipe(mod);
            recipe2.AddIngredient(null, "NoviceSorcererEmblem", 1);
            recipe2.AddIngredient(RecipeGroupID.IronBar, 10);
            recipe2.AddIngredient(ItemID.TissueSample, 15);
            recipe2.AddTile(TileID.Anvils);
            recipe2.SetResult(this);
            recipe.AddRecipe();
        }
    }
}