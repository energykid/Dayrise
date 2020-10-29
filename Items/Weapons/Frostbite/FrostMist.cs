using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Shaders;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;
using Terraria.GameContent.Achievements;
using Microsoft.Xna.Framework.Graphics;

namespace Dayrise.Items.Weapons.Frostbite
{
	public class FrostMist : ModDust
	{
        public override bool Autoload(ref string name, ref string texture)
        {
            return true;
        }
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
            dust.shader = GameShaders.Armor.GetSecondaryShader(85, Main.LocalPlayer);
        }
        public override bool Update(Dust dust)
        {
            dust.velocity.X *= 1.14f;
            dust.velocity.Y *= 1.14f;
            return true;
        }
        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            return new Color(190, 175, 255).MultiplyRGBA(new Color(255f, 255f, 255f, 0.5f));
        }
    }
}