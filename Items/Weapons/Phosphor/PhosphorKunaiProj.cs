using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace Dayrise.Items.Weapons.Phosphor
{
    public class PhosphorKunaiProj : ModProjectile
    {
        float started = 0;
        float timer = 0;

        Vector2[] pos1 = new Vector2[10] { Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero };

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Phosphor Kunai");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.WoodenArrowHostile);

            projectile.ranged = false;
            projectile.penetrate = 4;
            projectile.aiStyle = -1;
            projectile.thrown = true;
            projectile.tileCollide = true;
            projectile.width = 14;
            projectile.height = 32;
            projectile.scale = 1;
            projectile.friendly = true;
            projectile.hostile = false;
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            width = 22;
            height = 22;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough);
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];

            for (int i = 9; i >= 0; i--)
            {
                if (i == 0)
                {
                    pos1[i] = projectile.Center;
                }
                else if (i != 9)
                {
                    pos1[i] = pos1[i - 1];
                }
            }

            started++;
            if (started > 4) started = 4;

            projectile.spriteDirection = projectile.velocity.X >= 0 ? 1 : -1;

            projectile.velocity.Y += 0.2f;
            projectile.velocity *= 0.99f;

            projectile.rotation = projectile.AngleTo(projectile.Center + projectile.velocity) + MathHelper.ToRadians(90);

            projectile.spriteDirection = 1;
        }

        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 9; k++)
            {
                for (int i = 0; i < (k == 0 ? 30 : 12 / k); i++)
                {
                    Dust dust;
                    Vector2 position = pos1[k];
                    dust = Dust.NewDustDirect(position, 0, 0, mod.DustType("LightParticle"), 0f, 0f, 0, new Color(255, 255, 255), 0.7236842f);
                    dust.noGravity = true;
                }
            }

            Main.PlaySound(SoundID.Dig, projectile.Center);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            for (int i = 1; i < pos1.Length; i++)
            {
                pos1[i] = pos1[i - 1] + (pos1[i] - pos1[i - 1]).SafeNormalize(Vector2.Zero) * MathHelper.Min(Vector2.Distance(pos1[i - 1], pos1[i]), started);
            }
            Vector2 playerCenter = Main.player[projectile.owner].Center;
            Vector2 center = projectile.Center;
            Vector2 plusNinety = projectile.DirectionTo(playerCenter).RotatedBy(MathHelper.ToRadians(90)) * 5;
            Texture2D tex = mod.GetTexture("Items/Weapons/Phosphor/PhosphorKunaiProj");
            Texture2D tex2 = mod.GetTexture("Items/Weapons/Phosphor/PhosphorKunaiProj_Trail");
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.Additive);
            for (float i = 0; i < 10; i++)
            {
                spriteBatch.Draw(tex2, pos1[(int)i] - Main.screenPosition,
                            new Rectangle(0, 0, tex2.Width, tex2.Height), new Color(100f, 175f, 255f), projectile.rotation,
                            new Vector2(tex2.Width / 2, tex2.Height / 2), 1f, SpriteEffects.None, 0f);
            }
                 spriteBatch.Draw(tex, center - Main.screenPosition,
                 new Rectangle(0, 0, tex.Width, tex.Height), Color.White, projectile.rotation,
                 new Vector2(tex.Width / 2, tex.Height / 2), 1f, SpriteEffects.None, 0f);
            spriteBatch.End();
            spriteBatch.Begin();
            return false; 
        }
    }
}
