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
using Terraria.ModLoader;
using System.IO;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.UI;
using Terraria.GameContent.UI;
using System.Linq;

namespace Dayrise
{
	public class DayriseNPC : GlobalNPC
	{

        public override void NPCLoot(NPC npc)
        {
            if (npc.type == NPCID.GoblinSummoner && Main.rand.NextBool(4))
            {
                Item.NewItem(npc.getRect(), mod.ItemType("ShadowsoulStaff"));
            }
            if (npc.type == NPCID.TacticalSkeleton || npc.type == NPCID.SkeletonCommando || npc.type == NPCID.SkeletonSniper)
            {
                if (Main.rand.NextBool(65))
                {
                    Item.NewItem(npc.getRect(), mod.ItemType("GunStaff"));
                }
            }
        }

        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
            if (type == NPCID.ArmsDealer || type == NPCID.Demolitionist)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType("Bombshell"));
                nextSlot++;
            }
        }
    }
}