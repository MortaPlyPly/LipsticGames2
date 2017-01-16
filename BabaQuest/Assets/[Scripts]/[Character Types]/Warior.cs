﻿using System;
using UnityEngine;

namespace Assets._Scripts_._Character_Types_
{
	class Warior : MonoBehaviour, CharacterTypeInterface
	{
		int lvl;
		int att;
		int heal;
		int leftLife;
		int fullLife;
		int pos;

		Sprite iddle;
		Sprite attack;
		Sprite getHit;

		public Warior(int lvl)
		{
			CalculateStats(lvl);
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

		public int Lvl
		{
			get
			{
				return lvl;
			}
		}

		public int Pos
		{
			get
			{
				return this.pos;
			}

			set
			{
				this.pos = value;
			}
		}

		public int Attack()
		{
			return att;
		}

		public void CalculateStats(int lvl) //not balanced
		{
			this.lvl = lvl;
			fullLife = 100 + 20 * lvl;
			att = 5 + 7 * lvl;
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

		public void IddleAnim()
		{
			throw new NotImplementedException();
		}

		public void AttackAnim()
		{
			throw new NotImplementedException();
		}

		public void GetHitAnim()
		{
			throw new NotImplementedException();
		}
	}
}
