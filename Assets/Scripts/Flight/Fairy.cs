using UnityEngine;
using System.Collections;

public class Fairy : MonoBehaviour {
	
	public int life = 10;
	public GameObject[] powerups;
	public float powerupChance = 0.01f;
	public GameObject particlePrefab;
	

	
	FlightGameManager gameManager;
	

	
	
	void OnTriggerEnter(Collider other) {
		//Debug.Log(other.tag);
		if (other.CompareTag("PlayerBullet")) {
			Damage(1);
			other.GetComponent<FlightPlayerBullet>().Damage();
			if (particlePrefab)
			Instantiate(particlePrefab, other.transform.position, Quaternion.identity);
		} else if (other.CompareTag("Bomb")) {
			Damage(15);
		} else if (other.CompareTag("Player")) {
			gameManager.PlayerDamage(1);
			Damage (10);
		}
		
	}
	
	void Damage(int d) {
		
		gameManager.OnFairyDeath(1);
		life -= d;
		if (life<=0) {
			Death();
		}
	}
	
	void FixedUpdate() {
		if (gameManager.IsOutside(transform.position*0.3f)) Destroy(gameObject);
	}
	
	void Death() {
		gameManager.OnFairyDeath(5);
		if (Random.Range(0f,1f)<powerupChance) {
			Instantiate(powerups[Random.Range(0, powerups.Length)], transform.position, Quaternion.identity);
		}
		Destroy(gameObject);
	}

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find("GameManager").GetComponent<FlightGameManager>();
		transform.position = new Vector3(transform.position.x*gameManager.cam.aspect,transform.position.y,transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
