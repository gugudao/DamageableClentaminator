using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Audio;
using Humanizer;

namespace DamageableClentaminator.Projectiles
{
    public class MushSpore:ModProjectile
    {
		public override void SetDefaults()
		{
			Projectile.width = 12;
			Projectile.height = 18;
			Projectile.penetrate = 1;
			Projectile.friendly=true;
			Projectile.hostile = false;
		}
		public override void AI()
		{
			//Main.NewText(Projectile.ai[0]);
			//Main.NewText(Projectile.velocity.X);
			
			Projectile.ai[1]++;
			Projectile.velocity.Y += 0.02f;
			 
			if (Projectile.velocity.Y > 1f) Projectile.velocity.Y = 1f;

			if (Math.Abs(Projectile.velocity.X) > 1f )
			Projectile.velocity.X *= 0.97f;

			//Projectile.rotation = Projectile.velocity.X * 0.2f;

			//if(Projectile.ai[1]<30)
			Projectile.velocity.X = (float)((float)Math.Sin(Projectile.ai[1]/ 55) *2.1f* (Math.Sin(Projectile.ai[0])/6+1f));// * Projectile.ai[0] == 1 ? 1 : -1;
			/*
			
			if ((Projectile.ai[1] >= 90 && (Projectile.ai[1] - 30) % 75 == Main.rand.Next(0, 6))|| Projectile.ai[1] == 30)
			{
				Projectile.velocity.X *= Main.rand.NextBool() ? Main.rand.NextFloat(-0.8f, -0.9f) : Main.rand.NextFloat(-1.1f, -1.2f);
			}*/
			//Projectile.velocity.X *= Main.rand.NextBool() ? Main.rand.NextFloat(0.75f, 0.9f) : Main.rand.NextFloat(1.1f, 1.25f);
			if (Projectile.ai[1] %30==0)
			{
				if (Projectile.ai[1] % 60 == 0)
					Projectile.ai[0] = Main.rand.Next(-60,60);
				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.GlowingMushroom, 0);
			}
			

			if(Projectile.wet)Projectile.Kill();
		}
		public override void Kill(int timeLeft)
		{
			for(int i = 0; i < 10; i++) {
				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.GlowingMushroom, 0);
			}
			SoundEngine.PlaySound(SoundID.NPCDeath9, Projectile.Center);
		}
	}
}
