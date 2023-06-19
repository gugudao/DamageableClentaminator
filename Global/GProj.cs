using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
namespace DamageableClentaminator.Global
{
	public class GProj : GlobalProjectile
	{
		public override bool InstancePerEntity => true;
		List<int> Sprays = new List<int> {ProjectileID.CorruptSpray,ProjectileID.CrimsonSpray,
			ProjectileID.HallowSpray, ProjectileID.PureSpray,ProjectileID.MushroomSpray,
			ProjectileID.SandSpray,ProjectileID.DirtSpray,ProjectileID.SnowSpray
			};
		public bool Endgame = false;
		public override void SetDefaults(Projectile entity)
		{

		}

		public override void AI(Projectile projectile)
		{
			if (Sprays.Contains(projectile.type))
			{
				//Main.NewText("projectile.localNPCHitCooldown=" + projectile.localNPCHitCooldown);
				//Main.NewText("projectile.usesLocalNPCImmunity=" + projectile.usesLocalNPCImmunity);
				projectile.friendly = true;
				projectile.hostile = false;
				if (projectile.Fantasy().Endgame)
				{
					projectile.usesLocalNPCImmunity = true;
					projectile.localNPCHitCooldown = -1;
				}
				if(projectile.type==ProjectileID.SnowSpray) {
					projectile.coldDamage = true;
				}
				//Main.NewText("projectile.dmg=" + projectile.damage);
				//Main.NewText("projectile.usesLocalNPCImmunity" + projectile.usesLocalNPCImmunity);
				//Main.NewText("projectile.localNPCHitCooldown" + projectile.localNPCHitCooldown);
				//Main.NewText("projectile.Fantasy().Endgame" + projectile.Fantasy().Endgame);
				if(projectile.type==ProjectileID.PureSpray)
				{
					foreach(NPC npc in Main.npc)
					{
						if (!npc.friendly) continue;
						if (projectile.Hitbox.Intersects(npc.Hitbox) &&npc.friendly)
						npc.Fantasy().forest = 12 * 60 * 60;
					}
					
				}
				if (projectile.type == ProjectileID.HallowSpray)
				{
					foreach (NPC npc in Main.npc)
					{
						//if (!npc.friendly) continue;
						if (projectile.Hitbox.Intersects(npc.Hitbox) && npc.friendly)
						npc.Fantasy().holylight = 6 * 60 * 60;
					}

				}
				if (projectile.type == ProjectileID.SnowSpray)
				{
					foreach (NPC npc in Main.npc)
					{
						//if (!npc.friendly) continue;
						if (projectile.Hitbox.Intersects(npc.Hitbox) && npc.friendly)
						npc.Fantasy().Frozen = 120;
					}

				}
			}
		}
		public override void OnSpawn(Projectile projectile, IEntitySource source)
		{
			//Player player = Main.LocalPlayer;
			//if (Sprays.Contains(projectile.type))
			if(projectile.type== ProjectileID.MushroomSpray)
			{
				//projectile.ai[0] = Main.rand.Next(0, 2);
			}
			if (projectile.type == ProjectileID.DirtSpray)
			{
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					int rand = Main.rand.Next(4);
					switch (rand)
					{
						case 0:
							projectile.damage /= 4;
							break;
						case 1:
							projectile.damage /= 2;
							break;
						case 2:
							projectile.damage *= 2;
							break;
						case 3:
							projectile.damage *= 4;
							break;
					}
					NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projectile.whoAmI);
				}
			}

				if (source is EntitySource_ItemUse_WithAmmo)
			//如果生成源是"由物品射击生成的"
			{
				EntitySource_ItemUse_WithAmmo ammo = source as EntitySource_ItemUse_WithAmmo;
				// 创个EntitySource_ItemUse_WithAmmo实例
				if (ammo.Item.type == ItemID.Clentaminator2)
				// 如果生成这个source的是一个Item
				// 而且这个Item是泰拉改造枪
				{
					projectile.Fantasy().Endgame = true;
				}
			}
		}
		public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
		{
			if (Sprays.Contains(projectile.type))
			{
				if (!projectile.Fantasy().Endgame)projectile.damage = (int)(projectile.damage * 0.9);
			}
				
		}
		public override bool? CanDamage(Projectile projectile)
		{
			if (Sprays.Contains(projectile.type))
			{
				return true;
			}
			return null;
		}
	}
}
