using UnityEngine;
using System.Collections;

public class Wizard : MonoBehaviour {
	CandyWizardGameManager candyScript;
	// Use this for initialization
	void Start () {
		candyScript = GameObject.Find ("GameManager").GetComponent<CandyWizardGameManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter(Collision collision) {
		if(collision.collider.CompareTag("Candy")) {
			//collision.collider.enabled = false;
			collision.collider.gameObject.SetActive(false);
			candyScript.OnCandyEaten();	
		}
	}
}
