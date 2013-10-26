using UnityEngine;
using System.Collections;

public class BrickGameManager : GameManager 
{

//	int maxScore = 3000;
//	int goldScore = 2000;
//	int silverScore = 1000;
//	int bronzeScore = 500;
	
	public float timeToComplete = 30f; //time in which you have to complete the level
	
	public float gameFinishedDelay = 1f;
	public float gameLooseNoSpheresDelay = 0.5f;
	public float sphereExplosionTime = 0.5f;
	//TODO synchronize with other time game, put in super class
	
	public Paddle paddle;
	//public Sphere mainSphere;
	
	public Powerup[] powerups;
		
	//public Transform extraBallPrefab;
	public Transform pickupPrefab; // Prefabs that will be instantinated on the fly
	
	public int powerUpSpawnRangeMin = 1; // These values define how often powerups will spawn from destroyed bricks
	public int powerUpSpawnRangeMax = 2; // in this case, every 1-2 bricks will result in a pickup
	
	public Rect pauseButtonRelativePosRect = new Rect(0.9f, 0, 0.1f, 0.1f); //X and Y are fraction of screen width
	
	
	int maxSpheres = 3;
	int spheres = 3; // The number of spheres you can lose before you lose the game
	
	float gameStartDelay = 0.1f; //a delay before actual game start, after reading the tutorial, so that the paddle will not be affected by the user clicking buttons
	
	Powerup currentPowerup;

	
	int bricksLeft; // This variable stores the amount of bricks left on the field
	int bricksDestroyed = 0;
	int bricksToNextPickup;
	bool powerupActive = false;
	
	bool firstSphereLaunched = false; //true when at least one sphere was launched
	float firstLaunchTime = 0;
	bool gameOver = false;
	//bool gamePaused = false;
	//GameState gameState = GameState.Running;
	//Medal medal = Medal.None;
	
	ScoreManager gameScore;
	ScoreGUI scoreGUI;
	InputManager gameInput;
	
	

	public override void Start() 
	{	
		base.Start ();//do some stuff common to all games
		
		gameScore = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
		
		gameInput = GameObject.Find ("GameInput").GetComponent<InputManager>();
		gameInput.IgnoreButtonEventsAt(new Rect(pauseButtonRelativePosRect.x * Screen.width , pauseButtonRelativePosRect.y * Screen.width, pauseButtonRelativePosRect.width *  Screen.width , pauseButtonRelativePosRect.height * Screen.width));
		
		
		scoreGUI = GetComponent<ScoreGUI>();
		scoreGUI.setMaxTimer((int)timeToComplete);
		scoreGUI.SetMedalRequirements(bronzeMedalScore, silverMedalScore, goldMedalScore);
		
		bricksToNextPickup = Random.Range(powerUpSpawnRangeMin, powerUpSpawnRangeMax);
		RecalculateBrickCount(); // Count the number of bricks at the start of the game
		
		Invoke("StartGame", gameStartDelay);
	}
	
	void StartGame() {
		SetGameState(GameState.Running);	
	}
	
	
	void Update() {
		if(!IsGameRunning()) return;
		
		scoreGUI.setTimer(Mathf.Floor(GetRemainingTime()));
		
		if (powerupActive)
			currentPowerup.OnUpdate();
		
		if(GetRemainingTime() == 0 && gameState == GameState.Running)
			OnGameOver();
		
		if(!firstSphereLaunched && !paddle.IsOccupied()) {
			firstLaunchTime = Time.timeSinceLevelLoad;
			firstSphereLaunched = true;
		}
			
#if UNITY_EDITOR		
		EnableCheats();
#endif
	}

		
	float GetRemainingTime() {
		if(firstSphereLaunched)
			return 	Mathf.Max(0f, timeToComplete - Time.timeSinceLevelLoad + firstLaunchTime);	
		else 
			return timeToComplete;
	}
		
	void EnableCheats() {
		if(Input.GetKeyUp(KeyCode.LeftControl) || Input.touchCount == 3) {
			powerupActive = true;
			
			currentPowerup = GameObject.Find("LazerGunPowerup").GetComponent<Powerup>();
			currentPowerup.Activate();
		}
		
		if(Input.GetKeyUp(KeyCode.LeftAlt)) {
			powerupActive = true;
			
			currentPowerup = GameObject.Find("LifePowerup").GetComponent<Powerup>();
			currentPowerup.Activate();	
		}
		
		if(Input.GetKeyUp(KeyCode.LeftShift) || Input.touchCount == 5) {
			powerupActive = true;
			
			currentPowerup = GameObject.Find("ExtraSpherePowerup").GetComponent<Powerup>();
			currentPowerup.Activate();	
		}
		
		if(Input.GetKeyUp(KeyCode.Tab) || Input.touchCount == 4) {
			powerupActive = true;
			
			currentPowerup = GameObject.Find("PadResizePowerup").GetComponent<Powerup>();
			currentPowerup.Activate();	
		}
		
		if(Input.GetKeyUp(KeyCode.E))
			OnGameOver();
		
		if(Input.GetKeyUp(KeyCode.P)) {
			OnPickup();
		}	
	}
	
	public void OnShinyBrickDestroyed(Vector3 position) {
		OnBrickDestroyed(position);
	}
		
	public void OnBrickDestroyed(Vector3 position) {	
		bricksDestroyed ++;
		bricksLeft --;
#if UNITY_EDITOR
		if( GameObject.FindGameObjectsWithTag("Brick").Length - 1 != bricksLeft)
			Debug.LogError("bricksLeft is out of sync! bricksLeft=" + bricksLeft + ", real amount = " + (GameObject.FindGameObjectsWithTag("Brick").Length -1));
#endif		
		
		
		if (bricksLeft <=0)
			OnGameOver();
			
		if (!powerupActive) { // No additional pickups are generated while a powerup is active
			bricksToNextPickup --;
			if(bricksToNextPickup == 0)
			{		
				// Sometimes the brick might be mid-air when it breaks. We want the pickup to be generated at a fixed altitude so that it properly hits the pad
				position = new Vector3(position.x, position.y + pickupPrefab.localScale.y, position.z); 
				Instantiate(pickupPrefab, position , Quaternion.identity);
				bricksToNextPickup = Random.Range(powerUpSpawnRangeMin, powerUpSpawnRangeMax);
			}
		}
		
	}
	

	// This is called when a pickup is acquired by the pad
	public void OnPickup() {
		powerupActive = true;
		
		DestroyAllPickups();

		// Select a powerup at random
		currentPowerup = powerups[Random.Range (0, powerups.Length)];
	
		// Start the new powerup
		currentPowerup.Activate();
		
	}
	
	void DestroyAllPickups() {
		// All pickups are destroyed to prevent having muliple powerups active at the same time
		foreach ( GameObject pickup in GameObject.FindGameObjectsWithTag("Pickup")) {
			Destroy (pickup);
		}
	}
		
	
	// This method counts every object with the "Brick" tag
	public void RecalculateBrickCount() {
		bricksLeft = GameObject.FindGameObjectsWithTag("Brick").Length;
	}	
	
	// Called from the powerup when it ends
	public void OnPowerupEnd() {
		currentPowerup = null;
		powerupActive = false;
	}
	
	void OnGameOver() {
		//a safeguard to protect calling twice
		if(gameOver) return;
		gameOver = true;
		//finilazing all scores, combos
		gameScore.OnBeforeGameFinished();
		scoreGUI.timerEnabled = false;
		
		int score = gameScore.GetScore();
		//determinde the medal deserved, none for defeat
		if (score >= goldMedalScore && spheres == 3) // we loose if any shiny bricks left or no spheres left
			medal = Medal.Gold;
		else if (score >= silverMedalScore && spheres >= 2)
			medal = Medal.Silver; // The game ends in victory when there are no bricks left	
		else if (score >= bronzeMedalScore && spheres >= 1) 
			medal = Medal.Silver;
		else
			medal = Medal.None;
		
		SetMedal(medal);
		
		//stop all spheres exisiting
		foreach(GameObject sphere in GameObject.FindGameObjectsWithTag("Sphere"))
			sphere.GetComponent<Sphere>().Launch(Vector2.zero);
		
		//do effects unless game is lost by loosing all spheres
		if(spheres > 0) {
			Invoke("DestroyAllPickups", 0.01f); //wait for the last pickup to be generated, then destroy all
			
			PlaySpheresFinishEffect();
					
			Invoke("EndGame", gameFinishedDelay);
		} else
			Invoke("EndGame", gameLooseNoSpheresDelay);
	}
	
	void PlaySpheresFinishEffect() {
			
		Invoke ("ExplodeSpheres", gameFinishedDelay - sphereExplosionTime );
	}
	
	void ExplodeSpheres() {
		foreach(GameObject sphere in GameObject.FindGameObjectsWithTag("Sphere"))
			sphere.GetComponent<Sphere>().Explode();
	}
	
	
	
	// Called from the main sphere when it collides with the KillZone object
	public void OnMainSphereLost() {
		spheres --;
		
		if (spheres<=0)
			OnGameOver();
		else
			paddle.AttachSphere(); // Simply reset the sphere position if we have a "spare"
		
		scoreGUI.SetMaxMedals(spheres);
	}
	
	//adds one more life (spheres), used by life powerup, DO NOT USE FOR CHEATING, DAMN IT!
	public void AddSphere() {
		if(spheres < maxSpheres) spheres++;
		scoreGUI.SetMaxMedals(spheres);
	}	
}
