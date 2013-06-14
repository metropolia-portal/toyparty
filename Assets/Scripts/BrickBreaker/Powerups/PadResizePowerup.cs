using UnityEngine;
using System.Collections;

public class PadResizePowerup : Powerup {
	
	public GameObject paddleModelLarge;
	public GameObject paddleModelSmall;	
	public Paddle paddle;
	
	float padResizeTime = 5f;
		
	float timeLeft;
	
	// OnStart event called from the BrickGameManager object when the powerup is activated
	// For this powerup, the pad is going to be replaced by either the larger or the smaller version (at random) for 5 seconds
	override protected void Start() {
		base.Start ();
		
		//paddle = GameObject.Find ("Paddle").GetComponent<Paddle>(); // Load resources and establish a link to the paddle
		//paddleModelLarge = (GameObject)Resources.Load("PaddleModelLarge");
		//paddleModelSmall = (GameObject)Resources.Load("PaddleModelSmall");		
	}
		
	override public void Activate () {	
		base.Activate();
		
		if (Random.Range(0,3) == 1)
			paddle.SetPaddleModel(paddleModelLarge); // Replace the normal pad with the enlarged paddle
		else
			paddle.SetPaddleModel(paddleModelSmall); // Replace the normal pad with the small paddle
		
		timeLeft = padResizeTime;
		
	}
	
	// OnUpdate event called once per frame
	override public void OnUpdate () {
		// Decrease timeLeft by the amount of time that has passed since last frame
		timeLeft -= Time.deltaTime; 
		if (timeLeft <= 0) {
			Deactivate ();
		}
		
	}
	
	// OnEnd event called (normally from the OnUpdate event) when the powerup is over
	override public void Deactivate() {
		paddle.ResetPaddleModel(); // Revert the paddle changes
		base.Deactivate();
	}
	

	
	
}
