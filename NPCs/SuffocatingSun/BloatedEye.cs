using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using IL.Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;

namespace Dayrise.NPCs.SuffocatingSun
{
	public class BloatedEye : ModNPC
	{
        public override void SetStaticDefaults()
        {
			DisplayName.SetDefault("Bloated Eye");
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return DayriseWorld.suffocatingSun ? 0.25f : 0f;
		}

		public override void SetDefaults()
		{
			npc.aiStyle = 2;
			npc.noTileCollide = false;
			npc.width = 34;
			npc.height = 36;
			npc.noGravity = true;
			npc.life = 200;
			npc.lifeMax = 200;
			npc.knockBackResist = 0f;
			npc.scale = 1;
			npc.dontTakeDamage = false;
			npc.damage = 23;
			npc.defense = 1;
			npc.DeathSound = SoundID.NPCDeath12;
		}

        public override bool PreAI()
        {
			npc.dontTakeDamage = false;
            return true;
        }

        public override void AI()
		{
			npc.TargetClosest();

			Player player = Main.player[npc.target];

			npc.ai[0]++;

			if (npc.ai[0] > 120 && npc.ai[0] < 150)
			{
				npc.velocity += npc.DirectionTo(player.Center) * 0.2f;
			}
			if (npc.ai[0] > 200)
			{
				npc.ai[0] = 100;
			}

			if (npc.Distance(player.Center) < 120 && npc.ai[0] > 120)
			{
				npc.aiStyle = -1;
				if (npc.ai[1] < 5) npc.ai[1] = 5;
			}

			if (npc.ai[1] >= 5)
            {
				npc.ai[1]++;
				npc.velocity *= 0.79f;

				if (npc.ai[1] > 20)
				{
					for (int i = 0; i < 20; i++)
					{
						Dust dust = Dust.NewDustDirect(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, mod.DustType("BlueBloodDust"));
					}
					for (int i = 0; i < 5; i++)
					{
						Projectile.NewProjectile(npc.Center, new Vector2(0, Main.rand.Next(7, 12)).RotatedBy(MathHelper.ToRadians(Main.rand.Next(360))), mod.ProjectileType("BlueBloodProj"), 24, 2f, player.whoAmI);
					}
					for (int i = 0; i < 7; i++)
					{
						Projectile.NewProjectile(npc.Center, new Vector2(0, Main.rand.Next(2, 9)).RotatedBy(MathHelper.ToRadians(Main.rand.Next(360))), mod.ProjectileType("BlueBloodProj"), 24, 2f, player.whoAmI);
					}
					npc.StrikeNPC(npc.lifeMax + Main.rand.Next(10, 154), 1f, 1, true);
                }
			}
			else
			{
				npc.rotation = npc.AngleFrom(npc.Center + npc.velocity);
			}

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