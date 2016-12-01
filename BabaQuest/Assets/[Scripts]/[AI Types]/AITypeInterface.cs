using System.Collections.Generic;
using Assets._Scripts_._Character_Types_;

namespace Assets._Scripts_._AI_Types_
{
    interface AITypeInterface
    {
        int[] Turn(List<CharacterTypeInterface> characters, int[] possitions, int myNr, bool[] good);
    }
}
