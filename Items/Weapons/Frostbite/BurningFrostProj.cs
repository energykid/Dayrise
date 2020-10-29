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
    class BurningFrostProj : ModProjectile
    {
        int vel = 17;
        int sca = 1;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Burning Frost");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 7;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 14;
            projectile.damage = 1;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.penetrate = 1;
            projectile.tileCollide = true;
            projectile.timeLeft = 120;
            projectile.scale = 1f;
        }
        public override void AI()
        {
            projectile.rotation += MathHelper.ToRadians(vel);
            vel = (int)MathHelper.Lerp((float)vel, Math.Abs(projectile.velocity.X * 5) + 5, 0.001f);

            projectile.velocity.Y += 0.3f;
            projectile.velocity.X *= 0.985f;
        }

        public override void Kill(int timeLeft)
        {
            Projectile.NewProjectile(projectile.Center, Vector2.Zero, mod.ProjectileType("BurningFrostExplosion"), projectile.damage / 2, 1f, projectile.owner);
            for (int i = 0; i < 45; i++)
            {
                Dust dust;
                Vector2 position = projectile.Center;
                dust = Dust.NewDustPerfect(position, mod.DustType("FrostMist"));
                dust.scale = 0.7f;
                dust.velocity = new Vector2(0, Main.rand.Next(-4, -1)).RotatedBy(MathHelper.ToRadians(Main.rand.Next(360)));
            }
            Main.PlaySound(SoundID.DD2_ExplosiveTrapExplode, projectile.Center);         
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            for (int i = 1; i < projectile.oldPos.Length; i++)
            {
                projectile.oldPos[i] = projectile.oldPos[i - 1] + (projectile.oldPos[i] - projectile.oldPos[i - 1]).SafeNormalize(Vector2.Zero) * MathHelper.Min(Vector2.Distance(projectile.oldPos[i - 1], projectile.oldPos[i]), 4);
            }
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.Additive);
            for (int i = 0; i < 7; i++)
            {
                spriteBatch.Draw(mod.GetTexture("Items/Weapons/Frostbite/BurningFrostProj"), projectile.oldPos[i] + new Vector2(7, 7) - Main.screenPosition,
                    new Rectangle(0, 0, 14, 14), Color.BlueViolet, -projectile.rotation + ((vel / 7) * i),
                    new Vector2(14 * 0.5f, 14 * 0.5f), (MathHelper.Lerp(20f, 4f, (float)i / 5) / 20) + 0.2f, SpriteEffects.None, 0f);
            }
            spriteBatch.Draw(mod.GetTexture("Items/Weapons/Frostbite/BurningFrostProj"), projectile.Center - Main.screenPosition,
                new Rectangle(0, 0, 14, 14), Color.White, -projectile.rotation,
                new Vector2(14 * 0.5f, 14 * 0.5f), projectile.scale * 1.4f, SpriteEffects.None, 0f);
            spriteBatch.End();
            spriteBatch.Begin();
            return false;
        }
    }
}