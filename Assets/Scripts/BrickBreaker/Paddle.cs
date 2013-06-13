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
	
#region Interface
	// Changes the paddle model to the clone of the given argument
	// This allows a Powerup to change the model of the paddle, e.g. to a wider one
	public void SetPaddleModel(GameObject newModel) {
		StartCoroutine(AnimateAndSetPaddleModel(newModel));
	}
	
	IEnumerator AnimateAndSetPaddleModel(GameObject newModel) {
		if(currentPaddleModel) {
			//print ("--- animating disable");
			//wait for disable animation
			yield return StartCoroutine(currentPaddleModel.GetComponent<PaddleAnimation>().AnimateDisable());
			//print ("---destroying");
			Destroy (currentPaddleModel);		
		}

		currentPaddleModel = (GameObject) Instantiate(newModel);
		currentPaddleModel.transform.position = transform.position;
		currentPaddleModel.transform.parent = transform;
		
		UpdateMovementBounds();
		
		//print ("--- animating enable");
		yield return StartCoroutine(currentPaddleModel.GetComponent<PaddleAnimation>().AnimateEnable());
		//print ("--- animating enable end");
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
		gameManager = GameObject.Find("GameManager").GetComponent<BrickGameManager>();
		//movementBounds = GameObject.Find("PaddleMovementBounds");
		//mainSphere = GameObject.Find("Sphere").GetComponent<MainSphere>();
		//stuckTransform = GameObject.Find ("StuckPosition").transform;
		inputManager = GameObject.Find("GameInput").GetComponent<InputManager>();
			
		ResetPaddleModel();
		AttachSphere();
	}
	
	
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
			//print (col);
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
	
			audio.PlayOneShot(shootsound);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!gameManager.IsGameRunning()) return;
	
		// Getting a projection ray from the mouse position on the screen, and saving it's first hit on the field
		RaycastHit hit;
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
