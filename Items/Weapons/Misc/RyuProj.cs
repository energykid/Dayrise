using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace Dayrise.Items.Weapons.Misc
{
    public class RyuProj : ModProjectile
    {
        public Vector2 pos = Vector2.Zero;
        NPC target;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ryu");
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.WoodenArrowHostile);

            projectile.thrown = true;
            projectile.penetrate = -1;
            projectile.aiStyle = -1;
            projectile.tileCollide = true;
            projectile.width = 40;
            projectile.timeLeft = 600;
            projectile.height = 52;
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

        public override void Kill(int timeLeft)
        {

        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.velocity.X != oldVelocity.X) projectile.velocity.X = -oldVelocity.X;
            if (projectile.velocity.Y != oldVelocity.Y) projectile.velocity.Y = -oldVelocity.Y; 
            return false;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];

            if (projectile.ai[0] == 0)
            {
                projectile.velocity = Vector2.Lerp(projectile.velocity, projectile.DirectionTo(pos) * projectile.Distance(pos) / 10, 0.2f);
                if ((projectile.velocity.X + projectile.velocity.Y) / 2 < 2f)
                {
                    foreach (Projectile proj in Main.projectile)
                    {
                        if (proj.type == ProjectileID.Bullet || proj.type == ProjectileID.BulletHighVelocity)
                        if (proj.Distance(projectile.Center) < 22)
                        {
                            projectile.velocity = proj.velocity.SafeNormalize(Vector2.Zero) * 6f;

                            if (UsefulMethods.ClosestNPC(ref target, 1000, projectile.Center, true))
                            {
                                proj.velocity = proj.DirectionTo(target.Center) * 15f;
                            }
                            else
                            {
                                proj.velocity = proj.DirectionFrom(projectile.Center) * 15f;
                            }
                            Main.PlaySound(SoundID.NPCHit4, projectile.Center);
                            Main.PlaySound(SoundID.NPCHit5, projectile.Center);
                        }
                    }    
                }
            }
            else
            {
                float vel = (projectile.velocity.X + projectile.velocity.Y) / 2;
                projectile.ai[1]++;
                projectile.velocity = Vector2.Lerp(projectile.velocity, projectile.DirectionTo(player.Center) * projectile.ai[1] / 2, 0.1f);
                if (projectile.Distance(player.Center) < vel * 3)
                {
                    projectile.active = false;
                }
            }

            projectile.rotation += MathHelper.ToRadians(19f);
            
            projectile.spriteDirection = 1;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D ryu = ModContent.GetTexture("Dayrise/Items/Weapons/Misc/RyuProj");
            Texture2D ryutrail = ModContent.GetTexture("Dayrise/Items/Weapons/Misc/RyuTrail");

            for (int i = 0; i < 10; i++)
            {
                spriteBatch.Draw(ryutrail, projectile.Center - Main.screenPosition,
                new Rectangle(0, 0, 40, 52), new Color(255 - (i * 50), 255 - (i * 50), 255 - (i * 50), 255 - (i * 50)), projectile.rotation - (i * MathHelper.ToRadians(10)),
                new Vector2(40 / 2, 52 / 2), projectile.scale, SpriteEffects.None, 0f);
            }
            spriteBatch.Draw(ryu, projectile.Center - Main.screenPosition,
            new Rectangle(0, 0, 40, 52), Lighting.GetColor((int)projectile.Center.X / 16, (int)projectile.Center.Y / 16), projectile.rotation,
            new Vector2(40 / 2, 52 / 2), projectile.scale, SpriteEffects.None, 0f);

            return false;
        }
    }
}
