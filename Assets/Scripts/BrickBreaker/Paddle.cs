using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour {
	
	public AudioClip shootsound;
	public GameObject paddleModelDefault;

	// The GameObject that defines movement constraints for the paddle
	GameObject movementBounds;
	
	
	GameInputManager inputManager;
	
	float leftBound;
	float rightBound;
	
	// The paddle's model can be changed through Powerups
	GameObject currentPaddleModel;

	MainSphere mainSphere;
	GameManager gameManager;
	
	// Position at which the sphere is attached to the paddle
	Transform stuckTransform;
	
	bool sphereAttached = true;
	
#region Interface
	// Changes the paddle model to the clone of the given argument
	// This allows a Powerup to change the model of the paddle, e.g. to a wider one
	public void SetPaddleModel(GameObject newModel) {

		Destroy (currentPaddleModel);

		currentPaddleModel = (GameObject) Instantiate(newModel);
		currentPaddleModel.transform.position = transform.position;
		currentPaddleModel.transform.parent = transform;
		
		UpdateMovementBounds();
	}
	
	//resets the paddle model to normal one
	public void ResetPaddleModel() {
		SetPaddleModel (paddleModelDefault);
	}
	
	public bool IsOccupied() {
		return sphereAttached;
	}
#endregion
	
	// Use this for initialization
	void Start () {
		
		movementBounds = GameObject.Find("PaddleMovementBounds");
		mainSphere = GameObject.Find("Sphere").GetComponent<MainSphere>();
		stuckTransform = GameObject.Find ("StuckPosition").transform;
		inputManager = GameObject.Find("GameInput").GetComponent<GameInputManager>();
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
			
		ResetPaddleModel();
		AttachSphere();
	}
	
	
	// This method is called each time paddle model has changed (e.g. width powerup) so that bounds are accodingly updated
	void UpdateMovementBounds() {
		// Getting bounds from bounds GameObject
		float width =  movementBounds.renderer.bounds.size.x;
		leftBound  = movementBounds.transform.position.x - width/2;
		rightBound = leftBound + width;
		
		// Shifting the bounds so that the half of paddle won't pop out on the borders
		float paddleWidth = currentPaddleModel.collider.bounds.size.x;
		leftBound  += paddleWidth/2;
		rightBound -= paddleWidth/2;
	}
	
	// This method puts the main sphere in the launching position and attaches it to the paddle
	public void AttachSphere() {
		sphereAttached = true;
		
		mainSphere.gameObject.transform.position = stuckTransform.position;
	    mainSphere.gameObject.transform.parent = transform;
		
		// Disable sphere movement
		mainSphere.Freeze();
	}
	
	// Unattach the sphere from the pad, launch it and play a sound
	void LaunchSphere() {
		if(sphereAttached) {
			sphereAttached = false;
			
			mainSphere.gameObject.transform.parent = null;
	    	mainSphere.Launch();
	
			audio.PlayOneShot(shootsound,0.5f);
		}
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		if (!gameManager.IsGameRunning()) return;
	
		// Getting a projection ray from the mouse position on the screen, and saving it's first hit on the field
		
		if(Physics.Raycast(Camera.main.ScreenPointToRay(inputManager.GetCursorPosition()), out hit)) {
		 	//changing x position of the paddle
			
			transform.position = new Vector3(Mathf.Clamp(hit.point.x, leftBound, rightBound), transform.position.y, transform.position.z);
		}
		
		// Launching sphere
		if(inputManager.IsButtonDown() && sphereAttached ) {
			LaunchSphere();
        } 
		
		// For debugging
		if(Input.GetKeyUp(KeyCode.Space)) {
			AttachSphere();
		}


		

	}
}
