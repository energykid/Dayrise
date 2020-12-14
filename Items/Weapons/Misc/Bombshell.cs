using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Dayrise.Items.Weapons.Misc
{
	public class Bombshell : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Bombshell");
			Tooltip.SetDefault("Explodes on contact with any object \nThe Arms Dealer and Demolitionist really seem to love fighting over these");
		}

		public override void SetDefaults() 
		{
            item.damage = 21;
			item.consumable = true;
			item.maxStack = 250;
			item.thrown = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 35;
			item.useAnimation = 35;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.noUseGraphic = true;
			item.knockBack = 6;
			item.shoot = mod.ProjectileType("BombshellProj");
			item.shootSpeed = 10;
			item.value = 400;
			item.rare = 2;
			item.UseSound = SoundID.Item1;
			item.autoReuse = false;
		}
    }
}