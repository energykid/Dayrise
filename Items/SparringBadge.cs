using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Dayrise.Items
{
	public class SparringBadge : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sparring Badge");
			Tooltip.SetDefault("Keep in your inventory to spar with the Guide upon his request");
		}

		public override void SetDefaults()
		{
			item.width = 40;
			item.height = 40;
			item.maxStack = 1;
			item.value = 0;
			item.rare = 2;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(RecipeGroupID.IronBar, 5);
			recipe.AddIngredient(ItemID.StoneBlock, 20);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}