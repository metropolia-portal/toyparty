using UnityEngine;
using System.Collections;

public class Bucket : MonoBehaviour {
	
	InputManager inputManager;
	public Camera cam;
	public DropsGameManager gameManager;
	
	int direction = 0;
	float idleTime;
	float currentPosition = 0;
	float turnDetection = 1;
	float previousPosition = 0;
	float timeDelta = 0.5f;
	float tapSpeed = 0.2f;
	float untapDelay = 0.2f;
	float untapDelayLeft = 0;
	float speed;
	float maxNoiseSpeed = 0.01f;
	
	Animation2D dollSprite;
	
	// Use this for initialization
	void Start () {
		inputManager = GetComponent<InputManager>();
		dollSprite = GetComponent<Animator2D>().Child("Plane").GetCurrentAnimation();
		dollSprite.InitMaterial();
		dollSprite.SetFrame(1);
	}
	
	// Update is called once per frame
	void Update () {
		if (gameManager.IsGameRunning()) {
			Ray ray = cam.ScreenPointToRay(inputManager.GetCursorPosition());
		   	RaycastHit hit;
	
		    if (Physics.Raycast(ray, out hit)) {
				transform.position = new Vector3(
					Mathf.Clamp(hit.point.x, -gameManager.maxDistanceFromCenter, gameManager.maxDistanceFromCenter)
					, 1, -3.3f);
			}			
			
			if (idleTime > timeDelta) {
				idleTime = 0;
				
				previousPosition = transform.position.x;
			} else idleTime += Time.deltaTime;
			
			float movement = transform.position.x - previousPosition;
			speed = Mathf.Abs(movement);
			
			if (speed > tapSpeed) {
				untapDelayLeft = untapDelay;
				direction = (int)Mathf.Sign(movement);
			}
			
			if (speed < tapSpeed) {
					
				if (untapDelayLeft < 0) {
					direction = 0;
				} else untapDelayLeft -= Time.deltaTime;
			}
			if (speed < maxNoiseSpeed) {
					
				if (untapDelayLeft < 0) {
					direction = 0;
				} else untapDelayLeft -= 2*Time.deltaTime;
			}
			
			float deltaX = transform.position.x - currentPosition;
			
			if ((int)Mathf.Sign(deltaX) == direction) {
				currentPosition = transform.position.x;
			} else {
				if (Mathf.Abs(deltaX) > turnDetection) {
					previousPosition = currentPosition;
					currentPosition = transform.position.x;
					direction  = -direction;
				}
			}
			
			if (direction > 0)
					dollSprite.SetFrame(2);
				else if (direction < 0)
					dollSprite.SetFrame(3);
				else
					dollSprite.SetFrame(1);
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
