using UnityEngine;
using System.Collections;

public class GameControllerScript : MonoBehaviour
{

    public new Camera camera; //to stop and move camera and players character
    public GameObject player; //just to set animations? Do I need this? and I need to store lvl somehow
    public GameObject playerControlls; //to activate players fight moves
    public GameObject ai;

    void Start ()
    {
	
	}

	void Update ()
    {
        if (camera.GetComponent<CameraResoliutionScript>().fight)
        {
            camera.GetComponent<CameraResoliutionScript>().fight = false;
            camera.GetComponent<CameraResoliutionScript>().speed = 0f;
            //AI.SpawnMob(player.GetLevel());
            //playerControlls.transform.position = ???; //start to show
            Moves();
            StartCoroutine(Wait()); //this is fight next if
        }
    }

    void Moves()
    {
        /*
        if (AI.isDead)
          {
               camera.GetComponent<CameraResoliutionScript>().speed = 100f;
               playerControlls.transform.position = ???;
          }
          else
          {
            if (player.turn!)
            {
                AI.MoveMOB(player.lastMoveDMG, player.fullLife, player.leftLife);
                player.dmgFromMOB = AI.myDMG;
                player.turn = true;
            }
            else
            {
                //do nothing till player does its job and sais its mobs turn
            }
          }
        */
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5); //this is not after 10 seconds, but after fight
        camera.GetComponent<CameraResoliutionScript>().speed = 100f;
    }
}
