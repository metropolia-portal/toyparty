using UnityEngine;
using System.Collections;

public class FinalCountdown : MonoBehaviour {
	public float time = 3;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		time -= Time.deltaTime;
		if (time < 0) {
			GameObject.Find("GameManager").GetComponent<FlightGameManager>().OnExit();
		}
	}
}
