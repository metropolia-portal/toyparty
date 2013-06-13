using UnityEngine;
using System.Collections;

public class LifePowerup : Powerup {

	// This powerup creates an ExtraSphere object next to the paddle
	public override void Activate () {
		base.Activate();
		
		gameManager.AddSphere();
		Deactivate();
	}

	// When this powerup is over, the sphere is disposed of
	public override void Deactivate() {
		base.Deactivate();
	}
}