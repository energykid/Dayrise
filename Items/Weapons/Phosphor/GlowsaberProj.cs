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

namespace Dayrise.Items.Weapons.Phosphor
{
    class GlowsaberProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Glowsaber");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 7;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            projectile.width = 80;
            projectile.height = 32;
            projectile.damage = 20;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.penetrate = -1;
            projectile.alpha = 255;
            projectile.tileCollide = false;
            projectile.timeLeft = 60;
            projectile.scale = 1.5f;
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            width = 46;
            height = 46;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough);
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox = new Rectangle((int)projectile.Center.X - 23, (int)projectile.Center.Y - 23, 46, 46);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (target.boss) projectile.Kill();
        }

        public override void AI()
        {
            projectile.velocity.Y *= 0.9f;
            projectile.velocity.X *= 0.9f;
            projectile.scale *= 0.9f;

            projectile.alpha -= 10;

            projectile.rotation = projectile.AngleTo(projectile.Center + projectile.velocity) + MathHelper.ToRadians(90);

            if (projectile.scale <= 0.1f) projectile.Kill();
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            for (int i = 1; i < projectile.oldPos.Length; i++)
            {
                projectile.oldPos[i] = projectile.oldPos[i - 1] + (projectile.oldPos[i] - projectile.oldPos[i - 1]).SafeNormalize(Vector2.Zero) * MathHelper.Min(Vector2.Distance(projectile.oldPos[i - 1], projectile.oldPos[i]), 4);
            }
            Texture2D tex = mod.GetTexture("Items/Weapons/Phosphor/GlowsaberProj");
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.Additive);
            for (int i = 0; i < 7; i++)
            {
                spriteBatch.Draw(tex, projectile.oldPos[i] + new Vector2(23, 23) + new Vector2(tex.Width * 0.5f, tex.Height * 0.5f) - Main.screenPosition,
                    new Rectangle(0, 0, tex.Width, tex.Height), new Color(155, 200, 255, projectile.alpha), projectile.rotation,
                    new Vector2(tex.Width * 0.5f, tex.Height * 0.5f), new Vector2((MathHelper.Lerp(20f, 4f, (float)i / 5) / 20) * projectile.scale + 0.1f, (MathHelper.Lerp(20f, 4f, (float)i / 5) / 20) * projectile.scale + 0.1f), SpriteEffects.None, 0f);
            }
            spriteBatch.Draw(tex, projectile.position + new Vector2(23, 23) + new Vector2(tex.Width * 0.5f, tex.Height * 0.5f) - Main.screenPosition,
                new Rectangle(0, 0, tex.Width, tex.Height), new Color(255, 255, 255, projectile.alpha), projectile.rotation,
                new Vector2(tex.Width * 0.5f, tex.Height * 0.5f), new Vector2(projectile.scale, projectile.scale), SpriteEffects.None, 0f);
            spriteBatch.End();
            spriteBatch.Begin();
            return false;
        }
    }
}