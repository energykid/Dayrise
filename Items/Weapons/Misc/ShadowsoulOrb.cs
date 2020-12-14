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
    class ShadowsoulOrb : ModProjectile
    {
        int vel = 17;
        float start = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shadowsoul Orb");
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
            projectile.tileCollide = false;
            projectile.timeLeft = 50;
            projectile.scale = 0.5f;
        }
        public override void AI()
        {
            projectile.velocity.Y += 0.12f;
            start += (4 - start) * 0.2f;
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust dust;
                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = projectile.position + new Vector2(-4, 0);
                dust = Main.dust[Terraria.Dust.NewDust(position, 12, 12, 181, 0f, 0f, 0, new Color(255, 255, 255), 1.184211f)];
                dust.noGravity = true;
                dust.shader = GameShaders.Armor.GetSecondaryShader(36, Main.LocalPlayer);
            }
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            if (projectile.timeLeft <= 182) fallThrough = false;
            else fallThrough = true;
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
            Texture2D tex = mod.GetTexture("Items/Weapons/Misc/ShadowsoulOrb");
            for (int i = 0; i < 7; i++)
            {
                spriteBatch.Draw(tex, projectile.oldPos[i] + new Vector2(6 * projectile.scale, 6 * projectile.scale) - Main.screenPosition,
                    new Rectangle(0, 0, 12, 12), new Color(235, 155, 255, 155), -projectile.rotation + ((vel / 7) * i),
                    new Vector2(12 * 0.5f, 12 * 0.5f), (MathHelper.Lerp(20f, 4f, (float)i / 5) / 20) + 0.2f, SpriteEffects.None, 0f);
            }
            spriteBatch.Draw(tex, projectile.Center - Main.screenPosition,
                new Rectangle(0, 0, 12, 12), new Color(235, 155, 255), -projectile.rotation,
                new Vector2(12 * 0.5f, 12 * 0.5f), projectile.scale * 1.4f, SpriteEffects.None, 0f);
            spriteBatch.End();
            spriteBatch.Begin();
            return false;
        }
    }
}