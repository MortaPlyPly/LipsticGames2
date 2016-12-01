using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Assets._Scripts_._Character_Types_;

public class GameControllerScriptNew : MonoBehaviour
{
<<<<<<< HEAD
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

	public GameObject background; //background prefab
	GameObject background1; //background gameobj
	GameObject background2; //background gameobj
	Vector3 backgroundPossition = new Vector3(18.2f, 0, 0);
	public float backgroundSpeed = 0.01f; //backround moving speed script
	public bool backgroundMove = false;

	private bool encounter = false;
	//private int characterCount;
	private bool playerTurn = true;
	private int playerMoveCount = 0;

	public GameObject ai;
	public GameObject player;
	private List<GameObject> friends = new List<GameObject>();
	private List<GameObject> enemies = new List<GameObject>();

	void Start ()
	{
		background1 = (GameObject)Instantiate(background, new Vector3(0, 0, 0), Quaternion.identity);
		/*
		SET EVERYTHING
		public GameObject ai;
		public GameObject player;
		private List<GameObject> friends = new List<GameObject>();
		private List<GameObject> enemies = new List<GameObject>();
		*/
	}
	
	void Update ()
	{
		MoveBackground();
		if (encounter) //starting from playing and iterating through others
		{
			if (playerTurn)//this is where player pokes screen
			{
				//do nothing.. comands are called through different functions in this code
			}
			else
			{
				playerMoveCount = 0;
				//...
				//ai move your ass and move everyone thats left
				//...
				playerTurn = true;
			}
		}
	}

	void OnTriggerEnter(Collider other) //spawning trigger
	{
		if (other.transform.tag == "Spawn")
		{
			encounter = true;
			StartEncounter();
		}
	}

	private void StartEncounter()
	{
		//call AI to spawn MOB/MOBS
	}

	private void OneCharTurn(int charIndex)
	{

	}

	private void PlayerWalk ()
	{
		//check if can walk here if not error & return... wait for another command
		//if yes, send info to player to walk
		playerMoveCount++;
		if (playerMoveCount == 3)
		{
			playerTurn = false;
		}
	}

	private void PlayerHeal()
	{
		//send command to heal player
		playerMoveCount++;
		if (playerMoveCount == 3)
		{
			playerTurn = false;
		}
	}

	private void PlayerAttack()
	{
		//get info from player about attack
		//can he attack?
		//if no return and wait for another command
		//if yes attack -> KINTAMIEJI!!!
		playerMoveCount++;
		if (playerMoveCount == 3)
		{
			playerTurn = false;
		}
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
		if(SceneManager.GetActiveScene().name == "Village")
			backgroundMove = false;
		else
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
=======
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
    //public GameObject player;
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

    void Start ()
    {
        background1 = (GameObject)Instantiate(background, new Vector3(0, 0, 0), Quaternion.identity);
        
        int i = 0;
        foreach (Transform child in grid.transform) // getting grid tiles
        {
            gridParts.Add(child.gameObject);
            i++;
        }

        // get player from save file or smth
        player = new Rogue();
        // SET STATS BEFORE DEBUGGING
    }
	
	void Update ()
    {
        MoveBackground();
        // ENCOUNTER
        if (encounter) //starting from playing and iterating through others
        {
            grid.SetActive(true);
            if (playerTurn)//this is where player pokes screen
            {
                //do nothing.. comands are called through different functions in this code
            }
            else
            {
                ai.GetComponent<AIScriptNew>().finishedTurn = false;
                ai.GetComponent<AIScriptNew>().NPCTurn(); // i hope that ai sends dmg for player too
                ai.GetComponent<AIScriptNew>().finishedTurn = false;
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
            grid.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other) //spawning trigger
    {
        if (other.transform.tag == "Spawn")
        {
            encounter = true;
            StartEncounter();
        }
    }

    private void StartEncounter()
    {
        ai.GetComponent<AIScriptNew>().Spawn(player); // PLAYER
        ai.GetComponent<AIScriptNew>().finishedTurn = true;
        playerTurn = true;
    }

    private void PlayerWalk ()
    {
        //check if can walk here if not error & return... wait for another command
        //if yes, send info to player to walk
        player.Walk(); // animation
        playerMoveCount++;
        if (playerMoveCount == 3)
        {
            playerTurn = false;
        }
    }

    private void PlayerHeal() // player tapped himself
    {
        player.Heal();
        playerMoveCount++;
        if (playerMoveCount == 3)
        {
            playerTurn = false;
        }
    }

    private void PlayerAttack(int i) // i - pushed person
    {
        player.Attack();
        playerMoveCount++;
        if (playerMoveCount == 3)
        {
            playerTurn = false;
        }
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
>>>>>>> refs/remotes/origin/Mortos
}
