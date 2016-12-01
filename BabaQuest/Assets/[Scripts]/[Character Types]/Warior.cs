﻿using System;
using UnityEngine;

namespace Assets._Scripts_._Character_Types_
{
    class Warior : MonoBehaviour, CharacterTypeInterface
    {
        int att;
        int heal;
        int leftLife;
        int fullLife;

        // appearance variable list...

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
                return 1;
            }
        }

        public int ReachW
        {
            get
            {
                return 1;
            }
        }

        public int Attack()
        {
            return att;
        }

        public void CalculateStats(int lvl) //not balanced
        {
            fullLife = 100 * lvl;
            att = 10 * lvl;
            heal = fullLife / 10;
            // set appearance...
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