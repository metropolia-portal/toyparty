using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class InGameMenuGUI : MonoBehaviour {
	
	public Texture gameTitleTexture;
	GameManager gameManager;
	Texture Restart, PlayButton, MainMenuButton, PauseButton, podiumTexture, backgroundTexture, characterTexture, loseTexture, pauseTexture;
	
	int count = 0;
	
	AudioSource audioSource;
	bool callOnce = true;
	bool showMedal = false;
	// Use this for initialization
	void Start () {
		
		
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		PlayButton = (Texture)Resources.Load("MenuCommon/play_" + MainMenuGUI.selectedGameName);
		MainMenuButton = (Texture)Resources.Load("MenuCommon/home_" + MainMenuGUI.selectedGameName);
		Restart = (Texture)Resources.Load("PauseMenu/replay_" + MainMenuGUI.selectedGameName);
		PauseButton = (Texture)Resources.Load("PauseMenu/pause_" + MainMenuGUI.selectedGameName);
		pauseTexture = (Texture)Resources.Load("PauseMenu/" + MainMenuGUI.selectedGameName);
		podiumTexture = (Texture)Resources.Load("MedalMenu/podium");
		backgroundTexture = (Texture)Resources.Load("MedalMenu/backgrounds/" + MainMenuGUI.selectedGameName);
		characterTexture = (Texture)Resources.Load("MedalMenu/characters/" + MainMenuGUI.selectedGameName);
		loseTexture = (Texture)Resources.Load("MedalMenu/losescreen");
		
		audio.clip = (AudioClip)Resources.Load("Music/Medal/MedalScreen");
		audio.volume = 0;
		audio.loop = true;
		audio.playOnAwake = false;
		//aspect = (float)Screen.width / Screen.height;
		audioSource = Camera.main.GetComponent<AudioSource>();
	}
	
	void OnGUI() {		
		float screenUnitW = Screen.width/100;
		
		// While the game is in progress, only display the pause button
		if ((gameManager.GetGameState() == GameManager.GameState.Running)||(gameManager.GetGameState() == GameManager.GameState.Pregame)) {
			if (GUI.Button(new Rect(Screen.width - screenUnitW*10, 0, (Screen.width/10), (Screen.width/10)), PauseButton, MGUI.NoStyle)) {	
				gameManager.PauseGame();
				
			}
			
		} else {
				// define the medal and show the corresponding texture
				string medalWon = null;
				switch (gameManager.GetGameState()) {
					case GameManager.GameState.Paused: 
						GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), pauseTexture);
						ShowBottomMenu();
						break;
					case GameManager.GameState.Over:
						if(callOnce){
							StartCoroutine(FadeOutMusic(audioSource));
							audio.Play();
							StartCoroutine(FadeInMusic(audio));
							callOnce = false;
						}
						if(showMedal)
						{
							
							string medal = GetMedal();
							
							GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), backgroundTexture);
							if(medal == "lose"){
						
								GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), loseTexture);

								GUI.DrawTexture(new Rect(Screen.width/5 - Screen.width/18, Screen.height/2 -Screen.height/21, Screen.width/4, Screen.height/3), characterTexture);
			
							}
							else{
						
								GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), podiumTexture);
								
								if(medal == "gold"){
							
									GUI.DrawTexture(new Rect(Screen.width/2 - Screen.width/6 , Screen.height/4 -Screen.height/15, Screen.width/4, Screen.height/3), characterTexture);
								}
								else if(medal == "silver"){
							
									GUI.DrawTexture(new Rect(Screen.width/5 - Screen.width/6, Screen.height/2 -Screen.height/5, Screen.width/4, Screen.height/3), characterTexture);
								}
								else{
								
									GUI.DrawTexture(new Rect(Screen.width - Screen.width/3 + 15, Screen.height/2 -Screen.height/7, Screen.width/4, Screen.height/3), characterTexture);
								}
							}
							ShowBottomMenu();
							
						}
					break;
			}
		}			
	}
	
	string GetMedal(){
		string medalWon = null;
		switch (gameManager.GetMedal()) {
			case GameManager.Medal.None: 
				medalWon = "lose";
				break;
			case GameManager.Medal.Bronze: 
				medalWon = "bronze";
				break;
			case GameManager.Medal.Silver: 
				medalWon = "silver";
				break;
			case GameManager.Medal.Gold: 
				medalWon = "gold";
				break;
		}	
		return medalWon;
	}
	
	IEnumerator LoadMainMenu(AudioSource source){
		if (source != null)
		while(source.volume > 0){
			source.volume -= 0.02f;	
			yield return null;
		}
		Time.timeScale = 1.0f;
		Application.LoadLevel("MainMenu");
	}
	IEnumerator FadeInMusic(AudioSource source){
		if (source != null)
		while(source.volume < 1){
			source.volume += 0.02f;	
			yield return null;
		}
	}
	IEnumerator FadeOutMusic(AudioSource source){
		if (source != null)
		while(source.volume > 0){
			source.volume -= 0.02f;	
			yield return null;
		}
		showMedal = true;
	}
	IEnumerator WaitAndLoadNext(){
		yield return StartCoroutine(FadeOutMusic(audio));
		gameManager.GoToNextLevel();
	}
	
	void ShowBottomMenu(){
		// Left button
		if (MGUI.HoveredButton(new Rect(MGUI.Margin, Screen.height - (Screen.width/6), Screen.width/7, Screen.width/7), MainMenuButton)) {
			switch(gameManager.GetGameState()){
				case GameManager.GameState.Paused: 
					StartCoroutine(LoadMainMenu(audioSource));
					break;
				case GameManager.GameState.Over:
					StartCoroutine(LoadMainMenu(audio));
					break;
			}
		}
		
		// Middle button
		if (MGUI.HoveredButton(new Rect(Screen.width -(Screen.width/2 + Screen.width/14),Screen.height - (Screen.width/6), Screen.width/7, Screen.width/7), Restart)) {
			gameManager.RestartGame();
		}
		
		// Right button
		if (
			(gameManager.GetGameState() == GameManager.GameState.Over) &&
			(gameManager.GetMedal() == GameManager.Medal.None)
			) GUI.enabled = false; // Resume button is grayed out on the loss screen
		
		if (MGUI.HoveredButton(new Rect(Screen.width -Screen.width/6, Screen.height - (Screen.width/6), Screen.width/7, Screen.width/7), PlayButton)) {
			if (gameManager.GetGameState()== GameManager.GameState.Paused){
				gameManager.UnpauseGame();
				
			}
			if ((gameManager.GetGameState() == GameManager.GameState.Over)){
				StartCoroutine(WaitAndLoadNext());
			}
		}
		GUI.enabled = true;
	}
}

