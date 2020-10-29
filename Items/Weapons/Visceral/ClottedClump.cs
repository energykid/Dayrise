using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using IL.Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;

namespace Dayrise.Items.Weapons.Visceral
{
	public class ClottedClump : ModProjectile
	{
		int timer = 0;
        float cos1 = 0;

        public override void SetStaticDefaults()
        {
			DisplayName.SetDefault("Clotted Clump");
        }

        public override void SetDefaults()
		{
            projectile.CloneDefaults(ProjectileID.WoodenArrowFriendly);

            projectile.aiStyle = -1;
            projectile.tileCollide = true;
            projectile.width = 28;
            projectile.height = 34;
            projectile.scale = 1;
            projectile.magic = true;
            projectile.damage = 23;
		}

        public override void Kill(int timeLeft)
        {

            Main.PlaySound(SoundID.NPCDeath12, projectile.Center);
            for (int i = 0; i < 25; i++)
            {
                Dust dust2;
                Vector2 position2 = projectile.Center - new Vector2(4, 4);
                Vector2 velocity = new Vector2(0, Main.rand.Next(5, 9)).RotatedBy(MathHelper.ToRadians(Main.rand.Next(360)));
                dust2 = Main.dust[Terraria.Dust.NewDust(position2, 20, 20, 5, velocity.X, velocity.Y, 0, new Color(255, 255, 255), Main.rand.NextFloat())];
            }
            for (int i = 0; i < 25; i++)
            {
                Dust dust2;
                Vector2 position2 = projectile.Center - new Vector2(4, 4);
                Vector2 velocity = new Vector2(0, Main.rand.Next(2, 4)).RotatedBy(MathHelper.ToRadians(Main.rand.Next(360)));
                dust2 = Main.dust[Terraria.Dust.NewDust(position2, 20, 20, 183, velocity.X, velocity.Y, 0, new Color(255, 255, 255), Main.rand.NextFloat())];
            }
            for (int i = 0; i < Main.rand.Next(5, 11); i++)
            {
                Projectile.NewProjectile(projectile.Center, (new Vector2(0, Main.rand.Next(-10, -3)).RotatedBy(MathHelper.ToRadians(Main.rand.Next(360)))), mod.ProjectileType("ClottedBlast"), projectile.damage * 7/10, 6f, projectile.owner);
            }
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
            projectile.velocity.X *= 0.94f;
            projectile.velocity.Y *= 0.94f;

            cos1++;

            timer++;

            if (timer >= 50)
            {
                projectile.scale += 0.02f;
                if (projectile.scale >= 1.4f)
                {
                    projectile.Kill();
                }
            }

            Dust dust;
            // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
            Vector2 position = projectile.Center - new Vector2(10, 10);
            dust = Main.dust[Terraria.Dust.NewDust(position, 20, 20, 5, 0f, 0f, 0, new Color(255, 255, 255), Main.rand.NextFloat())];
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.Additive);
            spriteBatch.Draw(mod.GetTexture("Items/Weapons/Visceral/ClottedClump"), projectile.Center - Main.screenPosition,
            new Rectangle(0, 0, 28, 34), Color.Red, 0,
            new Vector2(28 * 0.5f, 34 * 0.5f), 1.4f + ((float)Math.Cos((double)cos1 * 0.07f) * 0.3f) * projectile.scale, SpriteEffects.None, 0f);
            spriteBatch.Draw(mod.GetTexture("Items/Weapons/Visceral/ClottedClump"), projectile.Center - Main.screenPosition,
            new Rectangle(0, 0, 28, 34), Color.Orange, 0,
            new Vector2(28 * 0.5f, 34 * 0.5f), 1.2f + ((float)Math.Cos((double)cos1 * 0.07f) * 0.15f) * projectile.scale, SpriteEffects.None, 0f);
            spriteBatch.End();
            spriteBatch.Begin();
            spriteBatch.Draw(mod.GetTexture("Items/Weapons/Visceral/ClottedClump"), projectile.Center - Main.screenPosition,
            new Rectangle(0, 0, 28, 34), Lighting.GetColor((int)projectile.Center.X / 16, (int)projectile.Center.Y / 16), 0,
            new Vector2(28 * 0.5f, 34 * 0.5f), 1 + ((float)Math.Cos(((double)cos1 * 0.07f) + 0.5f) * 0.1f), SpriteEffects.None, 0f);
            return false;
        }
    }
}