using UnityEngine;
using System.Collections;

public class RocketOwl : MonoBehaviour {
	
	float acceleration = 3f;
	Vector3 direction;
	float fuel = 2f;
	float maxSpeed = 4f;
	float preparation = 1f;
	Vector3 velocity = new Vector3 (0,0,0);
	FlightGameManager gameManager;
	
	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find("GameManager").GetComponent<FlightGameManager>();
	}
	
	


	void FixedUpdate(){
		if (preparation > 0) {
			preparation -= Time.fixedDeltaTime;
			if (preparation < 0) {
				Vector3 target = gameManager.GetDragon().transform.position;
				target.y = transform.position.y;
				direction = (target - transform.position).normalized;
			}
			return;
		}
		if (fuel > 0) {
			velocity += direction * acceleration * Time.fixedDeltaTime;
			fuel -= Time.fixedDeltaTime;
		} else
			velocity += Vector3.back * Time.fixedDeltaTime*2;
		if (velocity.magnitude > maxSpeed) velocity = velocity.normalized * maxSpeed;
		transform.LookAt(transform.position+velocity *Time.deltaTime);
		transform.localRotation *= Quaternion.Euler(0, 90, 0);
		
		transform.position += velocity * Time.fixedDeltaTime;
		
		
		
	}

}
