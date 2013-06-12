using UnityEngine;
using System.Collections;

public class LazerGunPowerup : Powerup {
	public float lazerOnTime = 0.3f;
	
	GameObject padModelGun;
	GameObject lazerModel;
	Paddle paddle;
	
	GameObject lazer = null; // Keeping the link to laser prefub instance to know when it's off
	
	int chargesLeft;
	
	// OnStart event called from the GameManager object when the powerup is activated
	// This powerup will replace the paddle model with its "lazer-equipped" version
	override public void OnStart () {
		base.OnStart();
		
		paddle = GameObject.Find ("Paddle").GetComponent<Paddle>(); 
		padModelGun = (GameObject)Resources.Load("PaddleModelGun");
		lazerModel = (GameObject)Resources.Load("Lazer");
		
		paddle.SetPaddleModel(padModelGun);
		chargesLeft = 5;
	}
	
	// OnUpdate event called once per frame
	override public void OnUpdate () {
		if ( Input.GetMouseButtonDown(1) && chargesLeft > 0 && !paddle.IsOccupied() ) { // shoot only when paddle is free as we are using same input for shooting and launching the sphere
			Lazer ();
			chargesLeft --;						
		}
		
		if (chargesLeft <= 0 && !lazer)  // The powerup's effects end after shooting 5 times, wait for lazer to dissapear
			OnEnd ();	
	}
	
	// OnEnd event called (normally from the OnUpdate event) when the powerup is over
	override public void OnEnd() {
		paddle.ResetPaddleModel();
		base.OnEnd();
	}
	
	// Lazer method that draws the lazer and checks for bricks
	void Lazer() {
		// A ray is cast forward from the paddle (Spheres use the IgnoreRaycast layer and will not interfere with this)
		RaycastHit hit;
		if (Physics.Raycast(paddle.transform.position, new Vector3(0,0,1), out hit)) { 		
			lazer = (GameObject) GameObject.Instantiate(lazerModel, paddle.transform.position, paddle.transform.rotation);
			GameObject.Destroy(lazer, lazerOnTime); // Create the lazer model and destroy it after 0.3 seconds
			
			GameObject lazerTarget = hit.collider.gameObject;
			if (lazerTarget.CompareTag("Brick")) { // If it hits a brick, the brick is destroyed
				
				//yield return new WaitForSeconds (lazerOnTime);
				lazerTarget.GetComponent<Brick>().OnHit();
			}
		}	
	}
}
