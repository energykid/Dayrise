using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Dayrise.Items
{
	public class StrangledChalice : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Strangled Chalice");
			Tooltip.SetDefault("Use to start a Suffocating Sun");
		}

		public override void SetDefaults()
		{
			item.width = 40;
			item.height = 40;
			item.maxStack = 1;
			item.value = 0;
			item.useStyle = 1;
			item.useTime = 20;
			item.useAnimation = 20;
			item.rare = 2;
		}

        public override bool UseItem(Player player)
        {
			DayriseWorld.suffocatingSun = true;
			return true;
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