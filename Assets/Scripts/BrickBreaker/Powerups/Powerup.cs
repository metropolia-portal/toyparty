using UnityEngine;
using System.Collections;

// This is the base class for Powerups. Powerups are buffs that affect various parts of the game. 
// Each powerup has one instance in game and can be applied several times
public class Powerup : MonoBehaviour {
	
	public AudioClip activationSound;
	
	protected BrickGameManager gameManager;
	
	//called once in game, finds needed references to gameObject
	virtual protected void Start() {
		gameManager = GameObject.Find ("GameManager").GetComponent<BrickGameManager>();
	}
	
	// Activate event called from the BrickGameManager object when the powerup is activated
	//should always put powerup in initial state as it will be called several times
	//TODO rename method in comment
	virtual public void Activate () {
		audio.PlayOneShot(activationSound);
	}
	
	// OnUpdate event called once per frame
	virtual public void OnUpdate () {
		
	}
	
	// Deactivate event called (normally from the OnUpdate event) when the powerup is over
	virtual public void Deactivate () {
		gameManager.OnPowerupEnd();	
	}
}
