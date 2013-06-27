using UnityEngine;
using System.Collections;

public class FairyMovement : MonoBehaviour {
	
	public float startingDelay = 0;
	public Vector3 shift;
	public float rotShift;
	public float duration = 0;
	public bool easing = true;
	
	float phase = 0;
	float endTime;
	Vector3 v;
	float r;
	Vector3 d;

	// Use this for initialization
	void Start () {
		endTime = duration+startingDelay;
		v = new Vector3(0,0,0);
		r = 0;
		d = Vector3.right;
	}
	
	// Update is called once per frame
	void Update () {
		phase += Time.deltaTime;
		if ((phase>startingDelay)&&(phase<endTime)) {
			if (easing) {
				if (phase-startingDelay<endTime-phase) v += shift * Time.deltaTime;
				else
					v -= shift*Time.deltaTime;
			} else {
				v = shift * Time.deltaTime;
			}
				r += rotShift * Time.deltaTime;
			
				transform.position += Quaternion.Euler(0,r,0) * v;
				
		}
	}
	
	void reset() {
		phase = 0;
	}
}
