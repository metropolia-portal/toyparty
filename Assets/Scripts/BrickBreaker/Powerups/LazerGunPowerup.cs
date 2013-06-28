using UnityEngine;
using System.Collections;

public class LazerGunPowerup : Powerup {
	public float lazerOnTime = 0.3f;
	
	public GameObject padModelGun;
	
	public GameObject lazerModel;
	public Vector3 lazerShotOffset;
	
	public Paddle paddle;
	
	InputManager gameInput;
	
	GameObject lazer = null; // Keeping the link to laser prefub instance to know when it's off
	
	int initialChargerLeft = 5;
	
	int chargesLeft;
	

	override protected void Start() {
		base.Start ();
		
		//paddle = GameObject.Find ("Paddle").GetComponent<Paddle>(); 
		//TODO switch from resources
		//padModelGun = (GameObject)Resources.Load("PaddleModelGun");
		//lazerModel = (GameObject)Resources.Load("Lazer");
		
		gameInput = GameObject.Find ("GameInput").GetComponent<InputManager>();
	}
	
	// Activate event called from the BrickGameManager object when the powerup is activated
	// This powerup will replace the paddle model with its "lazer-equipped" version
	override public void Activate () {
		base.Activate();
		
		chargesLeft = initialChargerLeft;
		
		paddle.SetPaddleModel(padModelGun);
	}
	
	// OnUpdate event called once per frame
	override public void OnUpdate () {	
		if(!paddle.IsOccupied()) {
			//print ( gameInput.IsSecondButtonDown());
			//print ( chargesLeft);
			if ( gameInput.IsSecondButtonDown() && chargesLeft > 0 ) { // shoot only when paddle is free as we are using same input for shooting and launching the sphere
				Lazer ();
				chargesLeft --;						
			}
		}
		
		if (chargesLeft <= 0 && !lazer)  // The powerup's effects end after shooting 5 times, wait for lazer to dissapear
			Deactivate ();	
	}
	
	// Deactivate event called (normally from the OnUpdate event) when the powerup is over
	override public void Deactivate() {
		paddle.ResetPaddleModel();
		base.Deactivate();
	}
	
	// Lazer method that draws the lazer and checks for bricks
	void Lazer() {		
		// A ray is cast forward from the paddle (Spheres use the IgnoreRaycast layer and will not interfere with this)
		RaycastHit hit;
		Vector3 lazerSourcePosition = paddle.transform.position + lazerShotOffset;
		if (Physics.Raycast(lazerSourcePosition, new Vector3(0,0,1), out hit)) { 		
			lazer = (GameObject) GameObject.Instantiate(lazerModel, lazerSourcePosition, Quaternion.identity);
			GameObject.Destroy(lazer, lazerOnTime); // Create the lazer model and destroy it after 0.3 seconds
			
			GameObject lazerTarget = hit.collider.gameObject;
			if (lazerTarget.CompareTag("Brick")) { // If it hits a brick, the brick is destroyed
				lazerTarget.GetComponent<Brick>().OnHit();
				lazerModel.GetComponent<Lazer>().OnBrickDestroy(); //notify lazer that it hits brick so it can play sound and other effects
			}
		}	
	}
}
