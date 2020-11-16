using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace Dayrise.Items.Weapons.Visceral
{
    public class GashBringingSlasherProj : ModProjectile
    {
        public ref float Time => ref projectile.ai[0];
        public ref float HitTile => ref projectile.ai[1];

        float started = 0;

        Vector2[] pos1 = new Vector2[10] { Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero };

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gash-Bringing Slasher");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.WoodenArrowHostile);

            projectile.ranged = false;
            projectile.penetrate = 4;
            projectile.aiStyle = -1;
            projectile.melee = true;
            projectile.tileCollide = true;
            projectile.width = 46;
            projectile.height = 36;
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

            Time++;

            for (int i = 9; i >= 0; i--)
            {
                if (i == 0)
                {
                    pos1[i] = projectile.Center + new Vector2(-12, -12).RotatedBy(player.Center.X <= projectile.Center.X ? projectile.rotation - MathHelper.ToRadians(90f) : -projectile.rotation + MathHelper.ToRadians(90f));
                }
                else if (i != 9)
                {
                    pos1[i] = pos1[i - 1];
                }
            }

            float flySpeed = 14f;
            float inertia = 20f;

            started++;
            if (started > 10) started = 10;

            projectile.rotation += MathHelper.ToRadians(17f);

            projectile.spriteDirection = projectile.velocity.X >= 0 ? 1 : -1;

            if (HitTile != 1)
            {
                if (Time > 90)
                {
                    projectile.velocity = ((projectile.velocity * (inertia - 1f) + projectile.DirectionTo(player.Center) * flySpeed) / inertia);
                    if (projectile.Distance(player.Center) < 24) projectile.active = false;
                }
                else
                {
                    projectile.velocity.X *= 0.96f;
                    projectile.velocity.Y += 0.7f;
                }
            }
            else if (HitTile == 1)
            {
                projectile.penetrate = -1;
                projectile.velocity = Vector2.Lerp(projectile.velocity, projectile.DirectionTo(Main.player[projectile.owner].Center) * (12 + (Time / 10f)), 0.1f);
                if (projectile.Distance(player.Center) < 24) projectile.active = false;
            }
            
            projectile.spriteDirection = 1;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.penetrate == 2) HitTile = 1;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            HitTile = 1;
            Time = 0;
            if (projectile.velocity.X != oldVelocity.X) projectile.velocity.X = -oldVelocity.X;
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.velocity.Y = -oldVelocity.Y;
                projectile.velocity.X *= 0.6f;
            }
            return false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            for (int i = 1; i < pos1.Length; i++)
            {
                pos1[i] = pos1[i - 1] + (pos1[i] - pos1[i - 1]).SafeNormalize(Vector2.Zero) * MathHelper.Min(Vector2.Distance(pos1[i - 1], pos1[i]), started);
            }
            for (int i = 1; i < projectile.oldPos.Length; i++)
            {
                pos1[i] = pos1[i - 1] + (pos1[i] - pos1[i - 1]).SafeNormalize(Vector2.Zero) * MathHelper.Min(Vector2.Distance(pos1[i - 1], pos1[i]), started);
            }
            Vector2 playerCenter = Main.player[projectile.owner].Center;
            Vector2 center = projectile.Center;
            Vector2 plusNinety = projectile.DirectionTo(playerCenter).RotatedBy(MathHelper.ToRadians(90)) * 5;
            for (float i = 0; i < 10; i++)
            {
                Vector2 pos = pos1[(int)i];
                spriteBatch.Draw(mod.GetTexture("Items/Weapons/Visceral/GashBringingSlasherChain"), Vector2.Lerp(pos, projectile.oldPos[(int)i] + new Vector2(projectile.width / 2, projectile.height / 2), i / 10) - Main.screenPosition,
                            new Rectangle(0, 0, 16, 16), Lighting.GetColor((int)pos.X / 16, (int)pos.Y / 16), 0f,
                            new Vector2(8, 8), 0.7f, SpriteEffects.None, 0f);
            }
                 spriteBatch.Draw(mod.GetTexture("Items/Weapons/Visceral/GashBringingSlasherProj"), center - Main.screenPosition,
                 new Rectangle(0, 0, 46, 36), Lighting.GetColor((int)center.X / 16, (int)center.Y / 16), playerCenter.X <= center.X ? projectile.rotation : -projectile.rotation,
                 new Vector2(46 / 2, 36 / 2), 1f, playerCenter.X <= center.X ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            return false; 
        }
    }
}
