using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Assets._Scripts_._Character_Types_;

public class GameControllerScriptNew : MonoBehaviour
{
	// UI
	public GameObject meniuB;
	public GameObject meniuT;
	public GameObject fullMeniuB;
	public GameObject village;
	public GameObject returnToGame;
	public GameObject exit;
	public GameObject vilageB;
	public GameObject vilageT;
	public GameObject vilageY;
	public GameObject vilageN;
	public GameObject exitB;
	public GameObject exitT;
	public GameObject exitY;
	public GameObject exitN;
	// GAME OBJECTS OR PREFABS
	public GameObject grid;
	public GameObject ai;
	public Sprite red;
	public Sprite blue;
	public GameObject background; //background prefab
	// PUBLIC
	public float backgroundSpeed = 0.04f; //backround moving speed script
	public bool backgroundMove = false;
	// PRIVATE
	GameObject background1; //background gameobj
	GameObject background2; //background gameobj
	Vector3 backgroundPossition = new Vector3(18.2f, 0, 0);
	private bool encounter = false;
	private bool playerTurn = true;
	private int playerMoveCount = 0;
	private List<GameObject> gridParts = new List<GameObject>();
	private CharacterTypeInterface player;
	private GameObject playerGameObj;

	void Start ()
	{
		///////////////////////////
		//////////DEBUG////////////
		///////////////////////////
		Debug.Log("GameControllerScriptNew -> Start()");
		///////////////////////////

		background1 = (GameObject)Instantiate(background, new Vector3(0, 0, 0), Quaternion.identity);
		
		int i = 0;
		foreach (Transform child in grid.transform) // getting grid tiles
		{
			gridParts.Add(child.gameObject);
			i++;
		}

		// get player from save file or smth
		player = new Rogue(); // CAN BE AN ERROR!!! Monobehavior is not allowed with new keyword!
		// SET STATS BEFORE DEBUGGING
		player.CalculateStats(10);
		playerGameObj = (GameObject)Instantiate(ai.GetComponent<AIScriptNew>().charTry, new Vector3(-7.5f, -2.3f, 0), Quaternion.identity);
		// set appearance
	}
	
	void Update ()
	{
		///////////////////////////
		//////////DEBUG////////////
		///////////////////////////
		//Debug.Log("GameControllerScriptNew -> Update()");
		///////////////////////////

		MoveBackground();
		// ENCOUNTER
		if (encounter) //starting from playing and iterating through others
		{
			///////////////////////////
			//////////DEBUG////////////
			///////////////////////////
			//Debug.Log("ENCOUNTER");
			///////////////////////////
			backgroundMove = false;
			grid.SetActive(true);
			if (playerTurn)//this is where player pokes screen
			{
				///////////////////////////
				//////////DEBUG////////////
				///////////////////////////
				//Debug.Log("PLAYERS TURN");
				///////////////////////////
				/*Touch touch = Input.GetTouch(0);
				Vector2 v = touch.position;*/

				Vector2 v = new Vector2(100, 100);


				int x = Mathf.FloorToInt(v.x / 41.143f); // turn x coord to index [0..6]
				int playerPos = 0;
				for (int i = 0; i < 7; i++) // getting players possition
				{
					if (ai.GetComponent<AIScriptNew>().possition[i] == ai.GetComponent<AIScriptNew>().characters.Count - 1)
					{
						playerPos = i;
					}
				}
				if (gridParts[x].GetComponent<SpriteRenderer>().sprite == blue && v.y < 130f)
				{
					if (ai.GetComponent<AIScriptNew>().possition[x] == ai.GetComponent<AIScriptNew>().characters.Count - 1)
					{
						player.Heal();
						playerMoveCount++;
					}
					else
					{
						player.Walk();
						playerGameObj.transform.position = new Vector3(-7.5f + 2.5f * x, -2.3f, 0);
						ai.GetComponent<AIScriptNew>().possition[x] = ai.GetComponent<AIScriptNew>().characters.Count - 1;
						ai.GetComponent<AIScriptNew>().possition[playerPos] = -1;
						playerMoveCount++;
					}
				}
				else if (Mathf.Abs(x - playerPos) <= player.ReachA && v.y < 130f) // if red tile in players reach
				{
					for (int i = 0; i < ai.GetComponent<AIScriptNew>().characters.Count - 1; i++)
					{
						if (ai.GetComponent<AIScriptNew>().possition[x] == i) // itterating through enemes is any of them on this tile?
						{
							ai.GetComponent<AIScriptNew>().characters[i].GetHurt(player.Attack()); // bug? every enemy on this tile will get hurt
							playerMoveCount++;
						}
					}
				}
				if (playerMoveCount >= 3)
				{
					playerTurn = false;
				}
			}
			else
			{
				///////////////////////////
				//////////DEBUG////////////
				///////////////////////////
				Debug.Log("NPC TURN");
				///////////////////////////
				ai.GetComponent<AIScriptNew>().finishedTurn = false;
				ai.GetComponent<AIScriptNew>().NPCTurn(); // i hope that ai sends dmg for player too
				ai.GetComponent<AIScriptNew>().finishedTurn = false;
				if (ai.GetComponent<AIScriptNew>().allEnemiesDead)
				{
					///////////////////////////
					//////////DEBUG////////////
					///////////////////////////
					Debug.Log("YOU WIN");
					///////////////////////////
					encounter = false;
				}
				if (player.LeftLife <= 0)
				{
					///////////////////////////
					//////////DEBUG////////////
					///////////////////////////
					Debug.Log("YOU LOSE");
					///////////////////////////
					SceneManager.LoadScene(1);
				}
				playerMoveCount = 0;
				playerTurn = true;
			}
			// TILES
			int myTile = 0;
			for (int i = 0; i < 7; i++) // finding players tile
			{
				if (ai.GetComponent<AIScriptNew>().possition[i] == ai.GetComponent<AIScriptNew>().characters.Count - 1)
				{
					myTile = i;
				}
			}
			for (int i = 0; i < 7; i++)
			{
				if (i == myTile) // blue if player stans there
				{
					gridParts[i].GetComponent<SpriteRenderer>().sprite = blue;
				}
				else
				{
					if (Mathf.Abs(i - myTile) <= player.ReachW) // if its in players reach
					{
						if (ai.GetComponent<AIScriptNew>().possition[i] > -1) // is enemies standing there?
						{
							gridParts[i].GetComponent<SpriteRenderer>().sprite = red;
						}
						else // its free
						{
							gridParts[i].GetComponent<SpriteRenderer>().sprite = blue;
						}
					}
					else //its not in players reach
					{
						gridParts[i].GetComponent<SpriteRenderer>().sprite = red;
					}
				}
			}
		}
		else
		{
			backgroundMove = true;
			grid.SetActive(false);
		}
	}

	void OnTriggerEnter2D(Collider2D other) //spawning trigger
	{
		///////////////////////////
		//////////DEBUG////////////
		///////////////////////////
		Debug.Log("GameControllerScriptNew -> OnTriggerEnter2D()");
		///////////////////////////

		if (other.transform.tag == "Spawn")
		{
			encounter = true;
			other.enabled = false;
			StartEncounter();
		}
	}

	private void StartEncounter()
	{
		///////////////////////////
		//////////DEBUG////////////
		///////////////////////////
		Debug.Log("GameControllerScriptNew -> StartEncounter()");
		///////////////////////////
		ai.GetComponent<AIScriptNew>().Spawn(player); // PLAYER
		ai.GetComponent<AIScriptNew>().finishedTurn = true;
		playerTurn = true;
	}

	private void MoveBackground()
	{
		if (backgroundMove) //move background and spawn new next
		{
			background1.GetComponent<Transform>().position = //anim background1
				new Vector2(background1.GetComponent<Transform>().position.x - backgroundSpeed,
				background1.GetComponent<Transform>().position.y);
			if (background1.GetComponent<Transform>().position.x < -0.3f && background1.GetComponent<Transform>().position.x > -0.35f) //spawn background2 when time
			{
				if(background2 != null)
				{
					Destroy(background2.gameObject);
				}
				background2 = (GameObject)Instantiate(background, backgroundPossition, Quaternion.identity);
			}
			if (background2 != null)
			{
				background2.GetComponent<Transform>().position = //anim background2
				new Vector2(background2.GetComponent<Transform>().position.x - backgroundSpeed,
				background2.GetComponent<Transform>().position.y);
				if (background2.GetComponent<Transform>().position.x < -0.3f && background2.GetComponent<Transform>().position.x > -0.35f) //spawn background1 when time
				{
					Destroy(background1.gameObject);
					background1 = (GameObject)Instantiate(background, backgroundPossition, Quaternion.identity);
				}
			}
		}
	}

	public void MeniuButton ()
	{
		backgroundMove = false;
		SetMeniuButton(false);
		SetMeniu(true);
	}

	public void ReturnButton()
	{
		backgroundMove = true;
		SetMeniuButton(true);
		SetMeniu(false);
	}

	public void VillageButton ()
	{
		if (!encounter)
		{
			SetMeniu(false);
			SetVillage(true);
		}
	}

	public void ExitButton()
	{
		SetMeniu(false);
		SetExit(true);
	}

	public void ReturnToMeniuButton()
	{
		SetMeniu(true);
		SetVillage(false);
		SetExit(false);
	}

	public void YesVillageButton()
	{
		//load village level if not encounter
		SceneManager.LoadScene(0);
	}

	public void YesExitButton()
	{
		Application.Quit();
	}

	private void SetMeniuButton(bool b)
	{
		meniuB.SetActive(b);
		meniuT.SetActive(b);
	}

	private void SetMeniu(bool b)
	{
		fullMeniuB.SetActive(b);
		village.SetActive(b);
		returnToGame.SetActive(b);
		exit.SetActive(b);
	}

	private void SetVillage(bool b)
	{
		vilageB.SetActive(b);
		vilageT.SetActive(b);
		vilageY.SetActive(b);
		vilageN.SetActive(b);
}

	private void SetExit(bool b)
	{
		exitB.SetActive(b);
		exitT.SetActive(b);
		exitY.SetActive(b);
		exitN.SetActive(b);
}
}
