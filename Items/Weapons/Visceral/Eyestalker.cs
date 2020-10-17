using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Dayrise;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace Dayrise.Items.Weapons.Visceral
{
    public class Eyestalker : ModProjectile
    {
        NPC target;
        int yOffset = 0;

        int timer = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eyestalker");
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.Homing[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
        }

        public override void SetDefaults()
        {
            projectile.netImportant = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.damage = 18;
            projectile.width = 20;
            projectile.height = 18;
            projectile.minion = true;
            projectile.minionSlots = 1;
            projectile.penetrate = -1;
            projectile.timeLeft = 18000;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
        }

        public override bool PreAI()
        {
            Player player = Main.player[projectile.owner];
            return true;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            bool isTargetingNPC = false;

            if (UsefulMethods.ClosestNPC(ref target, 500, player.Center))
            {
                isTargetingNPC = true;
            }

            timer++;

            if (timer % 60 == 0)
            {
                if (isTargetingNPC)
                {
                    if (target != null && target.active) 
                    {
                        for (int i = 0; i < Main.rand.Next(4, 7); i++)
                        {
                            Projectile.NewProjectile(projectile.Center, (projectile.DirectionTo(target.Center).RotatedBy(MathHelper.ToRadians(Main.rand.Next(-12, 12)))) * Main.rand.Next(5, 10) + new Vector2(0, -1), mod.ProjectileType("BloodProj3"), 9, 6f, player.whoAmI);
                        }
                    }
                }
            }

            projectile.rotation = 0;
            projectile.rotation += projectile.velocity.X * 0.01f;
            if (projectile.rotation > 0.5f) projectile.rotation = 0.5f;
            if (projectile.rotation < -0.5f) projectile.rotation = -0.5f;

            Vector2 flyTo;
            flyTo = Main.player[projectile.owner].Center + new Vector2(-player.direction * (23 * (projectile.ai[0]+1)), -60 + (3 * projectile.ai[0]));

            projectile.velocity = projectile.DirectionTo(flyTo) * projectile.Distance(flyTo) / 25 + new Vector2((float)Math.Cos((projectile.ai[0] * 0.2f) + timer * 0.04) * 2f, (float)Math.Cos((projectile.ai[0] * 0.2f) + timer * 0.03) * 3f);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.velocity.X = -projectile.velocity.X * .75f;
            projectile.velocity.Y = -projectile.velocity.Y * .75f;
        }

        public override void PostAI()
        {
            Player player = Main.player[projectile.owner];
            if (!player.HasBuff(mod.BuffType("EyestalkerBuff"))) projectile.Kill();
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = ModContent.GetTexture("Dayrise/Items/Weapons/Visceral/EyestalkerStalk");
            Vector2 position = projectile.Center;
            Vector2 mountedCenter = Main.player[projectile.owner].MountedCenter;
            Rectangle? sourceRectangle = new Rectangle?();
            Vector2 origin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
            float num1 = texture.Height;
            Vector2 vector24 = mountedCenter - position;
            float rotation = (float)Math.Atan2(vector24.Y, vector24.X) - 1.57f;
            bool flag = true;
            if (float.IsNaN(position.X) && float.IsNaN(position.Y))
                flag = false;
            if (float.IsNaN(vector24.X) && float.IsNaN(vector24.Y))
                flag = false;
            while (flag)
            {
                if (vector24.Length() < num1 + 1.0)
                {
                    flag = false;
                }
                else
                {
                    Vector2 vector21 = vector24;
                    vector21.Normalize();
                    position += vector21 * num1;
                    vector24 = mountedCenter - position;
                    Color color2 = Lighting.GetColor((int)position.X / 16, (int)(position.Y / 16.0));
                    color2 = projectile.GetAlpha(color2);
                    Main.spriteBatch.Draw(texture, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, 1f, SpriteEffects.None, 0.0f);
                }
            }
            return true;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            if (target != null && target.active)
            {
                float dist = projectile.Distance(target.Center);
                if (dist > 3) dist = 3;

                Vector2 funny_location = projectile.Center + ((projectile.DirectionTo(target.Center) * dist)) + new Vector2(0, 0);

                //Draw pupil

                spriteBatch.Draw(mod.GetTexture("Items/Weapons/Visceral/Eyestalker_Pupil"), funny_location - Main.screenPosition,
                    new Rectangle(0, 0, 2, 2), Lighting.GetColor((int)projectile.Center.X / 16, (int)projectile.Center.Y / 16), 0,
                    new Vector2(2 * 0.5f, 2 * 0.5f), 1f, SpriteEffects.None, 0f);
            }
            else
            {
                Vector2 cen = projectile.Center + new Vector2(Main.player[projectile.owner].direction * 5, 0) + (-projectile.velocity * 1.6f) + (Main.player[projectile.owner].velocity * 0.7f);

                float dist = projectile.Distance(cen);
                if (dist > 3) dist = 3;

                Vector2 funny_location = projectile.Center + ((projectile.DirectionTo(cen) * dist)) + new Vector2(0, 0);

                //Draw pupil

                spriteBatch.Draw(mod.GetTexture("Items/Weapons/Visceral/Eyestalker_Pupil"), funny_location - Main.screenPosition,
                    new Rectangle(0, 0, 2, 2), Lighting.GetColor((int)projectile.Center.X / 16, (int)projectile.Center.Y / 16), 0,
                    new Vector2(2 * 0.5f, 2 * 0.5f), 1f, SpriteEffects.None, 0f);
            }
        }
    }
}