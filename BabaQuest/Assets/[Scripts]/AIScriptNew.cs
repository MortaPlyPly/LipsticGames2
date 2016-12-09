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
	public List<bool> good1 = new List<bool>();
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
	public bool created = false;

	void Start()
	{
		for (int i = 0; i < 7; i++)
		{
			possition[i] = -1;
		}
	}

	void Update()
	{
		lvl = 5;
		int bad = 0;
		if (created)
		{
            foreach (bool b in good1)
            {
                if (!b)
                {
                    bad++;
                }
            }
            for (int i = 1; i < characters.Count; i++) // checking every not player is it dead
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
					Debug.Log("NPC " + i + " DIED");
                    /*characters.RemoveAt(i);
					good1.Remove(good1[i]);
					aiType.Remove(aiType[i]);
					gameObj[i - 1].SetActive(false);*/
                    characters.RemoveAt(i);
                    good1.RemoveAt(i);
                    aiType.RemoveAt(i);
                    gameObj[i - 1].SetActive(false);
                    //break;
                    //*********
                    //gameObj.ElementAt(i).SetActive(false);
                }
			}
			if (bad == 0) // FIX THIS!
			{
				///////////////////////////
				//////////DEBUG////////////
				///////////////////////////
				Debug.Log("ALL ENEMIES DEAD");
				///////////////////////////
				allEnemiesDead = true;
			}
		}

        for (int i = 1; i < characters.Count(); i++)
        {
            gameObj[i - 1].GetComponentInChildren<TextMesh>().text = "NAME: " + i + System.Environment.NewLine + "LIFE: " + characters[i].LeftLife
                                                                + System.Environment.NewLine + "TYPE: " + characters[i].GetType().Name;
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
		// CHARACTER SPAWNING
		characters.Add(player);
		good1.Add(true);
		possition[2] = 0;
		aiType.Add(new AISmart()); // not used but needed for better indexing
		// SPAWN ELSE
		SpawnFriends(); //done (empty)
		Spawnenemies(player); //done
		RollTurns(); //whatevs for now

        

	}

	public void EmptyLists()
	{
		good1.Clear();
		characters.Clear();
		aiType.Clear();
		for (int i = 0; i < 7; i++)
		{
			possition[i] = -1;
		}
	}

	public int NPCTurn()
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
							gameObj[i - 1].transform.position = new Vector3(-7.5f + 2.5f * actions[j + 3], -2.3f, 0);
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
							Debug.Log("ATTACK WITH " + characters[i].Attack());
							Debug.Log("ATTACK WHO " + actions[j + 6]);
                            if (actions[j + 6] > characters.Count() - 1)
                            {
                                break;
                            }
							characters[actions[j + 6]].GetHurt(characters[i].Attack()); // ERROR
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
		return 0;
	}

	private void SpawnFriends()
	{
		// AI types from save files!
		// Later 0-2 friends, for now it's only 1
		characters.Add(new Warior(lvl)); // [1]
		good1.Add(true);
		aiType.Add(new AISmart());
		possition[1] = 1;
		gameObj.Add((GameObject)Instantiate(charTry, new Vector3(-7.5f + 2.5f * 1, -2.3f, 0), Quaternion.identity));
        gameObj[0].GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 0.5f);
        characters.Add(new Mage(lvl)); // [2]
        good1.Add(true);
        aiType.Add(new AISmart());
        possition[0] = 2;
        gameObj.Add((GameObject)Instantiate(charTry, new Vector3(-7.5f + 2.5f * 0, -2.3f, 0), Quaternion.identity));
        gameObj[1].GetComponent<SpriteRenderer>().color = new Color(0, 0, 1, 0.5f);

    }

	private void Spawnenemies(CharacterTypeInterface player)
	{
		int mages = 0;
		if (player.GetType().Name == "Mage")
		{
			mages++;
		}
		/*System.Random r = new System.Random();
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
		//characters.Add(player); // add player
		foreach (CharacterTypeInterface g in characters) // setting AI type
		{
			switch (r.Next(1, 2)) // DO NOT DELETE!!!
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
			aiType.Add(new AISimple()); // simplyfied for destytojas :D
		}*/
		Debug.Log("lvl " + lvl);
		characters.Add(new Warior(lvl)); // [1]
		good1.Add(false);
		aiType.Add(new AISimple());
        //possition[4] = 2;
		Debug.Log("mob 1 " + characters[2].FullLife);
		characters.Add(new Rogue(lvl)); // [2]
		good1.Add(false);
		aiType.Add(new AISimple());
        //possition[5] = 3;
        Debug.Log("mob 2 " + characters[3].FullLife);
		characters.Add(new Mage(lvl)); // [3]
		good1.Add(false);
		aiType.Add(new AISimple());
        //possition[6] = 4;
        Debug.Log("mob 3 " + characters[4].FullLife);
		int y = 4;
		for (int i = 3; i < characters.Count(); i++) // setting good/bad, prefabs and TILES
		{
			if (characters[i].GetType().Name == "Mage")
			{
				possition[6] = i;
                gameObj.Add((GameObject)Instantiate(charTry, new Vector3(-7.5f + 2.5f * 6, -2.3f, 0), Quaternion.identity));
            }
			else
			{
				possition[y] = i;
                gameObj.Add((GameObject)Instantiate(charTry, new Vector3(-7.5f + 2.5f * y, -2.3f, 0), Quaternion.identity));
                y++;
			}
			// create prefab -> set vector by possition index
			
			// set appearance ...
		}
        gameObj[2].GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.5f);
        gameObj[3].GetComponent<SpriteRenderer>().color = new Color(0.75f, 0, 0, 0.5f);
        gameObj[4].GetComponent<SpriteRenderer>().color = new Color(0.5f, 0, 0, 0.5f);
        created = true;
	}

	private void RollTurns()
	{
		//... whatevs
	}
}
