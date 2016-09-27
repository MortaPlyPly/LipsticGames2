using UnityEngine;
using System.Collections;
using Assets.Scripts;
using Assets.Scripts.Fighting_Styles;

public class AIScript : MonoBehaviour //shows warings... ??? bloodina :D
{
    //for mob starter movement
    private Vector2 vector = new Vector2(0, 1);
    float speed = 100f;

    bool isDead = true; //is mob dead?
    int myDMG = 0; //dmg for players character
    public AbstractFightingStyle style; //mobs fighting style
    MOBScript m = new MOBScript(); //mobs script.. idk if i need this or do it in other way... btw, need mob prefab
    int mobTurn; //what will mob do? heal? attack? evade/block??

    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SpawnMOB(int playerLvl)
    {
        isDead = false;
        //new mob?? prefab = new prefab??
        //m -> prefab!
        m.lvlForMOB = playerLvl;
        //who the fuck mob is: wiz, figt, roug RANDOM?
        m.CountYourStats();
        m.SetAppearance(); //do inside randomising;
        //roll the dice for fighting style
        style = new Aggressive();
        m.SetEmotion(style.normal);
        //move mob with that float speed
        mobTurn = m.ChooseWhatToDo();//nustatomas sekanciam kartui
    }

    public void MoveMOB(int dmg, int full, int left)
    {
        if (mobTurn == 1)
        {
            m.Attack(dmg);
            myDMG = m.Damage;
        }
        if (mobTurn == 2)
        {
            m.HealMove(dmg);
        }
        if (mobTurn == 3)
        {
            m.EvadeBlock(dmg);
        }
        //emocijos atsiranda po veiksmu XD
        ////////// bad emotions
        if (m.LeftLife/m.FullLife < 0.1) //dot?? 0,5??
        {
            m.SetEmotion(style.fear3);
        }
        else if (m.LeftLife/m.FullLife < 0.25)
        {
            m.SetEmotion(style.fear2);
        }
        else if (dmg/m.FullLife > 0.6)
        {
            m.SetEmotion(style.anger2);
        }
        else if (m.LeftLife/m.FullLife < 0.5)
        {
            m.SetEmotion(style.fear1);
        }
        else if (dmg/m.FullLife > 0.3)
        {
            m.SetEmotion(style.anger1);
        }
        //////////// good emotions
        else if (left/full < 0.2)
        {
            m.SetEmotion(style.finishIt);
        }
        else if (m.Damage/full > 0.6)
        {
            m.SetEmotion(style.winningMood2);
        }
        else if (m.Damage/full > 0.3)
        {
            m.SetEmotion(style.winningMood1);
        }
        else
        {
            m.SetEmotion(style.normal);
        }
        mobTurn = m.ChooseWhatToDo();//nustatomas sekanciam kartui
        if (m.LeftLife < 1)
        {
          isDead = true;
          Destroy(m); //and prefab
        }
        //perduodama zaidejui automatiskai dabar :D per GameController'i
    }
}
