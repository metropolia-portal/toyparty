using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour {
	
	public MainSphere mainSphere;
	// Position at which the sphere is attached to the paddle
	public Transform stuckTransform;
	// The GameObject that defines movement constraints for the paddle
	public GameObject movementBounds;
	
	public AudioClip shootsound;
	public GameObject paddleModelDefault;

	
	BrickGameManager gameManager;
	InputManager inputManager;
	
	float leftBound;
	float rightBound;
	
	// The paddle's model can be changed through Powerups
	GameObject currentPaddleModel;

	bool sphereAttached = true;
	
	bool paddleMoved = false;
	
	//Cached variables
	Transform tr;
	
	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform>();
		Vector3 vec = tr.position;
		vec.z = -18;
		tr.position = vec;
		gameManager = GameObject.Find("GameManager").GetComponent<BrickGameManager>();
		inputManager = GameObject.Find("GameInput").GetComponent<InputManager>();
			
		ResetPaddleModel();
		AttachSphere();
	}
	// Update is called once per frame
	void Update () {
		if(inputManager.GetCursorPosition() != new Vector2(0,0)) paddleMoved = true; //at game start don't move paddle until user touches the screen, as the GetCursorPosition() gives 0,0
		
		if (!gameManager.IsGameRunning() || !paddleMoved) return;
	
		// Getting a projection ray from the mouse position on the screen, and saving it's first hit on the field
		RaycastHit hit;
		if(Physics.Raycast(Camera.main.ScreenPointToRay(inputManager.GetCursorPosition()), out hit)) {
		 	//changing x position of the paddle
			Vector3 pos = tr.position;
			pos.x = Mathf.Clamp(hit.point.x, leftBound, rightBound);
			tr.position = pos;
		}
		
		// Launching sphere
		if(sphereAttached){ //separating condition because we can only check inputManager.IsSecondButtonDown() once per frame, and lazer conflicts with it! 
			if(inputManager.IsSecondButtonDown())
			{
				LaunchSphere();
		
			}
		}	
		// For debugging
		if(Input.GetKeyUp(KeyCode.Space)) {
			AttachSphere();
		}
	}
	
#region Interface
	// Changes the paddle model to the clone of the given argument
	// This allows a Powerup to change the model of the paddle, e.g. to a wider one
	
	public void SetPaddleModel(GameObject newModel) {
		StartCoroutine(AnimateAndSetPaddleModel(newModel));
	}
	
	IEnumerator AnimateAndSetPaddleModel(GameObject newModel) {
		if(currentPaddleModel) {
			yield return StartCoroutine(currentPaddleModel.GetComponent<PaddleAnimation>().AnimateDisable());
			Destroy (currentPaddleModel);		
		}

		currentPaddleModel = (GameObject) Instantiate(newModel);
		currentPaddleModel.transform.position = transform.position;
		currentPaddleModel.transform.parent = transform;
		
		UpdateMovementBounds();
	
		yield return StartCoroutine(currentPaddleModel.GetComponent<PaddleAnimation>().AnimateEnable());
	}
	
	//resets the paddle model to normal one
	public void ResetPaddleModel() {
		SetPaddleModel (paddleModelDefault);
	}
	
	public bool IsOccupied() {
		return sphereAttached;
	}
#endregion
	
	// This method is called each time paddle model has changed (e.g. width powerup) so that bounds are accodingly updated
	void UpdateMovementBounds() {
		// Getting bounds from bounds GameObject
		float width = movementBounds.transform.localScale.x;
		leftBound  = movementBounds.transform.position.x - width/2;
		rightBound = leftBound + width;
		
		
		//get overall collider of the model
		var totalBounds = currentPaddleModel.GetComponentInChildren<Collider>().bounds;
		var colliders = currentPaddleModel.GetComponentsInChildren<Collider>();
		
		foreach (Collider col in colliders) {
			totalBounds.Encapsulate(col.bounds);
		}
		
		// Shifting the bounds so that the half of paddle won't pop out on the borders
		float paddleWidth = totalBounds.size.x;
		
		leftBound  += paddleWidth/2;
		rightBound -= paddleWidth/2;
	}
	
	// This method puts the main sphere in the launching position and attaches it to the paddle
	public void AttachSphere() {
		sphereAttached = true;
		
		mainSphere.transform.position = stuckTransform.position;
	    mainSphere.transform.parent = tr;
		
		// Disable sphere movement
		mainSphere.Freeze();
	}
	
	// Unattach the sphere from the pad, launch it and play a sound
	void LaunchSphere() {
		if(sphereAttached) {
			sphereAttached = false;
			
			mainSphere.transform.parent = null;
	    	mainSphere.Launch(Vector2.up);
	
			audio.PlayOneShot(shootsound);
		}
	}
	
}
