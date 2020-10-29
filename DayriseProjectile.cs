using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Dayrise;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Dayrise
{
    class DayriseProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        public bool frostbiteSetExplosion = false;

        public override void AI(Projectile projectile)
        {
            if (Main.player[projectile.owner].GetModPlayer<DayrisePlayer>().chilledAmulet && projectile.magic)
            {
                for (float i = 0; i < 3; i++)
                {
                    Dust dust;
                    // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                    Vector2 position = Vector2.Lerp(projectile.Center, projectile.Center - projectile.velocity, (i / 3));
                    dust = Dust.NewDustPerfect(position, mod.DustType("FrostMist"));
                    dust.scale = 0.8f;
                    dust.velocity = Vector2.Zero;
                }
            }
        }

        public override void Kill(Projectile projectile, int timeLeft)
        {
            if (Main.player[projectile.owner].GetModPlayer<DayrisePlayer>().frostbiteSetBonus && projectile.magic && Main.rand.NextBool(3))
            {
                Projectile.NewProjectile(projectile.Center, Vector2.Zero, mod.ProjectileType("FrostbiteSetExplosion"), (int)MathHelper.Clamp((float)projectile.damage / 2f, 0f, 6f), 1f, projectile.owner);
                for (int i = 0; i < 45; i++)
                {
                    Dust dust;
                    // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                    Vector2 position = projectile.Center;
                    dust = Dust.NewDustPerfect(position, mod.DustType("FrostMist"));
                    dust.scale = 0.7f;
                    dust.velocity = new Vector2(0, Main.rand.Next(-4, -1)).RotatedBy(MathHelper.ToRadians(Main.rand.Next(360)));
                }
                Main.PlaySound(SoundID.DD2_ExplosiveTrapExplode, projectile.Center);
            }
        }

        public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
            if (Main.player[projectile.owner].GetModPlayer<DayrisePlayer>().chilledAmulet && projectile.magic)
            {
                target.AddBuff(BuffID.Frostburn, 240);
            }
        }
    }
}
