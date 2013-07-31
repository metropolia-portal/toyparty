using UnityEngine;
using System.Collections;

public class MainSphere : Sphere {
	// When the main sphere is lost, the appropriate method is called in the BrickGameManager
	protected override void OnSphereLost() {
		GameObject.Find("GameManager").GetComponent<BrickGameManager>().OnMainSphereLost();
	}
	
	protected override void SphereCollision (RaycastHit hit) {
		if(hit.collider.CompareTag("Brick")) {
			Brick brick = hit.collider.GetComponent<Brick>();
			
			if(brick.IsActive()) { 
				gameScore.OnSphereScore(brick.GetScore()); //if sphere hasn't hit it already
			}
		}
		
		base.SphereCollision(hit);
	}
}
