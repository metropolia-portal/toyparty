using UnityEngine;
using System.Collections;

public class FairyMovementTwo : MonoBehaviour {
	public bool shooting = true;
	float movementDuration = 4;
	float decelerationTime = 1; 
	float sidewaysSpeed = 0.5f;
	float forwardSpeed = 1;
	float idleTime = 1;
	float speed;
	float deceleration = 1;
	float bulletDelay = 1;
	float bulletDelayRemaining;
	public GameObject bulletPrefab;
	
	public float turnAngle = -70;
	int movementState = 0;
	Vector3 direction;
	public GameObject balls;
	
	
	void FixedUpdate() {
		if ((movementState != 0)&& shooting) {
			bulletDelayRemaining -= Time.fixedDeltaTime;
			if (bulletDelayRemaining < 0) {
				bulletDelayRemaining = bulletDelay;
				Instantiate(bulletPrefab, transform.position, Quaternion.identity);
			}
		}
		switch (movementState) {
		case 0:
			transform.position += speed * direction * Time.fixedDeltaTime;
			movementDuration -= Time.fixedDeltaTime;
			if (movementDuration < decelerationTime)
				speed -= Time.fixedDeltaTime * deceleration;
			if (speed < 0) speed = 0;
			if (movementDuration < 0) { 
				movementState ++;
				((GameObject)Instantiate(balls, transform.position, Quaternion.identity)).GetComponent<FairyBalls>().SetParent(transform);
				bulletDelayRemaining = bulletDelay;
			}
			break;
		case 1:
			idleTime -= Time.fixedDeltaTime;
			if (idleTime < 0) {
				movementState ++;
				direction = Quaternion.Euler(0,turnAngle+Random.Range(-5,5),0) * direction ;
			}
			break;
		case 2:
			transform.position += speed * direction * Time.fixedDeltaTime;
			if (speed < sidewaysSpeed) speed += Time.fixedDeltaTime;
			break;
		}
	}

	// Use this for initialization
	void Start () {
		direction = -transform.right;
		speed = forwardSpeed;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}