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
	[AutoloadEquip(EquipType.Head)]
	public class FrostbiteVeil : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Frostbite Veil");
			Tooltip.SetDefault("+3% magic damage");
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
			player.magicDamage *= 1.03f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("FrostbiteChestplate") && legs.type == mod.ItemType("FrostbiteLeggings");
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Magic attacks occasionally explode into a small freezing vortex";
			player.GetModPlayer<DayrisePlayer>().frostbiteSetBonus = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SilverBar, 5);
			recipe.AddIngredient(ItemID.IceBlock, 20);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
			ModRecipe recipe2 = new ModRecipe(mod);
			recipe2.AddIngredient(ItemID.TungstenBar, 5);
			recipe2.AddIngredient(ItemID.IceBlock, 20);
			recipe2.AddTile(TileID.Anvils);
			recipe2.SetResult(this);
			recipe2.AddRecipe();
		}
	}
}