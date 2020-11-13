using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Dayrise;
using Terraria.ID;
using Terraria.ModLoader;

namespace Dayrise.Items.Weapons.Phosphor
{
    public class Glowbulb : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.rare = 4;
            item.maxStack = 999;
            item.value = Item.sellPrice(0, 0, 1, 50);
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Glowbulb");
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 255, 255);
        }
    }
}