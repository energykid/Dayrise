using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using IL.Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;

namespace Dayrise.Items.Weapons.Visceral
{
	public class BleedingDart : ModProjectile
	{
        int timer = 0;

        int started = 0;

        public override void SetStaticDefaults()
        {
			DisplayName.SetDefault("Bleeding Dart");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 12;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults()
		{
            projectile.CloneDefaults(ProjectileID.WoodenArrowFriendly);

            projectile.aiStyle = -1;
            projectile.tileCollide = true;
            projectile.width = 22;
            projectile.height = 6;
            projectile.scale = 1;
            projectile.damage = 23;
            projectile.penetrate = -1;
		}

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            for (int k = 0; k < 12; k++)
            {
                for (int i = 0; i < (k == 0 ? 30 : 12 / k); i++)
                {
                    Dust dust;
                    Vector2 position = projectile.oldPos[k];
                    dust = Dust.NewDustDirect(position, 0, 0, 183, 0f, 0f, 0, new Color(255, 255, 255), 0.7236842f);
                    dust.noGravity = true;
                }
            }
            Main.PlaySound(SoundID.Dig, projectile.Center);
            return base.OnTileCollide(oldVelocity);
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            width = 6;
            height = 6;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Bleeding, 240);
        }

        public override void AI()
        {
            started++;
            if (started > 10) started = 10;

            projectile.velocity.X *= 0.975f;
            projectile.velocity.Y += 0.24f;

            projectile.rotation = projectile.AngleTo(projectile.Center + projectile.velocity);

            timer++;

            Dust dust;
            Vector2 position = projectile.Center;
            dust = Dust.NewDustDirect(position, 0, 0, 183, 0f, 0f, 0, new Color(255, 255, 255), 0.7236842f);
            dust.noGravity = true;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            for (int i = 1; i < projectile.oldPos.Length; i++)
            {
                projectile.oldPos[i] = projectile.oldPos[i - 1] + (projectile.oldPos[i] - projectile.oldPos[i - 1]).SafeNormalize(Vector2.Zero) * MathHelper.Min(Vector2.Distance(projectile.oldPos[i - 1], projectile.oldPos[i]), started);
            }
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.Additive);
            for (int i = 0; i < 12; i++)
            {
                spriteBatch.Draw(mod.GetTexture("Items/Weapons/Visceral/BleedingDart"), projectile.oldPos[i] + new Vector2(11, 3) - Main.screenPosition,
                new Rectangle(0, 0, 22, 6), Color.Red, projectile.rotation,
                new Vector2(11, 3), (MathHelper.Lerp(20f, 2f, (float)i / 12) / 20) + 0.2f, SpriteEffects.None, 0f);
            }
            spriteBatch.End();
            spriteBatch.Begin();
            spriteBatch.Draw(mod.GetTexture("Items/Weapons/Visceral/BleedingDart"), projectile.Center - Main.screenPosition,
            new Rectangle(0, 0, 22, 6), Lighting.GetColor((int)projectile.Center.X / 16, (int)projectile.Center.Y / 16), projectile.rotation,
            new Vector2(11, 3), 1f, SpriteEffects.None, 0f);
            return false;
        }
    }
}