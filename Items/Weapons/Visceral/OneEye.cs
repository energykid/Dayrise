using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace Dayrise.Items.Weapons.Visceral
{
	public class OneEye : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("One-Eye");
			Tooltip.SetDefault("'You know... 'cause it's just one eye... well, you gonna laugh?'");
		}
        public override void SetDefaults()
        {
            item.damage = 42;
            item.melee = true;
            item.noUseGraphic = true;
            item.width = 68;
            item.height = 68;
            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = 5;
            item.knockBack = 2;
            item.shoot = mod.ProjectileType("OneEyeProj");
            item.shootSpeed = 8;
            item.rare = 5;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
            item.useTurn = true;
        }
        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[item.shoot] == 0;
        }
    }
}