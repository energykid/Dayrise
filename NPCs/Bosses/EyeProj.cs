using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using IL.Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;

namespace Dayrise.NPCs.Bosses
{
	public class EyeProj : ModProjectile
	{
		int timer = 0;

		public override void SetStaticDefaults()
        {
			DisplayName.SetDefault("Visceral Eye");
        }

        public override void SetDefaults()
		{
            projectile.CloneDefaults(ProjectileID.WoodenArrowHostile);

            projectile.aiStyle = -1;
            projectile.tileCollide = true;
            projectile.width = 26;
            projectile.height = 18;
            projectile.scale = 1;
            projectile.damage = 23;
		}

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            for (int i = 0; i < 20; i++)
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
            Vector2 dest = new Vector2(projectile.ai[0], projectile.ai[1]);

            projectile.rotation = projectile.AngleTo(projectile.Center + projectile.velocity);

            timer++;
            if (timer < 35)
            {
                projectile.velocity += projectile.DirectionTo(dest) * 0.4f;
            }
            if (projectile.Distance(dest) < 15) timer = 35;

            Dust dust;
            // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
            Vector2 position = projectile.Center - new Vector2(10, 10);
            dust = Main.dust[Terraria.Dust.NewDust(position, 20, 20, 5, 0f, 0f, 0, new Color(255, 255, 255), Main.rand.NextFloat())];
        }
    }
}