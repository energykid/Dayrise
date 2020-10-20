using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;


namespace Dayrise
{
    class DayriseWorld : ModWorld
    {
        public static bool wonVsGuide = false;
        public static bool downedFourEyes = false;

        public override TagCompound Save()
        {
            var downed = new List<string>();
            if (wonVsGuide) downed.Add("wonVsGuide");
            if (downedFourEyes) downed.Add("downedFourEyes"); 
            
            return new TagCompound
            {
                {"downed", downed}
            };
        }
        public override void Load(TagCompound tag)
        {
            var downed = tag.GetList<string>("downed");
            wonVsGuide = downed.Contains("wonVsGuide");
            downedFourEyes = downed.Contains("downedFourEyes");
        }

        public override void NetSend(BinaryWriter writer)
        {
            BitsByte flags = new BitsByte();
            flags[0] = wonVsGuide;
            flags[1] = downedFourEyes;
            writer.Write(flags);
        }
        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            wonVsGuide = flags[0];
            downedFourEyes = flags[1];
        }

    }
}
