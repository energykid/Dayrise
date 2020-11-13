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

namespace Dayrise.Items.Accessories
{
    public class Gauntlet : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 42;
            item.rare = 4;
            item.defense = 6;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.accessory = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gauntlet");
            Tooltip.SetDefault("+2% critical strike chance");
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.meleeCrit += 2;
            player.rangedCrit += 2;
            player.magicCrit += 2;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Leather, 3);
            recipe.AddIngredient(RecipeGroupID.IronBar, 2);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}