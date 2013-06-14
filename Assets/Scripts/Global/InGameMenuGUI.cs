using UnityEngine;
using System.Collections;

public class InGameMenuGUI : MonoBehaviour {
	
	GameManager gameManager;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	
	
	void OnGUI() {
		
		float screenUnitW = Screen.width/100;
		float screenUnitH = Screen.height/100;
		string message = "";
	
		// While the game is in progress, only display the pause button
		if ((gameManager.GetGameState() == GameManager.GameState.Running)||(gameManager.GetGameState() == GameManager.GameState.Pregame)) {
			if (GUI.Button(new Rect(Screen.width - screenUnitW*10, 0, screenUnitW*10, screenUnitH*5), "PAUSE")) {
				gameManager.PauseGame();
			}
		} else {
			switch (gameManager.GetGameState()) {
			case GameManager.GameState.Paused: message = "GAME PAUSED"; break;
			case GameManager.GameState.Over:
					switch (gameManager.GetMedal()) {
					case GameManager.Medal.None: message = "YOU LOST"; break;
					case GameManager.Medal.Bronze: message = "Bronze medal. Stage clear."; break;
					case GameManager.Medal.Silver: message = "Silver medal. Good job."; break;
					case GameManager.Medal.Gold: message = "Gold medal. Garts."; break;
					}	
				break;
			}
		
			GUI.Box(new Rect(0, 0, Screen.width, Screen.height), message);
			
			if (GUI.Button(new Rect(0, Screen.height - (Screen.height/3), Screen.width/3, (Screen.height/3)), "Main Menu")) {
				Time.timeScale = 1; //releases timeScale back if it was pauses
				Application.LoadLevel("MainMenu");
			}
			
			if (GUI.Button(new Rect(Screen.width/3, Screen.height - (Screen.height/3), Screen.width/3, (Screen.height/3)), "RESTART")) {
				gameManager.RestartGame();
			}
			
			if (
				(gameManager.GetGameState() == GameManager.GameState.Over) &&
				(gameManager.GetMedal() == GameManager.Medal.None)
				) GUI.enabled = false; // Resume button is grayed out on the loss screen
			
			if (GUI.Button (new Rect(Screen.width-(Screen.width/3), Screen.height - (Screen.height/3), Screen.width/3, Screen.height/3), "CONTINUE")) {
				if (gameManager.GetGameState()== GameManager.GameState.Paused) gameManager.UnpauseGame();
				if (
					(gameManager.GetGameState() == GameManager.GameState.Over) &&
					(gameManager.GetMedal() != GameManager.Medal.None)
					) gameManager.GoToNextLevel();
			}
			GUI.enabled = true;
			
		}
				
	}
}

