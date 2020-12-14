using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace Dayrise.Items.Weapons.Misc
{
    public class BombshellProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bombshell");
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.Lerp(Lighting.GetColor((int)projectile.Center.X / 16, (int)projectile.Center.Y / 16), Color.OrangeRed, (float)Math.Sin(projectile.ai[0] / 5));
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item14, projectile.Center);
            Projectile.NewProjectile(projectile.Center, Vector2.Zero, mod.ProjectileType("BombshellExplosion"), projectile.damage / 2, 4f, projectile.owner);
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.WoodenArrowHostile);

            projectile.thrown = true;
            projectile.penetrate = 1;
            projectile.aiStyle = -1;
            projectile.tileCollide = true;
            projectile.width = 28;
            projectile.height = 12;
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

            projectile.ai[0]++;

            projectile.velocity.Y += 0.1f;
            projectile.velocity.X *= 0.99f;

            projectile.rotation = projectile.AngleTo(projectile.Center + projectile.velocity);
            
            projectile.spriteDirection = 1;
        }
    }
}
