using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;

namespace Dayrise.Items.Weapons.Visceral
{
    public class ClottedScripture : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Clotted Scripture");
            Tooltip.SetDefault("Creates a clotted clump that explodes into blood");
        }

        public override void SetDefaults()
        {
            item.damage = 23;
            item.magic = true;
            item.mana = 10;
            item.width = 34;
            item.height = 34;
            item.useTime = 40;
            item.useAnimation = 40;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 4f;
            item.value = Item.buyPrice(0, 60, 0, 0);
            item.rare = ItemRarityID.Green;
            item.UseSound = SoundID.Item20;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("ClottedClump");
            item.shootSpeed = 12f;
        }
    }
}
