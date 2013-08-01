using UnityEngine;
using System.Collections;

public class FlightBoss1 : MonoBehaviour {
	
	public int life = 500;
	public GameObject[] powerups;
	public float powerupChance = 0.01f;
	public GameObject bossBulletOne;
	public GameObject bossOrbs;
	
	int state = 0;
	int jumpDirection = 1;
	int jumpsLeft = 3;
	float idleTime;
	float movementTime = 5;
	float speed = 3;
	float initialSpeed;
	float deceleration = 2f;
	float shotDelayOne = 0.2f;
	float shotDelayTwo = 1f;
	float shotChargesOne;
	float shotChargesTwo;
	

	
	FlightGameManager gameManager;
	

	
	
	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("PlayerBullet")) {
			other.GetComponent<FlightPlayerBullet>().Damage();
			Damage(1);
			
		} else if (other.CompareTag("Bomb")) {
			Damage(15);
		} else if (other.CompareTag("Player")) {
			gameManager.PlayerDamage(1);
		}
		
	}
	
	void Damage(int d) {
		life -= d;
		if (life<=0) {
			Death();
		}
		gameManager.SetBossLife((float)life*100/500);
	}
	
	void FixedUpdate() {
	
		switch (state) {
		case 0: // boss floats into the screen
			
			speed -= deceleration*Time.deltaTime;
			transform.position += Vector3.left * speed * Time.fixedDeltaTime;
			if (speed < deceleration*Time.fixedDeltaTime*2) {
				speed = 0;
				state =3;
				speed = 2.5f;
				shotChargesOne = 3;
				shotChargesTwo = 3;
			}

			break;
		case 1: // boss shoots a few bullets
			if (shotDelayOne >= 0) shotDelayOne -= Time.fixedDeltaTime;
			Debug.Log(shotDelayOne);
			if ((shotDelayOne<0)&&(shotChargesOne>0)) {
			
				lookAtDragon();
				Instantiate(bossBulletOne, transform.position, transform.rotation * Quaternion.Euler(0,-10,0));
				Instantiate(bossBulletOne, transform.position, transform.rotation);
				Instantiate(bossBulletOne, transform.position, transform.rotation * Quaternion.Euler(0,10,0));
				transform.rotation = Quaternion.identity;
				shotChargesOne--;
				shotDelayOne = 0.2f;
			}
			if (shotChargesOne<=0) {
		
				shotDelayTwo-=Time.fixedDeltaTime;
				if (shotDelayTwo<0) {
					shotChargesOne = 3;
					shotChargesTwo --;
					shotDelayTwo = 1;
					if (shotChargesTwo <= 0) {
						idleTime = 1f;
						state ++;
					}
				}
			}
			
			
			break;
		case 2: // boss stays on one spot
			idleTime -= Time.fixedDeltaTime;
			if (idleTime <0) {
				jumpDirection = -jumpDirection;
				speed = 5;
				state++;
				Instantiate(bossOrbs, transform.position, Quaternion.identity);
			}
			break;
		case 3: // boss dodges
			speed -= deceleration*Time.deltaTime;
			transform.position += Vector3.forward * speed * Time.fixedDeltaTime * jumpDirection;
			if (speed < deceleration*Time.fixedDeltaTime*2) {
				speed = 0;
				shotChargesOne = 3;
				shotChargesTwo = 3;
				//jumpsLeft --;
				if (jumpsLeft < 0) {
					state ++;
				} else {
					state = 1;
				}
			}
			break;
		case 4: // boss launches orbs
			
			break;
		}
	
		if (gameManager.IsOutside(transform.position*0.7f)) Destroy(gameObject);
	}
	
	void lookAtDragon() {
		transform.LookAt(gameManager.GetDragon().transform);
		transform.rotation *= Quaternion.Euler(0, Random.Range(-10,10),0);
	}
	
	void Death() {
		gameManager.OnFairyDeath(15);
		if (Random.Range(0f,1f)<powerupChance) {
			Instantiate(powerups[Random.Range(0, powerups.Length)], transform.position, Quaternion.identity);
		}
		gameManager.OnExit();
		Destroy(gameObject);
	}
	
	void Start() {
		gameManager = GameObject.Find("GameManager").GetComponent<FlightGameManager>();
		gameManager.SetBossLife(100);
		gameManager.ShowBossLife();
		transform.position = new Vector3(transform.position.x*gameManager.cam.aspect,transform.position.y,transform.position.z);
	}

}
