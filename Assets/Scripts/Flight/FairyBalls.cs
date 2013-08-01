using UnityEngine;
using System.Collections;

public class FairyBalls : MonoBehaviour {
	
	Transform ptransform;
	bool activeScript = false;
	
	public void SetParent(Transform p) {
		ptransform = p;
		activeScript = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (activeScript) {
			if (ptransform != null) {
				transform.position = ptransform.position;
			} else {
				GameObject.Find ("GameManager").GetComponent<FlightGameManager>().OnFairyDeath(10);
				Destroy(gameObject);
			}
		}
	}
}
