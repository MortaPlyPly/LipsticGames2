using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIScriptNew : MonoBehaviour
{
    //PRIVATE
    private List<GameObject> characters = new List<GameObject>();
    //private aiStyle[] ai;

    //PUBLIC
    //...

	void Start ()
    {
	
	}
    
    public void Spawn()
    {
        SpawnFriends();
        Spawnenemies();
        RollTurns();
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
