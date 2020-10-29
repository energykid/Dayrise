using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace Dayrise.Items.Weapons.Phosphor
{
    public class LightspeedProj : ModProjectile
    {
        float started = 0;

        int hitNPC = 0;

        Vector2[] pos1 = new Vector2[10] { Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero };

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Phosphor Kunai");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            projectile.penetrate = -1;
            projectile.aiStyle = -1;
            projectile.melee = true;
            projectile.tileCollide = true;
            projectile.width = 34;
            projectile.height = 34;
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

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.velocity.X != oldVelocity.X) projectile.velocity.X = -oldVelocity.X;
            if (projectile.velocity.Y != oldVelocity.Y) projectile.velocity.Y = -oldVelocity.Y;
            return false;
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (projectile.ai[1] >= 15) knockback = 0f;
            else knockback = 5f;
            if (hitNPC == 0)
            {
                hitNPC = target.whoAmI;
                target.immune[0] = 3;
                target.immune[1] = 3;
                projectile.ai[0] = 0;
            }
            else
            {
                projectile.ai[1] = 10;
                hitNPC = -1;
            }
            projectile.velocity = projectile.DirectionFrom(target.Center) * 18f;
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

            projectile.ai[1]++;
            if (hitNPC > 0 && Main.npc[hitNPC].active)
            {
                projectile.velocity = Vector2.Lerp(projectile.velocity, projectile.DirectionTo(Main.npc[hitNPC].Center) * 35, 0.10f);
            }
            else if (projectile.ai[1] > 10)
            {
                projectile.tileCollide = false;
                projectile.velocity = Vector2.Lerp(projectile.velocity, projectile.DirectionTo(player.Center) * 35, 0.18f);
                if (projectile.Distance(player.Center) <= 17) projectile.Kill();
            }

            projectile.rotation += Math.Abs(MathHelper.ToRadians(24));

            projectile.spriteDirection = 1;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            for (int i = 1; i < pos1.Length; i++)
            {
                pos1[i] = pos1[i - 1] + (pos1[i] - pos1[i - 1]).SafeNormalize(Vector2.Zero) * MathHelper.Min(Vector2.Distance(pos1[i - 1], pos1[i]), started);
            }
            Texture2D tex = mod.GetTexture("Items/Weapons/Phosphor/LightspeedProj");
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.Additive);
            for (float i = 0; i < 10; i++)
            {
                spriteBatch.Draw(tex, pos1[(int)i] - Main.screenPosition,
                            new Rectangle(0, 0, tex.Width, tex.Height), new Color(100f, 175f, 255f, 85f), projectile.rotation,
                            new Vector2(tex.Width / 2, tex.Height / 2), 1f, SpriteEffects.None, 0f);
            }
            spriteBatch.End();
            spriteBatch.Begin();
            return false; 
        }
    }
}
