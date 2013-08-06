using UnityEngine;
using System.Collections;

public class FlightSuperAttackPickup : MonoBehaviour {
	
	float speed = 1f;
	FlightGameManager gameManager;
	
	
	void FixedUpdate() {
		transform.position += Vector3.left * Time.fixedDeltaTime * speed;
		if (gameManager.IsOutside(transform.position)) Destroy(gameObject);
	}	
	
	void OnTriggerEnter(Collider other) {
		
		if (other.CompareTag("Player")) {
			gameManager.ChargeUp();
			Destroy(gameObject);
		}
		
	}
	
	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find("GameManager").GetComponent<FlightGameManager>();	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
