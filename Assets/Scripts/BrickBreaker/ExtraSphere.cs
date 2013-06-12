using UnityEngine;
using System.Collections;

public class ExtraSphere : Sphere {
	
	bool isActive = false;
	
	override protected void OnSphereLost() {
		isActive = false;
	}
	
	// This method is used by the powerup to determine if the sphere is still around
	public bool IsActive() {
		return isActive;
	}
	
	// The extra sphere is launched right as it is created
	void Start () {
		isActive = true;
		Launch();
	}
}
