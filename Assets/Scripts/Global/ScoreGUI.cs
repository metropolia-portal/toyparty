using UnityEngine;
using System.Collections;

public class ScoreGUI : MonoBehaviour {
	
	public Texture2D bronzeCoinTexture;
	public Texture2D silverCoinTexture;
	public Texture2D goldCoinTexture;
	public Texture2D scoreBarTexture;
	public Texture2D timerTexture;
	public Texture2D crossTexture;
	public bool timerEnabled = false;
	float paddingLeft = Screen.width / 10;
	int score = 0;
	int maxScore = 100;
	int maxMedals = 3;
	float timer = 0;
	int maxTimer = 30;
	
	public void setTimer(float t) {
		timer = t;
		if (timer<0) timer = 0;
	}
	
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
	
	// Use this for initialization
	void Start () {
	
	}
	
	public void EnableTimer(bool enable = true) {
		timerEnabled = true;
	}
	
	void OnGUI() {
		
		if (timerEnabled) {
			float t = 0.1f*Mathf.Floor(10*timer/maxTimer);
			GUI.depth = 1;
			GUI.DrawTextureWithTexCoords(new Rect(9*Screen.width/10 - paddingLeft, Screen.width/10, Screen.width/10, Screen.width/5), timerTexture,
				new Rect(t,0,0.1f,1)); // draw timer
		}
		
		GUI.DrawTextureWithTexCoords(new Rect((10 - 3*(float)score/maxScore)*Screen.width/10 - paddingLeft, 0, 3*((float)score/maxScore)*Screen.width/10, Screen.width/10), scoreBarTexture,
			new Rect(3-(float)score/maxScore,0,(float)score/maxScore,1)); // draw score bar
			
		
		
		GUI.depth = 0;
		 // draw medals
		GUI.DrawTexture(new Rect(9*Screen.width/10 - paddingLeft, 0, Screen.width/10, Screen.width/10), bronzeCoinTexture);
		if (maxMedals < 1)
			GUI.DrawTexture(new Rect(9*Screen.width/10 - paddingLeft, 0, Screen.width/10, Screen.width/10), crossTexture); 
		
		GUI.DrawTexture(new Rect(8*Screen.width/10 - paddingLeft, 0, Screen.width/10, Screen.width/10), silverCoinTexture);
		if (maxMedals < 2)
			GUI.DrawTexture(new Rect(8*Screen.width/10 - paddingLeft, 0, Screen.width/10, Screen.width/10), crossTexture);
		
		GUI.DrawTexture(new Rect(7*Screen.width/10 - paddingLeft, 0, Screen.width/10, Screen.width/10), goldCoinTexture);
		if (maxMedals < 3)
			GUI.DrawTexture(new Rect(7*Screen.width/10 - paddingLeft, 0, Screen.width/10, Screen.width/10), crossTexture);
		
	}
}
