using UnityEngine;
using System.Collections;

public class InGameMenuGUI : MonoBehaviour {
	
	
	public string GameName = "";
	public string GameLevel = "";
	
	GameManager gameManager;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.gameState = GameManager.GameState.Pregame;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	
	
	void OnGUI() {
		
		float screenUnitW = Screen.width/100;
		float screenUnitH = Screen.height/100;
		string message = "";
		
		
	
		// While the game is in progress, only display the pause button
		if ((gameManager.gameState == GameManager.GameState.Running)||(gameManager.gameState == GameManager.GameState.Pregame)) {
			if (GUI.Button(new Rect(Screen.width - screenUnitW*10, 0, screenUnitW*10, screenUnitH*5), "PAUSE")) {
				gameManager.PauseGame();
			}
		} else {
			switch (gameManager.gameState) {
				case GameManager.GameState.Paused: message = "GAME PAUSED"; break;
				case GameManager.GameState.Victory: message = "VICTORY"; break;
				case GameManager.GameState.Defeat: message = "DEFEAT"; break;
			}
		}
			GUI.Box(new Rect(0, 0, Screen.width, Screen.height), message);
			
			if (GUI.Button(new Rect(0, Screen.height - (Screen.height/3), Screen.width/3, (Screen.height/3)), "Main Menu")) {
				Application.LoadLevel("MainMenu");
			}
				if (GUI.Button(new Rect(Screen.width/3, Screen.height - (Screen.height/3), Screen.width/3, (Screen.height/3)), "RESTART")) {
				GameName = GameName + "GameLevel" + GameLevel;
				Application.LoadLevel(GameName);
			}
			
			if (GUI.Button(new Rect(Screen.width/3, Screen.height - (Screen.height/3), Screen.width/3, (Screen.height/3)), "RESTART")) {
				gameManager.RestartGame();
			}
			
			if (gameManager.gameState == GameManager.GameState.Defeat) GUI.enabled = false; // Resume button is grayed out on the loss screen
			if (GUI.Button (new Rect(Screen.width-(Screen.width/3), Screen.height - (Screen.height/3), Screen.width/3, Screen.height/3), "CONTINUE")) {
				if (gameManager.gameState == GameManager.GameState.Paused) gameManager.ResumeGame();
				if (gameManager.gameState == GameManager.GameState.Victory) gameManager.GoToNextLevel();
			}
			GUI.enabled = true;
				
	}
}

