using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameInput;
using Terraria.ID;
using Microsoft.Xna.Framework;
using IL.Terraria.DataStructures;
using Dayrise.Items.Weapons.Phosphor;
using Microsoft.Xna.Framework.Graphics;

namespace Dayrise
{
    class DayrisePlayer : ModPlayer
    {
        public bool eyestalker = false;
        public bool visceralHeart = false;
        public bool visceralHeartVisual = false;

        public float rangedVelocity = 1.0f;

        public bool frostbiteSetBonus = false;
        public bool phosphorSetBonus = false;

        public bool chilledAmulet = false;
        public bool chilledAmuletVisual = false;

        public bool fourEyesLips = false;

        float timer = 0;

        public Vector2[] oldPos = new Vector2[8] { Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero };

        public override void ResetEffects()
        {
            eyestalker = false;
            visceralHeart = false;
            visceralHeartVisual = false;

            rangedVelocity = 1.0f;

            frostbiteSetBonus = false;
            phosphorSetBonus = false;

            chilledAmulet = false;
            chilledAmuletVisual = false;

            fourEyesLips = false;

            for (int i = 7; i >= 0; i--)
            {
                if (i == 0)
                {
                    oldPos[i] = player.Center;
                }
                else if (i != 7)
                {
                    oldPos[i] = oldPos[i - 1];
                }
            }
        }

        public override void PostUpdateRunSpeeds()
        {
            if (phosphorSetBonus)
            {
                player.maxRunSpeed += 0.6f;
                player.runAcceleration += 0.15f;
                player.runSlowdown += 0.15f;
            }
        }

        public override void UpdateBiomeVisuals()
        {
            player.ManageSpecialBiomeVisuals("Dayrise:SuffocatingSun", DayriseWorld.suffocatingSun);
        }

        public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
        {
            if (fourEyesLips)
            {
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/FourEyesLips_Pog"), player.Center);
                Main.NewText("POG");
            }
        }

        public override bool Shoot(Item item, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (item.ranged)
            {
                speedX *= rangedVelocity;
                speedY *= rangedVelocity;
            }
            return base.Shoot(item, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

        public override void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            timer++;
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
                    new Rectangle(0, 0, 14, 32), new Color(60, 10, 10, 100), 0,
                    new Vector2(7, 16), new Vector2(2 + Main.rand.NextFloat(0.2f), 2 + Main.rand.NextFloat(0.2f)), i < 4 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
                }
                spriteBatch.End();
                spriteBatch.Begin();
            }
            if (chilledAmuletVisual)
            {
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.Additive);
                for (int i = -2; i <= 2; i += 2)
                {
                    Texture2D tex = mod.GetTexture("Items/Weapons/Frostbite/ChilledAmulet");

                    float cos1 = MathHelper.Clamp((float)Math.Cos((timer / 10) + Math.Abs(i / 2)), 0, 1f) * 5;

                    spriteBatch.Draw(tex, player.Center - new Vector2(0, player.height / 2) + new Vector2(0, 5) - Main.screenPosition,
                    new Rectangle(0, 0, tex.Width, tex.Height), new Color(255, 255, 255, 255), MathHelper.ToRadians(180) + i * MathHelper.ToRadians(17),
                    new Vector2(tex.Width/2, -15 - cos1), 1f, SpriteEffects.None, 0f);
                }
                spriteBatch.End();
                spriteBatch.Begin();
            }
            if (player.GetModPlayer<DayriseDash>().DashTimer2 > 3)
            {
                for (int i = 0; i < 3; i++)
                {
                    Dust dust;
                    Vector2 position = player.position + new Vector2(5, 7);
                    dust = Dust.NewDustDirect(position, player.width - 10, player.height - 14, mod.DustType("LightParticle"), 0f, 0f, 0, new Color(255, 255, 255), 0.6f);
                    dust.noGravity = true;
                }
                float alpha = player.GetModPlayer<DayriseDash>().DashTimer2 * 9f;
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.Additive);
                for (int i = 0; i < 2; i++)
                {
                    spriteBatch.Draw(mod.GetTexture("Items/Weapons/Phosphor/DashTrail"), player.Center - Main.screenPosition,
                    new Rectangle(0, 0, 50, 50), new Color(alpha, alpha, alpha, alpha), 0,
                    new Vector2(25, 25), 1.3f, SpriteEffects.None, 0f);
                }
                for (int i = 0; i < 8; i++)
                {
                    spriteBatch.Draw(mod.GetTexture("Items/Weapons/Phosphor/DashTrail"), oldPos[i] - Main.screenPosition,
                    new Rectangle(0, 0, 50, 50), new Color(alpha, alpha, alpha, alpha), 0,
                    new Vector2(25, 25), 1 - (0.1f * i), SpriteEffects.None, 0f);
                }
                spriteBatch.End();
                spriteBatch.Begin();
            }
            if (player.GetModPlayer<DayriseDash>().DashTimer2 == 3)
            {
                player.fallStart = (int)player.Bottom.Y;
                player.fallStart2 = (int)player.Bottom.Y;
                for (int i = 0; i < 10; i++)
                {
                    Dust dust;
                    Vector2 position = player.position;
                    dust = Dust.NewDustDirect(position, player.width, player.height, mod.DustType("LightParticle"), 0f, 0f, 0, new Color(255, 255, 255), 1f);
                    dust.noGravity = true;
                }
            }

            a = refa;
        }
    }
}