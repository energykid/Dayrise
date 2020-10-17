using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace Dayrise
{
    class DayrisePlayer : ModPlayer
    {
        public bool eyestalker = false;

        public override void ResetEffects()
        {
            eyestalker = false;
        }
    }
}