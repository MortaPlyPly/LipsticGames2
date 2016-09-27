using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public abstract class AbstractFightingStyle
    {
        public int[] normal { get; set; }
        public int[] fear1 { get; set; }
        public int[] fear2 { get; set; }
        public int[] fear3 { get; set; }
        public int[] anger1 { get; set; }
        public int[] anger2 { get; set; }
        public int[] winningMood1 { get; set; }
        public int[] winningMood2 { get; set; }
        public int[] finishIt { get; set; }
    }
}
