using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public string NextLevelId = "BrickGameLevelOneTutorial";
	public string MenuLevelId = "BrickGameMenu";
	public string CurrentLevelId = "MainMenu";
	
	public enum GameState { Running, Paused, Victory, Defeat, MainMenu, GameMenu, Pregame};
	
	public GameState gameState = GameState.MainMenu;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void PauseGame() {
		gameState = GameState.Paused;
		Time.timeScale = 0;
	}
	
	public void ResumeGame() {
		gameState = GameState.Running;
		Time.timeScale = 1;
	}
	
	public void RestartGame() {
		ResumeGame();
		Application.LoadLevel(CurrentLevelId);
	}
	
	public bool IsGameRunning() {
		return gameState == GameState.Running;
	}
	
	public void GoToNextLevel() {
		ResumeGame();
		Application.LoadLevel(NextLevelId);
	}
	
	public void OnGameOver(bool victory) {
		if (victory) {
			Debug.LogWarning("Victory!");
			gameState = GameState.Victory;
		} else {
			Debug.LogWarning("Defeat!");
			gameState = GameState.Defeat;
		}
		Time.timeScale = 0;
	}
}
