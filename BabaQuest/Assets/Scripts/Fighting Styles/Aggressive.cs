using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Fighting_Styles
{
    class Aggressive : AbstractFightingStyle
    {
        public new int[] normal = { 70, 10, 20 };
        public new int[] fear1 = { 70, 10, 20 };
        public new int[] fear2 = { 60, 20, 20 };
        public new int[] fear3 = { 50, 20, 30 };
        public new int[] anger1 = { 90, 5, 5 };
        public new int[] anger2 = { 100, 0, 0 };
        public new int[] winningMood1 = { 70, 20, 10 };
        public new int[] winningMood2 = { 80, 20, 0 };
        private new int[] finishIt = { 100, 0, 0 };
    }
}
