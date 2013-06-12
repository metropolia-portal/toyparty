using UnityEngine;
using System.Collections;

public class MainSphere : Sphere {
	// When the main sphere is lost, the appropriate method is called in the GameManager
	protected override void OnSphereLost() {
		GameObject.Find("GameManager").GetComponent<BrickGameManager>().OnMainSphereLost();
	}
}
