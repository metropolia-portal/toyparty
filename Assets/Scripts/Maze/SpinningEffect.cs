using UnityEngine;
using System.Collections;

public class SpinningEffect : MonoBehaviour {
	
	public float rotationSpeed = 180;
	
	float phase = 0;
	
	// Update is called once per frame
	void Update () {
		phase += Time.deltaTime;
		transform.rotation = Quaternion.Euler(phase*rotationSpeed, phase*rotationSpeed, phase*rotationSpeed);
	}
}
