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

namespace Dayrise.Items.Weapons.Phosphor
{
	public class LightParticle : ModDust
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
            dust.velocity.X *= 0.98f;
            dust.velocity.Y *= 0.98f;
            return true;
        }
        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            return new Color(255, 255, 255, 255);
        }
    }
}