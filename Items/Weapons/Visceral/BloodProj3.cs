using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using IL.Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;

namespace Dayrise.Items.Weapons.Visceral
{
	public class BloodProj3 : ModProjectile
	{
		int timer = 0;

        public override void SetStaticDefaults()
        {
			DisplayName.SetDefault("Visceral Blood");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults()
		{
            projectile.CloneDefaults(ProjectileID.WoodenArrowFriendly);

            projectile.ranged = false;
            projectile.aiStyle = -1;
            projectile.tileCollide = true;
            projectile.width = 10;
            projectile.height = 10;
            projectile.scale = 0.75f;
            projectile.damage = 23;
		}

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            for (int i = 0; i < 20; i++)
            {
                Dust dust;
                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = projectile.Center - new Vector2(10, 10);
                dust = Main.dust[Terraria.Dust.NewDust(position, 20, 20, 5, 0f, 0f, 0, new Color(255, 255, 255), Main.rand.NextFloat())];
            }
            return base.OnTileCollide(oldVelocity);
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            width = 7;
            height = 7;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough);
        }

        public override void AI()
		{
            projectile.velocity.X *= 0.98f;
            projectile.velocity.Y += 0.12f;

            timer++;

            Dust dust;
            // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
            Vector2 position = projectile.Center - new Vector2(10, 10);
            dust = Main.dust[Terraria.Dust.NewDust(position, 20, 20, 5, 0f, 0f, 0, new Color(255, 255, 255), Main.rand.NextFloat())];
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            for (int i = 1; i < projectile.oldPos.Length; i++)
            {
                projectile.oldPos[i] = projectile.oldPos[i - 1] + (projectile.oldPos[i] - projectile.oldPos[i - 1]).SafeNormalize(Vector2.Zero) * MathHelper.Min(Vector2.Distance(projectile.oldPos[i - 1], projectile.oldPos[i]), 3f);
            }
            for (int i = 0; i <= 4; i++)
            {
                spriteBatch.Draw(mod.GetTexture("NPCs/Bosses/BloodProj"), projectile.oldPos[i] + new Vector2(2, 2) - Main.screenPosition,
                new Rectangle(0, 0, 10, 10), Lighting.GetColor((int)projectile.Center.X / 16, (int)projectile.Center.Y / 16), 0,
                new Vector2(10 * 0.5f, 10 * 0.5f), (MathHelper.Lerp(20f, 4f, (float)i / 5) / 20) * projectile.scale, SpriteEffects.None, 0f);
            }
            return false;
        }
    }
}