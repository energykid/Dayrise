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

namespace Dayrise.Items.Weapons.Visceral
{
    public class VisceralHeart : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 42;
            item.expert = true;
            item.rare = 2;
            item.value = Item.sellPrice(0, 2, 50, 0);
            item.accessory = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Visceral Heart");
            Tooltip.SetDefault("1% extra damage per missing 4HP \nThis value does not cap out");
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<DayrisePlayer>().visceralHeart = true;
            player.GetModPlayer<DayrisePlayer>().visceralHeartVisual = true;
            player.meleeDamage *= (1f + (((float)player.statLifeMax - (float)player.statLife) / 400f));
            player.rangedDamage *= (1f + (((float)player.statLifeMax - (float)player.statLife) / 400f));
            player.magicDamage *= (1f + (((float)player.statLifeMax - (float)player.statLife) / 400f));
            player.minionDamage *= (1f + (((float)player.statLifeMax - (float)player.statLife) / 400f));
        }
    }
}