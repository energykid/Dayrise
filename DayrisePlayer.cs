using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using IL.Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;

namespace Dayrise
{
    class DayrisePlayer : ModPlayer
    {
        public bool eyestalker = false;
        public bool visceralHeart = false;
        public bool visceralHeartVisual = false;

        public override void ResetEffects()
        {
            eyestalker = false;
            visceralHeart = false;
            visceralHeartVisual = false;
        }

        public override void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            float refa = a;
            a = ((player.statLifeMax - player.statLife) / 100);
            SpriteBatch spriteBatch = Main.spriteBatch;
            if (visceralHeartVisual)
            {
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.Additive);
                for (int i = 0; i < 8; i++)
                {
                    spriteBatch.Draw(mod.GetTexture("Items/Weapons/Visceral/ClottedClump"), player.Center + new Vector2(i < 4 ? -10 : 10, 0) + new Vector2(Main.rand.Next(-10, 10), Main.rand.Next(-10, 10)) - Main.screenPosition,
                    new Rectangle(0, 0, 14, 32), new Color(60, 10, 10), 0,
                    new Vector2(7, 16), new Vector2(2 + Main.rand.NextFloat(0.2f), 2 + Main.rand.NextFloat(0.2f)), i < 4 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
                }
                spriteBatch.End();
                spriteBatch.Begin();
            }
            a = refa;
        }
    }
}