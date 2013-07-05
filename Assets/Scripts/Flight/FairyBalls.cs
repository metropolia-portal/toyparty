using UnityEngine;
using System.Collections;

public class FairyBalls : MonoBehaviour {
	
	Transform ptransform;
	bool active = false;
	
	public void SetParent(Transform p) {
		ptransform = p;
		active = true;
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (active) {
			if (ptransform != null) {
				transform.position = ptransform.position;
			} else {
				GameObject.Find ("Game Manager").GetComponent<FlightGameManager>().OnFairyDeath(10);
				Destroy(gameObject);
			}
		}
	}
}
