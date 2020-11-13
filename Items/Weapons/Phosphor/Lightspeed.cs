using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using System;
using System.Collections.Generic;
using Terraria.GameContent.Shaders;
using Terraria.Graphics.Shaders;
using Terraria.GameContent.Achievements;
using Microsoft.Xna.Framework.Graphics;

namespace Dayrise.Items.Weapons.Phosphor
{
	public class Lightspeed : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Lightspeed");
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255);
		}

		public override void SetDefaults() 
		{
			item.damage = 16;
			item.melee = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 18;
			item.useAnimation = 18;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.noUseGraphic = true;
			item.knockBack = 6;
			item.shoot = mod.ProjectileType("LightspeedProj");
			item.shootSpeed = 35;
			item.value = 10000;
			item.rare = 2;
			item.UseSound = SoundID.Item1;
			item.autoReuse = false;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DemoniteBar, 9);
			recipe.AddIngredient(null, "Glowbulb", 12);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}