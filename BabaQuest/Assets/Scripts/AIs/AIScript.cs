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
    //MOBScript m = new MOBScript(); //mobs script.. idk if i need this or do it in other way... btw, need mob prefab
    int mobTurn; //what will mob do? heal? attack? evade/block??

    // Use this for initialization
    void Start () {
	    
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
        m.transform.position = v + new Vector2 (10, 0); //pastumiam i desine (i krasta) 
                                                        //nuo spawn point, kad galetu "ateiti" iki playerio
        m.GetComponent<Rigidbody2D>().velocity = vector.normalized * speed * Time.deltaTime;
        StartCoroutine(Wait()); //sustabdom moba po sekundes
        m.GetComponent<MOBScript>().lvlForMOB = playerLvl;
        System.Random rnd = new System.Random();
        m.GetComponent<MOBScript>().Proffession = rnd.Next(1, 4); //proffesion 1-4 //who the fuck mob is: wiz, figt, roug RANDOM?
        m.GetComponent<MOBScript>().CountYourStats();
        m.GetComponent<MOBScript>().SetAppearance(); //do inside randomising and setting;
        int fightStyle = rnd.Next(1, 101);//roll the dice for fighting style
        switch (m.GetComponent<MOBScript>().Proffession)
        {
            case 1:
                if (fightStyle < 50)//fight
                {
                    style = new Aggressive();
                }
                else if (fightStyle < 75)
                {
                    style = new Defensive();
                }
                else
                {
                    style = new Mixed();
                }
                break;
            case 2:
                if (fightStyle < 25) //wiz
                {
                    style = new Aggressive();
                }
                else if (fightStyle < 50)
                {
                    style = new Defensive();
                }
                else
                {
                    style = new Mixed();
                }
                break;
            case 3:
                if (fightStyle < 25)//rogue
                {
                    style = new Aggressive();
                }
                else if (fightStyle < 50)
                {
                    style = new Defensive();
                }
                else
                {
                    style = new Mixed();
                }
                break;
        }
        m.GetComponent<MOBScript>().SetEmotion(style.normal);
        //move mob with that float speed
        mobTurn = m.GetComponent<MOBScript>().ChooseWhatToDo();//nustatomas sekanciam kartui (pirma eina playeris)
    }

    public void MoveMOB(int dmg, int full, int left)
    {
        if (mobTurn == 1)
        {
            m.GetComponent<MOBScript>().Attack(dmg);
            myDMG = m.GetComponent<MOBScript>().Damage;
        }
        if (mobTurn == 2)
        {
            m.GetComponent<MOBScript>().HealMove(dmg);
        }
        if (mobTurn == 3)
        {
            m.GetComponent<MOBScript>().EvadeBlock(dmg);
        }
        //emocijos atsiranda po veiksmu XD
        ////////// bad emotions
        if (m.GetComponent<MOBScript>().LeftLife/ m.GetComponent<MOBScript>().FullLife < 0.1) //dot?? 0,5??
        {
            m.GetComponent<MOBScript>().SetEmotion(style.fear3);
        }
        else if (m.GetComponent<MOBScript>().LeftLife/ m.GetComponent<MOBScript>().FullLife < 0.25)
        {
            m.GetComponent<MOBScript>().SetEmotion(style.fear2);
        }
        else if (dmg/ m.GetComponent<MOBScript>().FullLife > 0.6)
        {
            m.GetComponent<MOBScript>().SetEmotion(style.anger2);
        }
        else if (m.GetComponent<MOBScript>().LeftLife/ m.GetComponent<MOBScript>().FullLife < 0.5)
        {
            m.GetComponent<MOBScript>().SetEmotion(style.fear1);
        }
        else if (dmg/ m.GetComponent<MOBScript>().FullLife > 0.3)
        {
            m.GetComponent<MOBScript>().SetEmotion(style.anger1);
        }
        //////////// good emotions
        else if (left/full < 0.2)
        {
            m.GetComponent<MOBScript>().SetEmotion(style.finishIt);
        }
        else if (m.GetComponent<MOBScript>().Damage/full > 0.6)
        {
            m.GetComponent<MOBScript>().SetEmotion(style.winningMood2);
        }
        else if (m.GetComponent<MOBScript>().Damage/full > 0.3)
        {
            m.GetComponent<MOBScript>().SetEmotion(style.winningMood1);
        }
        else
        {
            m.GetComponent<MOBScript>().SetEmotion(style.normal);
        }
        mobTurn = m.GetComponent<MOBScript>().ChooseWhatToDo();//nustatomas sekanciam kartui
        if (m.GetComponent<MOBScript>().LeftLife < 1)
        {
          isDead = true;
          Destroy(m);
        }
        //perduodama zaidejui automatiskai dabar :D per GameController'i
    }
}
