using DamageableClentaminator.Global;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;

namespace DamageableClentaminator
{
	public static class Utils
	{
		public static GProj Fantasy(this Projectile projectile)
		{
			return projectile.GetGlobalProjectile<GProj>();
		}
		public static GNPC Fantasy(this NPC npc)
		{
			return npc.GetGlobalNPC<GNPC>();
		}
		public static Color ColorSwap(Color firstColor, Color secondColor, float seconds)
		{
			float colorMePurple = (float)((Math.Sin((double)(Math.PI * 2 / seconds) * Main.GlobalTimeWrappedHourly) + 1.0) * 0.5);
			return Color.Lerp(firstColor, secondColor, colorMePurple);
		}
	}
}
