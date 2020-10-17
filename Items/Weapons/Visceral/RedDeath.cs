using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;

namespace Dayrise.Items.Weapons.Visceral
{
    public class RedDeath : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Red Death");
            Tooltip.SetDefault("Shoots a bleeding dart that pierces infinitely");
        }

        public override void SetDefaults()
        {
            item.damage = 13;
            item.ranged = true;
            item.width = 44;
            item.height = 18;
            item.useTime = 22;
            item.useAmmo = AmmoID.Dart;
            item.useAnimation = 22;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 4f;
            item.value = Item.buyPrice(0, 60, 0, 0);
            item.rare = ItemRarityID.Green;
            item.UseSound = SoundID.Item63;
            item.autoReuse = false;
            item.shoot = mod.ProjectileType("BleedingDart");
            item.shootSpeed = 25f;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(6, 3);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position, new Vector2(speedX, speedY), mod.ProjectileType("BleedingDart"), item.damage, 6f, player.whoAmI);
            return false;
        }
    }
}
