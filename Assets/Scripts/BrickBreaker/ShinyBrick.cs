using UnityEngine;
using System.Collections;

public class ShinyBrick : Brick {	
	
	protected override void OnBrickDestroyed() {
		manager.OnShinyBrickDestroyed(transform.position);
	}
}
