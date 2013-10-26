using UnityEngine;
using System.Collections;

public enum GameState { Pregame, Running, Paused, Over };
public enum Medal { None, Bronze, Silver, Gold };

public class GameManager : MonoBehaviour 
{
	#region MEMBERS
	protected GameState gameState;
	GameState prevGameState;
	
	public bool isLastLevel = true;
	public int currentLevel = 1;
	public string gameName;
	
	public static Medal medal = Medal.None;	
	
	public int bronzeMedalScore = 30;
	public int silverMedalScore = 60;
	public int goldMedalScore = 90;
	#endregion
	
	#region UNITY_METHODS
	virtual protected void Awake() 
	{		
		MainMenuGUI.selectedGameName = gameName;
		MainMenuGUI.currentLevel = currentLevel;
	}
	
	
	// Use this for initialization
	public virtual void Start () {
		SetGameState(GameState.Pregame); //reset the game state set by previous game, TODO  why do we need static gameState?
		Time.timeScale = 1;
	}
	#endregion
	
	#region METHODS
	public void SetMedal(Medal m) 
	{
		GameManager.medal = m;
	}
	
	public Medal GetMedal() 
	{
		return GameManager.medal;
	}
	
	public void SetGameState(GameState s) 
	{
		gameState = s;
	}
	
	public GameState GetGameState() 
	{
		return gameState;
	}
	
	public void PauseGame() 
	{
		prevGameState = GetGameState();
		gameState = GameState.Paused;
		Time.timeScale = 0;
	}
	
	public void UnpauseGame() 
	{
		gameState = prevGameState;
		Time.timeScale = 1;
	}
	
	public void ResumeGame() 
	{
		gameState = GameState.Running;
		Time.timeScale = 1;
	}
	
	public void RestartGame() 
	{
		//Reset global time scale
		Time.timeScale = 1;
		Application.LoadLevel(MainMenuGUI.selectedGameName + "_level_" + (MainMenuGUI.currentLevel).ToString());
	}
	
	public bool IsGameRunning() 
	{
		if (GetGameState() == GameState.Running) return true;
		return false;
	}
	
	public void GoToNextLevel() 
	{
		//Reset global time scale
		Time.timeScale = 1;
		if (!isLastLevel)
			Application.LoadLevel(MainMenuGUI.selectedGameName + "_level_" + (MainMenuGUI.currentLevel+1).ToString());
		else
			Application.LoadLevel("CreditsScreen");	
	}
	
	public void EndGame() 
	{
		PauseGame();
		SetGameState(GameState.Over);
		Time.timeScale = 0;
	}
	#endregion
}
