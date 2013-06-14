using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public string NextLevelId = "BrickGameLevelOneTutorial";
	public string MenuLevelId = "BrickGameMenu";
	
	public enum GameState { Pregame, Running, Paused, Over };
	public enum Medal { None, Bronze, Silver, Gold };
	
	public static Medal medal = Medal.None;
	public static GameState gameState;
	public static GameState prevGameState;
	
	// Use this for initialization
	void Start () {
	
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
		return GameManager.gameState == GameState.Running;
	}
	
	public void GoToNextLevel() {
		ResumeGame();
		Application.LoadLevel(MainMenuGUI.selectedGameName + "_level_" + (MainMenuGUI.currentLevel+1).ToString());
	}
	
	public void OnGameOver(bool victory) {
		SetGameState (GameState.Over);
		Time.timeScale = 0;
	}
}
