using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace Dayrise.Items.Emblems
{
	public class NebulaHealingBolt : ModProjectile
	{
		int start = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Healing Bolt");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 15;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			projectile.friendly = false;
			projectile.hostile = true;
			projectile.magic = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 200;
			projectile.height = 12;
			projectile.width = 12;
			projectile.alpha = 255;
			projectile.tileCollide = false;
			projectile.extraUpdates = 3;
			aiType = ProjectileID.Bullet;
		}
        public override void PostAI()
		{
			if (start < projectile.oldPos.Length) start++;
		}
        public override void AI()
		{
			projectile.rotation = projectile.velocity.ToRotation() + 1.57f;
			Player player = Main.player[projectile.owner];
			Vector2 direction = player.Center - projectile.position;
			if (direction.Length() < 20)
			{
				player.statLife += (int)(MathHelper.Clamp(projectile.ai[0], 0, 1));
                player.HealEffect((int)(MathHelper.Clamp(projectile.ai[0], 0, 1)));
				projectile.active = false;
			}
			direction.Normalize();
			direction *= projectile.ai[0];
			projectile.ai[0] += 0.1f;
			projectile.velocity = direction;
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
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
				for (int i = 0; i < start; i++)
				{
					spriteBatch.Draw(tex, projectile.oldPos[i] + new Vector2(6, 6) - Main.screenPosition,
					new Rectangle(0, 0, 12, 12), Color.Violet, 0,
					new Vector2(12 * 0.5f, 12 * 0.5f), (MathHelper.Lerp(20f, 4f, (float)i / 15) / 20) + 0.1f, SpriteEffects.None, 0f);
				}
				for (int i = 0; i < start; i++)
				{
					spriteBatch.Draw(tex, projectile.oldPos[i] + new Vector2(6, 6) - Main.screenPosition,
					new Rectangle(0, 0, 12, 12), Lighting.GetColor((int)projectile.Center.X / 16, (int)projectile.Center.Y / 16), 0,
					new Vector2(12 * 0.5f, 12 * 0.5f), MathHelper.Lerp(20f, 4f, (float)i / 15) / 20, SpriteEffects.None, 0f);
				}
				spriteBatch.End();
				spriteBatch.Begin();
			}

			return false;
		}
	}
}
