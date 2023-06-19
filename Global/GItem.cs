using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DamageableClentaminator.Global
{
	public class GItem : GlobalItem
	{
		public override bool InstancePerEntity => true;
		List<int> Solutions = new List<int> {780,781,
			782, 783,784,
			5392,5393,5394
			};
		public override void SetDefaults(Item item)
		{
			if(item.type==ItemID.Clentaminator)
			{
				item.damage = 60;
				item.DamageType = DamageClass.Default;
			}
			if (item.type == ItemID.Clentaminator2)
			{
				item.damage = 95;
				item.DamageType = DamageClass.Default;
			}
			if(Solutions.Contains(item.type))
			{
				item.damage = 0;
			}
		}
		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			if (item.type == ItemID.Clentaminator || item.type == ItemID.Clentaminator2)
			{
				if (item.crit <= 0)
				{
					int index = tooltips.FindIndex(tip => tip.Name.StartsWith("CritChance"));
					tooltips.RemoveAt(index);
				}
				
			}
		}
	}
}
