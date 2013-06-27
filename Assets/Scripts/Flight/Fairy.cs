using UnityEngine;
using System.Collections;

public class Fairy : MonoBehaviour {
	
	FlightGameManager gameManager;
	
	float life = 1;
	bool isActive = true;
	
	public GameObject[] powerups;
	public float powerupChance = 0.5f;
	
	

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find("ManagerObject").GetComponent<FlightGameManager>();
		isActive = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void Damage() {
		life --;
		if (life <=0) Death ();
	}
	
	void Death() {
		isActive = false;
		if (Random.Range(0f,1f) > powerupChance) {
			Debug.Log(powerups.Length);
			Instantiate(powerups[Random.Range(0,powerups.Length)], transform.position, Quaternion.identity);
		}
		Destroy(gameObject);		
	}
	
	void OnTriggerEnter(Collider other) {
		if (isActive) {

			if (other.CompareTag("PlayerBullet")) {
				Damage();
				other.GetComponent<PlayerBullet>().Damage();
			} else
			if (other.CompareTag("Player")) {
				gameManager.PlayerDamage();
			}else
			if (other.CompareTag("Bomb")) {
				Death ();
			}
		}
	}
}
