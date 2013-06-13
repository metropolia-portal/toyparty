using UnityEngine;
using System.Collections;

public class ExtraSpherePowerup : Powerup {
	public GameObject extraSpherePrefub;
	public GameObject sphereSpawn;
	
	ExtraSphere extraSphere;

	
	override protected void Start() {
		base.Start ();
		//sphereSpawn = GameObject.Find("StuckPosition"); // The "initial sphere position" object	
	}
		
	// This powerup creates an ExtraSphere object next to the paddle
	public override void Activate() {
		base.Activate();	
		
		GameObject sphere = (GameObject)
			GameObject.Instantiate(
				extraSpherePrefub, // Load the prefab from resources
				sphereSpawn.transform.position,
				Quaternion.identity 
			);
		
			
			extraSphere = sphere.GetComponent<ExtraSphere>(); // A link to the new sphere's script is stored to keep track of its life cycle	
	}
	
	// Update is called once per frame
	public override void OnUpdate () {
		if (!extraSphere.IsActive()) Deactivate(); // If the sphere is lost, the powerup is over
	}

	// When this powerup is over, the sphere is disposed of
	public override void Deactivate() {
		GameObject.Destroy(extraSphere.gameObject);
		base.Deactivate();
	}
}
