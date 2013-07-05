using UnityEngine;
using System.Collections;

public class BossBulletDecelerating : MonoBehaviour {
	float speed = 7;
	float deceleration = 0.3f;
	
	FlightGameManager gameManager;
	
	void FixedUpdate() {
		transform.position += transform.forward * speed * Time.fixedDeltaTime;
		speed -= deceleration*Time.fixedDeltaTime;
		if (speed < deceleration*Time.fixedDeltaTime*2) Destroy(gameObject);
		if (gameManager.IsOutside(transform.position*0.3f)) Destroy(gameObject);
	}
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other) {
		//Debug.Log(other.tag);
		if (other.CompareTag("PlayerBullet")) {
		} else if (other.CompareTag("Bomb")) {
			Destroy(gameObject);
		} else if (other.CompareTag("Player")) {
			gameManager.PlayerDamage(1);
			Destroy(gameObject);
		}
	}
	
	void lookAtDragon() {
		transform.LookAt(gameManager.GetDragon().transform);
		transform.rotation *= Quaternion.Euler(0, Random.Range(-10,10),0);
	}
	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find("GameManager").GetComponent<FlightGameManager>();
		//lookAtDragon();
	}
}
