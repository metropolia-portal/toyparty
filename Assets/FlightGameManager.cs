using UnityEngine;
using System.Collections;

public class FlightGameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector2.Distance(Input.mousePosition, new Vector2(Screen.width/20,Screen.width/20));
	}
}
