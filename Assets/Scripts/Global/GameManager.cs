using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public string NextLevelId = "BrickGameLevelOneTutorial";
	public string MenuLevelId = "BrickGameMenu";
	
	public enum GameState { Running, Paused, Victory, Defeat, Pregame};
	
	public static GameState gameState = GameState.Pregame;
	public static GameState prevGameState = GameState.Pregame;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
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
		if (victory) {
			Debug.LogWarning("Victory!");
			GameManager.gameState = GameState.Victory;
		} else {
			Debug.LogWarning("Defeat!");
			GameManager.gameState = GameState.Defeat;
		}
		Time.timeScale = 0;
	}
}
