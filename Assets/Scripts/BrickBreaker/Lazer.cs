using UnityEngine;
using System.Collections;

public class Lazer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<LineRenderer>().SetPosition(0, transform.position);
		
		RaycastHit hit;	
		if(Physics.Raycast(transform.position, Vector3.forward, out hit)) {
			GetComponent<LineRenderer>().SetPosition(1, hit.point);
		}
	}
	
	// Update is called once per frame
	void Update () {

	}
}
