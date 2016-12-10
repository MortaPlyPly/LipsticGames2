using UnityEngine;
using UnityEngine.SceneManagement;
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
	public GameObject noticeBoard;
	public GameObject page;
	public bool backgroundMove = false;
	// PRIVATE
	GameObject background1; //background gameobj
	private CharacterTypeInterface player;

	void Start()
	{
		background1 = (GameObject)Instantiate(background, new Vector3(0, 0, 0), Quaternion.identity);
		player = new Rogue(10); // GET PLAYER FROM MOB ZONE
	}

	void Update() { }

	public void NoticeBoard()
	{
		page.SetActive(true);
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
