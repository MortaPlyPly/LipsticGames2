using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class GameControllerScript : MonoBehaviour
{

    public new Camera camera; //to stop and move camera and players character
    public GameObject player; //just to set animations? Do I need this? and I need to store lvl somehow
    public GameObject ai;
    bool fightTime = false;
    //public Canvas playerControlls;

    void Start ()
    {
	
	}

	void Update ()
    {
        if (camera.GetComponent<CameraResoliutionScript>().fight)
        {
            Debug.Log("Fight is initiated.");
            //Instantiate(playerControlls);
            player.GetComponent<CharScript>().SpawnControlls();
            camera.GetComponent<CameraResoliutionScript>().fight = false;
            camera.GetComponent<CameraResoliutionScript>().speed = 0f;
            Vector2 v = new Vector2();
            v = camera.GetComponent<Transform>().position;
            ai.GetComponent<AIScript>().SpawnMOB(player.GetComponent<CharScript>().Lvl, v);
            player.GetComponent<CharScript>().SpawnControlls();
            Moves();
        }
    }

    void Moves()
    {
        while (fightTime)//jei playerio eile, tai sukasi ilgai sitas, kol ne jo eile ir kol mobas ne dead
        {
            if (ai.GetComponent<AIScript>().isDead)
            {
                Debug.Log("Fight is over. Player won.");
                player.GetComponent<CharScript>().CloseControlls();
                camera.GetComponent<CameraResoliutionScript>().speed = 100f;
                player.GetComponent<CharScript>().GetComponent<CharScript>().CloseControlls();
                player.GetComponent<CharScript>().GetComponent<CharScript>().exp = player.GetComponent<CharScript>().GetComponent<CharScript>().exp + 100;
                fightTime = false;
            }
            else if (!player.GetComponent<CharScript>().turn)
            {
                Debug.Log("MOBs turn.");
                Debug.Log("Sending dmg for MOB: " + player.GetComponent<CharScript>().Damage);
                ai.GetComponent<AIScript>().MoveMOB(player.GetComponent<CharScript>().Damage, player.GetComponent<CharScript>().FullLife, player.GetComponent<CharScript>().LeftLife);
                player.GetComponent<CharScript>().GetDMG(ai.GetComponent<AIScript>().myDMG);
                Debug.Log("Sending dmg for player: " + ai.GetComponent<AIScript>().myDMG);
                player.GetComponent<CharScript>().turn = true;
                Debug.Log("Players turn.");
            }
        }
    }

    /*IEnumerator Wait()
    {
        yield return new WaitForSeconds(5); //this is not after 10 seconds, but after fight
        camera.GetComponent<CameraResoliutionScript>().speed = 100f;
    }*/
}
