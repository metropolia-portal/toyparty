using UnityEngine;
using System.Collections;

public class Wizard : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter(Collision collision) {
		if(collision.collider.CompareTag("Candy")) {
			//collision.collider.enabled = false;
			collision.collider.gameObject.SetActive(false);
			CandyWizardGameManager.Instance().OnCandyEaten();	
		}
	}
}
