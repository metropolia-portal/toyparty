using UnityEngine;
using System.Collections;

public class MouseSpeedModifier : MonoBehaviour {
	
	public float speedModifier = 1;
	
	Mouse mouse;
	
	// Use this for initialization
	void Start () {
		mouse =  GameObject.Find("Mouse").GetComponent<Mouse>();
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")) {
			mouse.SetSpeedModifier(speedModifier);
		}
	}
	
	void OnTriggerExit(Collider other) {
		if (other.CompareTag("Player")) {
			mouse.SetSpeedModifier(1);
		}			
	}
}
