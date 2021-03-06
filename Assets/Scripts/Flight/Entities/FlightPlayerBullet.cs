using UnityEngine;
using System.Collections;

public class FlightPlayerBullet : MonoBehaviour {
	
	float speed = 10f;
	FlightGameManager gameManager;
	
	
	void FixedUpdate() {
		transform.position += transform.right * Time.fixedDeltaTime * speed;
		if (gameManager.IsOutside(transform.position)) Destroy(gameObject);
	}
	
	
	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find("GameManager").GetComponent<FlightGameManager>();
	}
	
	public void Damage() {
		Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
