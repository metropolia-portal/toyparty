using UnityEngine;
using System.Collections;

public class FlightSuperAttackPickup : MonoBehaviour {
	
	float speed = 1f;
	FlightGameManager gameManager;
	AudioSource pickupAudioSource;
	
	
	void FixedUpdate() {
		transform.position += Vector3.left * Time.fixedDeltaTime * speed;
		if (gameManager.IsOutside(transform.position)) Destroy(gameObject);
		pickupAudioSource = GetComponent<AudioSource>();
		pickupAudioSource.clip = gameManager.soundManager.Pickup;		
	}	
	
	void OnTriggerEnter(Collider other) {
		
		if (other.CompareTag("Player")) {
			pickupAudioSource.Play();
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
