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
				case GameManager.GameState.Victory: message = "VICTORY"; break;
				case GameManager.GameState.Defeat: message = "DEFEAT"; break;
			}
		
			GUI.Box(new Rect(0, 0, Screen.width, Screen.height), message);
			
			if (GUI.Button(new Rect(0, Screen.height - (Screen.height/3), Screen.width/3, (Screen.height/3)), "Main Menu")) {
				Application.LoadLevel("MainMenu");
			}
			
			if (GUI.Button(new Rect(Screen.width/3, Screen.height - (Screen.height/3), Screen.width/3, (Screen.height/3)), "RESTART")) {
				gameManager.RestartGame();
			}
			
			if (gameManager.GetGameState() == GameManager.GameState.Defeat) GUI.enabled = false; // Resume button is grayed out on the loss screen
			if (GUI.Button (new Rect(Screen.width-(Screen.width/3), Screen.height - (Screen.height/3), Screen.width/3, Screen.height/3), "CONTINUE")) {
				if (gameManager.GetGameState()== GameManager.GameState.Paused) gameManager.UnpauseGame();
				if (gameManager.GetGameState() == GameManager.GameState.Victory) gameManager.GoToNextLevel();
			}
			GUI.enabled = true;
			
		}
				
	}
}

