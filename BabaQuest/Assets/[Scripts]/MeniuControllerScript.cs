using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MeniuControllerScript : MonoBehaviour
{
	public GameObject cred;
	public GameObject credT;
	public GameObject cont;
	public GameObject contT;
	public GameObject exit;
	public GameObject play;
	public GameObject back;

	public void Exit()
	{
		Application.Quit();
	}

	public void Play()
	{
		SceneManager.LoadScene(1);
	}

	public void Credits()
	{
		cred.SetActive(false);
		credT.SetActive(true);
		cont.SetActive(false);
		contT.SetActive(false);
		exit.SetActive(false);
		play.SetActive(false);
		back.SetActive(true);
	}

	public void Controlls()
	{
		cred.SetActive(false);
		credT.SetActive(false);
		cont.SetActive(false);
		contT.SetActive(true);
		exit.SetActive(false);
		play.SetActive(false);
		back.SetActive(true);
	}

	public void Back()
	{
		cred.SetActive(true);
		credT.SetActive(false);
		cont.SetActive(true);
		contT.SetActive(false);
		exit.SetActive(true);
		play.SetActive(true);
		back.SetActive(false);
	}
}
