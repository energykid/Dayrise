using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;

namespace Dayrise.Items.Weapons.Frostbite
{
    public class BurningFrost : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Burning Frost");
            Tooltip.SetDefault("Summons a volley of three exploding frost fireballs");
        }

        public override void SetDefaults()
        {
            item.damage = 11;
            item.magic = true;
            item.mana = 9;
            item.width = 34;
            item.height = 34;
            item.useTime = 40;
            item.useAnimation = 40;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 4f;
            item.value = Item.buyPrice(0, 60, 0, 0);
            item.rare = 2;
            item.UseSound = SoundID.Item20;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("BurningFrostProj");
            item.shootSpeed = 4f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SilverBar, 9);
            recipe.AddIngredient(ItemID.IceBlock, 17);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
            ModRecipe recipe2 = new ModRecipe(mod);
            recipe2.AddIngredient(ItemID.TungstenBar, 9);
            recipe2.AddIngredient(ItemID.IceBlock, 17);
            recipe2.AddTile(TileID.Anvils);
            recipe2.SetResult(this);
            recipe2.AddRecipe();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            speedY -= 5f;
            speedX *= 1.6f;
            Projectile.NewProjectile(player.Center, new Vector2(speedX / 2, speedY * 1.2f), mod.ProjectileType("BurningFrostProj"), item.damage, 2f, player.whoAmI);
            Projectile.NewProjectile(player.Center, new Vector2(speedX * 1.5f, speedY * 0.8f), mod.ProjectileType("BurningFrostProj"), item.damage, 2f, player.whoAmI);
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }
    }
}
