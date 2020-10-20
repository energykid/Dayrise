using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Dayrise.Items.Weapons.Visceral
{
    public class FourEyesBag : ModItem
    {
        public override void SetDefaults()
        {

            item.maxStack = 99;
            item.consumable = true;
            item.width = 32;
            item.height = 32;
            item.expert = true;
            item.rare = 9;
            item.expert = true;
            item.value = Item.buyPrice(0, 0, 0, 0);
        }

        public override int BossBagNPC => mod.NPCType("FourEyes");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Treasure Bag");
            Tooltip.SetDefault("Right click to open");
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void OpenBossBag(Player player)
        {
            //player.QuickSpawnItem(mod.ItemType("DarklightEssence"), Main.rand.Next(30, 43));
            //player.QuickSpawnItem(mod.ItemType("VisceralHeart"));
            int numOfWeapons = 1;
            int weaponPoolCount = 7;
            int[] weaponLoot = new int[numOfWeapons];
            for (int n = 0; n < numOfWeapons; n++)
            {
                weaponLoot[n] = Main.rand.Next(weaponPoolCount - n);
                for (int j = 0; j < n; j++)
                {
                    if (weaponLoot[n] >= weaponLoot[j])
                    {
                        weaponLoot[n]++;
                    }
                    Array.Sort(weaponLoot);
                }
            }
            for (int i = 0; i < weaponLoot.Length; i++)
            {
                string dropName = "none";
                switch (weaponLoot[i])
                {
                    case 0:
                        dropName = "BentbloodStaff";
                        break;
                    case 1:
                        dropName = "RedDeath";
                        break;
                    case 2:
                        dropName = "BlooddropBow";
                        break;
                    case 3:
                        dropName = "ClottedScripture";
                        break;
                    case 4:
                        dropName = "GashBringingSlasher";
                        break;
                    case 5:
                        dropName = "OneEye";
                        break;
                    case 6:
                        dropName = "EyestalkerStaff";
                        break;
                }
                if (dropName != "none")
                {
                    Item.NewItem(player.getRect(), mod.ItemType(dropName));
                }
            }
        }
    }
}