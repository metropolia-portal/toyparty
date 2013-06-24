using UnityEngine;
using System.Collections;

public class FlightPickupPower : MonoBehaviour {
	
	FlightGameManager gameManager;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find("ManagerObject").GetComponent<FlightGameManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")) {
			Debug.Log("POWER");
			gameManager.AddPower(1);
			Destroy(gameObject);
		}
	}
}
