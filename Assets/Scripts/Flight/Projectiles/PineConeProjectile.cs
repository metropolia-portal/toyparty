using UnityEngine;
using System.Collections;

public class PineConeProjectile : MonoBehaviour {
	
	Vector3 speed;
	public GameObject explosion;
	FlightGameManager gameManager;
	
	// Use this for initialization
	void Start () {
		speed = new Vector3 (0,0,-0.3f);
		gameManager = GameObject.Find("GameManager").GetComponent<FlightGameManager>();		
	}
	
	// Update is called once per frame
	void Update () {
		speed += Vector3.back * Time.deltaTime * 3;
		transform.position += speed * Time.deltaTime;
	}
	
	


	
	void Die() {
		
		Destroy(gameObject);
		if (explosion)
			Instantiate(explosion, transform.position, Quaternion.identity);
	}
	
	void FixedUpdate() {
		if (gameManager.IsOutside(transform.position*0.3f)) Destroy(gameObject);		
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")) {
			other.GetComponent<Dragon>().Damage(1);
			Die();
		} 
	}	
}
