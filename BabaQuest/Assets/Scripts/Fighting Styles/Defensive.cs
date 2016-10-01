using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Fighting_Styles
{
    class Defensive : AbstractFightingStyle
    {
        public new int[] normal = { 20, 50, 30 };
        public new int[] fear1 = { 15, 50, 35 };
        public new int[] fear2 = { 10, 50, 40 };
        public new int[] fear3 = { 0, 60, 40 };
        public new int[] anger1 = { 30, 40, 30 };
        public new int[] anger2 = { 50, 30, 20 };
        public new int[] winningMood1 = { 50, 20, 30 };
        public new int[] winningMood2 = { 70, 20, 10 };
        public new int[] finishIt = { 80, 10, 10 };
    }
}
