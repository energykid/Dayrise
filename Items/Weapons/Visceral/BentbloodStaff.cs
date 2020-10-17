using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;

namespace Dayrise.Items.Weapons.Visceral
{
    public class BentbloodStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bloodbend Staff");
            Tooltip.SetDefault("Conjures a burst of blood");
            Item.staff[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.damage = 13;
            item.magic = true;
            item.mana = 8;
            item.width = 34;
            item.height = 34;
            item.useTime = 27;
            item.useAnimation = 27;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 4f;
            item.value = Item.buyPrice(0, 60, 0, 0);
            item.rare = ItemRarityID.Green;
            item.UseSound = SoundID.Item20;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("BloodProj2");
            item.shootSpeed = 12f;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            for (int i = 0; i < Main.rand.Next(2, 5); i++)
            {
                Projectile.NewProjectile(position, (new Vector2(speedX, speedY).RotatedBy(MathHelper.ToRadians(Main.rand.Next(-12, 12)))) * Main.rand.Next(12, 18) / 15, mod.ProjectileType("BloodProj2"), item.damage, 6f, player.whoAmI);
            }
            return true;
        }
    }
}
