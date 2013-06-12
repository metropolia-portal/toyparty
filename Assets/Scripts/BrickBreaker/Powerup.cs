using UnityEngine;
using System.Collections;

// This is the base class for Powerups. Powerups are buffs that affect various parts of the game.
public class Powerup {
	
	BrickGameManager gameManager;

	// OnStart event called from the GameManager object when the powerup is activated
	virtual public void OnStart () {
		gameManager = GameObject.Find ("GameManager").GetComponent<BrickGameManager>();		
	}
	
	// OnUpdate event called once per frame
	virtual public void OnUpdate () {
		
	}
	
	// OnEnd event called (normally from the OnUpdate event) when the powerup is over
	virtual public void OnEnd() {
		gameManager.OnPowerupEnd();	
	}
}
