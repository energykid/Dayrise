using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Dayrise.Items.Weapons.Visceral
{
    public class OneEyeProj : ModProjectile
    {
        Vector2 mountedCenter;
        int timer = 0;
        public override void SetDefaults()
        {
            projectile.width = 26;
            projectile.height = 28;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.penetrate = -1;
            projectile.ranged = true;
            projectile.ai[1] = 0;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = ModContent.GetTexture("Dayrise/Items/Weapons/Visceral/OneEyeChain");
            mountedCenter = Main.player[projectile.owner].MountedCenter;
            Vector2 position = projectile.Center;
            Rectangle? sourceRectangle = new Microsoft.Xna.Framework.Rectangle?();
            Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
            float num1 = (float)texture.Height;
            Vector2 vector2_4 = mountedCenter - position;
            float rotation = (float)Math.Atan2((double)vector2_4.Y, (double)vector2_4.X) - 1.57f;
            bool flag = true;
            if (float.IsNaN(position.X) && float.IsNaN(position.Y))
                flag = false;
            if (float.IsNaN(vector2_4.X) && float.IsNaN(vector2_4.Y))
                flag = false;
            while (flag)
            {
                if ((double)vector2_4.Length() < (double)num1 + 1.0)
                {
                    flag = false;
                }
                else
                {
                    Vector2 vector2_1 = vector2_4;
                    vector2_1.Normalize();
                    float xsc = 0.6f;
                    position += vector2_1 * num1;
                    vector2_4 = mountedCenter - position;
                    Color color2 = Lighting.GetColor((int)position.X / 16, (int)((double)position.Y / 16.0));
                    color2 = projectile.GetAlpha(color2);
                    Main.spriteBatch.Draw(texture, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, new Vector2(xsc, 1f), SpriteEffects.None, 0.0f);

                }
            }

            return true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            timer = 41;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (timer <= 40) Main.PlaySound(SoundID.Dig);
            timer = 41;
            return false;
        }

        public override void AI()
        {
            projectile.rotation = projectile.AngleFrom(Main.player[projectile.owner].Center) + MathHelper.ToRadians(90f);

            timer++;
            if (timer > 40)
            {
                projectile.velocity = Vector2.Lerp(projectile.velocity, projectile.DirectionTo(mountedCenter) * 18, 0.1f);
                if (projectile.Distance(mountedCenter) < 20)
                {
                    projectile.active = false;
                }
            }
            else
            {
                projectile.velocity.X *= 0.98f;
                projectile.velocity.Y *= 0.98f;
                projectile.velocity.Y += 0.14f;
            }
        }
    }
}