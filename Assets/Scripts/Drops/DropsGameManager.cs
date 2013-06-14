using UnityEngine;
using System.Collections;

public class DropsGameManager : GameManager {
	
	public float timeLeft = 60;
	public GUIText GUITimer;
	public GameObject greenBox;
	public GameObject redBox;
	
	int toyScore = 5;
	int maxScore = 100;
	int score = 0;
	int medals = 3;
	
	// Use this for initialization
	void Start () {
		SetGameState(GameManager.GameState.Running);
	}
	
	// Update is called once per frame
	void Update () {
		if (IsGameRunning()) {
			float newScale;
			timeLeft -= Time.deltaTime;
			GUITimer.text = Mathf.Floor(timeLeft).ToString();
			if (timeLeft <= 0) {
				int result = (int)Mathf.Floor(3f*score / maxScore);
				Debug.Log(result);
				if (result > medals) result = medals;
				
				
				
				if (result == 3) SetMedal(Medal.Gold);
				else if (result == 2) SetMedal(Medal.Silver);
				else if (result == 1) SetMedal(Medal.Bronze);
				else if (result == 0) SetMedal(Medal.None);
				EndGame ();
			}
			
			
			newScale = (3f/maxScore) * score;
			if (newScale > medals) newScale = 3;
			greenBox.transform.localScale = new Vector3(newScale,1,1);
			redBox.transform.localScale = new Vector3(3-medals, 1, 1);
		}
	}
	
	public void OnToy() {
		Debug.Log("Toy acquired.");
		score += toyScore;
		Debug.Log(score);
	}
	
	public void OnBomb() {
		medals --;
		if (medals <= 0) {
			OnGameOver(Medal.None);
		}
	}
	
	public void OnGameOver(Medal m) {
		SetGameState (GameState.Over);
		SetMedal(m);
	}
	
}
