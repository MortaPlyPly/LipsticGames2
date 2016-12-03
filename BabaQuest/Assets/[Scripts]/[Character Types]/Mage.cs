using System;
using UnityEngine;

namespace Assets._Scripts_._Character_Types_
{
    class Mage : MonoBehaviour, CharacterTypeInterface
    {
        int att;
        int heal;
        int leftLife;
        int fullLife;

        public int LeftLife
        {
            get
            {
                return leftLife;
            }
        }

        public int FullLife
        {
            get
            {
                return FullLife;
            }
        }

        public int ReachA
        {
            get
            {
                return 7;
            }
        }

        public int ReachW
        {
            get
            {
                return 0;
            }
        }

        public int Attack()
        {
            return att;
        }

        public void CalculateStats(int lvl) //not balanced
        {
            fullLife = 50 * lvl;
            att = 7 * lvl;
            heal = fullLife / 10;
        }

        public void GetHurt(int dmg)
        {
            leftLife -= dmg;
        }

        public void Heal()
        {
            leftLife += heal;
        }

        public void Walk()
        {
            //trigger animation?
            //for eatch tile walked
        }
    }
}
