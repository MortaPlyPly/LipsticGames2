using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets._Scripts_._Character_Types_;
using Assets._Scripts_._AI_Types_;

public class AIScriptNew : MonoBehaviour
{
    //PRIVATE
    private List<GameObject> characters = new List<GameObject>();
    private int[] possition = new int[7];
    private bool[] good = new bool[4]; //later 6 "is good"
    //private aiStyle[] ai;

    //PUBLIC
    public int dmgForPlayer;
    //public int[] dmgForMob this shoul be sent straight from player to mob by touch

	void Start ()
    {
	
	}
    
    public void Spawn()
    {
        SpawnFriends();
        Spawnenemies();
        RollTurns();
    }

    public void Move()
    {
        //itterate list
    }

    private List<GameObject> SpawnFriends()
    {
        //not implemented
        //AI types from save files!
        return new List<GameObject>(); //empty for now
    }

    private List<GameObject> Spawnenemies()
    {
        List<GameObject> enemies = new List<GameObject>();
        //roll what to spawn
        //generic enemy...
        foreach (GameObject g in enemies)
        {
            RollAIType(g.transform.name);
        }
        return enemies;
    }

    private void RollAIType(string name)
    {

    }

    private void RollTurns()
    {

    }
}
