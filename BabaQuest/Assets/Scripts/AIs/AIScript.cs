using UnityEngine;
using System.Collections;
using Assets.Scripts;
using Assets.Scripts.Fighting_Styles;

public class AIScript : MonoBehaviour {
    
    private Vector2 vector = new Vector2(0, 1);
    float speed = 100f;
    bool isDead = true;
    int myDMG = 0;
    bool healTurn = false;
    bool evadeBlockTurn = false;
    public AbstractFightingStyle style;

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
        /////count & add to MOB class/////
        //mob lvl
        //who the fuck mob is: wiz, figt, roug
        //roll mob appearance
        //int, dex, str
        //roll the dice for fighting style
        style = new Aggressive();
        //MOB.CountYourStats();
        //MOB.SetEmotion(style.normal);
    }

    public void MoveMOB(int dmg, int full, int left)
    {
        if (healTurn)
        {
            //Mob.Heal();
        }
        if (evadeBlockTurn)
        {
            dmg = 0; //or make calculations how much evade/block;
        }
        //MOB.leftLife = Mob.leftLife - dmg;
        //if (MOB.leftLife < 1)
        //{
        //  isDead = true;
        //  Destroy(MOB);
        //}
        //else
        //{
        /*
            ////////// bad emotions
            if (Mob.leftLife/Mob.fullLife < 0.1) //dot?? 0,5??
            {
                MOB.SetEmotion(style.fear3);
            }
            else if (Mob.leftLife/Mob.fullLife < 0.25)
            {
                MOB.SetEmotion(style.fear2);
            }
            else if (dmg/Mob.fullLife > 0.6)
            {
                MOB.SetEmotion(style.anger2);
            }
            else if (Mob.leftLife/Mob.fullLife < 0.5)
            {
                MOB.SetEmotion(style.fear1);
            }
            else if (dmg/Mob.fullLife > 0.3)
            {
                MOB.SetEmotion(style.anger1);
            }
            //////////// good emotions
            else if (left/full < 0.2)
            {
                MOB.SetEmotion(style.finishIt);
            }
            else if (Mob.damage/full > 0.6)
            {
                MOB.SetEmotion(style.winningMood2);
            }
            else if (Mob.damage/full > 0.3)
            {
                MOB.SetEmotion(style.winningMood1);
            }
            else
            {
                MOB.SetEmotion(style.normal);
            }
         */
        //  ChooseMove();
        //}
    }

    void ChooseMove()
    {
        bool attack = false;
        //do calculations with MOB.actionPercents
        //?? roll for attack, roll for heal, roll for evade. who won? roll
        // those again, till one left???
        if (attack)
        {
            //myDMG = MOB.damage;
        }
        //kitais atvejais healTurn = true; arba evadeBlockTurn = true;
    }
}
