using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Assets._Scripts_._Character_Types_;

public class GameControllerVillageScript : MonoBehaviour
{
	// UI
	public GameObject meniuB;
	public GameObject meniuT;
	public GameObject fullMeniuB;
	public GameObject mobZone;
	public GameObject returnToGame;
	public GameObject exit;
	public GameObject mobB;
	public GameObject mobT;
	public GameObject mobY;
	public GameObject mobN;
	public GameObject exitB;
	public GameObject exitT;
	public GameObject exitY;
	public GameObject exitN;
	// GAME OBJECTS OR PREFABS
	public GameObject background; //background prefab
								  // PUBLIC
	public bool backgroundMove = false;
	// PRIVATE
	GameObject background1; //background gameobj
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
		background1 = (GameObject)Instantiate(background, new Vector3(0, 0, 0), Quaternion.identity);
		player = new Rogue(10);
		//playerGameObj = (GameObject)Instantiate(ai.GetComponent<AIScriptNew>().charTry, new Vector3(-7.5f + 5f, -2.3f, 0), Quaternion.identity);
		// set appearance
	}

	void Update()
	{}	

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

	public void MobButton()
	{
			SetMeniu(false);
			SetMob(true);
	}

	public void ExitButton()
	{
		SetMeniu(false);
		SetExit(true);
	}

	public void ReturnToMeniuButton()
	{
		SetMeniu(true);
		SetMob(false);
		SetExit(false);
	}

	public void YesMobButton()
	{
		//load mobZone level
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
		mobZone.SetActive(b);
		returnToGame.SetActive(b);
		exit.SetActive(b);
	}

	private void SetMob(bool b)
	{
		mobB.SetActive(b);
		mobT.SetActive(b);
		mobY.SetActive(b);
		mobN.SetActive(b);
	}

	private void SetExit(bool b)
	{
		exitB.SetActive(b);
		exitT.SetActive(b);
		exitY.SetActive(b);
		exitN.SetActive(b);
	}
}
