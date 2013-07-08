using UnityEngine;
using System.Collections;

public class InGameMenuGUI : MonoBehaviour {
	
	public Texture gameTitleTexture;
	
	GameManager gameManager;
	Texture Restart, PlayButton, MainMenuButton, PauseButton, medalTexture, pauseTexture;
	GUIStyle NoStyle = new GUIStyle();
	
	float Margin = Screen.width/45;
	float aspect = 0.0f;
	AudioClip medalMusic;
	string medalWon;
	int count = 0;
	bool isPrinting = false;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		PlayButton = (Texture)Resources.Load("MenuCommon/play_" + MainMenuGUI.selectedGameName);
		MainMenuButton = (Texture)Resources.Load("MenuCommon/home_" + MainMenuGUI.selectedGameName);
		Restart = (Texture)Resources.Load("PauseMenu/replay_" + MainMenuGUI.selectedGameName);
		PauseButton = (Texture)Resources.Load("PauseMenu/pause_" + MainMenuGUI.selectedGameName);
		pauseTexture = (Texture)Resources.Load("PauseMenu/pause_menu");
		medalMusic = (AudioClip)Resources.Load("Music/Medal/MedalScreen");
		aspect = (float)Screen.width / Screen.height;
		
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
				
				//GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), pauseTexture);
				gameManager.PauseGame();
				if (isPrinting)
				isPrinting = false;
				else
				isPrinting = true;
			}
			
		} else {
				Texture medal;
				switch (gameManager.GetGameState()) {
				case GameManager.GameState.Paused: message = "GAME PAUSED"; break;
				case GameManager.GameState.Over:
						switch (gameManager.GetMedal()) {
						case GameManager.Medal.None: message = "YOU LOST"; 
						medalWon = "loss";
						medal =DisplayMedal();
						GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), medal);
						break;
						case GameManager.Medal.Bronze: message = "Bronze medal. Stage clear.";
						medalWon = "bronze";
						medal =DisplayMedal();
						GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), medal);
						break;
						case GameManager.Medal.Silver: message = "Silver medal. Good job.";
						medalWon = "silver";
						medal =DisplayMedal();
						GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), medal);
						break;
						case GameManager.Medal.Gold: message = "Gold medal. Garts.";
						medalWon = "gold";
						medal =DisplayMedal();
						GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), medal);
						break;
						}	
					break;
			}
		
			GUI.Box(new Rect(0, 0, Screen.width, Screen.height), message);
			if (isPrinting)
				printTexture();

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
					
				}
				if ((gameManager.GetGameState() == GameManager.GameState.Over)){
					gameManager.GoToNextLevel();
				}

			}
	
			GUI.enabled = true;
		}
				
	}
	
	Texture DisplayMedal(){
		if(count < 1){
			medalTexture = (Texture)Resources.Load("MedalMenu/" + medalWon);
			count++;
			Camera.main.audio.Stop();
			Camera.main.audio.PlayOneShot(medalMusic, 0.5f);
			return medalTexture;
		}
		return medalTexture;
	}
	
	void printTexture(){
 
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), pauseTexture); 
	}
}

