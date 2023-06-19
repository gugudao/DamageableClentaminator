using DamageableClentaminator.Projectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DamageableClentaminator.Global
{
	public class GNPC : GlobalNPC
	{
		public override bool InstancePerEntity => true;
		public int Frozen = 0;
		public int Forest = 0;
		public bool Mushroom = false;
		public int bloodpool = 0;
		public int holylight = 0;
		public float forest = 0;
		public override void UpdateLifeRegen(NPC npc, ref int damage)
		{

			if (npc.life >= 1 && npc.life < npc.lifeMax && npc.Fantasy().bloodpool > 0)
			{
				npc.lifeRegen -= 25;
				damage = 5;
				npc.Fantasy().bloodpool -= 1;
				if (npc.Fantasy().bloodpool < 0) npc.Fantasy().bloodpool = 0;
			}
		}
		public override void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone)
		{
			//Main.NewText("projectile.damage" + projectile.damage);

			if (projectile.type == ProjectileID.PureSpray) npc.Fantasy().forest = 12 * 60 * 60;


			//npc.color = Color.ForestGreen;

			if (projectile.type == ProjectileID.SnowSpray) npc.Fantasy().Frozen = 120;
			if (projectile.type == ProjectileID.MushroomSpray) npc.Fantasy().Mushroom = true;
			if (projectile.type == ProjectileID.CorruptSpray)
			{
				npc.AddBuff(195, 600);
				npc.AddBuff(BuffID.WitheredWeapon, 600);
			}
			if (projectile.type == ProjectileID.CrimsonSpray) npc.Fantasy().bloodpool = 300;
			if (projectile.type == ProjectileID.HallowSpray) npc.Fantasy().holylight = 6 * 60 * 60;
			if (projectile.type == ProjectileID.SandSpray)
			{
				//Main.NewText(npc.Hitbox);
				//Main.NewText(npc.position);

				// 计算NPC所在小格的坐标
				int gridX = (int)Math.Floor((double)npc.position.X / 16);
				int gridY = (int)Math.Floor((double)npc.position.Y / 16);
				for (int i = gridX; i < gridX + (int)Math.Ceiling((double)npc.width / 16); i++)
				{
					for (int j = gridY; j < gridY + (int)Math.Ceiling((double)npc.height / 16); j++)
					{
						int centerX = i * 16 + 8; // 小格的中心X坐标
						int centerY = j * 16 + 8; // 小格的中心Y坐标

						Projectile.NewProjectileDirect(npc.GetSource_OnHurt(projectile), new Vector2(centerX, centerY), npc.velocity,
				ProjectileID.SandBallFalling, projectile.damage, projectile.knockBack, projectile.owner);
					}
				}
				npc.active = false;
			}
		}

		public override void DrawEffects(NPC npc, ref Color drawColor)
		{
			//if (npc.Fantasy().Forest > 0) drawColor = Color.ForestGreen;
			//if (npc.Fantasy().Frozen > 0) drawColor = Color.SeaShell;
		}
		public override void AI(NPC npc)
		{
			//Main.NewText("bloodpool" + bloodpool);
			//if (npc.type == NPCID.EyeofCthulhu) Main.NewText("npc.denfense" + npc.defense);
			//if (npc.type == NPCID.EyeofCthulhu) Main.NewText("npc.dmg" + npc.damage);
			//Main.NewText("npc.Fantasy().forest" + npc.Fantasy().forest);
			//Main.NewText("npc.HasBuff(BuffID.WitheredArmor" + npc.HasBuff(BuffID.WitheredArmor));

			//Main.NewText("npc.Fantasy().Frozen" + npc.Fantasy().Frozen);
			if (npc.Fantasy().forest > 0) forest--;


			//npc.color = Color.ForestGreen *( Forest / 3600f);

			if (npc.Fantasy().Frozen > 0)
			{
				Frozen--;

				if (npc.noGravity) npc.position -= npc.velocity * 0.7f * (float)(Frozen / 120f);
				else
				{
					npc.position.X -= npc.velocity.X * 0.7f * (float)(Frozen / 120f);
					if (npc.velocity.Y < 0) npc.position.Y -= npc.velocity.Y * 0.7f * (float)(Frozen / 120f);
				}
			}
			if (npc.Fantasy().holylight > 0) holylight--;



			//npc.color = Main.DiscoColor;


			//else { npc.color = Color.White};
		}
		public override Color? GetAlpha(NPC npc, Color drawColor)
		{
			Color initialColor = drawColor;
			//Main.NewText("drawColor:" + drawColor);
			//Main.NewText("initialColor:" + initialColor);
			//Main.NewText("Color:" + Color.Lerp(Color.ForestGreen, initialColor, (1f - (float)(forest / 3600f))));
			//Main.NewText("color:" + new Color((int)(34 * (float)(forest / 600f)), (int)(139 * (float)(forest / 600f)), (int)(34 * (float)(forest / 600f)), 255));
			if (npc.Fantasy().forest > 0)// && npc.Fantasy().forest < 3600)
			{
				//Color green = Utils.ColorSwap(Color.ForestGreen, initialColor, Forest);
				return Color.Lerp(Color.ForestGreen, initialColor, 1f - (float)(forest / 3600f));
				//return new Color((int)(34 * (float)(forest / 600f)), (int)(139 * (float)(forest / 600f)), (int)(34 * (float)(forest / 600f)), 255);
			}
			if (npc.Fantasy().holylight > 0)// && npc.Fantasy().forest < 3600)
			{
				Lighting.AddLight(npc.Center, Main.DiscoR * 0.003f, Main.DiscoB * 0.003f, Main.DiscoG * 0.003f);
				return Color.Lerp(Main.DiscoColor, initialColor, 1f - (float)(holylight / 3600f));
			}
			if (npc.Fantasy().Frozen > 0)
			{
				return Color.Lerp(new Color(170, 215, 238), initialColor, 1f - (float)(Frozen / 60f));
			}

			return null;
		}

		public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
		{


			//NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projectile.owner, 0f, 0f, 0f, 0, 0, 0); 


		}
		public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
		{
			if (npc.HasBuff(BuffID.WitheredArmor))
			{
				modifiers.ArmorPenetration += npc.defense / 2;
			}
		}
		public override void OnKill(NPC npc)
		{
			if (npc.Fantasy().Mushroom)
			{
				for (int i = 0; i < npc.width * npc.height / 256; i++)
				{
					Projectile spore = Projectile.NewProjectileDirect(npc.GetSource_Death(),
					npc.Center + new Vector2(Main.rand.Next(-npc.width / 2, npc.width / 2), Main.rand.Next(-npc.height / 2, npc.height / 2))
					, Vector2.Zero, ModContent.ProjectileType<MushSpore>(), npc.damage / 2,
					0.05f, Main.myPlayer);

				}
			}
			npc.Fantasy().bloodpool = 0;
			npc.Fantasy().Forest = 0;
		}
		public override void ResetEffects(NPC npc)
		{
			npc.damage = npc.defDamage;
		}
	}
}
