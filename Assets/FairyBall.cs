using UnityEngine;
using System.Collections;

public class FairyBall : MonoBehaviour {
	
	float hiddenTime = 0;
	int movementState = 0;
	float distance = 0;
	float speed = 1;
	float maxDistance = 1;
	public float angle = 0;
	float spinSpeed = 60;
	FlightGameManager gameManager;
	int life = 1;
	GameObject model;
	
	void FixedUpdate() {
		angle += Time.fixedDeltaTime * spinSpeed;
		if (angle>360) angle -= 360;
		transform.position = transform.parent.position + Quaternion.Euler(0,angle,0)*(Vector3.left*distance);
		switch (movementState) {
		case 0: // the spirit is invisible
			hiddenTime -= Time.fixedDeltaTime;
			Debug.Log(hiddenTime);
			if (hiddenTime < 0) {
				model.SetActive(true);
				movementState ++;
			}
			break;
		case 1: // the spirit is moving outwards
			
			distance += Time.fixedDeltaTime * speed;
			speed -= Time.fixedDeltaTime*0.3f;
			
			if (speed < 0) {
				//distance = maxDistance;
				movementState ++;
			}
			break;
		}
	}
	
		
	void OnTriggerEnter(Collider other) {
//		Debug.Log(other.tag);
		if (other.CompareTag("PlayerBullet")) {
			Damage(1);
			other.GetComponent<FlightPlayerBullet>().Damage();
		} else if (other.CompareTag("Bomb")) {
			Damage(15);
		} else if (other.CompareTag("Player")) {
			gameManager.PlayerDamage(1);
			Damage (10);
		}
		
	}
	
	void Damage(int d) {
		life -= d;
		if (life<=0) {
			Death();
		}
	}
	
	void Death() {

		Destroy(gameObject);
	}	
	
	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find("GameManager").GetComponent<FlightGameManager>();
		model = transform.FindChild("Plane").gameObject;
		model.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
