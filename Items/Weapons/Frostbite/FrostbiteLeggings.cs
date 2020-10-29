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

namespace Dayrise.Items.Weapons.Frostbite
{
	[AutoloadEquip(EquipType.Legs)]
	public class FrostbiteLeggings : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Frostbite Leggings");
			Tooltip.SetDefault("+2% magic damage \n+2% magic critical strike chance");
			//Item.legType[item.type] = 1;
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = 10000;
			item.rare = 2;
			item.defense = 2;
		}
		public override void UpdateEquip(Player player)
		{
			player.magicDamage *= 1.02f;
			player.magicCrit += 2;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SilverBar, 4);
			recipe.AddIngredient(ItemID.IceBlock, 22);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
			ModRecipe recipe2 = new ModRecipe(mod);
			recipe2.AddIngredient(ItemID.TungstenBar, 4);
			recipe2.AddIngredient(ItemID.IceBlock, 22);
			recipe2.AddTile(TileID.Anvils);
			recipe2.SetResult(this);
			recipe2.AddRecipe();
		}
	}
}