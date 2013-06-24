using UnityEngine;
using System.Collections;

public class Fairy : MonoBehaviour {
	
	FlightGameManager gameManager;
	
	float life = 2;
	
	public GameObject[] powerups;
	public float powerupChance = 0.5f;
	
	

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find("ManagerObject").GetComponent<FlightGameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void Damage() {
		life --;
		if (life <=0) Death ();
	}
	
	void Death() {
		if (Random.Range(0f,1f) > powerupChance) {
			Instantiate(powerups[Random.Range(0,powerups.Length)], transform.position, Quaternion.identity);
		}
		Destroy(gameObject);		
	}
	
	void OnTriggerEnter(Collider other) {

		if (other.CompareTag("PlayerBullet")) {
			Damage();
		} else
		if (other.CompareTag("Player")) {
			gameManager.PlayerDamage();
		}else
		if (other.CompareTag("Bomb")) {
			Death ();
		}
	}
}
