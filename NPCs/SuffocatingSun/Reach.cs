using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using IL.Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;

namespace Dayrise.NPCs.SuffocatingSun
{
	public class Reach : ModNPC
	{
		float cos1 = 0;
		bool start = false;

        public override void SetStaticDefaults()
        {
			DisplayName.SetDefault("Reach");
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return DayriseWorld.suffocatingSun ? 0.11f : 0f;
		}

		public override void SetDefaults()
		{
			npc.CloneDefaults(NPCID.CaveBat);

			npc.aiStyle = -1;
			npc.noTileCollide = false;
			npc.width = 34;
			npc.height = 36;
			npc.noGravity = true;
			npc.life = 900;
			npc.lifeMax = 900;
			npc.knockBackResist = 0f;
			npc.scale = 1;
			npc.dontTakeDamage = false;
			npc.damage = 23;
			npc.defense = 1;
			npc.DeathSound = SoundID.NPCDeath12;
		}

        public override void HitEffect(int hitDirection, double damage)
		{
			for (int i = 0; i < 4; i++)
			{
				Dust dust = Dust.NewDustDirect(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, mod.DustType("BlueBloodDust"));
			}
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
				for (int i = 0; i < 7; i++)
				{
					NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("ReachHand"), ai0: npc.whoAmI, ai1: Main.rand.Next(360));
				}
				start = true;
			}


			Player player = Main.player[npc.target];

			cos1 += 0.05f;

			npc.velocity.Y += (float)Math.Cos((cos1) + 0.5f) * 0.09f;

			npc.TargetClosest();

			if (!player.dead)
			{
				npc.velocity += npc.DirectionTo(player.Center + new Vector2(0, -120)) * (0.06f + (float)Math.Cos(cos1 * 0.6f) * 0.03f);
			}

			if (Math.Abs(npc.velocity.X) >= 4f) npc.velocity.X *= 0.9f;
			if (Math.Abs(npc.velocity.Y) >= 4f) npc.velocity.Y *= 0.9f;

			npc.rotation = 0f;
			npc.rotation += MathHelper.ToRadians(npc.velocity.X * 4);

			/*if (player.dead || !Main.dayTime || !DayriseWorld.suffocatingSun)
			{
				npc.velocity.X *= 0.89f;
				npc.velocity.Y -= 0.03f;

				if (npc.Distance(player.Center) > Main.screenWidth)
				{
					npc.active = false;
				}
			}*/
		}
	}
}