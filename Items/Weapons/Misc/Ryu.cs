using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Dayrise.Items.Weapons.Misc
{
	public class Ryu : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Ryu");
			Tooltip.SetDefault("Bullets will ricochet off of the katana towards the nearest enemy");
		}

		public override void SetDefaults() 
		{
            item.damage = 13;
			item.maxStack = 1;
			item.thrown = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 35;
			item.useAnimation = 35;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.noUseGraphic = true;
			item.knockBack = 6;
			item.shoot = mod.ProjectileType("RyuProj");
			item.shootSpeed = 15;
			item.value = 400;
			item.rare = 2;
			item.UseSound = SoundID.Item1;
			item.autoReuse = false;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			foreach (Projectile projectile in Main.projectile)
			{
				if (projectile.owner == player.whoAmI && projectile.type == type) projectile.ai[0] = 1;
			}

			Projectile proj = Main.projectile[Projectile.NewProjectile(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI)];
				(proj.modProjectile as RyuProj).pos = position + new Vector2(speedX * 12, speedY * 12);
			return false;
        }
    }
}