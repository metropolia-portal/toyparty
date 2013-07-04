using UnityEngine;
using System.Collections;

public class InGameMenuGUI : MonoBehaviour {
	
	GameManager gameManager;
	Texture Restart, PlayButton, MainMenuButton, PauseButton, PauseMenu;
	GUIStyle NoStyle = new GUIStyle();
	public Texture gameTitleTexture;
	float Margin = Screen.width/45;
	float aspect = 0.0f;
	GameObject pauseBackground;
	GameObject medalScreen;
	string medalWon;
	int count = 0;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		PlayButton = (Texture)Resources.Load("MenuCommon/play_" + MainMenuGUI.selectedGameName);
		MainMenuButton = (Texture)Resources.Load("MenuCommon/home_" + MainMenuGUI.selectedGameName);
		Restart = (Texture)Resources.Load("PauseMenu/replay_" + MainMenuGUI.selectedGameName);
		PauseButton = (Texture)Resources.Load("PauseMenu/pause_" + MainMenuGUI.selectedGameName);
		pauseBackground = (GameObject)Resources.Load("PauseMenu/pause_background");
		
		aspect = (float)Screen.width / Screen.height;
		pauseBackground =	((GameObject)Instantiate (pauseBackground));
		pauseBackground.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	void OnGUI() {		
		float screenUnitW = Screen.width/100;
		
		string message = "";
	
		// While the game is in progress, only display the pause button
		if ((gameManager.GetGameState() == GameManager.GameState.Running)||(gameManager.GetGameState() == GameManager.GameState.Pregame)) {
			if (GUI.Button(new Rect(Screen.width - screenUnitW*10, 0, (Screen.width/10), (Screen.width/10)), PauseButton, NoStyle)) {
				gameManager.PauseGame();
				DisplayPause();
			}
		} else {
			switch (gameManager.GetGameState()) {
			case GameManager.GameState.Paused: message = "GAME PAUSED"; break;
			case GameManager.GameState.Over:
					switch (gameManager.GetMedal()) {
					case GameManager.Medal.None: message = "YOU LOST"; 
					medalWon = "none";
					break;
					case GameManager.Medal.Bronze: message = "Bronze medal. Stage clear.";
					medalWon = "bronze";
					DisplayMedal();
					break;
					case GameManager.Medal.Silver: message = "Silver medal. Good job.";
					medalWon = "silver";
					DisplayMedal();
					break;
					case GameManager.Medal.Gold: message = "Gold medal. Garts.";
					medalWon = "gold";
					DisplayMedal();
					break;
					}	
				break;
			}
		
			GUI.Box(new Rect(0, 0, Screen.width, Screen.height), message);
			

			if (GUI.Button(new Rect(16, Screen.height - (Screen.width/8), Screen.width/9, Screen.width/9), MainMenuButton, NoStyle)) {
				Time.timeScale = 1; //releases timeScale back if it was pauses
				Application.LoadLevel("MainMenu");
			}
			
			if (GUI.Button(new Rect(Screen.width -(Screen.width/2 + Screen.width/18),Screen.height - (Screen.width/8), Screen.width/9, Screen.width/9), Restart, NoStyle)) {
				gameManager.RestartGame();
			}
			
			if (
				(gameManager.GetGameState() == GameManager.GameState.Over) &&
				(gameManager.GetMedal() == GameManager.Medal.None)
				) GUI.enabled = false; // Resume button is grayed out on the loss screen
			
			if (GUI.Button (new Rect(Screen.width -Screen.width/6,Screen.height - (Screen.width/8), Screen.width/9, Screen.width/9), PlayButton, NoStyle)) {
				if (gameManager.GetGameState()== GameManager.GameState.Paused){
					
					gameManager.UnpauseGame();
					pauseBackground.SetActive(false);
				}
				if ((gameManager.GetGameState() == GameManager.GameState.Over)){
					medalScreen.SetActive(false);
					gameManager.GoToNextLevel();
				}

			}
			
			
			GUI.enabled = true;
			
			
		}
				
	}
	
	void DisplayPause(){
		
		if(aspect >= 1.24f && aspect <= 1.25f){ // 5/4 aspect ratio
			
			pauseBackground.SetActive(true);
		}
		else if(aspect >= 1.33f && aspect <= 1.34f){ // 4/3 aspect ratio
				
			pauseBackground.SetActive(true);
		}
		else if(aspect >= 1.49f && aspect <= 1.50f){ // 3/2 aspect ratio
		
			pauseBackground.SetActive(true);	
		}
		else{// all other ratios
			
			pauseBackground.SetActive(true);	
		}
	}
	void DisplayMedal(){
		if(count < 1){
			medalScreen = (GameObject)Resources.Load("MedalMenu/" + medalWon + "_screen");
			medalScreen =	((GameObject)Instantiate (medalScreen));
			medalScreen.SetActive(true);
			count++;
		}
	}
}

