using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Shaders;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;
using Terraria.GameContent.Achievements;
using Microsoft.Xna.Framework.Graphics;

namespace Dayrise.Items.Weapons.Frostbite
{
    class SnowstormProj : ModProjectile
    {
        int vel = 17;
        float start = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Snowstorm");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 7;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 14;
            projectile.damage = 1;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.penetrate = 1;
            projectile.tileCollide = true;
            projectile.timeLeft = 22;
            projectile.scale = 0.5f;
        }
        public override void AI()
        {
            projectile.rotation += MathHelper.ToRadians(vel);
            vel = (int)MathHelper.Lerp((float)vel, Math.Abs(projectile.velocity.X * 5) + 5, 0.001f);

            start += (4 - start) * 0.2f;

            projectile.velocity = projectile.velocity.RotatedBy(MathHelper.ToRadians(projectile.ai[0]));
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust dust;
                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = projectile.Center;
                dust = Terraria.Dust.NewDustDirect(position, 0, 0, 211, Main.rand.Next(-5, 6) / 3, Main.rand.Next(-5, 6) / 3, 0, new Color(255, 255, 255), 1f);
                dust.noGravity = true;

            }
            Main.PlaySound(SoundID.Item51, projectile.Center);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            for (int i = 1; i < projectile.oldPos.Length; i++)
            {
                projectile.oldPos[i] = projectile.oldPos[i - 1] + (projectile.oldPos[i] - projectile.oldPos[i - 1]).SafeNormalize(Vector2.Zero) * MathHelper.Min(Vector2.Distance(projectile.oldPos[i - 1], projectile.oldPos[i]), start);
            }
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.Additive);
            for (int i = 0; i < 7; i++)
            {
                spriteBatch.Draw(mod.GetTexture("Items/Weapons/Frostbite/SnowstormProj"), projectile.oldPos[i] + new Vector2(7 * projectile.scale, 7 * projectile.scale) - Main.screenPosition,
                    new Rectangle(0, 0, 14, 14), new Color(24, 18, 40, 100), -projectile.rotation + ((vel / 7) * i),
                    new Vector2(14 * 0.5f, 14 * 0.5f), (MathHelper.Lerp(20f, 4f, (float)i / 5) / 20) + 0.2f, SpriteEffects.None, 0f);
            }
            spriteBatch.End();
            spriteBatch.Begin();
            spriteBatch.Draw(mod.GetTexture("Items/Weapons/Frostbite/SnowstormProj"), projectile.Center - Main.screenPosition,
                new Rectangle(0, 0, 14, 14), Lighting.GetColor((int)projectile.Center.X / 16, (int)projectile.Center.Y / 16), -projectile.rotation,
                new Vector2(14 * 0.5f, 14 * 0.5f), projectile.scale * 1.4f, SpriteEffects.None, 0f);
            return false;
        }
    }
}