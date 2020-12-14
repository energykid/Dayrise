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

namespace Dayrise.Items.Weapons.Misc
{
    class Soulbullet : ModProjectile
    {
        int timer = 0;
        float start = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soulbullet");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 7;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.damage = 1;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.penetrate = 1;
            projectile.tileCollide = true;
            projectile.timeLeft = 120;
            projectile.scale = 0.5f;
        }
        public override void AI()
        {
            start += (6 - start) * 0.2f;

            if (projectile.ai[1] == 0)
            {
                projectile.velocity *= 1.01f;
            }
            else
            {
                projectile.velocity *= 0.99f;
            }
            projectile.rotation = projectile.AngleTo(projectile.Center + projectile.velocity);
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 3; i++)
            { 
                Dust dust;
                Vector2 position = projectile.Center;
                dust = Main.dust[Terraria.Dust.NewDust(position, 0, 0, 173, 0f, 0f, 0, new Color(255, 255, 255), 1f)];
                dust.noGravity = true;
                dust.shader = GameShaders.Armor.GetSecondaryShader(29, Main.LocalPlayer);
            }
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            width = 4;
            height = 4;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            for (int i = 1; i < projectile.oldPos.Length; i++)
            {
                projectile.oldPos[i] = projectile.oldPos[i - 1] + (projectile.oldPos[i] - projectile.oldPos[i - 1]).SafeNormalize(Vector2.Zero) * MathHelper.Min(Vector2.Distance(projectile.oldPos[i - 1], projectile.oldPos[i]), start);
            }
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.Additive);
            Texture2D tex = mod.GetTexture("Items/Weapons/Misc/Soulbullet");
            for (int i = 0; i < 7; i++)
            {
                spriteBatch.Draw(tex, projectile.oldPos[i] + new Vector2(6 * projectile.scale, 6 * projectile.scale) - Main.screenPosition,
                    new Rectangle(0, 0, 12, 2), new Color(255 - (i * 40), 255 - (i * 35), 255 - (i * 25), 155), projectile.rotation,
                    new Vector2(12 * 0.5f, 2 * 0.5f), 1f, SpriteEffects.None, 0f);
            }
            spriteBatch.Draw(tex, projectile.Center - Main.screenPosition,
                new Rectangle(0, 0, 12, 2), new Color(255, 255, 255), projectile.rotation,
                new Vector2(12 * 0.5f, 2 * 0.5f), 1f, SpriteEffects.None, 0f);
            spriteBatch.End();
            spriteBatch.Begin();
            return false;
        }
    }
}