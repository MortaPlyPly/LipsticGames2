using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Assets._Scripts_._Character_Types_;
using UnityEngine.UI;

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
	public GameObject lost;
	public GameObject turn;
	public GameObject prntDmg;
	// GAME OBJECTS OR PREFABS
	public GameObject grid;
	public GameObject ai;
	public Sprite red;
	public Sprite blue;
	public GameObject background; //background prefab
	// PUBLIC
	public float backgroundSpeed = 0.04f; //backround moving speed script
	public bool backgroundMove = true;
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
	int playerPos;
	//int x;
	private float myTimeWas;
	private float myTime;

	void Start()
	{
		//Debug.Log("GameControllerScriptNew -> Start()");

		background1 = (GameObject)Instantiate(background, new Vector3(0, 0, 0), Quaternion.identity);

		int i = 0;
		foreach (Transform child in grid.transform) // getting grid tiles
		{
			gridParts.Add(child.gameObject);
			i++;
		}


		// get player from save file or smth
		player = new Rogue(1); // CAN BE AN ERROR!!! Monobehavior is not allowed with new keyword!
								// SET STATS BEFORE DEBUGGING
								//player.CalculateStats(10);
		playerGameObj = (GameObject)Instantiate(ai.GetComponent<AIScriptNew>().charTry, new Vector3(-7.5f + 5f, -2.3f, 0), Quaternion.identity);
		playerGameObj.GetComponent<SpriteRenderer>().sprite = ai.GetComponent<AIScriptNew>().sprites[3]; // rogue iddle
																	   // set appearance
	}

	void Update()
	{
		Debug.Log(ai.GetComponent<AIScriptNew>().possition[0] + " " +
			ai.GetComponent<AIScriptNew>().possition[1] + " " +
			ai.GetComponent<AIScriptNew>().possition[2] + " " +
			ai.GetComponent<AIScriptNew>().possition[3] + " " +
			ai.GetComponent<AIScriptNew>().possition[4] + " " +
			ai.GetComponent<AIScriptNew>().possition[5] + " " +
			ai.GetComponent<AIScriptNew>().possition[6]);
		if (ai.GetComponent<AIScriptNew>().charHitAnim)
		{
			StartCoroutine(HurtAnim());
			ai.GetComponent<AIScriptNew>().charHitAnim = false;
		}

		grid.SetActive(encounter);
		//Debug.Log("Encounter " + encounter);
		playerGameObj.GetComponentInChildren<TextMesh>().text = "NAME: 0" + System.Environment.NewLine + "LIFE: " + player.LeftLife
																+ System.Environment.NewLine + "TYPE: " + player.GetType().Name;
		int myTile = 0;
		MoveBackground();
		prntDmg.SetActive(false);
		if (ai.GetComponent<AIScriptNew>().allEnemiesDead)
			encounter = false;
		// ENCOUNTER
		if (!encounter)
			backgroundMove = true;
		if (encounter) //starting from playing and iterating through others
		{
			prntDmg.SetActive(true);
			backgroundMove = false;
			// TILES
			for (int i = 0; i < 7; i++) // finding players tile
			{
				//Debug.Log(ai.GetComponent<AIScriptNew>().possition[i]);
				if (ai.GetComponent<AIScriptNew>().possition[i] == 0)
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
						//Debug.Log(!ai.GetComponent<AIScriptNew>().good1[ai.GetComponent<AIScriptNew>().possition[i]]);
						if (ai.GetComponent<AIScriptNew>().possition[i] > -1) // is someone standing there?
						{// ERROR
							Debug.Log("PLAYER ERROR WATCH: " + ai.GetComponent<AIScriptNew>().possition[i]);
							if (!ai.GetComponent<AIScriptNew>().good1[ai.GetComponent<AIScriptNew>().possition[i]]) // is enemy standing there?
							{
								gridParts[i].GetComponent<SpriteRenderer>().sprite = red;
							}
							else
							{
								gridParts[i].GetComponent<SpriteRenderer>().sprite = blue;
							}
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

			if (ai.GetComponent<AIScriptNew>().finishedTurn)
			{
				playerTurn = true;
				turn.SetActive(true);
			}
			else
			{
				turn.SetActive(false);
			}

			if (playerTurn /*&& (myTime - myTimeWas) > 0.3f */)//this is where player pokes screen
			{
				//Debug.Log("PLAYERS TURN");
				/*Touch touch = Input.GetTouch(0);
				Vector2 v = touch.position;*/
				//int x = Mathf.FloorToInt(v.x / 41.143f); // turn x coord to index [0..6]
				///Input.mousePosition
				float timeElapsed = 0.3f;
				if (Input.GetMouseButtonDown(0) && (Time.time - myTimeWas) > timeElapsed)
				{
					int x = Mathf.FloorToInt(Input.mousePosition.x / (Screen.width / 7));
					myTimeWas = Time.time;
					PlayerInputKey(x);
				}

				//float timeElapsed = 0.3f;
				/*if (Input.GetKey("q") && (Time.time - myTimeWas) > timeElapsed)
				{
					myTimeWas = Time.time;
					PlayerInputKey(0);
				}
				else if (Input.GetKey("w") && (Time.time - myTimeWas) > timeElapsed)
				{
					myTimeWas = Time.time;
					PlayerInputKey(1);
				}
				else if (Input.GetKey("e") && (Time.time - myTimeWas) > timeElapsed)
				{
					myTimeWas = Time.time;
					PlayerInputKey(2);
				}
				else if (Input.GetKey("r") && (Time.time - myTimeWas) > timeElapsed)
				{
					myTimeWas = Time.time;
					PlayerInputKey(3);
				}
				else if (Input.GetKey("t") && (Time.time - myTimeWas) > timeElapsed)
				{
					myTimeWas = Time.time;
					PlayerInputKey(4);
				}
				else if (Input.GetKey("y") && (Time.time - myTimeWas) > timeElapsed)
				{
					myTimeWas = Time.time;
					PlayerInputKey(5);
				}
				else if (Input.GetKey("u") && (Time.time - myTimeWas) > timeElapsed)
				{
					myTimeWas = Time.time;
					PlayerInputKey(6);
				}*/

				///
			}
			/*else
			{
				Debug.Log("NPC TURN");
			}*/
			if (ai.GetComponent<AIScriptNew>().allEnemiesDead)
			{
				Debug.Log("YOU WIN");
				///////////////////////////
				encounter = false;
				ai.GetComponent<AIScriptNew>().allEnemiesDead = false;
				ai.GetComponent<AIScriptNew>().EmptyLists();
				ai.GetComponent<AIScriptNew>().created = false;
			}
			if (player.LeftLife < 1)
			{

				Debug.Log("YOU LOSE");
				///////////////////////////
				StartCoroutine(Lost());
				//SceneManager.LoadScene(1);
			}
		}
		/*else
		{
			backgroundMove = true;
			//grid.SetActive(false);
		}*/
	}

	IEnumerator Lost()
	{
		yield return new WaitForSeconds(1.5f);
		fullMeniuB.SetActive(true);
		prntDmg.SetActive(false);
		turn.SetActive(false);
		encounter = false;
			// add btn for player input
		lost.SetActive(true);
		yield return new WaitForSeconds(3);
		SceneManager.LoadScene(1);
	}

	private void PlayerInputKey(int x)
	{
		//Debug.Log("input "+x);
		playerPos = 0;
		for (int i = 0; i < 7; i++) // getting players possition
		{
			if (ai.GetComponent<AIScriptNew>().possition[i] == 0)
			{
				playerPos = i;
			}
		}
		if (gridParts[x].GetComponent<SpriteRenderer>().sprite == blue /*&& v.y < 130f*/)
		{
			if (ai.GetComponent<AIScriptNew>().possition[x] == 0)
			{
				if (GameObject.Find("Dmg") != null)
					GameObject.Find("Dmg").GetComponent<Text>().text = "Healing.";
				player.Heal();  // COLOR
				//Debug.Log("Player heals: " + player.LeftLife);
				playerMoveCount++;
			}
			else
			{
				if (GameObject.Find("Dmg") != null)
					GameObject.Find("Dmg").GetComponent<Text>().text = "Walking.";
				//Debug.Log("Player walks.");
				player.Walk();
				playerGameObj.transform.position = new Vector3(-7.5f + 2.5f * x, -2.3f, 0);
				ai.GetComponent<AIScriptNew>().possition[x] = 0;
				ai.GetComponent<AIScriptNew>().possition[playerPos] = -1;
				playerMoveCount++;
			}
		}
		else if (Mathf.Abs(x - playerPos) <= player.ReachA /*&& v.y < 130f*/) // if red tile in players reach
		{
			for (int i = 1; i < ai.GetComponent<AIScriptNew>().characters.Count; i++)
			{
				if (ai.GetComponent<AIScriptNew>().possition[x] == i && !ai.GetComponent<AIScriptNew>().good1[i]) // itterating through enemes is any of them on this tile?
				{ // COLOR
					ai.GetComponent<AIScriptNew>().characters[i].GetHurt(player.Attack()); // bug? every enemy on this tile will get hurt
					//Debug.Log("Player attacks: " + player.Attack() + " tile " + x + " left life " + ai.GetComponent<AIScriptNew>().characters[i].LeftLife);
					if (GameObject.Find("Dmg") != null)
						GameObject.Find("Dmg").GetComponent<Text>().text = "Damage: 0" + player.Attack().ToString();
					StartCoroutine(Anim(i));
					playerMoveCount++;
				}
			}
		}
		if (playerMoveCount > 2)
		{
			playerTurn = false;
			ai.GetComponent<AIScriptNew>().finishedTurn = false;
			playerMoveCount = ai.GetComponent<AIScriptNew>().NPCTurn();
		}
	}

	IEnumerator Anim(int i)
	{
		playerGameObj.GetComponent<SpriteRenderer>().sprite = ai.GetComponent<AIScriptNew>().sprites[4];
		ai.GetComponent<AIScriptNew>().AnimHelp(i, 2);
		yield return new WaitForSeconds(0.5f);
		ai.GetComponent<AIScriptNew>().AnimHelp(i, 0);
		playerGameObj.GetComponent<SpriteRenderer>().sprite = ai.GetComponent<AIScriptNew>().sprites[3];
	}

	IEnumerator HurtAnim()
	{
		playerGameObj.GetComponent<SpriteRenderer>().sprite = ai.GetComponent<AIScriptNew>().sprites[5];
		yield return new WaitForSeconds(0.5f);
		playerGameObj.GetComponent<SpriteRenderer>().sprite = ai.GetComponent<AIScriptNew>().sprites[3];
	}

	void OnTriggerEnter2D(Collider2D other) //spawning trigger
	{
		///////////////////////////
		//////////DEBUG////////////
		///////////////////////////
		//Debug.Log("GameControllerScriptNew -> OnTriggerEnter2D()");
		///////////////////////////

		if (other.transform.tag == "Spawn")
		{
			ai.GetComponent<AIScriptNew>().lvl = player.Lvl; // should be from player...
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
		//Debug.Log("GameControllerScriptNew -> StartEncounter()");
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
				if (background2 != null)
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

	public void MeniuButton()
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

	public void VillageButton()
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
		//load mobZone level if not encounter
		SceneManager.LoadScene(3);
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
