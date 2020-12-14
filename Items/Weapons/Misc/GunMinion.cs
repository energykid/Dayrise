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
    public class GunMinion : ModProjectile
    {
        NPC target;
        Vector2 targetPos = Vector2.Zero;
        Vector2 flyTo = Vector2.Zero;

        bool start = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cursed Gun");
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

        public override void AI()
        {
            Player player = Main.player[projectile.owner];

            if (!start)
            {
                targetPos = projectile.Center + new Vector2(0, 100);
                flyTo = player.Center;
                start = true;
            }

            bool isTargetingNPC = false;

            projectile.ai[0] = projectile.minionPos % 3;

            float ang = MathHelper.ToRadians(360 / (float)Math.Floor(Main.player[projectile.owner].ownedProjectileCounts[mod.ProjectileType("GunMinion")] + 0.5f) * projectile.minionPos);
            if (Main.player[projectile.owner].ownedProjectileCounts[mod.ProjectileType("GunMinion")] <= 1) ang = 0f;

            if (UsefulMethods.ClosestNPC(ref target, 600, player.Center))
            {
                isTargetingNPC = true;
            }

            Vector2 gung = new Vector2(0, 75).RotatedBy(ang + (DayriseWorld.globalTimer / 75));
            gung.Y *= 0.7f;

            projectile.ai[1]++;

            if (isTargetingNPC)
            {
                float cos1 = (DayriseWorld.globalTimer * 0.04f) + (projectile.minionPos * 0.1f);

                int timeCap = 10;
                if (projectile.ai[0] == 0)
                {
                    flyTo = Vector2.Lerp(player.Center, target.Center, 0.25f) + new Vector2(0, -130 + (float)Math.Cos(cos1) * 32);
                }
                if (projectile.ai[0] == 1)
                {
                    timeCap = 65;
                    flyTo = target.Center + (target.DirectionTo(player.Center) * (126 + (target.width + target.height / 2)));
                }
                if (projectile.ai[0] == 2)
                {
                    timeCap = 120;
                    flyTo = Vector2.Lerp(player.Center, target.Center, 0.1f) + new Vector2(0, -60 + (float)Math.Cos(cos1) * 32);
                }

                if (projectile.ai[1] >= timeCap)
                {
                    Vector2 vel = projectile.DirectionTo(targetPos);

                    projectile.ai[1] = Main.rand.Next(-20, 1);

                    if (projectile.ai[0] == 2)
                    {
                        Main.PlaySound(SoundID.Item40, projectile.Center);
                        Projectile.NewProjectile(projectile.Center, (vel * Main.rand.Next(17, 21)), mod.ProjectileType("Soulbullet"), 102, 2f, player.whoAmI);
                    }
                    else if (projectile.ai[0] == 1)
                    {
                        Main.PlaySound(SoundID.Item36, projectile.Center);
                        for (int i = 0; i < 4; i++)
                        {
                            Projectile.NewProjectile(projectile.Center, (vel * Main.rand.Next(16, 20)).RotatedBy(MathHelper.ToRadians(Main.rand.Next(-12, 13))), mod.ProjectileType("Soulbullet"), 30, 2f, player.whoAmI, ai1: 1);
                        }
                    }
                    else
                    {
                        Main.PlaySound(SoundID.Item11, projectile.Center);
                        Projectile.NewProjectile(projectile.Center, (vel * Main.rand.Next(13, 16)).RotatedBy(MathHelper.ToRadians(Main.rand.Next(-4, 5))), mod.ProjectileType("Soulbullet"), 19, 2f, player.whoAmI);

                        projectile.ai[1] = Main.rand.Next(0);
                    }

                    projectile.velocity += -vel * 6f;
                }

                flyTo += gung;

                targetPos = Vector2.Lerp(targetPos, target.Center, 0.05f);

                //if (!target.active)
                {
                }
            }
            else
            {
                flyTo = player.Center + new Vector2(0, -65) + gung;
                targetPos = Vector2.Lerp(targetPos, player.Center + new Vector2(player.direction * 150, 0), 0.15f);

                projectile.ai[1] = 0;
            }

            projectile.rotation = projectile.AngleTo(targetPos);

            projectile.velocity = Vector2.Lerp(projectile.velocity, projectile.DirectionTo(flyTo) * projectile.Distance(flyTo) / 20, 0.2f);
        }

        public override void PostAI()
        {
            Player player = Main.player[projectile.owner];
            if (!player.HasBuff(mod.BuffType("GunMinionBuff"))) projectile.Kill();
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D uzi = ModContent.GetTexture("Dayrise/Items/Weapons/Misc/GunMinion_Uzi");
            Texture2D shotgun = ModContent.GetTexture("Dayrise/Items/Weapons/Misc/GunMinion_Shotgun");
            Texture2D sniper = ModContent.GetTexture("Dayrise/Items/Weapons/Misc/GunMinion_Sniper");

            Texture2D gun = uzi;


            if (projectile.ai[0] == 0)
            {
                gun = uzi;
            }
            if (projectile.ai[0] == 1)
            {
                gun = shotgun;
            }
            if (projectile.ai[0] == 2)
            {
                gun = sniper;
            }

            spriteBatch.Draw(gun, projectile.Center - Main.screenPosition,
            new Rectangle(0, 0, gun.Width, gun.Height), Color.CornflowerBlue, projectile.rotation,
            new Vector2(gun.Width * 0.5f, gun.Height * 0.5f), 1.1f, targetPos.X > projectile.Center.X ? SpriteEffects.None : SpriteEffects.FlipVertically, 0f);
            spriteBatch.Draw(gun, projectile.Center - Main.screenPosition,
            new Rectangle(0, 0, gun.Width, gun.Height), Lighting.GetColor((int)projectile.Center.X / 16, (int)projectile.Center.Y / 16), projectile.rotation,
            new Vector2(gun.Width * 0.5f, gun.Height * 0.5f), 1f, targetPos.X > projectile.Center.X ? SpriteEffects.None : SpriteEffects.FlipVertically, 0f);

            return false;
        }
    }
}