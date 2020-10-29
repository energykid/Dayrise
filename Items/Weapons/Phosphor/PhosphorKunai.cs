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
	public class PhosphorKunai : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Phosphor Kunai");
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255);
		}

		public override void SetDefaults() 
		{
			item.damage = 11;
			item.thrown = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 18;
			item.useAnimation = 18;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.noUseGraphic = true;
			item.knockBack = 2;
			item.shoot = mod.ProjectileType("PhosphorKunaiProj");
			item.shootSpeed = 24;
			item.value = 10000;
			item.rare = 2;
			item.UseSound = SoundID.Item1;
			item.autoReuse = false;
		}
    }
}