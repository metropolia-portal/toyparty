using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {
	
	public int life = 1;
	public int score = 1;
	public GameObject fallingObject;
	FlightGameManager gameManager;
	public bool reposition = true;
	
	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find("GameManager").GetComponent<FlightGameManager>();
		if (reposition)
		transform.position = new Vector3(transform.position.x*gameManager.cam.aspect,transform.position.y,transform.position.z);
	}
	
	void Die() {
		
		Destroy(gameObject);
		if (fallingObject)
			Instantiate(fallingObject, transform.position, Quaternion.identity);
	}
	
	void FixedUpdate() {
		if (gameManager.IsOutside(transform.position*0.3f)) Destroy(gameObject);		
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("PlayerBullet")) {
			other.GetComponent<FlightPlayerBullet>().Damage();
			life --;
			if (life<=0) {
				gameManager.OnFairyDeath(score*2);
				Die();
			}
		} else if (other.CompareTag("Player")) {
			other.GetComponent<Dragon>().Damage(1);
			gameManager.OnFairyDeath(score);
			Die();
		} 
	}
}
