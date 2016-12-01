using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets._Scripts_._Character_Types_;
using Assets._Scripts_._AI_Types_;

public class AIScriptNew : MonoBehaviour
{
    //PRIVATE
    private List<CharacterTypeInterface> characters = new List<CharacterTypeInterface>();
    private List<AITypeInterface> aiType = new List<AITypeInterface>();
    private int[] possition = new int[7];
    private bool[] good = new bool[4]; //later 6 "is good"
    //private aiStyle[] ai;

    //PUBLIC
    public int dmgForPlayer;
    public int lvl;
    public int encExp = 0;
    public bool allEnemiesDead = false;
    //public int[] dmgForMob this shoul be sent straight from player to mob by touch

	void Start ()
    {
	
	}

    void Update()
    {
        foreach(CharacterTypeInterface c in characters)
        {
            if (c.LeftLife < 1)
            {
                characters.Remove(c);
                //c.enabled = false;
            }
        }
        if (characters.Count == 0)
        {
            allEnemiesDead = true;
        }
    }
    
    public void Spawn()
    {
        //roll exp by lvl
        SpawnFriends(); //done (empty)
        Spawnenemies(); //done
        RollTurns(); //whatevs for now
    }

    public void Move()
    {
        int[] actions = new int[9]; // action, action, action, tile, tile, tile, tile, target, target, target
        foreach (CharacterTypeInterface c in characters)
        {
            if (c.LeftLife < 1)
            {
                characters.Remove(c);
                //c.enabled = false;
            }
        }
    }

    private List<GameObject> SpawnFriends()
    {
        //not implemented
        //AI types from save files!
        return new List<GameObject>(); //empty for now
    }

    private void Spawnenemies()
    {
        System.Random r = new System.Random();
        for (int i = 0; i < r.Next(1, 3); i++)
        {
            switch (r.Next(1, 3))
            {
                case 1:
                    {
                        characters.Add(new Mage());
                        break;
                    }
                case 2:
                    {
                        characters.Add(new Rogue());
                        break;
                    }
                case 3:
                    {
                        characters.Add(new Warior());
                        break;
                    }
            }
        }
        foreach (CharacterTypeInterface g in characters)
        {
            g.CalculateStats(lvl);
        }
        foreach (CharacterTypeInterface g in characters)
        {
            switch (r.Next(1, 2))
            {
                case 1:
                    {
                        aiType.Add(new AISimple());
                        break;
                    }
                case 2:
                    {
                        aiType.Add(new AISmart());
                        break;
                    }
            }
        }
    }

    private void RollTurns()
    {
        //... whatevs
    }
}
