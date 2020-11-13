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

namespace Dayrise.Items.Weapons.Phosphor
{
	[AutoloadEquip(EquipType.Body)]
	public class PhosphorArmor : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Phosphor Armor");
			Tooltip.SetDefault("+4 melee damage");
		}

		public override void DrawArmorColor(Player drawPlayer, float shadow, ref Color color, ref int glowMask, ref Color glowMaskColor)
		{
			color = Color.White;
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = 10000;
			item.rare = 2;
			item.defense = 3;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DemoniteBar, 12);
			recipe.AddIngredient(null, "Glowbulb", 16);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override void UpdateEquip(Player player)
		{
			player.meleeDamage += 0.04f;
		}
	}
}