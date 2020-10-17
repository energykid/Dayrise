using Terraria;
using Terraria.ModLoader;
using Dayrise;

namespace Dayrise.Items.Weapons.Visceral
{
    public class EyestalkerBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Eyestalker");
            Description.SetDefault("The Eyestalker will fight for you");
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.ownedProjectileCounts[mod.ProjectileType("Eyestalker")] > 0)
            {
                player.GetModPlayer<DayrisePlayer>().eyestalker = true;
            }
            if (!player.GetModPlayer<DayrisePlayer>().eyestalker)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
            else
            {
                player.buffTime[buffIndex] = 18000;
            }
        }
    }
}