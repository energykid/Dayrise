using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Dayrise.Items.StrangledSun
{
	public class StrangledChalice : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Strangled Chalice");
			Tooltip.SetDefault("Summons the Strangled Sun");
		}

		public override void SetDefaults() 
		{
			item.consumable = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 50;
			item.maxStack = 20;
			item.useAnimation = 50;
			item.useStyle = ItemUseStyleID.HoldingUp;
			item.value = 0;
			item.rare = 2;
			item.UseSound = SoundID.Item1;
			item.autoReuse = false;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Vertebrae, 15);
			recipe.AddIngredient(ItemID.ChlorophyteBar, 5);
			recipe.AddIngredient(ItemID.Lens, 1);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();

			ModRecipe recipe2 = new ModRecipe(mod);
			recipe2.AddIngredient(ItemID.RottenChunk, 15);
			recipe2.AddIngredient(ItemID.ChlorophyteBar, 5);
			recipe2.AddIngredient(ItemID.Lens, 1);
			recipe2.AddTile(TileID.Anvils);
			recipe2.SetResult(this);
			recipe2.AddRecipe();
		}

		public override bool CanUseItem(Player player)
        {
			return !DayriseWorld.suffocatingSun && Main.dayTime;
        }

        public override bool UseItem(Player player)
        {
			DayriseWorld.suffocatingSun = true;
			return true;
        }
    }
}