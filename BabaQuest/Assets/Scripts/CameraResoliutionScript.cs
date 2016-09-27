using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class CameraResoliutionScript : MonoBehaviour {

    public GameObject hiddenMeniu;
    public GameObject hiddenScene;
    public GameObject meniuButton;
    public float speed = 100f;
    private Vector2 vector = new Vector2(1,0);
    public bool fight = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EnemySpawnPoint"))
        {
            fight = true;
        }
        if (other.gameObject.CompareTag("Stop"))
        {
            speed = 0f;
            hiddenScene.transform.position = new Vector2(240, 150);
        }
        Destroy(other.gameObject);
    }
    
    void Update()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = vector.normalized * speed * Time.deltaTime;
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
        hiddenMeniu.transform.position = new Vector2(240, 150);
        meniuButton.transform.position = new Vector2(500, 500);
        speed = 0f;
        //char speed = 0f;
    }

    public void HideHiddenMeniu()
    {
        hiddenMeniu.transform.position = new Vector2(850, 500);
        meniuButton.transform.position = new Vector2(240, 150);
        speed = 100f;
        //char speed = 100f;6
    }
    
}
