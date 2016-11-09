using UnityEngine;
using System.Collections;
using Assets.Scripts;
using Assets.Scripts.Fighting_Styles;

public class AIScript : MonoBehaviour //shows warings... ??? bloodina :D
{
	//for mob starter movement
	private Vector2 vector = new Vector2(0, 1);
	float speed = 100f;

	public bool isDead = true; //is mob dead?
	public int myDMG = 0; //dmg for players character
	public AbstractFightingStyle style; //mobs fighting style
	public GameObject m;
	int mobTurn; //what will mob do? heal? attack? evade/block??

	// Use this for initialization
	void Start () {
		m.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator Wait()
	{
		yield return new WaitForSeconds(1); //this is not after 10 seconds, but after fight
		speed = 0f;
		m.GetComponent<Rigidbody2D>().velocity = vector.normalized * speed * Time.deltaTime;
	}

	public void SpawnMOB(int playerLvl, Vector2 v)
	{
		isDead = false;
		Instantiate(m); //new mob
		m.SetActive(true);
		m.transform.position = v + new Vector2 (10, 0); //pastumiam i desine (i krasta) 
														//nuo spawn point, kad galetu "ateiti" iki playerio
		m.GetComponent<Rigidbody2D>().velocity = vector.normalized * speed * Time.deltaTime;
		StartCoroutine(Wait()); //sustabdom moba po sekundes
		//m.GetComponent<MOBScript>().lvlForMOB = playerLvl;
		m.GetComponent<MOBScript>().Lvl = playerLvl;
		System.Random rnd = new System.Random();
		m.GetComponent<MOBScript>().Proffession = rnd.Next(1, 4); //proffesion 1-4 //who the fuck mob is: wiz, figt, roug RANDOM?
		m.GetComponent<MOBScript>().CountYourStats();
		m.GetComponent<MOBScript>().SetAppearance(); //do inside randomising and setting;
		int fightStyle = rnd.Next(1, 101);//roll the dice for fighting style
		switch (m.GetComponent<MOBScript>().Proffession)
		{
			case 1:		//fighter
				if (fightStyle < 50)
					style = new Aggressive();
				else if (fightStyle < 75)
					style = new Defensive();
				else
					style = new Mixed();
				Debug.Log("MOBs proffession: Fighter" + "\r\nMOBs style: " + style.GetType().Name);
				break;
			case 2:		//wizard
				if (fightStyle < 25)
					style = new Aggressive();
				else if (fightStyle < 50)
					style = new Defensive();
				else
					style = new Mixed();
				Debug.Log("MOBs proffession: Mage" + "\r\nMOBs style: " + style.GetType().Name);
				break;
			case 3:		//rogue
				if (fightStyle < 25)
					style = new Aggressive();
				else if (fightStyle < 50)
					style = new Defensive();
				else
					style = new Mixed();
				Debug.Log("MOBs proffession: Rogue" + "\r\nMOBs style: " + style.GetType().Name);
				break;
		}
		//Debug.Log("MOBs proffession: " + m.GetComponent<MOBScript>().Proffession + "\r\nMOBs style: " + style.GetType().Name);
		//m.GetComponent<MOBScript>().SetEmotion(style.normal);
		int[] em = new int[3];
		em[0] = style.normal[0];
		em[1] = style.normal[1];
		em[2] = style.normal[2];
		m.GetComponent<MOBScript>().SetEmotion(em);
		//move mob with that float speed
		mobTurn = m.GetComponent<MOBScript>().ChooseWhatToDo();//nustatomas sekanciam kartui (pirma eina playeris)
	}

	public void MoveMOB(int dmg, int full, int left)
	{
		//Debug.Log("MOBs turn from AI");
		if (mobTurn == 1)
		{
			m.GetComponent<MOBScript>().Attack(dmg);
			myDMG = m.GetComponent<MOBScript>().Damage;
		}
		if (mobTurn == 2)
		{
			m.GetComponent<MOBScript>().HealMove(dmg);
			myDMG = m.GetComponent<MOBScript>().Damage;
		}
		if (mobTurn == 3)
		{
			m.GetComponent<MOBScript>().EvadeBlock(dmg);
			myDMG = m.GetComponent<MOBScript>().Damage;
		}
		//emocijos atsiranda po veiksmu XD
		////////// bad emotions
		if (m.GetComponent<MOBScript>().LeftLife/ m.GetComponent<MOBScript>().FullLife < 0.1)
		{
			Debug.Log("MOBs emotion fear3");
			m.GetComponent<MOBScript>().SetEmotion(style.fear3);
		}
		else if (m.GetComponent<MOBScript>().LeftLife/ m.GetComponent<MOBScript>().FullLife < 0.25)
		{
			Debug.Log("MOBs emotion fear2");
			m.GetComponent<MOBScript>().SetEmotion(style.fear2);
		}
		else if (dmg/ m.GetComponent<MOBScript>().FullLife > 0.6)
		{
			Debug.Log("MOBs emotion anger2");
			m.GetComponent<MOBScript>().SetEmotion(style.anger2);
		}
		else if (m.GetComponent<MOBScript>().LeftLife/ m.GetComponent<MOBScript>().FullLife < 0.5)
		{
			Debug.Log("MOBs emotion fear1");
			m.GetComponent<MOBScript>().SetEmotion(style.fear1);
		}
		else if (dmg/ m.GetComponent<MOBScript>().FullLife > 0.3)
		{
			Debug.Log("MOBs emotion anger1");
			m.GetComponent<MOBScript>().SetEmotion(style.anger1);
		}
		//////////// good emotions
		else if (left/full < 0.2)
		{
			Debug.Log("MOBs emotion finishIT");
			m.GetComponent<MOBScript>().SetEmotion(style.finishIt);
		}
		else if (m.GetComponent<MOBScript>().Damage/full > 0.6)
		{
			Debug.Log("MOBs emotion winningMood2");
			m.GetComponent<MOBScript>().SetEmotion(style.winningMood2);
		}
		else if (m.GetComponent<MOBScript>().Damage/full > 0.3)
		{
			Debug.Log("MOBs emotion winningMood1");
			m.GetComponent<MOBScript>().SetEmotion(style.winningMood1);
		}
		else
		{
			Debug.Log("MOBs emotion normal");
			m.GetComponent<MOBScript>().SetEmotion(style.normal);
		}
		mobTurn = m.GetComponent<MOBScript>().ChooseWhatToDo();//nustatomas sekanciam kartui
		if (m.GetComponent<MOBScript>().LeftLife < 1)
		{
			isDead = true;
		}
		//perduodama zaidejui automatiskai dabar :D per GameController'i
	}

	public void DestroyMOB()
	{
		m.SetActive(false);
	}
}
