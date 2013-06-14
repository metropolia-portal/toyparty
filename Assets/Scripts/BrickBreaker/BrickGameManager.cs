using UnityEngine;
using System.Collections;

public class BrickGameManager : GameManager 
{
	//public string NextLevelId = "BrickGameLevelOneTutorial";
	//public string MenuLevelId = "BrickGameMenu";
	public string CurrentLevelId = "BrickGameLevelOne";
	public int shinyBricksGoal = 3;
	
	public float timeToComplete = 30f; //time in which you have to complete the level
	
	public Paddle paddle;
	
	public Powerup[] powerups;
		
	//public Transform extraBallPrefab;
	public Transform pickupPrefab; // Prefabs that will be instantinated on the fly
	
	public int powerUpSpawnRangeMin = 1; // These values define how often powerups will spawn from destroyed bricks
	public int powerUpSpawnRangeMax = 2; // in this case, every 1-2 bricks will result in a pickup
	
	public int spheres = 3; // The number of spheres you can lose before you lose the game
	
	Powerup currentPowerup;
	
	//enum GameState { Running, Paused, Victory, Defeat };
	enum Medal {None, Silver, Gold, Diamond};
	
	int bricksLeft; // This variable stores the amount of bricks left on the field
	int bricksDestroyed = 0;
	int bricksToNextPickup;
	bool powerupActive = false;
	//bool gamePaused = false;
	//GameState gameState = GameState.Running;
	Medal medal = Medal.None;
	

	ScoreManager gameScore;
	InputManager gameInput;
	
	

	void Start() {	
		//paddle = GameObject.Find("Paddle").GetComponent<Paddle>();
		gameScore = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
		gameInput = GameObject.Find ("GameInput").GetComponent<InputManager>();
		
		SetGameState(GameState.Running);
		bricksToNextPickup = Random.Range(powerUpSpawnRangeMin, powerUpSpawnRangeMax);
		RecalculateBrickCount(); // Count the number of bricks at the start of the game
	}
	
	void Update() {
		
		if (powerupActive)
			currentPowerup.OnUpdate();
		
	
		if(GetRemainingTime() == 0 && gameState == GameState.Running)
			OnGameOver();
		
#if UNITY_EDITOR		
		EnableCheats();
#endif
	}

		
	float GetRemainingTime() {
		return 	Mathf.Max(0f, timeToComplete - Time.timeSinceLevelLoad);	
	}
		

	
	void EnableCheats() {
		if(Input.GetKeyUp(KeyCode.LeftControl)) {
			powerupActive = true;
			
			currentPowerup = GameObject.Find("LazerGunPowerup").GetComponent<Powerup>();
			currentPowerup.Activate();
		}
		
		if(Input.GetKeyUp(KeyCode.LeftAlt)) {
			powerupActive = true;
			
			currentPowerup = GameObject.Find("LifePowerup").GetComponent<Powerup>();
			currentPowerup.Activate();	
		}
		
		if(Input.GetKeyUp(KeyCode.LeftShift)) {
			powerupActive = true;
			
			currentPowerup = GameObject.Find("ExtraSpherePowerup").GetComponent<Powerup>();
			currentPowerup.Activate();	
		}
		
		if(Input.GetKeyUp(KeyCode.Tab)) {
			powerupActive = true;
			
			currentPowerup = GameObject.Find("PadResizePowerup").GetComponent<Powerup>();
			currentPowerup.Activate();	
		}
		
		if(Input.GetKeyUp(KeyCode.P)) {
			OnPickup();
		}
			
	}
	
	public void OnShinyBrickDestroyed(Vector3 position) {
		OnBrickDestroyed(position);
		shinyBricksGoal --;
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
		
		// Select a powerup at random
		currentPowerup = powerups[Random.Range (0, powerups.Length)];
	
		// Start the new powerup
		currentPowerup.Activate();
		
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
		//determinde the medal deserved, none for defeat
		if (bricksLeft <=0)
			medal = Medal.Diamond; // The game ends in victory when there are no bricks left	
		else if (shinyBricksGoal > 0) 
			medal = Medal.None;
		else if (bricksDestroyed * 4 > bricksLeft * 3) 
			medal = Medal.Gold;
		else
			medal = Medal.Silver;
		
		gameScore.AddFinalScore(spheres, bricksLeft, GetRemainingTime());
		
		if (medal != Medal.None) {
			gameState = GameState.Victory;
		} else {
			gameState = GameState.Defeat;
		}
		
		FreezeGameplay();
	}
	
	
	
	// Called from the main sphere when it collides with the KillZone object
	public void OnMainSphereLost() {
		spheres --;
		
		if (spheres<=0)
			OnGameOver();
		else
			paddle.AttachSphere(); // Simply reset the sphere position if we have a "spare"
	}
	
	//adds one more life (spheres), used by life powerup, DO NOT USE FOR CHEATING, DAMN IT!
	public void AddSphere() {
		spheres++;		
	}
	
	void PauseGame() {
		gameState = GameState.Paused;
		FreezeGameplay();
	}
	
	
	void ResumeGame() {
		gameState = GameState.Running;
		DefreezeGameplay();
	}	
		
	void FreezeGameplay() {
		Time.timeScale = 0;
		gameInput.DisableInput();
	}
	
	void DefreezeGameplay() {
		Time.timeScale = 1;
		gameInput.EnableInput();
	}
	
//	void RestartGame() {
//		ResumeGame();
//		Application.LoadLevel(CurrentLevelId);
//	}
	
//	public bool IsGameRunning() {
//		return gameState == GameState.Running;
//	}
	
//	void GoToNextLevel() {
//		ResumeGame();
//		Application.LoadLevel(NextLevelId);
//	}
	
//	void OnGUI() {
//		float screenUnitW = Screen.width/100;
//		float screenUnitH = Screen.height/100;
//		string message = "";
//		
//	
//		// While the game is in progress, only display the pause button and HUD
//		if (gameState == GameState.Running) {
//			if (GUI.Button(new Rect(Screen.width - screenUnitW*10, 0, screenUnitW*10, screenUnitH*5), "PAUSE")) {
//				PauseGame();
//			}
//			
//			//HUD
//			GUI.Label(new Rect(0, 0, screenUnitW*10, screenUnitH*5), "Lifes: " + spheres);
//			GUI.Label(new Rect(0, screenUnitH*6, screenUnitW*100, screenUnitH*5), "Time left: " + GetRemainingTime());
//			
//			GUI.Label(new Rect(0, screenUnitH*12 , screenUnitW*30, screenUnitH*5), "Score: " + gameScore.GetScore());
//			
//			if(gameScore.GetCurrentComboScore() > 0) GUI.Label(new Rect(0, screenUnitH*18 , screenUnitW*20, screenUnitH*5), "X" + gameScore.GetCurrentComboCount() + " Combo Score! " + gameScore.GetCurrentComboScore());
//			
//		} else {
//			switch (gameState) {
//				case GameState.Paused: message = "GAME PAUSED"; break;
//				case GameState.Victory: message = "VICTORY"; break;
//				case GameState.Defeat: message = "DEFEAT"; break;
//			}
//			GUI.Box(new Rect(0, 0, Screen.width, Screen.height), message + "! Medal: " + medal.ToString().ToUpper());
//			
//			if(gameState == GameState.Victory)
//				GUI.Label( new Rect(Screen.width/2, Screen.height/3, screenUnitW*20, screenUnitH*5), "TOTAL SCORE: " + gameScore.GetScore());
//			
//			if (GUI.Button(new Rect(0, Screen.height - (Screen.height/3), Screen.width/3, (Screen.height/3)), "MENU")) {
//				ResumeGame();
//				Application.LoadLevel(MenuLevelId);
//			}
//			
//			if (GUI.Button(new Rect(Screen.width/3, Screen.height - (Screen.height/3), Screen.width/3, (Screen.height/3)), "RESTART")) {
//				RestartGame();
//			}
//			
//			if (gameState == GameState.Defeat) GUI.enabled = false; // Resume button is grayed out on the loss screen
//			if (GUI.Button (new Rect(Screen.width-(Screen.width/3), Screen.height - (Screen.height/3), Screen.width/3, Screen.height/3), "CONTINUE")) {
//				if (gameState == GameState.Paused) ResumeGame();
//				if (gameState == GameState.Victory) GoToNextLevel();
//			}
//			GUI.enabled = true;
//		}
//	}
		
	
}
