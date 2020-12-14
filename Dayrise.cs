using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using System.IO;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.UI;
using Terraria.GameContent.UI;
using MonoMod.Utils;
using static Mono.Cecil.Cil.OpCodes;
using MonoMod.Cil;
using MonoMod.ModInterop;
using MonoMod.RuntimeDetour;

namespace Dayrise
{
	public class Dayrise : Mod
	{
        internal static Dayrise instance;
		public Dayrise() => instance = this;
        public override void ModifySunLightColor(ref Color tileColor, ref Color backgroundColor)
        {
            if (DayriseWorld.suffocatingSun)
            {
                backgroundColor = tileColor = new Color(125, 125, 200);
            }
        }
        public static void PremultiplyTexture(Texture2D texture)
        {
            Color[] buffer = new Color[texture.Width * texture.Height];
            texture.GetData(buffer);
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = Color.FromNonPremultiplied(
                        buffer[i].R, buffer[i].G, buffer[i].B, buffer[i].A);
            }
            texture.SetData(buffer);
        }

        public override void PostSetupContent()
        {
            Mod bossList = ModLoader.GetMod("BossChecklist");
            if (bossList != null)
            {
                bossList.Call("AddBossWithInfo", "The Guide", 0.0001f, (Func<bool>)(() => DayriseWorld.wonVsGuide), string.Format("Challenge the Guide to a friendly sparring match while holding a [i:{0}]", ItemType("SparringBadge")));

                bossList.Call("AddBossWithInfo", "Four-Eyes", 2.2f, (Func<bool>)(() => DayriseWorld.downedFourEyes), string.Format("During a Blood Moon, hold out a [i:{0}]", ItemType("EpidermalIncense")));
            }
        }

        public static void DrawCustomSun(int x, int y, float rotation, float scale)
        {
            Texture2D texture = Main.sunTexture;
            if (DayriseWorld.suffocatingSun && !Main.gameMenu)
            {
                texture = ModContent.GetTexture("Dayrise/StrangledSun_Sun");
            }

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.BackToFront, texture == ModContent.GetTexture("Dayrise/StrangledSun_Sun") ? BlendState.NonPremultiplied : BlendState.Additive);
            Main.spriteBatch.Draw(texture, new Vector2(x, y), new Rectangle(0, 0, texture.Width, texture.Height), Color.White, rotation, texture.Size() / 2, scale, SpriteEffects.None, 0);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.Additive);
            if (texture == ModContent.GetTexture("Dayrise/StrangledSun_Sun"))
            {
                for (int i = 0; i <= Main.rand.Next(3, 6); i++)
                {
                    Main.spriteBatch.Draw(texture, new Vector2(x + Main.rand.Next(-10, 10), y + Main.rand.Next(-10, 10)), new Rectangle(0, 0, texture.Width, texture.Height), Color.White, rotation, texture.Size() / 2, scale, SpriteEffects.None, 0);
                }
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin();
        }

        public override void UpdateMusic(ref int music, ref MusicPriority musicPriority)
        {
            if (Main.musicVolume != 0)
            {
                if (Main.myPlayer != -1 && !Main.gameMenu && Main.LocalPlayer.active)
                {
                    if (DayriseWorld.suffocatingSun)
                    {

                        music = GetSoundSlot(SoundType.Music, "Sounds/Music/solar_strangle");
                        musicPriority = MusicPriority.Event;
                    }
                }
            }
        }

        public static Effect PrismShader;
        public override void Load()
        {
            PrismShader = instance.GetEffect("Effects/PrismShader");
            Ref<Effect> specialRef = new Ref<Effect>(GetEffect("Effects/Colour"));
            GameShaders.Misc["Dayrise:Colour"] = new MiscShaderData(specialRef, "ModdersToolkitShaderPass");

            IL.Terraria.Main.UpdateTime += il =>
            {
                var c = new ILCursor(il);

                if (!c.TryGotoNext(i => i.MatchLdcI4(20)))
                {
                    return;
                }

                c.Index += 3;

                c.Emit(Mono.Cecil.Cil.OpCodes.Call, typeof(DayriseWorld).GetMethod("StartSuffocatingSun"));

                var label = c.DefineLabel();
                c.Emit(Brtrue, label);

                c.Index += 4;

                c.MarkLabel(label);
            };


            IL.Terraria.Main.DoDraw += il =>
            {
                var c = new ILCursor(il);

                if (!c.TryGotoNext(i => i.MatchLdcR4(1.1f)))
                {
                    return;
                }
                c.Index--;

                var label = c.DefineLabel();
                var label2 = c.DefineLabel();

                c.Emit(Ldc_I4_1);
                c.Emit(Brfalse, label);
                c.Emit(Ldloc_3);
                c.Emit(Ldloc, 4);
                c.Emit(Ldloc, 7);
                c.Emit(Ldloc, 6);
                c.Emit(Mono.Cecil.Cil.OpCodes.Call, typeof(Dayrise).GetMethod("DrawCustomSun"));
                c.Emit(Br, label2);

                c.MarkLabel(label);

                if (!c.TryGotoNext(
                    i => i.MatchCallvirt(typeof(SpriteBatch), "Draw"),
                    i => i.MatchLdsfld(typeof(Main), "dayTime")))
                {
                    return;
                }
                c.Index++;
                c.Emit(Pop);
                c.Emit(Mono.Cecil.Cil.OpCodes.Call, typeof(DayriseWorld).GetMethod("EclipseChance"));
                c.Index += 2;

                c.MarkLabel(label2);
            };

            PremultiplyTexture(ModContent.GetTexture("Dayrise/StrangledSun_Sun"));

            Filters.Scene["Dayrise:SuffocatingSun"] = new Filter(new ScreenShaderData("FilterMiniTower").UseColor(-0.18f, -0.15f, 0.06f).UseOpacity(0.6f), EffectPriority.VeryHigh);
        }
    }
}