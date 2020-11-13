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
using static MonoMod.Cil.RuntimeILReferenceBag.FastDelegateInvokers;

namespace Dayrise.Items.Weapons.Phosphor
{
	[AutoloadEquip(EquipType.Head)]
	public class PhosphorHelm : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Phosphor Helm");
			Tooltip.SetDefault("+2% melee damage and melee speed");
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
			item.defense = 2;
		}

		public override void UpdateEquip(Player player)
		{
			player.meleeDamage += 0.02f;
			player.meleeSpeed += 0.02f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("PhosphorArmor") && legs.type == mod.ItemType("PhosphorBoots");
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "You now emit light \nHigher movement speed and acceleration";
			Lighting.AddLight(player.Center, new Vector3(0.7f, 0.7f, 1f));
			player.GetModPlayer<DayrisePlayer>().phosphorSetBonus = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DemoniteBar, 11);
			recipe.AddIngredient(null, "Glowbulb", 15);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}