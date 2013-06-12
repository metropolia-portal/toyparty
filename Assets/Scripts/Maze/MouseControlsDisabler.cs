using UnityEngine;
using System.Collections;

public class MouseControlsDisabler : MonoBehaviour {
	
	public float timeLimit = 5;
	public float speedModifier = 1;
	
	bool cooldown = false;
	bool working = false;
	float timeLeft;
	
	Mouse mouse;
	
	// Use this for initialization
	void Start () {
		mouse =  GameObject.Find("Mouse").GetComponent<Mouse>();
	}
	
	// Update is called once per frame
	void Update () {
		if (working) {
			timeLeft -= Time.deltaTime;
			if (timeLeft <= 0) {
				mouse.EnableControls();
				cooldown = true;
				working = false;
				mouse.SetSpeedModifier(1);				
			}
		}
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")) {
			if (!cooldown && !working) { 
				mouse.DisableControls();
				mouse.SetSpeedModifier(speedModifier);				
				working = true;
				timeLeft = timeLimit;
			}
		}
	}
	
	void OnTriggerExit(Collider other) {
		if (other.CompareTag("Player")) {
			mouse.SetSpeedModifier(1);			
			mouse.EnableControls();
			cooldown = false;
			working = false;
		}			
	}
	
}
