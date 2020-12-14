using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Dayrise;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace Dayrise.Items.Weapons.Misc
{
    public class Shadowsoul : ModProjectile
    {
        NPC target;
        int yOffset = 0;

        float timer = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shadowsoul");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 4;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
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
            projectile.width = 46;
            projectile.height = 46;
            projectile.minion = true;
            projectile.minionSlots = 1;
            projectile.penetrate = -1;
            projectile.timeLeft = 18000;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (projectile.ai[1] == 5)
            {
                for (int i = 0; i < 4; i++)
                {
                    Projectile.NewProjectile(projectile.Center, new Vector2(0, Main.rand.Next(4, 12)).RotatedBy(MathHelper.ToRadians(Main.rand.Next(360))), mod.ProjectileType("ShadowsoulOrb"), 17, 2f, Main.player[projectile.owner].whoAmI);
                }
                Main.PlaySound(SoundID.DD2_BetsyFireballImpact, projectile.Center);
                target.immune[0] = 8;
                target.immune[1] = 8;
                projectile.ai[1] = 0;
            }
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

            float ang = MathHelper.ToRadians(360 / (float)Math.Floor(Main.player[projectile.owner].ownedProjectileCounts[mod.ProjectileType("Shadowsoul")] + 0.5f) * projectile.minionPos);
            if (Main.player[projectile.owner].ownedProjectileCounts[mod.ProjectileType("Shadowsoul")] <= 1) ang = 0f;

            Vector2 flyTo = player.Center + new Vector2(0, 100).RotatedBy(ang + MathHelper.ToRadians(DayriseWorld.globalTimer * 2f));

            if (UsefulMethods.ClosestNPC(ref target, 1300, player.Center, true))
            {
                isTargetingNPC = true;
            }

            timer++;

            if (isTargetingNPC)
            {
                flyTo = target.Center + new Vector2(0, -100 + ((float)Math.Sin(DayriseWorld.globalTimer * 0.1f) * 200)).RotatedBy(ang + MathHelper.ToRadians(DayriseWorld.globalTimer * 2f));

                if (DayriseWorld.globalTimer * 0.1f % 6f <= 0.05f)
                {
                    projectile.ai[1] = 5;
                }
            }

            projectile.rotation = 0;
            projectile.rotation += projectile.velocity.X * 0.01f;
            if (projectile.rotation > 0.5f) projectile.rotation = 0.5f;
            if (projectile.rotation < -0.5f) projectile.rotation = -0.5f;

            projectile.velocity = projectile.DirectionTo(flyTo) * projectile.Distance(flyTo) / 22;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.velocity.X = -projectile.velocity.X * .75f;
            projectile.velocity.Y = -projectile.velocity.Y * .75f;
        }

        public override void PostAI()
        {
            Player player = Main.player[projectile.owner];
            if (!player.HasBuff(mod.BuffType("ShadowsoulBuff"))) projectile.Kill();
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = ModContent.GetTexture("Dayrise/Items/Weapons/Misc/ShadowsoulSigil");
            Texture2D tex2 = ModContent.GetTexture("Dayrise/Items/Weapons/Misc/Shadowsoul");

            for (int i = 1; i < projectile.oldPos.Length; i++)
            {
                projectile.oldPos[i] = projectile.oldPos[i - 1] + (projectile.oldPos[i] - projectile.oldPos[i - 1]).SafeNormalize(Vector2.Zero) * MathHelper.Min(Vector2.Distance(projectile.oldPos[i - 1], projectile.oldPos[i]), 2f);
            }
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);
            for (int i = 0; i < projectile.oldPos.Length; i++)
            {
                spriteBatch.Draw(texture, projectile.oldPos[i] + new Vector2(20, 20) - Main.screenPosition,
                new Rectangle(0, 0, texture.Width, texture.Height), new Color(235, 155, 255, 155), MathHelper.ToRadians(timer * 1.4f),
                new Vector2(texture.Width * 0.5f, texture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
            }
            spriteBatch.End();
            spriteBatch.Begin();

            spriteBatch.Draw(tex2, projectile.Center - Main.screenPosition,
            new Rectangle(0, 0, tex2.Width, tex2.Height), new Color(235, 155, 255), 0f,
            new Vector2(tex2.Width * 0.5f, tex2.Height * 0.5f), 1f, SpriteEffects.None, 0f);

            spriteBatch.Draw(texture, projectile.Center - Main.screenPosition,
            new Rectangle(0, 0, texture.Width, texture.Height), Lighting.GetColor((int)projectile.Center.X / 16, (int)projectile.Center.Y / 16), MathHelper.ToRadians(timer * 1.4f),
            new Vector2(texture.Width * 0.5f, texture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
            return false;
        }
    }
}