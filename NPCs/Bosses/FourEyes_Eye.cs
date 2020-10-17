using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using IL.Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;

namespace Dayrise.NPCs.Bosses
{
	public class FourEyes_Eye : ModNPC
	{
		float cos1 = 0;

        float rot_augment = 0;
        float scale_augment = 0;

        float rot = 20;
        float rot_spd = 0;

        int timer = 0;
        int loop = 0;

        public override void SetStaticDefaults()
        {
			DisplayName.SetDefault("Visceral Eye");
        }

        public override void SetDefaults()
		{
			npc.CloneDefaults(NPCID.CaveBat);

			npc.aiStyle = -1;
            npc.noTileCollide = false;
			npc.width = 30;
			npc.height = 30;
			npc.noGravity = true;
			npc.life = 650;
			npc.lifeMax = 650;
			npc.scale = 1;
			npc.dontTakeDamage = false;
			npc.damage = 23;
			npc.defense = 1;

            npc.DeathSound = SoundID.NPCDeath12;
        }

		public override void AI()
        {
            if (npc.collideY)
            {
                if (Main.tile[(int)(npc.Center.X / 16), (int)((npc.Center.Y + 30) / 16)].type == TileID.Platforms)
                {
                    npc.position.Y += 2;
                }
            }

            npc.TargetClosest();

            rot += rot_spd;

			Player player = Main.player[npc.target];

			cos1 += 0.05f;

			NPC parent = Main.npc[(int)npc.ai[0]];

            if (!parent.active)
            {
                npc.active = false;
            }
            else
            {
                parent.dontTakeDamage = true;
            }

            //npc.ai[0] = parent ID
            //npc.ai[1] = numeric ID of which eye it is

            //npc.ai[2] = eye rotation timer

            rot_augment += (0 - rot_augment) * 0.08f;
            npc.ai[2]--;
            if (npc.ai[2] == 2)
            {
                for (int i = 0; i < 10; i++)
                {
                    Dust dust;
                    // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                    Vector2 position = npc.Center - new Vector2(10, 10);
                    dust = Main.dust[Terraria.Dust.NewDust(position, 20, 20, 5, 0f, 0f, 0, new Color(255, 255, 255), 1.22f)];
                }

                for (int i = 0; i < Main.rand.Next(1, 4); i++)
                {
                    Projectile.NewProjectile(npc.Center, npc.DirectionFrom(player.Center).RotatedBy(MathHelper.ToRadians(Main.rand.Next(-30, 31))) * Main.rand.Next(5, 9), mod.ProjectileType("EyeProj"), 10, 6f, player.whoAmI, player.Center.X + Main.rand.Next(-50, 51), player.Center.Y + Main.rand.Next(-50, 51));
                }
            }

            if (timer > 185)
            {
                timer += Main.rand.Next(0, 3);
            }
            else
            {
                timer++;
            }

            if (timer > 350 + (npc.ai[1] * 15))
            {
                npc.ai[2] = 30;
                timer = 0 + ((int)npc.ai[1] * 15);
                if (NPC.CountNPCS(mod.NPCType("FourEyes_Eye")) <= 2) timer += Main.rand.Next(-20, 21);
                rot_augment = 360;
            }

            if (NPC.CountNPCS(mod.NPCType("FourEyes_Eye")) <= 1)
            {
                if (rot_spd < 2.2f)
                {
                    rot_spd += 0.015f;
                }

                if (timer % 70 == 0)
                {
                    for (int i = 0; i < 20; i++)
                    {
                        Dust dust;
                        // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                        Vector2 position = npc.Center - new Vector2(10, 10);
                        Vector2 velocity = new Vector2(0, -3).RotatedBy(MathHelper.ToRadians(Main.rand.Next(360)));
                        dust = Main.dust[Terraria.Dust.NewDust(position, 20, 20, 5, velocity.X, velocity.Y, 0, new Color(255, 255, 255), 1.22f)];
                    }

                    for (int i = 0; i < Main.rand.Next(3, 6); i++)
                    {
                        Projectile.NewProjectile(npc.Center, npc.DirectionTo(player.Center).RotatedBy(MathHelper.ToRadians(Main.rand.Next(-30, 31))) * Main.rand.Next(5, 11), mod.ProjectileType("BloodProj"), 10, 6f, player.whoAmI);
                    }
                }
            }

            if (NPC.CountNPCS(mod.NPCType("FourEyes_Eye")) <= 3 && timer > 125 && timer < 175)
            {
                scale_augment += (0.4f - scale_augment) * 0.06f;
                if (timer == 174)
                {
                    for (int i = 0; i < 20; i++)
                    {
                        Dust dust;
                        // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                        Vector2 position = npc.Center - new Vector2(10, 10);
                        Vector2 velocity = new Vector2(0, -3).RotatedBy(MathHelper.ToRadians(Main.rand.Next(360)));
                        dust = Main.dust[Terraria.Dust.NewDust(position, 20, 20, 5, velocity.X, velocity.Y, 0, new Color(255, 255, 255), 1.22f)];
                    }

                    for (int i = 0; i < Main.rand.Next(3, 6); i++)
                    {
                        Projectile.NewProjectile(npc.Center, npc.DirectionTo(player.Center).RotatedBy(MathHelper.ToRadians(Main.rand.Next(-30, 31))) * Main.rand.Next(5, 11), mod.ProjectileType("BloodProj"), 10, 6f, player.whoAmI);
                    }
                }
            }
            else
            {
                scale_augment += (0 - scale_augment) * 0.1f;
            }

            npc.velocity = ((parent.Center + (new Vector2(0, -120).RotatedBy(MathHelper.ToRadians((npc.ai[1] * 90) + rot + ((float)Math.Cos(cos1 * 0.7f + (npc.ai[1] * 0.25f)) * 20f))))) - npc.position) * 0.2f;

            npc.velocity.X += (float)Math.Cos(cos1 * 2f) * 0.05f;
            npc.velocity.Y += (float)Math.Cos((cos1 * 2f) + 0.5f) * 0.05f;

            if ((Math.Abs(npc.velocity.X + npc.velocity.Y / 2)) > 3.5f) npc.velocity *= 0.92f;
        }

        public override void NPCLoot()
        {
            if (NPC.CountNPCS(npc.type) == 2)
            {
                foreach (NPC target in Main.npc)
                {
                    if (target.whoAmI != npc.whoAmI && target.type == npc.type)
                    {
                        if (target.life < target.lifeMax * 0.7f)
                        {
                            target.life += (int)(target.lifeMax * 0.3f);
                        }
                        else
                        {
                            target.life = target.lifeMax;
                        }
                    }
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (npc.ai[0] != -1)
            {
                NPC parent = Main.npc[(int)npc.ai[0]];

                Vector2 neckOrigin = parent.Center;
                Vector2 center = npc.Center;
                Vector2 distToProj = neckOrigin - npc.Center;
                float projRotation = distToProj.ToRotation() - 1.57f;
                float distance = distToProj.Length();

                Vector2 plusNinety = Vector2.Normalize(distToProj).RotatedBy(MathHelper.ToRadians(90)) * 2;

                while (distance > 12f && !float.IsNaN(distance))
                {
                    distToProj.Normalize();                 //get unit vector
                    distToProj *= 12f;                      //speed = 30
                    center += distToProj;                   //update draw position
                    distToProj = neckOrigin - center;    //update distance
                    distance = distToProj.Length();

                    //Draw chain
                    spriteBatch.Draw(mod.GetTexture("NPCs/Bosses/FourEyes_Stalk"), center - Main.screenPosition + (plusNinety * (float)Math.Cos((distance / 12) + cos1)),
                        new Rectangle(0, 0, 12, 12), Lighting.GetColor((int)center.X / 16, (int)center.Y / 16), projRotation,
                        new Vector2(12 * 0.5f, 12 * 0.5f), 1f, SpriteEffects.None, 0f);
                }
                spriteBatch.Draw(mod.GetTexture("NPCs/Bosses/FourEyes_Stalk"), neckOrigin - Main.screenPosition,
                            new Rectangle(0, 0, 12, 12), Lighting.GetColor((int)neckOrigin.X / 16, (int)neckOrigin.Y / 16), projRotation,
                            new Vector2(12 * 0.5f, 12 * 0.5f), 1f, SpriteEffects.None, 0f);
            }
            return true;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Player target = Main.player[npc.target];

            float dist = npc.Distance(target.Center);
            if (dist > 7) dist = 7;

            Vector2 funny_location = npc.Center + ((npc.DirectionTo(target.Center).RotatedBy(MathHelper.ToRadians(rot_augment)) * dist)) + new Vector2(0, 4);

            //Draw pupil

            spriteBatch.Draw(mod.GetTexture("NPCs/Bosses/FourEyes_Pupil"), funny_location - Main.screenPosition,
                new Rectangle(0, 0, 10, 10), Lighting.GetColor((int)npc.Center.X / 16, (int)npc.Center.Y / 16), 0,
                new Vector2(10 * 0.5f, 10 * 0.5f), 1f + scale_augment, SpriteEffects.None, 0f);
        }
    }
}