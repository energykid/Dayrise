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
	public class Dayrise : Mod
	{
        public override void PostSetupContent()
        {
            Mod bossList = ModLoader.GetMod("BossChecklist");
            if (bossList != null)
            {
                bossList.Call("AddBossWithInfo", "The Guide", 0.0001f, (Func<bool>)(() => DayriseWorld.wonVsGuide), string.Format("Challenge the Guide to a friendly sparring match while holding a [i:{0}]", ItemType("SparringBadge")));

                bossList.Call("AddBossWithInfo", "Four-Eyes", 2.2f, (Func<bool>)(() => DayriseWorld.downedFourEyes), string.Format("During a Blood Moon, hold out a [i:{0}]", ItemType("EpidermalIncense")));
            }
        }
    }
}