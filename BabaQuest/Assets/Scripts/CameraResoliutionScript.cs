using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class CameraResoliutionScript : MonoBehaviour {
    ///////OLD CANVAS PAR
    //public Canvas menius;
    ///////NEW CANVAS PAR
    public GameObject canvasObj; //this is game obj with children: canvas(shitty) and event system
    // canvasObj -> canvas & event system
    //canvas -> hiddenMeniu, hiddenScene, meniuButton, fightMeniu;
    public Canvas hiddenMeniu; //function in CameraControllScript
    public Canvas hiddenScene; //function in CameraControllScript
    public Canvas meniuButton; //function in CameraControllScript
    public Canvas fightMeniu; //function in CharScript
    Canvas c;
    ///////
    public float speed = 100f;
    private Vector2 vector = new Vector2(1,0);
    public bool fight = false;

    void Start()
    {
        Debug.Log("This is where MENIU button should start to show");
        //c = canvasObj.transform.GetComponent<Canvas>();//Find<Canvas>("GameMainCanvas");
        /*Instantiate(menius);
        Instantiate(meniuButton);
        meniuButton.transform.parent = menius.transform;*/
        //Instantiate(canvasObj);
        Instantiate(meniuButton);
        //meniuButton.transform.parent = c.transform;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EnemySpawnPoint"))
        {
            Instantiate(fightMeniu);
            //fightMeniu.transform.parent = c.transform;
            fight = true;
        }
        if (other.gameObject.CompareTag("Stop"))
        {
            speed = 0f;
            //hiddenScene.transform.position = new Vector2(240, 150);
            Debug.Log("This is where HIDDEN SCENE button should start to show");
            Instantiate(hiddenScene);
            //hiddenScene.transform.parent = c.transform;

        }
        Destroy(other.gameObject);
    }
    
    void Update()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = vector.normalized * speed * Time.deltaTime;
    }

    void EndFight() //dar niekur nekvieciams
    {
        Destroy(fightMeniu);
    }

    ////canvas buttons & etc
    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(0);
    }

    public void ShowHiddenMeniu()
    {
        Destroy(meniuButton);
        Instantiate(hiddenMeniu);
        //hiddenMeniu.transform.parent = c.transform;
        speed = 0f;
        //char speed = 0f;
    }

    public void HideHiddenMeniu()
    {
        Destroy(hiddenMeniu);
        Instantiate(meniuButton);
        //meniuButton.transform.parent = c.transform;
        speed = 100f;
        //char speed = 100f;6
    }
    
}
