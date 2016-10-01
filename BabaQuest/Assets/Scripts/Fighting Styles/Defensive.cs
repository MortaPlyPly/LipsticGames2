using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Fighting_Styles
{
    class Defensive : AbstractFightingStyle
    {
        public Defensive()
		{
			normal = new int[] { 20, 50, 30 };
			fear1 = new int[] { 15, 50, 35 };
			fear2 = new int[] { 10, 50, 40 };
			fear3 = new int[] { 0, 60, 40 };
			anger1 = new int[] { 30, 40, 30 };
			anger2 = new int[] { 50, 30, 20 };
			winningMood1 = new int[] { 50, 20, 30 };
			winningMood2 = new int[] { 70, 20, 10 };
			finishIt = new int[] { 80, 10, 10 };
		}
    }
}
