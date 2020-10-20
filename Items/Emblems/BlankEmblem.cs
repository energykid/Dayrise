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

namespace Dayrise.Items.Emblems
{
    public class BlankEmblem : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 42;
            item.rare = 1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blank Emblem");
            Tooltip.SetDefault("Forge a Novice Emblem at an Anvil with this...");
        }
    }
}