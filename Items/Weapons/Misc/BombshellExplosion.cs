using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;

namespace Dayrise.Items.Weapons.Misc
{

	public class BombshellExplosion : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bombshell");
			Main.projFrames[base.projectile.type] = 5;
		}

		public override void SetDefaults()
		{
			projectile.width = 50;
			projectile.height = 58;
			projectile.friendly = true;
			projectile.thrown = true;
			projectile.tileCollide = false;
			projectile.penetrate = -1;
			projectile.timeLeft = 15;
			projectile.ignoreWater = true;
			projectile.light = 0.5f;
		}
		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			width = 40;
			height = 40;
			return base.TileCollideStyle(ref width, ref height, ref fallThrough);
		}

		public override void AI()
		{
			projectile.frameCounter++;
			if (projectile.frameCounter % 3 == 0) {
				projectile.frame++;
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D tex = Main.projectileTexture[projectile.type];

			if (tex != null)
			{
				spriteBatch.Draw(tex, projectile.Center - Main.screenPosition,
				new Rectangle(0, 58 * (int)MathHelper.Clamp(projectile.frame, 0, 5), 50, 58), Color.White, projectile.rotation,
				new Vector2(20, 32), projectile.scale, SpriteEffects.None, 0f);
			}

			return false;
		}
	}
}