using UnityEngine;
using System.Collections;

public class BrickPikUp : MonoBehaviour {

	float speed;
	bool isActive = false;
	
	BrickGameManager gameManager;
	

	// Use this for initialization
	void Start () {
		isActive = true;
		speed = 5;
		gameManager = GameObject.Find("GameManager").GetComponent<BrickGameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 temp = transform.position;
		temp.z -= speed * Time.deltaTime;
		transform.position = temp;
	}
	
	void OnTriggerEnter(Collider other) {
		if (!isActive) return; // This ensures that the pickup can only triggered once
		
		if(other.gameObject.CompareTag("Paddle")) {
			isActive = false;
			Destroy(gameObject);
			gameManager.OnPickup(); // Let the GameManager decide which Powerup is applied
		}
		
	    if(other.gameObject.CompareTag("DeathZone")) { 
	        Destroy(gameObject); // Destroy the Pickup if it reaches the bottom of the screen
	    }	
	}
}
