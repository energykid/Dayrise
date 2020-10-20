using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Dayrise.NPCs.Bosses
{
    [AutoloadBossHead]
	public class GuideBoss : ModNPC
	{
		bool start = false;

		int dodgeTimer = 0;

		bool aiming = false;

		Vector2 pos = Vector2.Zero;
		public override void SetStaticDefaults()
        {
			Main.npcFrameCount[npc.type] = 4;
		}

        public override void SetDefaults()
		{
			npc.CloneDefaults(NPCID.Zombie);

			npc.GivenName = npc.GivenName + " the Guide";
			npc.aiStyle = -1;
			npc.noTileCollide = false;
			npc.width = 22;
			npc.height = 52;
			npc.noGravity = false;
			npc.friendly = true;
			npc.dontTakeDamage = true;
			npc.life = 250;
			npc.lifeMax = 250;
			animationType = -1;
			npc.knockBackResist = 0f;
			npc.scale = 1;
			music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/scholarly_training");
			npc.damage = 13;
			npc.defense = 4;
		}

        public override void AI()
		{
			npc.TargetClosest();
			Player player = Main.player[npc.target];

			aiming = false;

			npc.direction = player.Center.X > npc.Center.X ? 1 : -1;

			if (!start)
			{
				foreach (Projectile projectile in Main.projectile)
				{
					if (projectile.active && projectile.friendly && dodgeTimer <= 0 && Math.Abs(npc.Distance(projectile.Center)) < (Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y)) * 10)
					{
						npc.velocity.X = -npc.direction * 3;
						npc.velocity.Y = -5;
						start = true;
						npc.boss = true;
						npc.lifeMax = 500;
						npc.life = 500;
						npc.dontTakeDamage = false;
					}
				}
			}

			if (start)
			{
				npc.friendly = false;
				if (npc.ai[1] == 0)
				{
					foreach (Projectile projectile in Main.projectile)
					{
						if (projectile.active && projectile.friendly && dodgeTimer <= 0 && Math.Abs(npc.Distance(projectile.Center)) < (Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y)) * 10)
						{
							npc.ai[0] += 1;
							if (npc.ai[0] < 3)
							{
								if (!npc.collideY)
								{
									for (int i = 0; i < 22; i++)
									{
										Dust dust;
										// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
										Vector2 position = npc.Center + new Vector2(0, npc.height / 2);
										dust = Main.dust[Dust.NewDust(position - new Vector2(5, 0), 10, 4, 16, Main.rand.Next(-3, 4), 0f, 0, new Color(255, 255, 255), 1.578947f)];
										dust.noGravity = true;
									}
									Main.PlaySound(16, npc.Center);
								}
								dodgeTimer = 15;
								if (Math.Abs(player.Center.X - npc.Center.X) > 600)
								{
									npc.velocity = new Vector2(projectile.direction * 10, -7.3f);
								}
								else if (Main.tile[(int)npc.Center.X / 16, (int)(npc.position.Y + npc.height + 5) / 16].type == TileID.Platforms)
								{
									if (Main.rand.NextBool(2))
									{
										npc.velocity = Vector2.Normalize(new Vector2(projectile.velocity.X, 0)) * 5;
										npc.velocity.Y = -7.3f;
									}
									else
									{
										npc.position.Y += 1;
										npc.velocity = new Vector2(-projectile.direction * 5, 7.3f);
									}
								}
								else
								{
									npc.velocity = Vector2.Normalize(new Vector2(projectile.velocity.X, 0)) * 5;
									npc.velocity.Y = -7.3f;
								}
							}
						}
					}
				}

				dodgeTimer--;

				npc.velocity.Y += 0.12f;

				npc.ai[2]++;
				if (npc.ai[2] == 120)
				{
					if (npc.life > npc.lifeMax * 0.4f) npc.ai[1] = Main.rand.Next(1, 3);
					else npc.ai[1] = Main.rand.Next(1, 4);
				}

				float attackTimer = npc.ai[2] - 120;

				if (npc.ai[1] == 1)
				{
					if (attackTimer == 1)
					{
						pos = player.Center + new Vector2(0, -200);
						npc.velocity.Y = -7.3f;
						npc.velocity.X = npc.direction * 2;
					}
					if (attackTimer > 1)
					{
						npc.velocity = Vector2.Lerp(npc.velocity, npc.DirectionTo(pos) * 8.5f + new Vector2(0, -5), 0.5f);
						aiming = true;
						if (npc.Distance(pos) < 10 || attackTimer >= 50)
						{
							for (int i = 0; i < 22; i++)
							{
								Dust dust;
								// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
								Vector2 position = npc.Center + new Vector2(0, npc.height / 2);
								dust = Main.dust[Dust.NewDust(position - new Vector2(5, 0), 10, 4, 16, Main.rand.Next(-3, 4), 0f, 0, new Color(255, 255, 255), 1.578947f)];
								dust.noGravity = true;
							}
							Main.PlaySound(SoundID.DoubleJump, npc.Center);
							Main.PlaySound(SoundID.Item5, npc.Center);

							int proj = Projectile.NewProjectile(npc.Center, npc.DirectionTo(player.MountedCenter) * 9, npc.life < npc.lifeMax * 0.7f ? ProjectileID.JestersArrow : ProjectileID.WoodenArrowHostile, 9, 5f, player.whoAmI);
							Main.projectile[proj].friendly = false;
							Main.projectile[proj].hostile = true;

							aiming = false;
							npc.ai[1] = 0;
							npc.ai[2] = 0;
							npc.velocity.Y = -7.3f;
							npc.velocity.X = -npc.direction * 2;
						}
					}
				}
				else if (npc.ai[1] == 2)
				{
					if (attackTimer == 1)
					{
						npc.velocity.Y = -12.3f;
						npc.velocity.X = npc.direction * 4;
					}
					if (attackTimer > 1)
					{
						if (Main.tile[(int)npc.Center.X / 16, (int)(npc.position.Y + npc.height + 5) / 16].type == TileID.Platforms && npc.collideY)
						{
							npc.position.Y += 1;
						}
						if (npc.velocity.Y > 0) npc.velocity.Y *= 0.88f;
						npc.velocity.X *= 0.99f;
						npc.velocity.Y -= 0.05f;
						aiming = true;
						if (attackTimer % 20 == 10 && attackTimer > 40)
						{
							Main.PlaySound(SoundID.Item5, npc.Center);

							int proj = Projectile.NewProjectile(npc.Center, npc.DirectionTo(player.MountedCenter) * 9, npc.life < npc.lifeMax * 0.7f ? ProjectileID.FlamingArrow : ProjectileID.WoodenArrowHostile, 9, 5f, player.whoAmI);
							Main.projectile[proj].friendly = false;
							Main.projectile[proj].hostile = true;
						}
						if (attackTimer >= 95)
						{
							aiming = false;
							npc.ai[1] = 0;
							npc.ai[2] = 0;
						}
					}
				}
				else if (npc.ai[1] == 3)
				{
					if (attackTimer == 1)
					{
						npc.velocity.Y = -12.3f;
						npc.velocity.X = npc.direction * 4;
					}
					if (attackTimer > 1)
					{
						if (npc.velocity.Y >= -0.25f) npc.velocity.Y = -0.25f;
						npc.velocity.X *= 0.99f;
						npc.velocity.Y -= 0.08f;
						aiming = true;
						if (attackTimer % 50 == 10 && attackTimer > 40)
						{
							Main.PlaySound(SoundID.Item5, npc.Center);

							for (int i = -2; i <= 2; i++)
							{
								int proj = Projectile.NewProjectile(npc.Center, npc.DirectionTo(player.MountedCenter).RotatedBy(MathHelper.ToRadians(35 * i)) * 9, ProjectileID.HellfireArrow, 11, 5f, player.whoAmI);
								Main.projectile[proj].friendly = false;
								Main.projectile[proj].hostile = true;
							}
						}
						if (attackTimer >= 95)
						{
							aiming = false;
							npc.ai[1] = 0;
							npc.ai[2] = 0;
						}
					}
				}
				else if (npc.Distance(player.Center) > 500)
				{
					npc.velocity += npc.DirectionTo(player.Center) * 0.8f;
				}

				if (npc.collideY)
				{
					aiming = false;
					npc.ai[0] = 0;
					npc.velocity.X = 0;
				}
			}
			else
			{
				npc.TargetClosest();
				int xCenterTileCoords = (int)(npc.spriteDirection == -1 ? npc.Left.X : npc.Right.X) / 16;
				int yBottomTileCoords = (int)(npc.Bottom.Y - 15) / 16;

				if (npc.velocity.X != 0)
                {
					npc.ai[0] += Math.Abs(npc.velocity.X) * 0.1f;
                }

				Tile tileAheadAboveTarget = Framing.GetTileSafely(new Point(xCenterTileCoords + npc.spriteDirection, yBottomTileCoords - 1));
				Tile tileBelowTarget = Framing.GetTileSafely(new Point(xCenterTileCoords, yBottomTileCoords + 1));
				Tile tileAheadBelowTarget = Framing.GetTileSafely(new Point(xCenterTileCoords + npc.spriteDirection, yBottomTileCoords + 1));

				npc.noGravity = false;
				npc.noTileCollide = false;

				if (npc.velocity.Y < 8f)
					npc.velocity.Y += 0.2f;

				float maxRunSpeed = 4f;
				float runAcceleration = 0.1f;

				if (Math.Abs(npc.Center.X - player.Center.X) >= 80) npc.velocity.X = Utils.Clamp(npc.velocity.X + runAcceleration * npc.direction, -maxRunSpeed, maxRunSpeed);
				else npc.velocity.X *= 0.75f;

				bool onSolidGround = tileBelowTarget.active() && Main.tileSolid[tileBelowTarget.type];
				float yDistanceFromTarget = player.Center.Y - npc.Center.Y;

				if (onSolidGround)
				{
					/*if (tileAheadAboveTarget.active() && Main.tileSolid[tileAheadAboveTarget.type])
					{
						npc.velocity.Y = -8f;
						npc.netUpdate = true;
					}

					if (!tileAheadBelowTarget.active() && Main.tileSolid[tileAheadBelowTarget.type])
					{
						npc.velocity.Y = -10f;
						npc.netUpdate = true;
					}

					if (npc.position.X == npc.oldPosition.X)
					{
						npc.velocity.Y = -12f;
						npc.netUpdate = true;
					}*/

					if (npc.collideY)
					{
						if (Main.player[npc.target].Center.Y > npc.Center.Y + 10 && Main.tile[(int)(npc.Center.X / 16), (int)((npc.Center.Y + 30) / 16)].type == TileID.Platforms) npc.position.Y += 2;
					}
				}
			}

			if (Main.player[npc.target].statLife <= 25)
			{
				int npc2 = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.Guide);
				Main.npc[npc2].GivenName = npc.GivenName;

				npc.active = false;
			}
		}

        public override void FindFrame(int frameHeight)
		{
			if (npc.velocity.Y > 0) npc.frame.Y = 52 * 2;
			if (npc.velocity.Y < 0) npc.frame.Y = 52 * 3;
			if (npc.velocity.Y == 0) npc.frame.Y = 0;
			if (npc.ai[1] == 2 || npc.ai[1] == 3) npc.frame.Y = 52 * 3;
		}

        public override bool CheckDead()
        {
			int npc2 = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.Guide);
			Main.npc[npc2].GivenName = npc.GivenName;
			if (!DayriseWorld.wonVsGuide)
            {
				Item.NewItem(npc.getRect(), mod.ItemType("BlankEmblem"));
            }
			DayriseWorld.wonVsGuide = true;
			return false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			if (start)
			{
				Texture2D tex = mod.GetTexture("NPCs/Bosses/GuideBoss");
				Texture2D tex2 = mod.GetTexture("NPCs/Bosses/GuideArmNoAim");
				Texture2D tex2a = mod.GetTexture("NPCs/Bosses/GuideArm");
				Player player = Main.player[npc.target];

				Vector2 pos = npc.Center;
				spriteBatch.Draw(tex, pos - Main.screenPosition,
				new Rectangle(0, npc.frame.Y, 66, 52), Lighting.GetColor((int)npc.Center.X / 16, (int)npc.Center.Y / 16), 0,
				new Vector2(33, 26), 1f, player.Center.X > npc.Center.X ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);

				spriteBatch.Draw(aiming && npc.frame.Y != 52 * 2 ? tex2a : tex2, pos - Main.screenPosition - (npc.position.X < player.position.X ? new Vector2(42 - 26, 0) : Vector2.Zero) + (aiming && npc.frame.Y != 52 * 2 ? new Vector2(8, -3) : new Vector2(8, -1)),
				new Rectangle(0, aiming ? 0 : (npc.frame.Y == 0 ? npc.frame.Y : npc.height), 66, 52), Lighting.GetColor((int)npc.Center.X / 16, (int)npc.Center.Y / 16), aiming ? npc.AngleTo(player.Center) + MathHelper.ToRadians(-90) : 0,
				new Vector2(player.Center.X > npc.Center.X ? 24 : 41, 25), 1f, player.Center.X > npc.Center.X ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
			}

			if (!start)
            {
				Texture2D tex = mod.GetTexture("NPCs/Bosses/GuideBoss_Walking");
				Player player = Main.player[npc.target];

				float walkFrame = (npc.ai[0] % 14) + 2;

				Vector2 pos = npc.Center;
				if (!npc.collideY)
                {
					spriteBatch.Draw(tex, pos - Main.screenPosition,
					new Rectangle(0, 56, 40, 54), Lighting.GetColor((int)npc.Center.X / 16, (int)npc.Center.Y / 16), 0,
					new Vector2(20, 27), 1f, player.Center.X > npc.Center.X ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
				}
				else if (Math.Abs(npc.velocity.X) >= 1)
				{
					spriteBatch.Draw(tex, pos - Main.screenPosition,
					new Rectangle(0, (int)Math.Round((int)walkFrame * 56f), 40, 54), Lighting.GetColor((int)npc.Center.X / 16, (int)npc.Center.Y / 16), 0,
					new Vector2(20, 27), 1f, player.Center.X > npc.Center.X ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
				}
                else
                {
					spriteBatch.Draw(tex, pos - Main.screenPosition,
					new Rectangle(0, 0, 40, 54), Lighting.GetColor((int)npc.Center.X / 16, (int)npc.Center.Y / 16), 0,
					new Vector2(20, 27), 1f, player.Center.X > npc.Center.X ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
				}
			}

			return false;
        }
    }

	public class GuideGlobal : GlobalNPC
	{
		public override bool InstancePerEntity => true;

        public override bool PreChatButtonClicked(NPC npc, bool firstButton)
        {
			if (npc.type == NPCID.Guide)
			{
				if (firstButton && Main.LocalPlayer.HasItem(mod.ItemType("SparringBadge")))
				{
					//Main.LocalPlayer.talkNPC = null;
					Main.NewText("<" + npc.FullName + "> Just take me to wherever you'd like to train. You can fire the first shot when we get there!");
					npc.active = false;
					int npc2 = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("GuideBoss"));
					Main.npc[npc2].GivenName = npc.GivenName;
				}
			}
			return base.PreChatButtonClicked(npc, firstButton);
		}
        public override void GetChat(NPC npc, ref string chat)
        {
			if (npc.type == NPCID.Guide)
            {
				if (Main.LocalPlayer.HasItem(mod.ItemType("SparringBadge")))
                {
					chat = "Ah, I see your badge! If you'd like to spar, just ask me for the help - I can do that!";
                }
			}
			base.GetChat(npc, ref chat);
        }
    }
}