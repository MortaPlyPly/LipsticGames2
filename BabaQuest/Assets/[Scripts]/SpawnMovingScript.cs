using UnityEngine;
using System.Collections;

public class SpawnMovingScript : MonoBehaviour
{
    private bool move;
    private float speed;

    void Update ()
    {
        move = GameObject.Find("GameController").GetComponent<GameControllerScriptNew>().backgroundMove;
        speed = GameObject.Find("GameController").GetComponent<GameControllerScriptNew>().backgroundSpeed;
        if (move)
        {
            gameObject.GetComponent<Transform>().position =
                new Vector2(gameObject.GetComponent<Transform>().position.x - speed,
                gameObject.GetComponent<Transform>().position.y);
        }
    }
}
