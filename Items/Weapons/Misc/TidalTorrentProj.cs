using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using IL.Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;

namespace Dayrise.Items.Weapons.Misc
{
	public class TidalTorrentProj : ModProjectile
	{
		int timer = 0;

        public override void SetStaticDefaults()
        {
			DisplayName.SetDefault("Tidal Torrent");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults()
		{
            projectile.CloneDefaults(ProjectileID.WoodenArrowFriendly);

            projectile.aiStyle = -1;
            projectile.tileCollide = true;
            projectile.width = 26;
            projectile.height = 14;
            projectile.scale = 1;
            projectile.magic = true;
            projectile.damage = 23;
		}

        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 4; k++)
            {
                for (int i = 0; i < 19; i++)
                {
                    Dust dust;
                    // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                    Vector2 position = projectile.oldPos[k] - new Vector2(10, 10);
                    dust = Main.dust[Terraria.Dust.NewDust(position, 20, 20, 33, 0f, -1f, 0, new Color(255, 255, 255), Main.rand.NextFloat())];
                }
            }
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            width = 14;
            height = 14;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough);
        }

        public override void AI()
		{
            float inc = MathHelper.ToRadians(projectile.ai[1]);

            projectile.rotation = projectile.AngleTo(projectile.Center + projectile.velocity);

            projectile.velocity *= 0.97f;

            timer++;
            if (timer >= 10)
            {
                projectile.ai[1] *= 1.01f;
                projectile.velocity = projectile.velocity.RotatedBy(inc / 6);
            }
            if (timer >= 30)
            {
                projectile.Kill();
            }

            for (int i = 0; i < 2; i++)
            {
                Dust dust;
                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = projectile.Center - new Vector2(10, 10);
                dust = Main.dust[Terraria.Dust.NewDust(position, 20, 20, 33, 0f, -1f, 0, new Color(255, 255, 255), Main.rand.NextFloat())];
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            for (int i = 1; i < projectile.oldPos.Length; i++)
            {
                projectile.oldPos[i] = projectile.oldPos[i - 1] + (projectile.oldPos[i] - projectile.oldPos[i - 1]).SafeNormalize(Vector2.Zero) * MathHelper.Min(Vector2.Distance(projectile.oldPos[i - 1], projectile.oldPos[i]), 7f);
            }
            for (int i = 0; i <= 4; i++)
            {
                Color col = Lighting.GetColor((int)projectile.Center.X / 16, (int)projectile.Center.Y / 16);
                col.A = 155;
                spriteBatch.Draw(mod.GetTexture("Items/Weapons/Misc/TidalTorrentProj"), projectile.oldPos[i] + new Vector2(13, 7) - Main.screenPosition,
                new Rectangle(0, 0, 26, 14), col, projectile.rotation,
                new Vector2(26 * 0.5f, 14 * 0.5f), MathHelper.Lerp(16f, 4f, (float)i / 5) / 20, SpriteEffects.None, 0f);
            }
            return false;
        }
    }
}