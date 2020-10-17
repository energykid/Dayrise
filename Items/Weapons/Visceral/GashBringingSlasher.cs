using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Dayrise.Items.Weapons.Visceral
{
	public class GashBringingSlasher : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Gash-Bringing Slasher");
			Tooltip.SetDefault("A chained wooden scythe that will cut through enemies, returning after hitting the ground");
		}

		public override void SetDefaults() 
		{
			item.damage = 25;
			item.melee = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 21;
			item.useAnimation = 21;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.noUseGraphic = true;
			item.knockBack = 6;
			item.shoot = mod.ProjectileType("GashBringingSlasherProj");
			item.shootSpeed = 24;
			item.value = 10000;
			item.rare = 2;
			item.UseSound = SoundID.Item1;
			item.autoReuse = false;
		}
    }
}