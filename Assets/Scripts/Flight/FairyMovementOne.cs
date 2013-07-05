using UnityEngine;
using System.Collections;

public class FairyMovementOne : MonoBehaviour {
	public float avgSpeedForward = 1f;
	public float avgSpeedBackward = 3f;
	public float avgIdleTime = 1f;
	float speed;
	FlightGameManager gameManager;	
	int movementState = 0;
	public float forwardMovementDuration = 4;
	float movementDuration;
	public float deceleration = 1f;
	public float decelerationTime = 1f;
	public float acceleration = 1;
	
	void FixedUpdate() {
//		Debug.Log(movementDuration);
		switch (movementState) {
		case 0:
			lookAtDragon();
			movementDuration = forwardMovementDuration;
			movementState ++;
			break;
		case 1:
			transform.position -= transform.right * Time.fixedDeltaTime * speed; 
			movementDuration -= Time.fixedDeltaTime;
			if (movementDuration < decelerationTime)
				speed -= Time.fixedDeltaTime * deceleration;
			if (speed <0) speed = 0;
			if (movementDuration <=0) {
				movementState ++;
				lookAtDragon(); 
			}
			break;
		case 2:
			avgIdleTime -= Time.fixedDeltaTime;
			if (avgIdleTime <= 0) {
				lookAtDragon();
				movementState ++;
			}
			break;
		case 3:
			transform.position += transform.right * Time.fixedDeltaTime * speed; 
			if (speed < avgSpeedBackward)
				speed += acceleration * Time.fixedDeltaTime;
			else
				speed = avgSpeedBackward;
			break;
		default:
			break;
		}
		//transform.position += Vector3.left * Time.fixedDeltaTime * speed;
		
	}
	
	void lookAtDragon() {
		transform.LookAt(gameManager.GetDragon().transform);
		transform.rotation *= Quaternion.Euler(0, 90+Random.Range(-10,10),0);
	}
	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find("GameManager").GetComponent<FlightGameManager>();
		avgSpeedForward *= Random.Range(0.99f, 1.01f);
		avgSpeedBackward *= Random.Range(0.9f, 1.1f);
		avgIdleTime *= Random.Range(0.9f, 1.1f);
		speed = avgSpeedForward;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
