using Terraria;
using Terraria.ModLoader;
using Dayrise;

namespace Dayrise.Items.Weapons.Misc
{
    public class GunMinionBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Cursed Gun");
            Description.SetDefault("I am going to shoot you");
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.ownedProjectileCounts[mod.ProjectileType("GunMinion")] > 0)
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