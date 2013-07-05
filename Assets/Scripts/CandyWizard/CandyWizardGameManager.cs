using UnityEngine;
using System.Collections;

public class CandyWizardGameManager : GameManager {
	public float minVelocity = 0.01f;
	public Brush lineBrush;
	public Candy candy;
	
	//we have only one instance each game
	static public CandyWizardGameManager Instance() {
		return instance;
	}
	
	public void OnStarCollected() {
		collectedStars ++;
	}
	
	public void OnCandyEaten() {
		Debug.Log("Candy Eaten");
		
		Medal medal = Medal.None;
		switch (collectedStars) {
			case 0:
				medal = Medal.Bronze;
				break;
			case 1:
				medal = Medal.Silver;
				break;
			case 2:
				medal = Medal.Gold;
				break;
		}
		SetMedal(medal);
		
		SetGameState(GameState.Over);
	}
	
	// Use this for initialization
	void Start () {
		instance = this;
		SetGameState(GameState.Pregame);
		lineBrush.Enable();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.Space)) {
			StartGame();
		}
		
		//TODO velocity 0 check
		//if(candy.gameObject.velocity)
	}
	
	void StartGame() {
		SetGameState(GameState.Running);
		lineBrush.Disable();
		candy.Drop ();	
	}
	
	//TODO check if that's reset if going to main menu then back here
	int collectedStars = 0;
	static CandyWizardGameManager instance = null;
}