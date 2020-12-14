using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using IL.Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;

namespace Dayrise.NPCs.SuffocatingSun
{
	public class ReachHand : ModNPC
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
			DisplayName.SetDefault("Reach's Hand");
        }

        public override void SetDefaults()
		{
			npc.CloneDefaults(NPCID.CaveBat);

			npc.aiStyle = -1;
            npc.noTileCollide = false;
			npc.width = 12;
			npc.height = 16;
			npc.noGravity = true;
			npc.life = 650;
			npc.lifeMax = 650;
			npc.scale = 1;
			npc.dontTakeDamage = true;
			npc.damage = 23;
			npc.defense = 1;

            npc.DeathSound = SoundID.NPCDeath12;
        }

		public override void AI()
        {
            if (npc.collideY)
            {
                if (Main.tile[(int)(npc.Center.X / 16), (int)((npc.Center.Y + 16) / 16)].type == TileID.Platforms)
                {
                    npc.position.Y += 2;
                }
            }

            npc.TargetClosest();

			cos1 += 0.05f;

			NPC parent = Main.npc[(int)npc.ai[0]];

            Player player = Main.player[parent.target];

            if (Main.rand.NextBool(20)) 
                npc.ai[1] = Main.rand.Next(360);

            npc.velocity = (parent.Center + (new Vector2(60, 0).RotatedBy(MathHelper.ToRadians(npc.ai[1])) - npc.position)) * 0.4f;
            if (npc.ai[2] == 2) npc.velocity = (parent.Center + (new Vector2(25, 0).RotatedBy(MathHelper.ToRadians(npc.ai[1])) - npc.position)) * 0.4f;

            if ((Math.Abs(npc.velocity.X + npc.velocity.Y / 2)) > 3.5f) npc.velocity *= 0.92f;

            if (!parent.active)
            {
                for (int i = 0; i < 8; i++)
                {
                    Dust dust = Dust.NewDustDirect(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, mod.DustType("BlueBloodDust"));
                }
                npc.active = false;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (npc.ai[0] != -1)
            {
                float scale = 1f;

                if (npc.ai[2] == 2) scale = 0.6f;

                NPC parent = Main.npc[(int)npc.ai[0]];

                Vector2 neckOrigin = parent.Center;
                Vector2 center = npc.Center;
                Vector2 distToProj = neckOrigin - npc.Center;
                float projRotation = distToProj.ToRotation() - 1.57f;
                float distance = distToProj.Length();

                Vector2 plusNinety = Vector2.Normalize(distToProj).RotatedBy(MathHelper.ToRadians(90)) * 2;

                spriteBatch.Draw(mod.GetTexture("NPCs/SuffocatingSun/ReachHand"), center - Main.screenPosition,
                            new Rectangle(0, 0, 12, 20), Lighting.GetColor((int)neckOrigin.X / 16, (int)neckOrigin.Y / 16), npc.AngleTo(neckOrigin) + MathHelper.ToRadians(90),
                            new Vector2(12 * 0.5f, 20 * 0.5f), scale, SpriteEffects.None, 0f);

                while (distance > 12f && !float.IsNaN(distance))
                {
                    distToProj.Normalize();                 //get unit vector
                    distToProj *= 20f;                      //speed = 30
                    distToProj *= scale;
                    center += distToProj;                   //update draw position
                    distToProj = neckOrigin - center;    //update distance
                    distance = distToProj.Length();

                    //Draw chain
                    spriteBatch.Draw(mod.GetTexture("NPCs/SuffocatingSun/ReachArm"), center - Main.screenPosition,
                            new Rectangle(0, 0, 12, 20), Lighting.GetColor((int)center.X / 16, (int)center.Y / 16), projRotation,
                            new Vector2(12 * 0.5f, 20 * 0.5f), scale, SpriteEffects.None, 0f);
                    npc.rotation = npc.AngleTo(neckOrigin) + MathHelper.ToRadians(90);
                }

                spriteBatch.Draw(mod.GetTexture("NPCs/SuffocatingSun/ReachArm"), center - Main.screenPosition,
                            new Rectangle(0, 0, 12, 20), Lighting.GetColor((int)center.X / 16, (int)center.Y / 16), projRotation,
                            new Vector2(12 * 0.5f, 20 * 0.5f), scale, SpriteEffects.None, 0f);
            }
            return false;
        }
    }
}