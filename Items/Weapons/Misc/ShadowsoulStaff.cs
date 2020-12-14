using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Dayrise.Items.Weapons.Misc
{
    public class ShadowsoulStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shadowsoul Staff");
            Tooltip.SetDefault("Summons a shadowsoul to fight for you");
        }

        public override void SetDefaults()
        {
            item.damage = 33;
            item.summon = true;
            item.mana = 10;
            item.width = 40;
            item.height = 40;
            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = 1;
            item.noMelee = true;
            item.knockBack = 3;
            item.value = Item.buyPrice(0, 3, 0, 0);
            item.rare = 7;
            item.UseSound = SoundID.Item44;
            item.shoot = mod.ProjectileType("Shadowsoul");
            item.shootSpeed = 10f;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            position = player.Center;
            if (player.numMinions < player.maxMinions)
            {
                player.AddBuff(mod.BuffType("ShadowsoulBuff"), 10);
                Projectile.NewProjectile(position, Vector2.Zero, mod.ProjectileType("Shadowsoul"), item.damage, item.knockBack, player.whoAmI, player.ownedProjectileCounts[mod.ProjectileType("Shadowsoul")]);
            }
            return false;
        }
    }
}