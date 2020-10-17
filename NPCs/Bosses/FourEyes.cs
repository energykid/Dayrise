using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using IL.Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;

namespace Dayrise.NPCs.Bosses
{
	[AutoloadBossHead]
	public class FourEyes : ModNPC
	{
		float cos1 = 0;
		bool start = false;

        public override void SetStaticDefaults()
        {
			DisplayName.SetDefault("Four-Eyes");

			Main.npcFrameCount[npc.type] = 6;
		}

        public override void SetDefaults()
		{
			npc.CloneDefaults(NPCID.CaveBat);

			npc.aiStyle = -1;
			npc.noTileCollide = false;
			npc.width = 44;
			npc.height = 46;
			npc.noGravity = true;
			npc.life = 2500;
			npc.lifeMax = 2500;
			npc.knockBackResist = 0f;
			npc.scale = 1;
			npc.boss = true; 
			music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/epidermis");
			npc.dontTakeDamage = true;
			npc.damage = 23;
			npc.defense = 1;

			npc.DeathSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/NPC/FourEyes_Death");
		}

        public override bool PreAI()
        {
			npc.dontTakeDamage = false;
            return true;
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

			if (!start)
			{
				for (int i = 0; i < 4; i++)
				{
					NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("FourEyes_Eye"), ai0: npc.whoAmI, ai1: i);
				}
				start = true;
			}


			Player player = Main.player[npc.target];

			cos1 += 0.05f;

			npc.rotation = 0f;
			npc.rotation += MathHelper.ToRadians(npc.velocity.X * 4);

			if (player.dead || Main.dayTime || !Main.bloodMoon)
            {
				if (NPC.CountNPCS(mod.NPCType("FourEyes_Eye")) > 0)
                {
					npc.velocity += npc.DirectionFrom(player.Center) * (0.06f + (float)Math.Cos(cos1 * 0.6f) * 0.03f);
				}
				else
				{
					npc.velocity.X *= 0.89f;
					npc.velocity.Y -= 0.2f;
				}

				if (npc.Distance(player.Center) > Main.screenWidth)
				{
					npc.active = false;
				}
			}
			
			npc.TargetClosest();

			if (NPC.CountNPCS(mod.NPCType("FourEyes_Eye")) > 0)
			{
				npc.velocity.X += (float)Math.Cos(cos1 * 2f) * 0.05f;
				npc.velocity.Y += (float)Math.Cos((cos1 * 2f) + 0.5f) * 0.05f;

				if (!player.dead)
				{
					npc.velocity += npc.DirectionTo(player.Center + new Vector2(0, -120)) * (0.06f + (float)Math.Cos(cos1 * 0.6f) * 0.03f);
				}

				if ((Math.Abs(npc.velocity.X + npc.velocity.Y / 2)) > 3.5f) npc.velocity *= 0.92f;
			}
			else
			{
				if ((Math.Abs(npc.velocity.X + npc.velocity.Y / 2)) > 3.5f) npc.velocity *= 0.92f;

				npc.ai[1]++;

				if (npc.ai[0] == 0)
				{
					npc.dontTakeDamage = true;

					npc.velocity *= 0.91f;

					if (npc.ai[1] % 70 == 0)
					{
						if (npc.frame.Y < 10) Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/NPC/FourEyes_Transit1"), npc.Center);
                        else if (npc.frame.Y >= 45 * 2 && npc.frame.Y <= 47 * 2)
							Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/NPC/FourEyes_Transit2"), npc.Center);
						if (npc.frame.Y < 46 * 4) npc.frame.Y += 46;
					}
					if (npc.ai[1] == 160)
                    {
						Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/NPC/FourEyes_Roar"), npc.Center);
					}
					if (npc.ai[1] == 170)
					{
						npc.frame.Y = 46 * 5;

						for (int i = 0; i < 20; i++)
						{
							Dust dust;
							// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
							Vector2 position = npc.Center - new Vector2(10, 10);
							Vector2 velocity = new Vector2(0, -3).RotatedBy(MathHelper.ToRadians(Main.rand.Next(360)));
							dust = Main.dust[Terraria.Dust.NewDust(position, 20, 20, 5, velocity.X, velocity.Y, 0, new Color(255, 255, 255), 1.22f)];
						}
					}
					if (npc.ai[1] == 190)
                    {
						npc.ai[0] = 1;
						npc.ai[1] = 0;
						npc.ai[2] = 0;
					}
				}
				else
                {
					npc.dontTakeDamage = false;
				}

				if (npc.ai[0] == 1)
				{
					npc.velocity *= 0.89f;

					npc.velocity.X += (float)Math.Cos(cos1 * 2) * 0.22f;
					npc.velocity.Y += (float)Math.Cos(cos1 * 4) - 0.15f * 0.4f;
					npc.velocity.Y += 0.05f;

					if (npc.ai[1] % 23 == 0)
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
							Projectile.NewProjectile(npc.Center, npc.DirectionTo(npc.Center + npc.velocity + new Vector2(npc.velocity.X, -9)).RotatedBy(MathHelper.ToRadians(Main.rand.Next(-30, 31))) * Main.rand.Next(5, 11), mod.ProjectileType("BloodProj"), 10, 6f, player.whoAmI);
						}
					}

					if (npc.ai[1] > 240)
                    {
						npc.ai[0] = Main.rand.NextBool(3) ? 3 : 2;
						npc.ai[1] = 0;
						npc.ai[2] = 2;
					}
				}

				if (npc.ai[0] == 2)
                {
					npc.ai[1] += npc.ai[2];

					if (npc.ai[1] % 90 < 10)
					{
						npc.velocity += npc.DirectionTo(player.Center).RotatedBy(MathHelper.ToRadians(npc.ai[2] == 2 ? 90 : -90)) * (0.7f * npc.ai[2]);
					}
					else if (npc.ai[1] % 90 < 30)
					{
						npc.velocity += npc.DirectionTo(player.Center) * (2f * npc.ai[2]);
					}
					else if (npc.ai[1] % 90 < 90)
					{
						npc.velocity *= 0.96f;
					}

					if (npc.ai[1] % 90 > 12)
                    {
						
						// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
						Vector2 position = npc.Center + (npc.DirectionTo(npc.Center + npc.velocity) * 44);

						float vel_val = Math.Abs(npc.velocity.X + npc.velocity.Y / 2);
						if (vel_val > 14) vel_val = 14;

						Dust dust;
						dust = Main.dust[Dust.NewDust(position, 0, 0, 5, 0f, 0f, 0, new Color(255, 255, 255), vel_val / 10f)];

						dust.velocity = npc.DirectionTo(npc.Center + npc.velocity).RotatedBy(MathHelper.ToRadians(90)) * 6f;

						Dust dust2;
						dust2 = Main.dust[Dust.NewDust(position, 0, 0, 5, 0f, 0f, 0, new Color(255, 255, 255), vel_val / 10f)];

						dust2.velocity = npc.DirectionTo(npc.Center + npc.velocity).RotatedBy(MathHelper.ToRadians(-90)) * 6f;
					}

					if (npc.ai[1] % 90 == 89)
                    {
						npc.ai[2] = Main.rand.Next(1, 3);
                    }

					if (npc.ai[1] > 680)
					{
						npc.ai[0] = 3;
						npc.ai[1] = 0;
						npc.ai[2] = 0;
					}
                }

				if (npc.ai[0] == 3)
                {
					npc.velocity.X += (float)Math.Cos(cos1 * 2f) * 0.05f;
					npc.velocity.Y += (float)Math.Cos((cos1 * 2f) + 0.5f) * 0.05f;

					npc.velocity += npc.DirectionTo(player.Center + new Vector2(0, -50)) * (0.06f + (float)Math.Cos(cos1 * 0.6f) * 0.03f) * 4;

					npc.velocity *= 0.99f;

					if (npc.ai[1] % 20 == 0 && npc.ai[1] > 50)
                    {
						if (Main.rand.Next(4) > 0)
                        {
							for (int i = 0; i < 10; i++)
							{
								Dust dust;
								// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
								Vector2 position = npc.Center - new Vector2(10, 10);
								dust = Main.dust[Terraria.Dust.NewDust(position, 20, 20, 5, 0f, 0f, 0, new Color(255, 255, 255), 1.22f)];
							}

							for (int i = 0; i < Main.rand.Next(5); i++)
							{
								Projectile.NewProjectile(npc.Center, npc.DirectionFrom(player.Center).RotatedBy(MathHelper.ToRadians(Main.rand.Next(360))) * Main.rand.Next(4, 13), mod.ProjectileType("EyeProj"), 10, 6f, player.whoAmI, player.Center.X, player.Center.Y);
							}

							npc.velocity += npc.DirectionFrom(player.Center).RotatedBy(MathHelper.ToRadians(Main.rand.Next(-20, 21))) * 2.3f;
						}
					}

					if (npc.ai[1] > 240)
					{
						npc.ai[0] = 1;
						npc.ai[1] = 0;
						npc.ai[2] = 0;
					}
				}
            }
		}

        public override void BossLoot(ref string name, ref int potionType)
        {
			if (Main.expertMode)
            {
				Item.NewItem(npc.getRect(), mod.ItemType("FourEyesBag"));
			}
			else
            {
				int numOfWeapons = 1;
				int weaponPoolCount = 6;
				int[] weaponLoot = new int[numOfWeapons];
				for (int n = 0; n < numOfWeapons; n++)
				{
					weaponLoot[n] = Main.rand.Next(weaponPoolCount - n);
					for (int j = 0; j < n; j++)
					{
						if (weaponLoot[n] >= weaponLoot[j])
						{
							weaponLoot[n]++;
						}
						Array.Sort(weaponLoot);
					}
				}
				for (int i = 0; i < weaponLoot.Length; i++)
				{
					string dropName = "none";
					switch (weaponLoot[i])
					{
						case 0:
							dropName = "BentbloodStaff";
							break;
						case 1:
							dropName = "RedDeath";
							break;
						case 2:
							dropName = "BlooddropBow";
							break;
						case 3:
							dropName = "ClottedScripture";
							break;
						case 4:
							dropName = "GashBringingSlasher";
							break;
						case 5:
							dropName = "OneEye";
							break;
						case 6:
							dropName = "EyestalkerStaff";
							break;
					}
					if (dropName != "none")
					{
						Item.NewItem(npc.getRect(), mod.ItemType(dropName));
					}
				}
			}
		}

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D tex = mod.GetTexture("NPCs/Bosses/FourEyes");
			int width = tex.Width / 2;
			int height = 46 / 2;

			float wonk_increment = 0.2f;

			float xscale = 1f - ((float)Math.Cos(cos1) * wonk_increment) / 1.75f;
			float yscale = 1f + ((float)Math.Cos(cos1) * wonk_increment) / 1.75f;
				
			Vector2 pos = npc.position;
			spriteBatch.Draw(tex, pos - Main.screenPosition,
			new Rectangle(0, npc.frame.Y, npc.width, npc.height), Lighting.GetColor((int)npc.Center.X / 16, (int)npc.Center.Y / 16), 0,
			Vector2.Zero, new Vector2(xscale, yscale), SpriteEffects.None, 0f);

			if (npc.ai[0] != 0 || npc.ai[1] >= 170)
			{
				Player target = Main.player[npc.target];

				float dist = npc.Distance(target.Center);
				if (dist > 7) dist = 7;

				Vector2 funny_preinc = npc.DirectionTo(target.Center).RotatedBy(MathHelper.ToRadians(10));
				funny_preinc.X *= 1.2f;
				funny_preinc.Y *= 0.7f;

				Vector2 funny_inc = ((funny_preinc * dist)).RotatedBy(MathHelper.ToRadians(-10));

				Vector2 funny_location = npc.Center + funny_inc + new Vector2(0, 4);

				//Draw pupil

				spriteBatch.Draw(mod.GetTexture("NPCs/Bosses/FourEyes_Pupil"), funny_location - Main.screenPosition,
					new Rectangle(0, 0, 10, 10), Lighting.GetColor((int)npc.Center.X / 16, (int)npc.Center.Y / 16), 0,
					new Vector2(10 * 0.5f, 10 * 0.5f), 1f, SpriteEffects.None, 0f);
			}
			return false;
        }
	}
}