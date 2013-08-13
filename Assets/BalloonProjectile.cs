using UnityEngine;
using System.Collections;

public class BalloonProjectile : MonoBehaviour {
	
	Vector3 speed;
	Transform model;
	public GameObject explosion;
	FlightGameManager gameManager;
	
	// Use this for initialization
	void Start () {
		speed = new Vector3 (-3,0,3);
		model = transform.FindChild("Model");
		gameManager = GameObject.Find("GameManager").GetComponent<FlightGameManager>();		
	}
	
	// Update is called once per frame
	void Update () {
		speed += Vector3.back * Time.deltaTime * 3;
		transform.position += speed * Time.deltaTime;
		//model.localRotation *= Quaternion.Euler(0, -360*Time.deltaTime,0);
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
