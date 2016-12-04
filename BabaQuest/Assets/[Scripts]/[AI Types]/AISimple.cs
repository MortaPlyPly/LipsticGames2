using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets._Scripts_._Character_Types_;
using UnityEngine;

namespace Assets._Scripts_._AI_Types_
{
	class AISimple : MonoBehaviour, AITypeInterface
	{
		int myNewPos;
		bool twoMages = false;
		int[] actions = new int[9]; //1 - walk; 2 - attack; 3 - heal;
		int target = 0;

		// public int[] Turn(List<CharacterTypeInterface> characters, int[] possitions, int myNr, bool[] good)
		public int[] Turn(List<CharacterTypeInterface> characters, int[] possitions, int myNr, List<bool> good1)
		{
			int mages = 0;
			target = myNr;
			for (int i = 0; i < 7; i++) // calculating my possition
			{
				if (possitions[i] == myNr)
				{
					myNewPos = i;
				}
			}

			foreach(CharacterTypeInterface c in characters) // special for mages
			{
				if (c.GetType().Name == "Mage")
				{
					mages++;
				}
			}
			if (mages > 1)
			{
				twoMages = true;
			}

			if (characters[myNr].LeftLife/characters[myNr].FullLife < 0.1) // how many times do I heal?
			{
				actions[0] = 3;
				actions[1] = 3;
				actions[2] = 3;
				actions[3] = myNewPos;
				actions[4] = myNewPos;
				actions[5] = myNewPos;
				actions[6] = target;
				actions[7] = target;
				actions[8] = target;
			}
			else if (characters[myNr].LeftLife / characters[myNr].FullLife < 0.2)
			{
				actions[0] = 3;
				actions[1] = 3;
				actions[3] = myNewPos;
				actions[4] = myNewPos;
				actions[6] = target;
				actions[7] = target;
				CheckAttack(characters[myNr], possitions, good1.ElementAt(myNr), good1, 2);
			}
			else if (characters[myNr].LeftLife / characters[myNr].FullLife < 0.3)
			{
				actions[0] = 3;
				actions[3] = myNewPos;
				actions[6] = target;
				CheckAttack(characters[myNr], possitions, good1.ElementAt(myNr), good1, 1);
				CheckAttack(characters[myNr], possitions, good1.ElementAt(myNr), good1, 2);
			}
			else
			{
				CheckAttack(characters[myNr], possitions, good1.ElementAt(myNr), good1, 0);
				CheckAttack(characters[myNr], possitions, good1.ElementAt(myNr), good1, 1);
				CheckAttack(characters[myNr], possitions, good1.ElementAt(myNr), good1, 2);
			}
			return actions; //action, action, action, tile, tile, tile, target, target, target
		}

		//private void CheckAttack(CharacterTypeInterface character, int[] possitions, bool myAlignment, bool[] good, int action)
		private void CheckAttack(CharacterTypeInterface character, int[] possitions, bool myAlignment, List<bool> good1, int action)
		{
			Debug.Log("a");
			int min = 7;
			int myReach = character.ReachA;
			int myWalk = character.ReachW;
			int nr = target;
			int pos = myNewPos;

			for (int j = 0; j < 7; j++) // itterating through tiles
			{
				for(int i = 0; i < 4; i++) //later 6; itterating through alignment
				{
					if (good1.ElementAt(i) != myAlignment && possitions[j] == i) // is it against me and is he standing on this tile?
					{
						if(Math.Abs(j - myNewPos) <= min) //calculate possition; is it closest?
						{
							min = Math.Abs(j - myNewPos);
							nr = i; //i will attack the one with this index
							pos = j;
						}
					}
				}
			}
			if (myReach > min) //atack
			{
				actions[action] = 2;
				actions[action + 3] = myNewPos;
				actions[action + 6] = target;
			}
			else if (character.GetType().Name != "Mage") //walk for rogue & warrior
			{
				int walk = character.ReachW;
				while (walk != 0)
				{
					if (good1.ElementAt(possitions[myNewPos + ((pos - myNewPos) / Math.Abs(pos - myNewPos)) * myWalk]) != myAlignment) // is it against me and is he standing on this tile?
					{
						walk--;
						if (walk == 0) // I cannot neither walk, nor attack, I just heal
						{
							actions[action] = 3;
							actions[action + 3] = myNewPos;
							actions[action + 6] = target;
						}
					}
					else
					{
						myNewPos = myNewPos + ((pos - myNewPos) / Math.Abs(pos - myNewPos)) * myWalk;
						actions[action] = 1;
						actions[action + 3] = myNewPos;
						actions[action + 6] = target;
						walk = 0;
					}
				}
			}
			else // walk for mage
			{
				actions[action] = 1;
				actions[action + 6] = target;
				int tile0 = Math.Abs(0 - pos); // how far from target
				int tile6 = Math.Abs(6 - pos);
				int tile3 = Math.Abs(3 - pos);
				bool t0 = true; // is it free?
				bool t6 = true;
				bool t3 = true;
				for (int i = 0; i < 4; i++) //later 6; where are possible teleportation tiles?
				{
					if (good1.ElementAt(i) != myAlignment && i == 0) // is it against me and is he standing on this tile?
					{
						t0 = false;
					}
					if (good1.ElementAt(i) != myAlignment && i == 3) // is it against me and is he standing on this tile?
					{
						t3 = false;
					}
					if (good1.ElementAt(i) != myAlignment && i == 6) // is it against me and is he standing on this tile?
					{
						t6 = false;
					}
				}
				if (twoMages) // if two mages
				{
					if(tile0 < tile3 && t0 == true)
					{
						myNewPos = 0;
					}
					else if(tile6 < tile3 && t6 == true)
					{
						myNewPos = 6;
					}
					else if (t3 == true)
					{
						myNewPos = 3;
					}
					else
					{
						actions[action] = 3; // heal
					}
				}
				else if (tile0 < tile6 && t0 == true) // if only one mage
				{
					myNewPos = 0;
				}
				else if (tile6 < tile0 && t6 == true) // if only one mage
				{
					myNewPos = 6;
				}
				else // if only one mage
				{
					actions[action] = 3; // heal
				}
				actions[action + 3] = myNewPos;
			}
		}
	}
}
