using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Dayrise.Items.Weapons.Visceral
{
	public class EpidermalIncense : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Epidermal Incense");
			Tooltip.SetDefault("Summons Four-Eyes during a Blood Moon on the surface");
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
			recipe.AddIngredient(ItemID.Vertebrae, 5);
			recipe.AddIngredient(ItemID.CrimtaneBar, 2);
			recipe.AddIngredient(ItemID.Lens, 4);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();

			ModRecipe recipe2 = new ModRecipe(mod);
			recipe2.AddIngredient(ItemID.RottenChunk, 5);
			recipe2.AddIngredient(ItemID.DemoniteBar, 2);
			recipe2.AddIngredient(ItemID.Lens, 4);
			recipe2.AddTile(TileID.Anvils);
			recipe2.SetResult(this);
			recipe2.AddRecipe();
		}

		public override bool CanUseItem(Player player)
        {
			return NPC.CountNPCS(mod.NPCType("FourEyes")) >= 1 || Main.dayTime || !Main.bloodMoon ? false : true;
        }

        public override bool UseItem(Player player)
        {
			NPC.NewNPC((int)player.Center.X, (int)player.Center.Y - Main.screenHeight / 2, mod.NPCType("FourEyes"));
			Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/NPC/FourEyes_Roar"), player.Center);
			return true;
        }
    }
}