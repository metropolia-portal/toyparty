using UnityEngine;
using System.Collections;

public class OwlBossUnit : Unit {
	

	protected override IEnumerator MainScript() {
		SetMovementSpeed(2);
		// ENTRANCE
		yield return MoveTo(0.7f,0.5f);
		// PHASE 1
		SetMovementSpeed(3);
		SetInterruptWhenHealthLoweredBy(2);
		while (!interrupt) {
			yield return MoveTo (Random.Range(0.2f,0.8f), Random.Range(0.2f,0.8f));

			if (!interrupt) yield return Idle(2,"attack");

			if (!interrupt) Shoot (bullet);		

			if (!interrupt) yield return Idle(2);

		}
		// PHASE 2
		SetInterruptWhenHealthLoweredBy(-1);
		int phase = Random.Range(0,2);
		yield return MoveTo(0.7f,0.7f);
		
		if (phase == 0) { // PHASE 2.0, NO BALLOONS
			while (true) {
				int randomMovements = Random.Range(4,8);
				for (int i=0; i<randomMovements; i++) { // Move randomly a few times
					SetMovementSpeed(Random.Range(4f,6f));
					yield return MoveTo (Random.Range(0.2f,0.8f), Random.Range(0.2f,0.8f));
					yield return Idle(Random.Range(0, 0.2f));
				}
				Vector2 dragonPosition = FindDragonPosition();
				yield return Idle (1, "charge");
				SetMovementSpeed(15);
				yield return MoveTo (dragonPosition);
				yield return Idle (2);
			}
		} else
		if (phase == 1) { // PHASE 2.1, NO STEERING WHEEL
			yield return MoveTo (0.7f,0.3f);
			while (true) {
				
				yield return MoveTo (0.7f,0.7f);
				for (int i=0; i<3; i++) {
					yield return Idle (0.5f, "attack");
					Shoot (bullet);
				}
				yield return MoveTo (0.7f,0.3f);
				for (int i=0; i<3; i++) {
					yield return Idle (0.5f, "attack");
					Shoot (bullet);
				}
				
				yield return Idle(1);
			}
		}			
			

	}
	
}
