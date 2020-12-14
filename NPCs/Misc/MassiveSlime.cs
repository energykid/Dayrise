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
        public override void SetDefaults()
		{
			npc.aiStyle = 1;
            npc.noTileCollide = false;
			npc.width = 66;
			npc.noGravity = false;
			npc.height = 38;
			npc.life = 650;
			npc.lifeMax = 650;
			npc.scale = 2.5f;
			npc.dontTakeDamage = false;
			npc.damage = 23;
			npc.defense = 1;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
        }
		public override void AI()
        {
			npc.scale = MathHelper.Lerp(npc.scale, MathHelper.Lerp(0.5f, 2.5f, (float)npc.life / (float)npc.lifeMax), 0.2f);

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
		float alpha;
		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			alpha += 0.05f;
			SpriteEffects spriteEffects = SpriteEffects.None;
			if (npc.spriteDirection == 1)
			{
				spriteEffects = SpriteEffects.FlipHorizontally;
			}
			 Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);

             Color shadeColor = drawColor;
           Dayrise.PrismShader.Parameters["coloralpha"].SetValue(alpha);
             Dayrise.PrismShader.CurrentTechnique.Passes[0].Apply();
            Vector2 drawOrigin = new Vector2(Main.npcTexture[npc.type].Width * npc.scale / 2, Main.npcTexture[npc.type].Height * npc.scale / 2);
            Vector2 drawPos = npc.Center - Main.screenPosition;
            shadeColor.A = 150;
			float extraDrawY = Main.NPCAddHeight(npc.whoAmI);
			Vector2 origin = new Vector2(Main.npcTexture[npc.type].Width / 2, Main.npcTexture[npc.type].Height / Main.npcFrameCount[npc.type] / 2);
            Main.spriteBatch.Draw(Main.npcTexture[npc.type], new Vector2(npc.position.X - Main.screenPosition.X + npc.width / 2 - Main.npcTexture[npc.type].Width * npc.scale / 2f + origin.X * npc.scale, npc.position.Y - Main.screenPosition.Y + npc.height - Main.npcTexture[npc.type].Height * npc.scale / Main.npcFrameCount[npc.type] + 4f + extraDrawY + origin.Y * npc.scale + npc.gfxOffY), npc.frame, npc.GetColor(drawColor), npc.rotation, origin, npc.scale, spriteEffects, 0f);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.GameViewMatrix.TransformationMatrix);
			return false;
			/*
			SpriteEffects spriteEffects = SpriteEffects.None;
			if (npc.spriteDirection == 1)
			{
				spriteEffects = SpriteEffects.FlipHorizontally;
			}
			Main.spriteBatch.End();
			Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);
			// Retrieve reference to shader
			var deathShader = GameShaders.Misc["Dayrise:Colour"];
			deathShader.UseColor(new Color(25, 200, 105));
			deathShader.Apply(null);
			float extraDrawY = Main.NPCAddHeight(npc.whoAmI);
			Vector2 origin = new Vector2(Main.npcTexture[npc.type].Width / 2, Main.npcTexture[npc.type].Height / Main.npcFrameCount[npc.type] / 2);
			Main.spriteBatch.Draw(Main.npcTexture[npc.type],
				new Vector2(npc.position.X - Main.screenPosition.X + npc.width / 2 - (float)Main.npcTexture[npc.type].Width * npc.scale / 2f + origin.X * npc.scale,
				npc.position.Y - Main.screenPosition.Y + npc.height - Main.npcTexture[npc.type].Height * npc.scale / Main.npcFrameCount[npc.type] + 4f + extraDrawY + origin.Y * npc.scale + npc.gfxOffY),
				npc.frame,
				npc.GetAlpha(drawColor), npc.rotation, origin, npc.scale, spriteEffects, 0f);
			if (npc.color != default(Color))
			{
				Main.spriteBatch.Draw(Main.npcTexture[npc.type], new Vector2(npc.position.X - Main.screenPosition.X + npc.width / 2 - Main.npcTexture[npc.type].Width * npc.scale / 2f + origin.X * npc.scale, npc.position.Y - Main.screenPosition.Y + npc.height - Main.npcTexture[npc.type].Height * npc.scale / Main.npcFrameCount[npc.type] + 4f + extraDrawY + origin.Y * npc.scale + npc.gfxOffY), npc.frame, npc.GetColor(drawColor), npc.rotation, origin, npc.scale, spriteEffects, 0f);
			}
			// Restart spriteBatch to reset applied shaders
			Main.spriteBatch.End();
			Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.Transform);
			// Prevent Vanilla drawing
			return false;
			*/
		}
	}
}