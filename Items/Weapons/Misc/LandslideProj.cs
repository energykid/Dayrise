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
    class LandslideProj : ModProjectile
    {
        int timer = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Landslide");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 7;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            projectile.width = 48;
            projectile.height = 16;
            projectile.damage = 1;
            projectile.aiStyle = -1;
            projectile.friendly = false;
            projectile.hostile = false;
            projectile.magic = true;
            projectile.penetrate = 1;
            projectile.tileCollide = true;
            projectile.timeLeft = 17;
            projectile.scale = 0.5f;
        }

        public override void AI()
        {
            timer++;
            if (timer % 2 == 0)
            {
                Vector2 vec2 = projectile.position + new Vector2(Main.rand.Next(48), Main.rand.Next(16));

                for (int i = 0; i < 10; i++)
                {
                    Dust dust;
                    // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                    Vector2 position = vec2;
                    dust = Main.dust[Dust.NewDust(position, 0, 0, 181, 0f, 0f, 255, new Color(255, 75, 0), 0.9210526f)];
                    dust.noGravity = true;
                    dust.shader = GameShaders.Armor.GetSecondaryShader(36, Main.LocalPlayer);

                }

                Projectile proj = Projectile.NewProjectileDirect(vec2, new Vector2(0, Main.rand.Next(0, 4)), mod.ProjectileType("LandslideProj2"), 10, 5f, projectile.owner);
                proj.scale = Main.rand.NextFloat(0.4f, 0.8f);
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            return false;
        }
    }
}