using UnityEngine;
using System.Collections;

public class BalloonSquirrelParts : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<AudioSource>().clip = GameObject.Find("GameManager").GetComponent<FlightGameManager>().soundManager.OwlBossDamage;
		GetComponent<AudioSource>().Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
