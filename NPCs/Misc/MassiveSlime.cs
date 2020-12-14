using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using Terraria.Graphics.Shaders;
using IL.Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;

namespace Dayrise.NPCs.Misc
{
	public class MassiveSlime : ModNPC
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Massive Slime");
            Main.npcFrameCount[npc.type] = 4;
        }

        /*
        public override Color? GetAlpha(Color drawColor)
        {
            return Color.GreenYellow;
        }
		*/

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return NPC.downedGolemBoss ? 0.05f : 0f;
        }

        public override void SetDefaults()
        {
            npc.color = Color.GreenYellow;
            npc.aiStyle = 1;
            npc.noTileCollide = false;
            npc.width = 66;
            npc.noGravity = false;
            npc.height = 38;
            npc.life = 650;
            npc.lifeMax = 650;
            npc.scale = 2f;
            npc.dontTakeDamage = false;
            npc.damage = 23;
            npc.defense = 1;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
        }

        public override void AI()
        {
            npc.scale = MathHelper.Lerp(npc.scale, MathHelper.Lerp(0.5f, 2f, (float)npc.life / (float)npc.lifeMax), 0.2f);

            Player player = Main.player[npc.target];

            if (npc.collideY)
            {
                if (Main.tile[(int)(npc.Center.X / 16), (int)((npc.Center.Y + 30) / 16)].type == TileID.Platforms && player.Center.Y > npc.Center.Y)
                {
                    npc.position.Y += 2;
                }
            }

            npc.frameCounter += 0.2;
            if (npc.frameCounter >= 4) npc.frameCounter = 0;
            if (npc.collideY)
            {
                npc.frame.Y = ((int)npc.frameCounter) * 38;
            }
        }

        public override void NPCLoot()
        {

        }
    }
}