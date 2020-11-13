using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace Dayrise.Items.Emblems
{

	public class SolarSlash : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Solar Slash");
			Main.projFrames[base.projectile.type] = 13;
		}

		public override void SetDefaults()
		{
			projectile.width = 128;
			projectile.height = 128;
			projectile.friendly = true;
			projectile.melee = true;
			projectile.tileCollide = false;
			projectile.penetrate = -1;
			projectile.timeLeft = 26;
			projectile.ignoreWater = true;
			projectile.light = 1f;
		}

		public override void AI()
		{
			projectile.frameCounter++;
			if (projectile.frameCounter >= 2) {
				projectile.frame++;
				projectile.frameCounter = 0;
			}
			Vector2 angle = new Vector2(projectile.ai[0], projectile.ai[1]);
			projectile.rotation = angle.ToRotation();
			Player player = Main.player[projectile.owner];
			projectile.position = player.Center + angle - new Vector2(projectile.width / 2, projectile.height / 2);
			if (projectile.timeLeft == 2) {
				projectile.friendly = false;
			}
			Dust.NewDust(projectile.position, projectile.width, projectile.height, 158);
		}

	}
}