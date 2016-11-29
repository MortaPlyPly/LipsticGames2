using UnityEngine;
using System.Collections;

public class GameControllerScriptNew : MonoBehaviour
{
    public GameObject background; //background prefab
    GameObject background1; //background gameobj
    GameObject background2; //background gameobj
    Vector3 backgroundPossition = new Vector3(18.2f, 0, 0);
    public float backgroundSpeed = 0.01f; //backround moving speed script
    private bool backgroundMove = true;

	void Start ()
    {
        background1 = (GameObject)Instantiate(background, new Vector3(0, 0, 0), Quaternion.identity);
    }
	
	void Update ()
    {
        MoveBackground();
    }

    private void MoveBackground()
    {
        if (backgroundMove) //move background and spawn new next
        {
            background1.GetComponent<Transform>().position = //anim background1
                new Vector2(background1.GetComponent<Transform>().position.x - backgroundSpeed,
                background1.GetComponent<Transform>().position.y);
            if (background1.GetComponent<Transform>().position.x < -0.3f && background1.GetComponent<Transform>().position.x > -0.35f) //spawn background2 when time
            {
                if(background2 != null)
                {
                    Destroy(background2.gameObject);
                }
                background2 = (GameObject)Instantiate(background, backgroundPossition, Quaternion.identity);
            }
            if (background2 != null)
            {
                background2.GetComponent<Transform>().position = //anim background2
                new Vector2(background2.GetComponent<Transform>().position.x - backgroundSpeed,
                background2.GetComponent<Transform>().position.y);
                if (background2.GetComponent<Transform>().position.x < -0.3f && background2.GetComponent<Transform>().position.x > -0.35f) //spawn background1 when time
                {
                    Destroy(background1.gameObject);
                    background1 = (GameObject)Instantiate(background, backgroundPossition, Quaternion.identity);
                }
            }
        }
    }
}
