using UnityEngine;
using System.Collections;

public class CandyWizardGameManager : GameManager {
	public Brush lineBrush;
	public Candy candy;
	// Use this for initialization
	void Start () {
		SetGameState(GameState.Pregame);
		lineBrush.Enable();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.Space))
			candy.Drop();
	}
}
