using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Assets._Scripts_._Character_Types_;
using Assets._Scripts_._AI_Types_;

public class AIScriptNew : MonoBehaviour
{
	//PRIVATE
	private List<AITypeInterface> aiType = new List<AITypeInterface>();
	private List<GameObject> gameObj = new List<GameObject>();
	//private bool[] good = new bool[4]; //later 6 "is good"
	private List<bool> good1 = new List<bool>();
	//private aiStyle[] ai;

	//PUBLIC
	public List<CharacterTypeInterface> characters = new List<CharacterTypeInterface>();
	public int[] possition = new int[7] { -1, -1, -1, -1, -1, -1, -1 };
	public GameObject charTry;
	public int dmgForPlayer;
	public int lvl;
	public int encExp = 0;
	public bool allEnemiesDead = false;
	public bool finishedTurn = true;
	//public int[] dmgForMob this shoul be sent straight from player to mob by touch

	void Start()
	{
		for (int i = 0; i < 7; i++)
		{
			possition[i] = -1;
		}
	}

	void Update()
	{
		for (int i = 1; i < characters.Count; i++) // checking who is dead and is everybody dead
		{
			if (characters[i].LeftLife < 1)
			{
				for (int j = 0; j < 7; j++)
				{
					if (possition[j] == i)
					{
						possition[j] = -1;
					}
				}
				///////////////////////////
				//////////DEBUG////////////
				///////////////////////////
				Debug.Log("ENEMY " + i + "DIED");
				///////////////////////////
				characters.Remove(characters[i]);

				//*********
				//gameObj.ElementAt(i).SetActive(false);
			}
		}
		if (characters.Count == 2) // FIX THIS!
		{
			///////////////////////////
			//////////DEBUG////////////
			///////////////////////////
			Debug.Log("ALL ENEMIES DEAD");
			///////////////////////////
			allEnemiesDead = true;
		}
	}

	public void Spawn(CharacterTypeInterface player)
	{
		///////////////////////////
		//////////DEBUG////////////
		///////////////////////////
		Debug.Log("SPAWNING NPCS");
		///////////////////////////
		//roll exp by lvl
		SpawnFriends(); //done (empty)
		Spawnenemies(player); //done
		RollTurns(); //whatevs for now
	}

	public void NPCTurn()
	{
		int[] actions = new int[9]; // action, action, action, tile, tile, tile, target, target, target
		for (int i = 1; i < characters.Count; i++) // turns for everybody, except player, who is last in characters array
		{
			///////////////////////////
			//////////DEBUG////////////
			///////////////////////////
			Debug.Log("TURN FOR NPC " + i);
			///////////////////////////

			///////////////////////////
			//////////DEBUG////////////
			///////////////////////////
			Debug.Log("LEFT LIFE" + characters[i].LeftLife);
			///////////////////////////

			actions = aiType[i].Turn(characters, possition, i, good1);
			for (int j = 0; j < 3; j++)
			{
				switch (actions[j])
				{
					case 1:
						{
							///////////////////////////
							//////////DEBUG////////////
							///////////////////////////
							Debug.Log("WALK");
							///////////////////////////
							gameObj[i].transform.position = new Vector3(-7.5f + 2.5f * possition[j + 3], -2.3f, 0);
							characters[i].Walk(); //animation shit
												  // tiles/possition
							for (int x = 0; x < 7; x++)
							{
								if (possition[x] == i)
								{
									possition[x] = -1; // empty possition
								}
								possition[j + 3] = i;
							}
							break;
						}
					case 2:
						{
							///////////////////////////
							//////////DEBUG////////////
							///////////////////////////
							Debug.Log("ATTACK WITH " + characters[i].Attack());
							///////////////////////////
							///////////////////////////
							//////////DEBUG////////////
							///////////////////////////
							Debug.Log("ATTACK WHO " + characters[actions[j + 6]].LeftLife);
							///////////////////////////
							//actions[j+6] -> target
							characters[actions[j + 6]].GetHurt(characters[i].Attack());
							break;
						}
					case 3:
						{
							///////////////////////////
							//////////DEBUG////////////
							///////////////////////////
							Debug.Log("HEAL");
							///////////////////////////
							characters[i].Heal();
							break;
						}
				}
			}
			///////////////////////////
			//////////DEBUG////////////
			///////////////////////////
			Debug.Log("LEFT LIFE" + characters[i].LeftLife);
			///////////////////////////
		}
		finishedTurn = true;
	}

	private List<GameObject> SpawnFriends()
	{
		//not implemented
		//AI types from save files!
		return new List<GameObject>(); //empty for now
	}

	private void Spawnenemies(CharacterTypeInterface player)
	{
		int mages = 0;
		if (player.GetType().Name == "Mage")
		{
			mages++;
		}
		System.Random r = new System.Random();
		for (int i = 0; i < r.Next(1, 3); i++) // how much enemies
		{
			switch (r.Next(1, 3)) // getting proffession
			{
				case 1:
					{
						if (mages < 2)
						{
							characters.Add(new Mage());
							mages++;
						}
						else
						{
							switch (r.Next(1, 2))
							{
								case 1:
									{
										characters.Add(new Warior());
										break;
									}
								case 2:
									{
										characters.Add(new Rogue());
										break;
									}
							}
						}
						break;
					}
				case 2:
					{
						characters.Add(new Rogue());
						break;
					}
				case 3:
					{
						characters.Add(new Warior());
						break;
					}
			}
		}
		foreach (CharacterTypeInterface g in characters) // setting stats
		{
			g.CalculateStats(lvl);
		}
		characters.Add(player); // add player
		foreach (CharacterTypeInterface g in characters) // setting AI type
		{
			switch (r.Next(1, 2))
			{
				case 1:
					{
						aiType.Add(new AISimple());
						break;
					}
				case 2:
					{
						aiType.Add(new AISmart());
						break;
					}
			}
		}
		int y = 4;
		for (int i = 1; i < characters.Count; i++) // setting good/bad, prefabs and TILES
		{
			//good[i] = false;
			good1.Add(false);
			if (characters[i].GetType().Name == "Mage")
			{
				possition[6] = i;
			}
			else
			{
				possition[y] = i;
				y++;
			}
			// create prefab -> set vector by possition index
			gameObj.Add((GameObject)Instantiate(charTry, new Vector3(-7.5f + 2.5f * y, -2.3f, 0), Quaternion.identity));
			// set appearance ...
		}
		//good[0] = true;
		good1.Insert(0, true);
		possition[0] = 0;
	}

	private void RollTurns()
	{
		//... whatevs
	}
}
