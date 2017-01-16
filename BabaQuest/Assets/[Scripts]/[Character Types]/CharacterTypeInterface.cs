﻿namespace Assets._Scripts_._Character_Types_
{
    public interface CharacterTypeInterface
    {
		int Lvl { get; }
        int LeftLife { get; }
        int FullLife { get; }
        int ReachA { get; }
        int ReachW { get; }
		int Pos { get; set; }
        //int: lvl, exp, hp, att, heal, leftLife, fullLife, str, int, dex
        void CalculateStats(int lvl);
        int Attack(); //basically return att
        void Heal(); //heal yourself
        void Walk(); //just for triggering walking animation
        void GetHurt(int dmg);

		void IddleAnim();
		void AttackAnim();
		void GetHitAnim();
	}
}
