using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets._Scripts_._Character_Types_;
using UnityEngine;

namespace Assets._Scripts_._AI_Types_
{
	class AISmart : MonoBehaviour, AITypeInterface
	{
		int minLife = 0;
		int myNewPos;
		int targetPos;
		bool twoMages = false;
		int[] actions = new int[9]; //1 - walk; 2 - attack; 3 - heal;
		int walk = 0;

		//public int[] Turn(List<CharacterTypeInterface> characters, int[] possitions, int myNr, bool[] good)
		public int[] Turn(List<CharacterTypeInterface> characters, int[] possitions, int myNr, List<bool> good1)
		{
			List<int> lives = new List<int>();
			int mages = 0;
			int target = myNr;
			for (int i = 0; i < 7; i++) // calculating my possition
			{
				if (possitions[i] == myNr)
				{
					myNewPos = i; // my possition
				}
			}

			foreach (CharacterTypeInterface c in characters) // special for mages
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
			for (int i = 0; i < good1.Count(); i++) // later 6; getting our target with least left life
			{
				//if (good1.ElementAt(i] != good1.ElementAt(myNr])
				if (good1[i] != good1[myNr])
				{
					lives.Add(i);
				}
			}
			minLife = characters[lives[0]].LeftLife;
			foreach (int l in lives)
			{
				if (characters[l].LeftLife < minLife)
				{
					minLife = characters[l].LeftLife;
					target = l; // target index
				}
			}
			for (int i = 0; i < 7; i++)
			{
				if (possitions[i] == target)
				{
					targetPos = i; //target possition
				}
			}
			if (characters[myNr].LeftLife / characters[myNr].FullLife < 0.1) // how many times do I heal?
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
				//CheckAttack(characters[myNr], good, possitions, 1, target, myNr);
				CheckAttack(characters[myNr], good1, possitions, 1, target, myNr);
			}
			else if (characters[myNr].LeftLife / characters[myNr].FullLife < 0.3)
			{
				actions[0] = 3;
				actions[3] = myNewPos;
				actions[6] = target;
				//CheckAttack(characters[myNr], good, possitions, 2, target, myNr);
				CheckAttack(characters[myNr], good1, possitions, 2, target, myNr);
			}
			else
			{
				//CheckAttack(characters[myNr], good, possitions, 3, target, myNr);
				CheckAttack(characters[myNr], good1, possitions, 3, target, myNr);
			}
			return actions; //action, action, action, tile, tile, tile, target, target, target
		}

		//private void CheckAttack(CharacterTypeInterface character, bool[] good, int[] possitions, int movesLeft, int targetNr, int myNr)
		private void CheckAttack(CharacterTypeInterface character, List<bool> good1, int[] possitions, int movesLeft, int targetNr, int myNr)
		{
			int myReach = character.ReachA;
			int myWalk = character.ReachW + 1;

			// WARRIOR & ROGUE
			if (character.GetType().Name != "Mage")
			{
				// ONE ACTION LEFT
				if (movesLeft == 1) // not full life, 1 move left
				{
					if (Math.Abs(targetPos - myNewPos) <= myReach) // if i can reach my target, than attack
					{
						actions[2] = 2;
						actions[5] = myNewPos;
						actions[8] = targetNr;
					}
					else // otherwise heal more
					{
						actions[2] = 3;
						actions[5] = myNewPos;
						actions[8] = myNr;
					}
				}
				// TWO ACTIONS LEFT
				if (movesLeft == 2) // just healed, do not have full life yet
				{
					// HIT AND RETREAT
					if (Math.Abs(targetPos - myNewPos) <= myReach) // if i can reach it, then attack and go away if I can
					{
						actions[1] = 2;
						actions[4] = myNewPos;
						actions[7] = targetNr;

						walk = character.ReachW;

						while (walk > 0) // can I go away?
						{
							if ((myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk) >= 0 && (myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk) < 7)
							{
								if (possitions[myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk] == -1)
								{
									myNewPos = myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk;
									actions[2] = 1;
									actions[5] = myNewPos;
									actions[8] = targetNr;
									walk = 0;
								}
								else if (good1[possitions[myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk]] == good1[myNr])
								{
									myNewPos = myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk;
									actions[2] = 1;
									actions[5] = myNewPos;
									actions[8] = targetNr;
									walk = 0;
								}
								else
								{
									walk--;
									if (walk == 0)
									{
										actions[2] = 3;
										actions[5] = myNewPos;
										actions[8] = myNr;
									}
								}
							}
							else
							{
								walk--;
							}
							/*if (good1[possitions[myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * myWalk]] != good1[myNr]
							&& myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * myWalk < 7 
							&& myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * myWalk >= 0) // can I go there?
						{
							walk--;
							if (walk == 0) // I cannot neither walk, nor attack, I just heal
							{
								actions[2] = 3;
								actions[5] = myNewPos;
								actions[8] = myNr;
							}
						}
						else // I can go there
						{
							myNewPos = myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * myWalk;
							actions[2] = 1;
							actions[5] = myNewPos;
							actions[8] = targetNr;
							walk = 0;
						}*/
						}
					}
					// GET CLOSE AND HIT
					else if (Math.Abs(targetPos - (myNewPos + ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk)) <= myReach) // if I can go near him and hit him
					{
						walk = character.ReachW;
						while (walk > 0) // can I go closer?
						{
							if ((myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk) >= 0 && (myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk) < 7)
							{
								if (possitions[myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk] == -1)
								{
									myNewPos = myNewPos + ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk;
									actions[2] = 1;
									actions[5] = myNewPos;
									actions[8] = targetNr;
									walk = 0;
								}
								else if (good1[possitions[myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk]] == good1[myNr])
								{
									myNewPos = myNewPos + ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk;
									actions[2] = 1;
									actions[5] = myNewPos;
									actions[8] = targetNr;
									walk = 0;
								}
								else
								{
									walk--;
									if (walk == 0)
									{
										actions[2] = 3;
										actions[5] = myNewPos;
										actions[8] = myNr;
									}
								}
							}
							else
							{
								walk--;
							}
							/*if (good1[possitions[myNewPos + ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * myWalk]] != good1[myNr]) // can I go there?
							{
								walk--;
								if (walk == 0) // I cannot neither walk, nor attack, I just heal
								{
									actions[2] = 3;
									actions[5] = myNewPos;
									actions[8] = myNr;
								}
							}
							else // I can go there
							{
								myNewPos = myNewPos + ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * myWalk;
								actions[2] = 1;
								actions[5] = myNewPos;
								actions[8] = targetNr;
								walk = 0;
							}*/
						}

						if (Math.Abs(targetPos - myNewPos) <= myReach) // if I am close enough now
						{
							actions[2] = 2;
							actions[5] = myNewPos;
							actions[8] = targetNr;
						}
						else //heal otherwise, because I still am hurt
						{
							actions[2] = 3;
							actions[5] = myNewPos;
							actions[8] = myNr;
						}
					}
					// GET CLOSE AND HEAL AGAIN
					else // if I nead more than one move to reach him, than heal and move towards
					{
						walk = character.ReachW;
						while (walk > 0) // can I go closer?
						{
							if ((myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk) >= 0 && (myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk) < 7)
							{
								if (possitions[myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk] == -1)
								{
									myNewPos = myNewPos + ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk;
									actions[2] = 1;
									actions[5] = myNewPos;
									actions[8] = targetNr;
									walk = 0;
								}
								else if (good1[possitions[myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk]] == good1[myNr])
								{
									myNewPos = myNewPos + ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk;
									actions[2] = 1;
									actions[5] = myNewPos;
									actions[8] = targetNr;
									walk = 0;
								}
								else
								{
									walk--;
									if (walk == 0)
									{
										actions[2] = 3;
										actions[5] = myNewPos;
										actions[8] = myNr;
									}
								}
							}
							else
							{
								walk--;
							}
							/*if (good1[possitions[myNewPos + ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * myWalk]] != good1[myNr]) // can I go there?
							{
								walk--;
								if (walk == 0) // I cannot neither walk, nor attack, I just heal
								{
									actions[2] = 3;
									actions[5] = myNewPos;
									actions[8] = myNr;
								}
							}
							else // I can go there
							{
								myNewPos = myNewPos + ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * myWalk;
								actions[2] = 1;
								actions[5] = myNewPos;
								actions[8] = targetNr;
								walk = 0;
							}*/
						}

						actions[2] = 3;
						actions[5] = myNewPos;
						actions[8] = myNr;
					}
				}
				// THREE ACTIONS LEFT
				if (movesLeft == 3) // I do not need heal yet and have three moves left
				{
					// HIT TWO TIMES AND RETREAT
					if (Math.Abs(targetPos - myNewPos) <= myReach) // if I can immediatly hit, I hit two times and go away
					{
						actions[0] = 2;
						actions[3] = myNewPos;
						actions[6] = targetNr;
						actions[1] = 2;
						actions[4] = myNewPos;
						actions[7] = targetNr;

						walk = character.ReachW;
						while (walk > 0) // can I go away?
						{
							if ((myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk) >= 0 && (myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk) < 7)
							{
								if (possitions[myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk] == -1)
								{
									myNewPos = myNewPos + ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk;
									actions[2] = 1;
									actions[5] = myNewPos;
									actions[8] = targetNr;
									walk = 0;
								}
								else if (good1[possitions[myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk]] == good1[myNr])
								{
									myNewPos = myNewPos + ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk;
									actions[2] = 1;
									actions[5] = myNewPos;
									actions[8] = targetNr;
									walk = 0;
								}
								else
								{
									walk--;
									if (walk == 0)
									{
										actions[2] = 2;
										actions[5] = myNewPos;
										actions[8] = targetPos;
									}
								}
							}
							else
							{
								walk--;
							}
							/*if (good1[possitions[myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * myWalk]] != good1[myNr]
								&& myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * myWalk < 7
								&& myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * myWalk >= 0) // can I go there?
							{
								walk--;
								if (walk == 0) // I cannot walk away, I attack
								{
									actions[2] = 2;
									actions[5] = myNewPos;
									actions[8] = targetPos;
								}
							}
							else // I can go there
							{
								myNewPos = myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * myWalk;
								actions[2] = 1;
								actions[5] = myNewPos;
								actions[8] = targetNr;
								walk = 0;
							}*/
						}
					}
					// GET CLOSE, HIT, RETREAT
					else if (Math.Abs(targetPos - (myNewPos + ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * myWalk)) <= myReach) // if I need one move to go toward and
					{                                       // reach him with my attack: I go, attack, go back
						walk = character.ReachW;
						while (walk > 0) // can I go closer?
						{
							if ((myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk) >= 0 && (myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk) < 7)
							{
								if (possitions[myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk] == -1)
								{
									myNewPos = myNewPos + ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk;
									actions[2] = 1;
									actions[5] = myNewPos;
									actions[8] = targetNr;
									walk = 0;
								}
								else if (good1[possitions[myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk]] == good1[myNr])
								{
									myNewPos = myNewPos + ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk;
									actions[2] = 1;
									actions[5] = myNewPos;
									actions[8] = targetNr;
									walk = 0;
								}
								else
								{
									walk--;
									if (walk == 0)
									{
										actions[2] = 3;
										actions[5] = myNewPos;
										actions[8] = myNr;
									}
								}
							}
							else
							{
								walk--;
							}
							/*if (good1[possitions[myNewPos + ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * myWalk]] != good1[myNr]) // can I go there?
							{
								walk--;
								if (walk == 0) // I cannot neither walk, nor attack, I just heal
								{
									actions[2] = 3;
									actions[5] = myNewPos;
									actions[8] = myNr;
								}
							}
							else // I can go there
							{
								myNewPos = myNewPos + ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * myWalk;
								actions[2] = 1;
								actions[5] = myNewPos;
								actions[8] = targetNr;
								walk = 0;
							}*/
						}
						if (Math.Abs(targetPos - myNewPos) <= myReach) // if I am close enough now
						{
							actions[2] = 2;
							actions[5] = myNewPos;
							actions[8] = targetNr;
						}
						else //heal otherwise, because I still am hurt
						{
							actions[2] = 3;
							actions[5] = myNewPos;
							actions[8] = myNr;
						}
						myNewPos = myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk;
						actions[2] = 1;
						actions[5] = myNewPos;
						actions[8] = targetNr;
					}
					// GET CLOSE WITH TWO ACTIONS AND HIT
					else if (Math.Abs(targetPos - (myNewPos + ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * myWalk * 2)) <= myReach) // if I need two moves to get close
					{                                       // I go near, go near, attack
						walk = character.ReachW;
						while (walk > 0) // can I go closer?
						{
							if ((myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk) >= 0 && (myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk) < 7)
							{
								if (possitions[myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk] == -1)
								{
									myNewPos = myNewPos + ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk;
									actions[2] = 1;
									actions[5] = myNewPos;
									actions[8] = targetNr;
									walk = 0;
								}
								else if (good1[possitions[myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk]] == good1[myNr])
								{
									myNewPos = myNewPos + ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk;
									actions[2] = 1;
									actions[5] = myNewPos;
									actions[8] = targetNr;
									walk = 0;
								}
								else
								{
									walk--;
									if (walk == 0)
									{
										actions[2] = 3;
										actions[5] = myNewPos;
										actions[8] = myNr;
									}
								}
							}
							else
							{
								walk--;
							}
							/*if (good1[possitions[myNewPos + ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * myWalk]] != good1[myNr]) // can I go there?
							{
								walk--;
								if (walk == 0) // I cannot neither walk, nor attack, I just heal
								{
									actions[2] = 3;
									actions[5] = myNewPos;
									actions[8] = myNr;
								}
							}
							else // I can go there
							{
								myNewPos = myNewPos + ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * myWalk;
								actions[2] = 1;
								actions[5] = myNewPos;
								actions[8] = targetNr;
								walk = 0;
							}*/
						}
						walk = character.ReachW;
						while (walk > 0) // can I go closer?
						{
							if ((myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk) >= 0 && (myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk) < 7)
							{
								if (possitions[myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk] == -1)
								{
									myNewPos = myNewPos + ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk;
									actions[2] = 1;
									actions[5] = myNewPos;
									actions[8] = targetNr;
									walk = 0;
								}
								else if (good1[possitions[myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk]] == good1[myNr])
								{
									myNewPos = myNewPos + ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk;
									actions[2] = 1;
									actions[5] = myNewPos;
									actions[8] = targetNr;
									walk = 0;
								}
								else
								{
									walk--;
									if (walk == 0)
									{
										actions[2] = 3;
										actions[5] = myNewPos;
										actions[8] = myNr;
									}
								}
							}
							else
							{
								walk--;
							}
							/*if (good1[possitions[myNewPos + ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * myWalk]] != good1[myNr]) // can I go there?
							{
								walk--;
								if (walk == 0) // I cannot neither walk, nor attack, I just heal
								{
									actions[2] = 3;
									actions[5] = myNewPos;
									actions[8] = myNr;
								}
							}
							else // I can go there
							{
								myNewPos = myNewPos + ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * myWalk;
								actions[2] = 1;
								actions[5] = myNewPos;
								actions[8] = targetNr;
								walk = 0;
							}*/
						}
						if (Math.Abs(targetPos - myNewPos) <= myReach) // if I am close enough now
						{
							actions[2] = 2;
							actions[5] = myNewPos;
							actions[8] = targetNr;
						}
						else //heal otherwise, because I still am hurt
						{
							actions[2] = 3;
							actions[5] = myNewPos;
							actions[8] = myNr;
						}
					}
					// GET CLOSE WITH THREE ACTIONS
					else // if I am very far away, I spend three moves to get closer to target
					{
						walk = character.ReachW;
						while (walk > 0) // can I go closer?
						{
							if ((myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk) >= 0 && (myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk) < 7)
							{
								if (possitions[myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk] == -1)
								{
									myNewPos = myNewPos + ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk;
									actions[2] = 1;
									actions[5] = myNewPos;
									actions[8] = targetNr;
									walk = 0;
								}
								else if (good1[possitions[myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk]] == good1[myNr])
								{
									myNewPos = myNewPos + ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk;
									actions[2] = 1;
									actions[5] = myNewPos;
									actions[8] = targetNr;
									walk = 0;
								}
								else
								{
									walk--;
									if (walk == 0)
									{
										actions[2] = 3;
										actions[5] = myNewPos;
										actions[8] = myNr;
									}
								}
							}
							else
							{
								walk--;
							}
							/*if (good1[possitions[myNewPos + ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * myWalk]] != good1[myNr]) // can I go there?
							{
								walk--;
								if (walk == 0) // I cannot neither walk, nor attack, I just heal
								{
									actions[2] = 3;
									actions[5] = myNewPos;
									actions[8] = myNr;
								}
							}
							else // I can go there
							{
								myNewPos = myNewPos + ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * myWalk;
								actions[2] = 1;
								actions[5] = myNewPos;
								actions[8] = targetNr;
								walk = 0;
							}*/
						}
						walk = character.ReachW;
						while (walk > 0) // can I go closer?
						{
							if ((myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk) >= 0 && (myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk) < 7)
							{
								if (possitions[myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk] == -1)
								{
									myNewPos = myNewPos + ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk;
									actions[2] = 1;
									actions[5] = myNewPos;
									actions[8] = targetNr;
									walk = 0;
								}
								else if (good1[possitions[myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk]] == good1[myNr])
								{
									myNewPos = myNewPos + ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk;
									actions[2] = 1;
									actions[5] = myNewPos;
									actions[8] = targetNr;
									walk = 0;
								}
								else
								{
									walk--;
									if (walk == 0)
									{
										actions[2] = 3;
										actions[5] = myNewPos;
										actions[8] = myNr;
									}
								}
							}
							else
							{
								walk--;
							}
							/*if (good1[possitions[myNewPos + ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * myWalk]] != good1[myNr]) // can I go there?
							{
								walk--;
								if (walk == 0) // I cannot neither walk, nor attack, I just heal
								{
									actions[2] = 3;
									actions[5] = myNewPos;
									actions[8] = myNr;
								}
							}
							else // I can go there
							{
								myNewPos = myNewPos + ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * myWalk;
								actions[2] = 1;
								actions[5] = myNewPos;
								actions[8] = targetNr;
								walk = 0;
							}*/
						}
						walk = character.ReachW;
						while (walk > 0) // can I go closer?
						{
							if ((myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk) >= 0 && (myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk) < 7)
							{
								if (possitions[myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk] == -1)
								{
									myNewPos = myNewPos + ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk;
									actions[2] = 1;
									actions[5] = myNewPos;
									actions[8] = targetNr;
									walk = 0;
								}
								else if (good1[possitions[myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk]] == good1[myNr])
								{
									myNewPos = myNewPos + ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk;
									actions[2] = 1;
									actions[5] = myNewPos;
									actions[8] = targetNr;
									walk = 0;

								}
								else
								{
									walk--;
									if (walk == 0)
									{
										actions[2] = 3;
										actions[5] = myNewPos;
										actions[8] = myNr;
									}
								}
							}
							else
							{
								walk--;
							}
							/*if (good1[possitions[myNewPos + ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * myWalk]] != good1[myNr]) // can I go there?
							{
								walk--;
								if (walk == 0) // I cannot neither walk, nor attack, I just heal
								{
									actions[2] = 3;
									actions[5] = myNewPos;
									actions[8] = myNr;
								}
							}
							else // I can go there
							{
								myNewPos = myNewPos + ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * myWalk;
								actions[2] = 1;
								actions[5] = myNewPos;
								actions[8] = targetNr;
								walk = 0;
							}*/
						}
					}
				}
			}
			else // walk for mage
			{
				// SETTING OUR THREE TILES: IS IT FREE TO TELEPORT?
				int tile0 = Math.Abs(0 - targetPos); // how far from target
				int tile6 = Math.Abs(6 - targetPos);
				int tile3 = Math.Abs(3 - targetPos);
				bool t0 = true; // is it free?
				bool t6 = true;
				bool t3 = true;
				for (int i = 0; i < good1.Count(); i++) //later 6; where are possible teleportation tiles?
				{
					if (good1[i] != good1[myNr] && i == 0) // is it against me and is he standing on this tile?
					{
						t0 = false;
					}
					if (good1[i] != good1[myNr] && i == 3) // is it against me and is he standing on this tile?
					{
						t3 = false;
					}
					if (good1[i] != good1[myNr] && i == 6) // is it against me and is he standing on this tile?
					{
						t6 = false;
					}
				}
				// IF TWO MAGES WE HAVE THREE TILES & WE TELEPORT TO THE CLOSEST ONE TO THE ENEMY
				if (twoMages) // if two mages
				{
					// TILE 0
					if (tile0 < tile3 && t0 == true)
					{
						if (myNewPos == 0) // I am already here, so I attack
						{
							for (int i = (3 - movesLeft); i < 3; i++) // cycle to iterate through my left moves
							{
								actions[i] = 2;
								actions[i + 3] = myNewPos;
								actions[i + 6] = targetNr;
							}
						}
						else // I teleport and then attack
						{
							myNewPos = 0;
							actions[3 - movesLeft] = 2;
							actions[3 - movesLeft + 3] = myNewPos;
							actions[3 - movesLeft + 6] = targetNr;
							movesLeft--;
							for (int i = (3 - movesLeft); i < 3; i++)
							{
								actions[i] = 2;
								actions[i + 3] = myNewPos;
								actions[i + 6] = targetNr;
							}
						}
					}
					// TILE 6
					else if (tile6 < tile3 && t6 == true)
					{
						if (myNewPos == 6) // I am already here, so I attack
						{
							for (int i = (3 - movesLeft); i < 3; i++) // cycle to iterate through my left moves
							{
								actions[i] = 2;
								actions[i + 3] = myNewPos;
								actions[i + 6] = targetNr;
							}
						}
						else // I teleport and then attack
						{
							myNewPos = 6;
							actions[3 - movesLeft] = 2;
							actions[3 - movesLeft + 3] = myNewPos;
							actions[3 - movesLeft + 6] = targetNr;
							movesLeft--;
							for (int i = (3 - movesLeft); i < 3; i++)
							{
								actions[i] = 2;
								actions[i + 3] = myNewPos;
								actions[i + 6] = targetNr;
							}
						}
					}
					// TILE 3
					else if (t3 == true)
					{
						if (myNewPos == 3) // I am already here, so I attack
						{
							for (int i = (3 - movesLeft); i < 3; i++) // cycle to iterate through my left moves
							{
								actions[i] = 2;
								actions[i + 3] = myNewPos;
								actions[i + 6] = targetNr;
							}
						}
						else // I teleport and then attack
						{
							myNewPos = 3;
							actions[3 - movesLeft] = 2;
							actions[3 - movesLeft + 3] = myNewPos;
							actions[3 - movesLeft + 6] = targetNr;
							movesLeft--;
							for (int i = (3 - movesLeft); i < 3; i++)
							{
								actions[i] = 2;
								actions[i + 3] = myNewPos;
								actions[i + 6] = targetNr;
							}
						}
					}
					// CANNOT INTO COMFY TELEPORT (SIMPLIFIED: AUTOMATICALLY HEALS)
					else
					{
						for (int i = (3 - movesLeft); i < 3; i++) // heal
						{
							actions[i] = 3;
							actions[i + 3] = myNewPos;
							actions[i + 6] = myNr;
						}
					}
				}
				// IF ONLY ONE MAGE WE TELEPORT TO CLOSEST TO THE ENEMY
				//TILE 0
				else if (tile0 < tile6 && t0 == true) // if only one mage
				{
					if (myNewPos == 0) // I am already here, so I attack
					{
						for (int i = (3 - movesLeft); i < 3; i++) // cycle to iterate through my left moves
						{
							actions[i] = 2;
							actions[i + 3] = myNewPos;
							actions[i + 6] = targetNr;
						}
					}
					else // I teleport and then attack
					{
						myNewPos = 0;
						actions[3 - movesLeft] = 2;
						actions[3 - movesLeft + 3] = myNewPos;
						actions[3 - movesLeft + 6] = targetNr;
						movesLeft--;
						for (int i = (3 - movesLeft); i < 3; i++)
						{
							actions[i] = 2;
							actions[i + 3] = myNewPos;
							actions[i + 6] = targetNr;
						}
					}
				}
				// TILE 6
				else if (tile6 < tile0 && t6 == true) // if only one mage
				{
					if (myNewPos == 6) // I am already here, so I attack
					{
						for (int i = (3 - movesLeft); i < 3; i++) // cycle to iterate through my left moves
						{
							actions[i] = 2;
							actions[i + 3] = myNewPos;
							actions[i + 6] = targetNr;
						}
					}
					else // I teleport and then attack
					{
						myNewPos = 6;
						actions[3 - movesLeft] = 2;
						actions[3 - movesLeft + 3] = myNewPos;
						actions[3 - movesLeft + 6] = targetNr;
						movesLeft--;
						for (int i = (3 - movesLeft); i < 3; i++)
						{
							actions[i] = 2;
							actions[i + 3] = myNewPos;
							actions[i + 6] = targetNr;
						}
					}
				}
				// CANNOT COMFY TELEPORT (SIMPLIFIED: AUTOMATICALLY HEALS)
				else // if only one mage
				{
					for (int i = (3 - movesLeft); i < 3; i++) // heal
					{
						actions[i] = 3;
						actions[i + 3] = myNewPos;
						actions[i + 6] = myNr;
					}
				}
			}
		}

		private void WalkFunction()
		{
			/* walk = character.ReachW;

			 while (walk > 0) // can I go away?
			 {
				 if ((myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk) >= 0 && (myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk) < 7)
				 {
					 if (possitions[myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk] == -1)
					 {
						 myNewPos = myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk;
						 actions[2] = 1;
						 actions[5] = myNewPos;
						 actions[8] = targetNr;
						 walk = 0;
					 }
					 else if (good1[possitions[myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk]] == good1[myNr])
					 {
						 myNewPos = myNewPos - ((targetPos - myNewPos) / Math.Abs(targetPos - myNewPos)) * walk;
						 actions[2] = 1;
						 actions[5] = myNewPos;
						 actions[8] = targetNr;
						 walk = 0;
					 }
					 else
					 {
						 walk--;
						 if (walk == 0)
						 {
							 actions[2] = 3;
							 actions[5] = myNewPos;
							 actions[8] = myNr;
						 }
					 }
				 }
				 else
				 {
					 walk--;
				 }
			 }*/
		}
	}
}
