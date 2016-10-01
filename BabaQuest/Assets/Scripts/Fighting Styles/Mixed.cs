using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Fighting_Styles
{
    class Mixed : AbstractFightingStyle
    {
        public new int[] normal = { 40, 30, 30 };
        public new int[] fear1 = { 40, 30, 30 };
        public new int[] fear2 = { 30, 35, 35 };
        public new int[] fear3 = { 30, 35, 35 };
        public new int[] anger1 = { 50, 30, 20 };
        public new int[] anger2 = { 70, 20, 10 };
        public new int[] winningMood1 = { 70, 30, 0 };
        public new int[] winningMood2 = { 90, 10, 0 };
        public new int[] finishIt = { 90, 10, 0 };
    }
}
