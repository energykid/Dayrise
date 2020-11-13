using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dayrise;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace Dayrise
{
    class DayriseTile : GlobalTile
    {
        public override bool Drop(int i, int j, int type)
        {
            if (Main.tile[i, j].type == TileID.MushroomTrees)
            {
                Item.NewItem(new Rectangle(i * 16, j * 16, 16, 16), mod.ItemType("Glowbulb"), Main.rand.Next(3));
            }

            if (Main.tile[i, j].type == TileID.MushroomPlants)
            {
                if (Main.rand.NextBool(2))
                    Item.NewItem(new Rectangle(i * 16, j * 16, 16, 16), mod.ItemType("Glowbulb"), Main.rand.Next(3));
            }

            return base.Drop(i, j, type);
        }
    }
}
