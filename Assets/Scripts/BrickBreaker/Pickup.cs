using UnityEngine;
using System.Collections;

// This script handles the items that sometimes drop from destroyed bricks and apply Powerups if picked up
public class Pickup : MonoBehaviour {
	
	float speed;
	BrickGameManager gameManager;
	bool isActive = false;

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
			gameManager.OnPickup(); // Let the BrickGameManager decide which Powerup is applied
		}
		
	    if(other.gameObject.CompareTag("DeathZone")) { 
	        Destroy(gameObject); // Destroy the Pickup if it reaches the bottom of the screen
	    }
		
	}	
	
}
