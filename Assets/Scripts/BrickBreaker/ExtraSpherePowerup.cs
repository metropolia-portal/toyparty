using UnityEngine;
using System.Collections;

public class ExtraSpherePowerup : Powerup {
	
	ExtraSphere extraSphere;
	GameObject sphereSpawn;
	
	// This powerup creates an ExtraSphere object next to the paddle
	public override void OnStart () {
		base.OnStart();
		sphereSpawn = GameObject.Find("StuckPosition"); // The "initial sphere position" object
		extraSphere =  
			((GameObject)
				GameObject.Instantiate(
					Resources.Load("ExtraSphere"), // Load the prefab from resources
					sphereSpawn.transform.position,
					Quaternion.identity 
				)
			).GetComponent<ExtraSphere>(); // A link to the new sphere's script is stored to keep track of its life cycle		
	}
	
	// Update is called once per frame
	public override void OnUpdate () {
		if (!extraSphere.IsActive()) OnEnd(); // If the sphere is lost, the powerup is over
	}

	// When this powerup is over, the sphere is disposed of
	public override void OnEnd() {
		GameObject.Destroy(extraSphere.gameObject);
		base.OnEnd();
	}
}
