using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {
	public float comboMaxDeltaTime = 2f; //Max delta time between brick hits to keep the combo mode
	public int sphereLeftScore = 200; //extra score per each saved life
	public int secondLeftScore = 100; //extra score per each second left to finish the level
	public int noBricksScore = 10000; //extra score granted for destruction of all bricks
	
	public GameObject floatingScoreTextPrefab;

	int score = 0;
	
	float lastSphereScoreTime = 0;
	int comboMultiplier;
	int comboScore;
	
	
	// Use this for initialization
	void Start () {
		ReleaseComboScore();
	}
	
	public void AddScore(int score) {
		this.score += score;	
	}
	
	public void OnSphereScore(int hitScore) {
		comboMultiplier ++;
	
		if(comboMultiplier > 1) 
			comboScore = (comboScore + hitScore)*2; // more that one hit - combo!
		else
			comboScore = hitScore;
		
		lastSphereScoreTime = Time.timeSinceLevelLoad;		
		//print ("combo increase to " + comboScore);		
	}
	
	
	public void AddFinalScore(int spheres, int bricks, float time) {
		AddScore(spheres * sphereLeftScore + (int)time * secondLeftScore);
		if (bricks==0) AddScore(noBricksScore);
	}
	
	public void ShowFloatingScore( int score, Vector3 position) {
		GameObject obj = (GameObject)Instantiate (floatingScoreTextPrefab, position, Quaternion.identity);
		//obj.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
		FloatingScore floatingScore = obj.GetComponent<FloatingScore>();
		floatingScore.FireScore(score.ToString());
	}
	
	
	
	void ReleaseComboScore() {
		
		if(comboMultiplier > 1) {
			//print ("Combo! +" + comboScore);
			AddScore(comboScore);
		}
		
		comboMultiplier = 0;
		lastSphereScoreTime = 0;
		comboScore = 0;
	}
	
	public int GetCurrentComboCount() {
		return comboMultiplier;
	}
	
	public int GetCurrentComboScore() {
		if(comboMultiplier > 1) 
			return 	comboScore;
		else
			return 0;
	}
	
	public int GetScore() {
		return score;
	}
	
	void Update() {
		if(lastSphereScoreTime > 0 && Time.timeSinceLevelLoad  > lastSphereScoreTime + comboMaxDeltaTime ) {
			//print ("time run out! release combo if there is");
			ReleaseComboScore();
		}
	}
}
