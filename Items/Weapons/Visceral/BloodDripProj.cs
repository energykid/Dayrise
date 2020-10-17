using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using IL.Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;

namespace Dayrise.Items.Weapons.Visceral
{
	public class BloodDripProj : ModProjectile
	{
        public override void SetStaticDefaults()
        {
			DisplayName.SetDefault("Blood Drip");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults()
		{
            projectile.CloneDefaults(ProjectileID.WoodenArrowFriendly);

            projectile.aiStyle = -1;
            projectile.tileCollide = true;
            projectile.width = 10;
            projectile.height = 10;
            projectile.scale = 1;
            projectile.damage = 23;
		}

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            for (int i = 0; i < 5; i++)
            {
                Dust dust;
                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = projectile.Center - new Vector2(10, 10);
                dust = Main.dust[Terraria.Dust.NewDust(position, 20, 20, 5, 0f, 0f, 0, new Color(255, 255, 255), Main.rand.NextFloat())];
            }
            return base.OnTileCollide(oldVelocity);
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            width = 14;
            height = 14;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough);
        }

        public override void AI()
		{
            projectile.velocity.Y += 0.13f;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            spriteBatch.Draw(mod.GetTexture("Items/Weapons/Visceral/BloodDripProj"), projectile.Center - Main.screenPosition,
            new Rectangle(0, 0, 2, 1), Lighting.GetColor((int)projectile.Center.X / 16, (int)projectile.Center.Y / 16).MultiplyRGB(new Color(1.5f, 1, 1)), 0,
            new Vector2(1, 1), new Vector2(1, 1 + Math.Abs(projectile.velocity.Y) * 2), SpriteEffects.None, 0f);
            return false;
        }
    }
}