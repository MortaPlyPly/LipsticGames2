using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Fighting_Styles
{
    class Mixed : AbstractFightingStyle
    {
        public Mixed()
		{
			normal = new int[] { 40, 30, 30 };
			fear1 = new int[] { 40, 30, 30 };
			fear2 = new int[] { 30, 35, 35 };
			fear3 = new int[] { 30, 35, 35 };
			anger1 = new int[] { 50, 30, 20 };
			anger2 = new int[] { 70, 20, 10 };
			winningMood1 = new int[] { 70, 30, 0 };
			winningMood2 = new int[] { 90, 10, 0 };
			finishIt = new int[] { 90, 10, 0 };
		}
    }
}
