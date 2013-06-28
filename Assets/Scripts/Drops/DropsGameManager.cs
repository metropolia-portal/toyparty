using UnityEngine;
using System.Collections;

public class DropsGameManager : GameManager {
	
	public float timeLeft = 60;
	public GUIText GUITimer;
	public GameObject greenBox;
	public GameObject redBox;
	public Camera camera;
	public GameObject background;
	public float maxDistanceFromCenter;
	public int currentLevel;
	
	int toyScore = 5;
	int maxScore = 100;
	int score = 0;
	int medals = 3;
	
	ScoreGUI scoreGUI;
	
	// Use this for initialization
	void Start () {
		MainMenuGUI.selectedGameName = "grab";
		MainMenuGUI.currentLevel = currentLevel;
		maxDistanceFromCenter *= camera.aspect;
		background.transform.localScale = new Vector3(camera.aspect, 1, 1);
		base.Start();
		Debug.Log("waha");
		scoreGUI = GetComponent<ScoreGUI>();
		scoreGUI.SetMaxScore(maxScore);
		scoreGUI.setMaxTimer((int)timeLeft);
		SetGameState(GameState.Running);
		Debug.Log(GetGameState());	
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(IsGameRunning());
		if (IsGameRunning()) {
			Debug.Log("waha");
			scoreGUI.SetScore(score);
			float newScale;
			timeLeft -= Time.deltaTime;
			scoreGUI.setTimer(Mathf.Floor(timeLeft));
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
			
			if (timeLeft < 5) {
				GetComponent<ItemGenerator>().Stop();
			}
			
			
			newScale = (3f/maxScore) * score;
			if (newScale > medals) newScale = 3;
			greenBox.transform.localScale = new Vector3(newScale,1,1);
			redBox.transform.localScale = new Vector3(3-medals, 1, 1);
		}
	}
	
	public void OnToy() {

		score += toyScore;

	}
	
	public void OnBomb() {
		medals --;
		scoreGUI.SetMaxMedals(medals);
		if (medals <= 0) {
			SetMedal(Medal.None);
			EndGame();
		}
	}

	
}
