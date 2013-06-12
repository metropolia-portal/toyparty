using UnityEngine;
using System.Collections;

public class BrickGameManager : GameManager {
	
	public Transform extraBallPrefab; // Prefabs that will be instantinated on the fly
	public Transform pickupPrefab;
	
	public int powerUpSpawnRangeMin = 1; // These values define how often powerups will spawn from destroyed bricks
	public int powerUpSpawnRangeMax = 2; // in this case, every 1-2 bricks will result in a pickup
	
	public int spheres = 3; // The number of spheres you can lose before you lose the game
	
	int brickCount; // This variable stores the amount of bricks left on the field
	int bricksToNextPickup;
	bool powerupActive = false;
	//bool gamePaused = false;

	Powerup currentPowerup;
	Paddle paddle;

	void Start() {	
		paddle = GameObject.Find("Paddle").GetComponent<Paddle>();
		bricksToNextPickup = Random.Range(powerUpSpawnRangeMin, powerUpSpawnRangeMax);
		RecalculateBrickCount(); // Count the number of bricks at the start of the game
	}
	
	void Update() {
		if (gameState == GameManager.GameState.Pregame) {
			gameState = GameManager.GameState.Running;
		}
			
		if (powerupActive)
			currentPowerup.OnUpdate();
		
		EnableCheats();
	}
	
	void EnableCheats() {
		if(Input.GetKeyUp(KeyCode.LeftControl)) {
			powerupActive = true;
		
			// All pickups are destroyed to prevent having muliple powerups active at the same time
			foreach ( GameObject pickup in GameObject.FindGameObjectsWithTag("Pickup")) {
				Destroy (pickup);
			}
			
			currentPowerup = new LazerGunPowerup();
			currentPowerup.OnStart();
		}
			
	}
	
	public void OnBrickDestroyed(Vector3 position) {	

		brickCount --;
		if (brickCount <=0) {
			OnGameOver(true); // The game ends in victory when there are no bricks left
		}
		
		if (!powerupActive) { // No additional pickups are generated while a powerup is active
			bricksToNextPickup --;
			if(bricksToNextPickup == 0)
			{		
				// Sometimes the brick might be mid-air when it breaks. We want the pickup to be generated at a fixed altitude so that it properly hits the pad
				position = new Vector3(position.x, 0, position.z); 
				Instantiate(pickupPrefab, position , Quaternion.identity);
				bricksToNextPickup = Random.Range(powerUpSpawnRangeMin, powerUpSpawnRangeMax);
			}
		}
		
	}
	

	// This is called when a pickup is acquired by the pad
	public void OnPickup() {
		powerupActive = true;
		
		// All pickups are destroyed to prevent having muliple powerups active at the same time
		foreach ( GameObject pickup in GameObject.FindGameObjectsWithTag("Pickup")) {
			Destroy (pickup);
		}
		
		// Generate a powerup at random
		switch( Random.Range (0,2) )
		{			
            case 0: 
            	currentPowerup = new PadResizePowerup();
            break;
			
            case 1:
            	currentPowerup = new LazerGunPowerup();
            break;
			
			case 2:
				currentPowerup = new ExtraSpherePowerup();
            break;  
        }
		
		// Start the new powerup
		currentPowerup.OnStart();
		
	}
	
	// This method counts every object with the "Brick" tag
	public void RecalculateBrickCount() {
		brickCount = GameObject.FindGameObjectsWithTag("Brick").Length;
	}
	
	// Called from the powerup when it ends
	public void OnPowerupEnd() {
		currentPowerup = null;
		powerupActive = false;
	}
	
	// Called from the main sphere when it collides with the KillZone object
	public void OnMainSphereLost() {
		spheres --;
		if (spheres<=0) {
			OnGameOver(false);
		}else{
			paddle.AttachSphere(); // Simply reset the sphere position if we have a "spare"
		}
	}
	
}
