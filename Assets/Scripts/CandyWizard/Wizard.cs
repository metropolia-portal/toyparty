using UnityEngine;
using System.Collections;

public class Wizard : MonoBehaviour {
	CandyWizardGameManager candyScript;
	// Use this for initialization
	void Start () {
		candyScript = GameObject.Find ("GameManager").GetComponent<CandyWizardGameManager>();
	}
	
	void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.CompareTag("Candy")) {
			collision.gameObject.SetActive(false);
			candyScript.OnCandyEaten();	
		}
	}
}
