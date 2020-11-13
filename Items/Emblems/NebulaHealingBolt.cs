using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Dayrise.Items.Emblems
{
	public class NebulaHealingBolt : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Healing Bolt");
		}
		public override void SetDefaults()
		{
			projectile.friendly = false;
			projectile.hostile = true;
			projectile.magic = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 200;
			projectile.height = 16;
			projectile.width = 16;
			projectile.alpha = 255;
			projectile.tileCollide = false;
			projectile.extraUpdates = 3;
			aiType = ProjectileID.Bullet;
		}
		public override void AI()
		{
			projectile.rotation = projectile.velocity.ToRotation() + 1.57f;
			Player player = Main.player[projectile.owner];
			Vector2 direction = player.Center - projectile.position;
			if (direction.Length() < 20)
			{
				player.statLife += (int)(projectile.ai[0]);
                player.HealEffect((int)(projectile.ai[0]));
				projectile.active = false;
			}
			direction.Normalize();
			direction *= 5;
			projectile.velocity = direction;
			for (int i = 0; i < 6; i++) {
				Dust dust;
				Vector2 position = projectile.Center;
				dust = Main.dust[Terraria.Dust.NewDust(position, 0, 0, 272, 0f, 0f, 0, new Color(255, 255, 255), 0.3947368f)];
				dust.velocity = Vector2.Zero;
			}
		
		}
	}
}
