using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class InGameMenuGUI : MonoBehaviour {
	
	public Texture gameTitleTexture;
	
	GameManager gameManager;
	Texture Restart, PlayButton, MainMenuButton, PauseButton, medalTexture, pauseTexture;
	GUIStyle NoStyle = new GUIStyle();
	
	float Margin = Screen.width/45;
	float aspect = 0.0f;
	
	int count = 0;
	bool isPrinting = false;
	
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
		pauseTexture = (Texture)Resources.Load("TutorialMenu/Previews/" + MainMenuGUI.selectedGameName);
		audio.clip = (AudioClip)Resources.Load("Music/Medal/MedalScreen");
		audio.volume = 0;
		audio.loop = true;
		audio.playOnAwake = false;
		aspect = (float)Screen.width / Screen.height;
		audioSource = Camera.main.GetComponent<AudioSource>();
		
	}
	
	void OnGUI() {		
		float screenUnitW = Screen.width/100;
		
		// While the game is in progress, only display the pause button
		if ((gameManager.GetGameState() == GameManager.GameState.Running)||(gameManager.GetGameState() == GameManager.GameState.Pregame)) {
			if (GUI.Button(new Rect(Screen.width - screenUnitW*10, 0, (Screen.width/10), (Screen.width/10)), PauseButton, NoStyle)) {	
				gameManager.PauseGame();
				isPrinting = true;
			}
			
		} else {
				// define the medal and show the corresponding texture
				string medalWon = null;
				switch (gameManager.GetGameState()) {
					case GameManager.GameState.Paused: 
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
							Texture medal = DisplayMedal(GetMedal());
							GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), medal);
							ShowBottomMenu();
						}
					break;
			}
		
			if (isPrinting)
				printTexture();
		}			
	}
	
	string GetMedal(){
		string medalWon = null;
		switch (gameManager.GetMedal()) {
			case GameManager.Medal.None: 
				medalWon = "loss";
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
	
	Texture DisplayMedal(string medalWon){
		if(count < 1){
			medalTexture = (Texture)Resources.Load("MedalMenu/" + medalWon);
			count++;
			return medalTexture;
		}
		return medalTexture;
	}
	
	IEnumerator LoadMainMenu(AudioSource source){
		while(source.volume > 0){
			source.volume -= 0.02f;	
			yield return null;
		}
		Time.timeScale = 1.0f;
		Application.LoadLevel("MainMenu");
	}
	IEnumerator FadeInMusic(AudioSource source){
		while(source.volume < 1){
			source.volume += 0.02f;	
			yield return null;
		}
	}
	IEnumerator FadeOutMusic(AudioSource source){
		while(source.volume > 0){
			print (source+" "+source.volume);
			source.volume -= 0.02f;	
			yield return null;
		}
		showMedal = true;
	}
	IEnumerator WaitAndLoadNext(){
		yield return StartCoroutine(FadeOutMusic(audio));
		gameManager.GoToNextLevel();
	}
	void printTexture(){
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), pauseTexture); 
	}
	void ShowBottomMenu(){
		// Left button
		if (GUI.Button(new Rect(Margin, Screen.height - (Screen.width/6), Screen.width/7, Screen.width/7), MainMenuButton, NoStyle)) {
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
		if (GUI.Button(new Rect(Screen.width -(Screen.width/2 + Screen.width/14),Screen.height - (Screen.width/6), Screen.width/7, Screen.width/7), Restart, NoStyle)) {
			gameManager.RestartGame();
		}
		
		// Right button
		if (
			(gameManager.GetGameState() == GameManager.GameState.Over) &&
			(gameManager.GetMedal() == GameManager.Medal.None)
			) GUI.enabled = false; // Resume button is grayed out on the loss screen
		
		if (GUI.Button (new Rect(Screen.width -Screen.width/6, Screen.height - (Screen.width/6), Screen.width/7, Screen.width/7), PlayButton, NoStyle)) {
			if (gameManager.GetGameState()== GameManager.GameState.Paused){
				gameManager.UnpauseGame();
				isPrinting = false;
			}
			if ((gameManager.GetGameState() == GameManager.GameState.Over)){
				StartCoroutine(WaitAndLoadNext());
			}
		}
		GUI.enabled = true;
	}
}

