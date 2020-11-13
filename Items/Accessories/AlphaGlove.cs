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
    public class AlphaGlove : ModItem
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
            DisplayName.SetDefault("Alpha Glove");
            Tooltip.SetDefault("Increases melee knockback \n12% increase on melee speed and all damage types \n+4% critical strike chance");
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.meleeCrit += 4;
            player.rangedCrit += 4;
            player.magicCrit += 4;
            player.kbGlove = true;
            player.kbBuff = true;
            player.meleeDamage += 0.12f;
            player.meleeSpeed += 0.12f;
            player.rangedDamage += 0.12f;
            player.magicDamage += 0.12f;
            player.minionDamage += 0.12f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 10);
            recipe.AddIngredient(ItemID.MechanicalGlove, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}