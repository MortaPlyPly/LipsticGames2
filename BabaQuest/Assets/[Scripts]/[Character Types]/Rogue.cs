using System;
using UnityEngine;

namespace Assets._Scripts_._Character_Types_
{
	class Rogue : MonoBehaviour, CharacterTypeInterface
	{
		int lvl;
		int att;
		int heal;
		int leftLife;
		int fullLife;

		public Rogue(int lvl)
		{
			Debug.Log(lvl);
			CalculateStats(lvl);
			//Debug.Log(leftLife);
		}

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
				return fullLife;
			}
		}

		public int ReachA
		{
			get
			{
				return 2;
			}
		}

		public int ReachW
		{
			get
			{
				return 2;
			}
		}

		public int Lvl
		{
			get
			{
				return lvl;
			}
		}

		public int Attack()
		{
			return att;
		}

		public void CalculateStats(int lvl) //not balanced
		{
			this.lvl = lvl;
			fullLife = 70 + 15 * lvl;
			att = 10 + 7 * lvl;
			heal = fullLife / 10;
			leftLife = fullLife;
		}

		public void GetHurt(int dmg)
		{
			leftLife -= dmg;
		}

		public void Heal()
		{
			if (leftLife + heal <= fullLife)
				leftLife += heal;
			else
				leftLife = fullLife;
		}

		public void Walk()
		{
			//trigger animation?
			//for eatch tile walked
		}
	}
}
