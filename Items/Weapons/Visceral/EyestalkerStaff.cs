using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Dayrise.Items.Weapons.Visceral
{
    public class EyestalkerStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eyestalker Staff");
            Tooltip.SetDefault("Summons an eye on a stalk connected to you to fight");
        }

        public override void SetDefaults()
        {
            item.damage = 17;
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
            item.rare = 2;
            item.UseSound = SoundID.Item44;
            item.shoot = mod.ProjectileType("Eyestalker");
            item.shootSpeed = 10f;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            position = player.Center;
            if (player.numMinions < player.maxMinions)
            {
                player.AddBuff(mod.BuffType("EyestalkerBuff"), 10);
                Projectile.NewProjectile(position, Vector2.Zero, mod.ProjectileType("Eyestalker"), item.damage / 2, item.knockBack, player.whoAmI, player.ownedProjectileCounts[mod.ProjectileType("Eyestalker")]);
            }
            return false;
        }
    }
}