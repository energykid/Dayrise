using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;

namespace Dayrise.Items.Emblems
{

	public class Vortexplosion : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Vortexplosion");
			Main.projFrames[base.projectile.type] = 10;
		}

		public override void SetDefaults()
		{
			projectile.width = 64;
			projectile.height = 64;
			projectile.friendly = true;
			projectile.ranged = true;
			projectile.tileCollide = false;
			projectile.penetrate = -1;
			projectile.rotation = MathHelper.ToRadians(Main.rand.Next(360));
			projectile.timeLeft = 20;
			projectile.scale = Main.rand.NextFloat(1f, 1.3f);
			projectile.ignoreWater = true;
			projectile.light = 0.5f;
		}

		public override void AI()
		{
			projectile.frameCounter++;
			if (projectile.frameCounter % 2 == 0) {
				projectile.frame++;
			}

			if (projectile.frameCounter == 10 && projectile.ai[0] < 4)
			{
				Main.PlaySound(SoundID.Item14, projectile.Center);
				Projectile.NewProjectile(new Vector2(projectile.Center.X + Main.rand.Next(-10, 11), projectile.Center.Y + Main.rand.Next(-10, 11)), Vector2.Zero, mod.ProjectileType("Vortexplosion"), 125, 0f, projectile.owner, projectile.ai[0] + 1);
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
					spriteBatch.Draw(tex, projectile.Center - Main.screenPosition,
					new Rectangle(0, 64 * (int)MathHelper.Clamp(projectile.frame - i, 0, 10), 64, 64), Color.DarkCyan, projectile.rotation,
					new Vector2(64 * 0.5f, 64 * 0.5f), projectile.scale, SpriteEffects.None, 0f);
				}
				spriteBatch.End();
				spriteBatch.Begin();
			}

			return false;
		}
	}
}