using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Fighting_Styles
{
	class Aggressive : AbstractFightingStyle
	{
		public Aggressive()
		{
		normal = new int[] { 70, 10, 20 };
		fear1 = new int[] { 70, 10, 20 };
		fear2 = new int[] { 60, 20, 20 };
		fear3 = new int[] { 50, 20, 30 };
		anger1 = new int[] { 90, 5, 5 };
		anger2 = new int[] { 100, 0, 0 };
		winningMood1 = new int[] { 70, 20, 10 };
		winningMood2 = new int[] { 80, 20, 0 };
		finishIt = new int[] { 100, 0, 0 };
	}
}
}
