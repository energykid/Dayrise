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
    public class NoviceSorcererEmblem : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 42;
            item.rare = 3;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.accessory = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Novice Sorcerer Emblem");
            Tooltip.SetDefault("7% increased magic damage");
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.magicDamage *= 1.07f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "BlankEmblem", 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}