using UnityEngine;
using System.Collections;

public class Bucket : MonoBehaviour {
	
	InputManager inputManager;
	public Camera cam;
	public DropsGameManager gameManager;
	

	int direction = 0;
	float idleTime;


	
	float previousPosition = 0;
	float timeDelta = 0.5f;
	float tapSpeed = 0.2f;
	float untapDelay = 0.5f;
	float untapDelayLeft = 0;
	float speed;
	float maxSpeed = 0;
	float maxSpeedDisp = 0;
	int speedCount = 0;
	
	// Use this for initialization
	void Start () {
		inputManager = GetComponent<InputManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if (gameManager.IsGameRunning()) {
			Ray ray = cam.ScreenPointToRay(inputManager.GetCursorPosition());
		   	RaycastHit hit;
	
		    if (Physics.Raycast(ray, out hit)) {
				transform.position = new Vector3(
					Mathf.Clamp(hit.point.x, -gameManager.maxDistanceFromCenter, gameManager.maxDistanceFromCenter)
					, 1, -4);
			}
			
	
			
			
			if (idleTime > timeDelta) {
				idleTime = 0;
				
				previousPosition = transform.position.x;
			} else idleTime += Time.deltaTime;
			
			float movement = transform.position.x - previousPosition;
			speed = Mathf.Abs(movement);
			if (speed > maxSpeed) {
				maxSpeed = speed;
			}
			speedCount += 1;
			
			if (speedCount > 100) {
				maxSpeedDisp = maxSpeed;
				maxSpeed = 0;
				speedCount = 0;
			}
			
			if (speed > tapSpeed) {
				untapDelayLeft = untapDelay;
				direction = (int)Mathf.Sign(movement);
			}
			
			if (speed < tapSpeed) {
					
				if (untapDelayLeft < 0) {
					direction = 0;
				} else untapDelayLeft -= Time.deltaTime;
			}
			
			string doll_asc = "";
			if (direction > 0)
					doll_asc = ("/o/");
				else if (direction < 0)
					doll_asc = ("\\o\\");
				else
					doll_asc = ("|o|");
			
			Debug.Log(doll_asc +" "+ speed.ToString() + " " + maxSpeedDisp.ToString());

		}
	}
	
	void OnTriggerEnter (Collider other) {
		if (other.CompareTag("Toy")) {
			gameManager.OnToy();
		} else
		if (other.CompareTag("Bomb")) {
			Debug.Log("Kablam, sir.");
			gameManager.OnBomb();
		} else return;
		
		Destroy(other.gameObject);
		
	}
}
