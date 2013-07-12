using UnityEngine;
using System.Collections;

public class BalloonOwl : MonoBehaviour {
	
	float floatPhase = 0;
	float floatSpeed = 1f;
	float forwardSpeed = 0.7f;

	
	void FixedUpdate() {
		floatPhase += Time.fixedDeltaTime * floatSpeed;
		if (floatPhase > Mathf.PI*2) floatPhase -= Mathf.PI*2;
		transform.position += Vector3.left * forwardSpeed * Time.fixedDeltaTime + Vector3.forward * Mathf.Sin(floatPhase)/2 * Time.fixedDeltaTime;

	}

}
