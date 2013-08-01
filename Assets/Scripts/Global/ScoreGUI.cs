using UnityEngine;
using System.Collections;

public class ScoreGUI : MonoBehaviour {
	
	// test for push
	public Texture2D bronzeCoinTexture;
	public Texture2D silverCoinTexture;
	public Texture2D goldCoinTexture;
	public Texture2D scoreBarTexture;
	public Texture2D timerTexture;
	public Texture2D crossTexture;
	public Texture2D scoreBackgroundTexture;
	public bool timerEnabled = false;
	float paddingRight = Screen.width / 10f;
	int score = 0;
	int maxScore = 100;
	int maxMedals = 3;
	float timer = 0;
	int maxTimer = 30;
	
	int bronzeScore = 30;
	int silverScore = 60;
	int goldScore = 90;
	
	GameManager gameManager;
	
	float coinSize = Screen.width / 26;
	
	// Use this for initialization
	void Start () {
		gameManager = GetComponent<GameManager>();
	}

	public void SetMedalRequirements(int bronze, int silver, int gold) {
		bronzeScore = bronze;
		silverScore = silver;
		goldScore = gold;
		maxScore = goldScore;
	}
	
	public void setTimer(float t) {
		timer = t;
		if (timer<0) timer = 0;
	}
	
	//TODO fix to Set convention
	public void setMaxTimer(int t) {
		maxTimer = t;
	}
	
	public void SetMaxMedals(int m) {
		maxMedals = m;
	}
	
	public void SetScore(int s) {
		score = s;
		if (score>maxScore) score = maxScore;
	}
	public void SetMaxScore(int s) {
		maxScore = s;
		if (score>maxScore) score = maxScore;
	}
	
	float ScoreBarWidth() {
		if (score<bronzeScore) {
			return ((float)score/bronzeScore)/3;
		} else if (score<silverScore) {
			return (1 + ((float)(score-bronzeScore)/(silverScore-bronzeScore)))/3;
		} else if (score<goldScore) {
			
			return (2 + ((float)(score-silverScore)/(goldScore-silverScore)))/3;
		} else {
			return 0.99f;
		}
	}
	
	public void EnableTimer(bool enable = true) {
		timerEnabled = true;
	}
	
	void OnGUI() {
		if (!(gameManager.IsGameRunning() || gameManager.GetGameState() == GameManager.GameState.Pregame )) return;
		
		if (timerEnabled) {
			float t = 0.1f*Mathf.Floor(10*timer/maxTimer);
			GUI.depth = 1;
			GUI.DrawTextureWithTexCoords(new Rect(0, 0, Screen.width/20, Screen.width/10), timerTexture,
				new Rect(t,0,0.1f,1)); // draw timer
		}
		
		
		
		GUI.DrawTextureWithTexCoords(new Rect(Screen.width - (3*ScoreBarWidth())*coinSize - paddingRight, 0, 3*ScoreBarWidth()*coinSize, coinSize), scoreBarTexture,
			new Rect(3-ScoreBarWidth(),0,ScoreBarWidth(),1)); // draw score bar
			
		GUI.DrawTexture(new Rect(Screen.width - coinSize*3 - paddingRight, 0, coinSize*3, coinSize), scoreBackgroundTexture);
		
		GUI.depth = 0;
		
		 // draw medals
		GUI.DrawTexture(new Rect(Screen.width - coinSize - paddingRight, 0, coinSize, coinSize), bronzeCoinTexture);
		if (maxMedals < 1)
			GUI.DrawTexture(new Rect(Screen.width - coinSize - paddingRight, 0, coinSize, coinSize), crossTexture); 
		
		GUI.DrawTexture(new Rect(Screen.width - coinSize*2 - paddingRight, 0, coinSize, coinSize), silverCoinTexture);
		if (maxMedals < 2)
			GUI.DrawTexture(new Rect(Screen.width - coinSize*2 - paddingRight, 0, coinSize, coinSize), crossTexture);
		
		GUI.DrawTexture(new Rect(Screen.width - coinSize*3 - paddingRight, 0, coinSize, coinSize), goldCoinTexture);
		if (maxMedals < 3)
			GUI.DrawTexture(new Rect(Screen.width - coinSize*3 - paddingRight, 0, coinSize, coinSize), crossTexture);
		
	}
}
