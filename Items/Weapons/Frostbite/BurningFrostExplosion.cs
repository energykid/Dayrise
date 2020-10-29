using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;
using Terraria.GameContent.Achievements;
using Microsoft.Xna.Framework.Graphics;

namespace Dayrise.Items.Weapons.Frostbite
{
    class BurningFrostExplosion : ModProjectile
    {
        int vel = 17;
        int sca = 1;
        public override void SetDefaults()
        {
            projectile.width = 52;
            projectile.height = 52;
            projectile.damage = 1;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.timeLeft = 28;
            projectile.scale = 0.7f;
        }
        public override void AI()
        {
            projectile.rotation += MathHelper.ToRadians(vel);
            vel = (int)MathHelper.Lerp((float)vel, 0f, 0.001f);

            if (projectile.timeLeft <= 20) projectile.scale -= 0.025f;

            for (int i = 0; i < 2; i++)
            {
                Dust dust;
                Vector2 position = projectile.Center;
                dust = Dust.NewDustPerfect(position, mod.DustType("FrostMist"));
                dust.scale = 0.7f;
                dust.velocity = new Vector2(0, Main.rand.Next(-4, -1)).RotatedBy(MathHelper.ToRadians(Main.rand.Next(360)));
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            for (int i = 0; i <= 3; i++)
            {
                spriteBatch.Draw(mod.GetTexture("Items/Weapons/Frostbite/FrostbiteSetExplosion"), projectile.Center - Main.screenPosition,
                new Rectangle(0, 0, 52, 52), new Color(i * 55, i * 65, i * 120, projectile.alpha * sca), projectile.rotation - MathHelper.ToRadians(vel / 3 * i),
                new Vector2(52 * 0.5f, 52 * 0.5f), projectile.scale * 1.2f, SpriteEffects.None, 0f);
            }
            for (int i = 0; i <= 5; i++)
            {
                spriteBatch.Draw(mod.GetTexture("Items/Weapons/Frostbite/FrostbiteSetExplosion"), projectile.Center - Main.screenPosition,
                new Rectangle(0, 0, 52, 52), new Color(i * 40, i * 50, i * 100, projectile.alpha * sca), projectile.rotation - MathHelper.ToRadians(vel / 8 * i),
                new Vector2(52 * 0.5f, 52 * 0.5f), projectile.scale, SpriteEffects.None, 0f);
            }
            return false;
        }
    }
}