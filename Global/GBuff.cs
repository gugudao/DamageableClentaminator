using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DamageableClentaminator.Global
{
	public class GBuff:GlobalBuff
	{
		public override void Update(int type, NPC npc, ref int buffIndex)
		{
			if(type==BuffID.WitheredWeapon) {
				npc.damage /= 2;
			}
		}
	}
}
