using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;

namespace Dayrise.Items.Emblems
{

	public class SolarSlash : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Solar Slash");
			Main.projFrames[base.projectile.type] = 13;
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
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
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D tex = Main.projectileTexture[projectile.type];

			if (tex != null)
			{
				for (int i = 1; i < projectile.oldPos.Length; i++)
				{
					projectile.oldPos[i] = projectile.oldPos[i - 1] + (projectile.oldPos[i] - projectile.oldPos[i - 1]).SafeNormalize(Vector2.Zero) * MathHelper.Min(Vector2.Distance(projectile.oldPos[i - 1], projectile.oldPos[i]), 6f);
				}
				spriteBatch.End();
				spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.Additive);
				for (int i = 0; i < 4; i++)
				{
					spriteBatch.Draw(tex, projectile.oldPos[i] + new Vector2(64, 64) - Main.screenPosition,
					new Rectangle(0, 128 * (int)MathHelper.Clamp(projectile.frame - i, 0, 13), 128, 128), Color.DarkOrange, projectile.rotation,
					new Vector2(128 * 0.5f, 128 * 0.5f), 1f, SpriteEffects.None, 0f);
				}
				spriteBatch.End();
				spriteBatch.Begin();
			}

			return false;
		}
	}
}