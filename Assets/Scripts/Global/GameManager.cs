using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public bool isLastLevel = true;
	
	public enum GameState { Pregame, Running, Paused, Over };
	public enum Medal { None, Bronze, Silver, Gold };
	
	public static Medal medal = Medal.None;
	public static GameState gameState;
	public static GameState prevGameState;
	
	
	// Use this for initialization
	public void Start () {
		Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void SetMedal(Medal m) {
		GameManager.medal = m;
	}
	
	public Medal GetMedal() {
		return GameManager.medal;
	}
	
	public void SetGameState(GameState s) {
		GameManager.gameState = s;
	}
	
	public GameState GetGameState() {
		return GameManager.gameState;
	}
	
	public void PauseGame() {
		GameManager.prevGameState = GetGameState();
		GameManager.gameState = GameState.Paused;
		Time.timeScale = 0;
	}
	
	public void UnpauseGame() {
		GameManager.gameState = GameManager.prevGameState;
		Time.timeScale = 1;
	}
	
	public void ResumeGame() {
		GameManager.gameState = GameState.Running;
		Time.timeScale = 1;
	}
	
	public void RestartGame() {
		ResumeGame();
		Application.LoadLevel(MainMenuGUI.selectedGameName + "_level_" + (MainMenuGUI.currentLevel).ToString());
	}
	
	public bool IsGameRunning() {
		if (GetGameState() == GameState.Running) return true;
		return false;
	}
	
	public void GoToNextLevel() {
		ResumeGame();
		if (!isLastLevel)
			Application.LoadLevel(MainMenuGUI.selectedGameName + "_level_" + (MainMenuGUI.currentLevel+1).ToString());
		else
			Application.LoadLevel("MainMenu");
	}
	
	public void EndGame() {
		PauseGame();
		SetGameState(GameState.Over);
		Time.timeScale = 0;
	}


}
