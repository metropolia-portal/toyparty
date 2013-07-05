using UnityEngine;
using System.Collections;

public class CandyWizardGameManager : GameManager {
	public Brush lineBrush;
	public Candy candy;
	
	//we have only one instance each game
	static public CandyWizardGameManager Instance() {
		return instance;
	}
	
	public void OnCandyEaten() {
		Debug.Log("Candy Eaten");
		SetMedal(Medal.Bronze);
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
		if(Input.GetKeyUp(KeyCode.Space))
			candy.Drop();
	}
	
	static CandyWizardGameManager instance = null;
}
