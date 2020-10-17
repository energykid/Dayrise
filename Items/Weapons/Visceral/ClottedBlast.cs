using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;
using Terraria.GameContent.Achievements;
using Microsoft.Xna.Framework.Graphics;

namespace Dayrise.Items.Weapons.Visceral
{
    class ClottedBlast : ModProjectile
    {
        int FrameCountMeter = 0;
        public override void SetDefaults()
        {
            projectile.width = 28;
            projectile.height = 10;
            projectile.damage = 1;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.timeLeft = 27;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 5;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(235, 20, 20, projectile.alpha);
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f - MathHelper.ToRadians(90f);
            projectile.velocity *= 0.92f;
            FrameCountMeter++;
            if (FrameCountMeter >= 4)
            {
                projectile.frame++;
                FrameCountMeter = 0;
                if (projectile.frame >= 5)
                {
                    projectile.Kill();
                }
            }
        }
    }
}