using UnityEngine;
using System.Collections;

public class TurnOnOff : MonoBehaviour
{
	public GameObject turn;

	void Start ()
	{
		turn.SetActive(false);
	}
	
	public void Set(bool active)
	{
		turn.SetActive(active);
	}
}
