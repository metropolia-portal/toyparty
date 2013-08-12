using UnityEngine;
using System.Collections;

public class Wizard : MonoBehaviour {
	CandyWizardGameManager candyScript;
	Animator2D animator;
	// Use this for initialization
	
	public void OnStartDrawing() {
		print ("anim start");
		animator.PlayAnimation("cast");
	}
	
	public void OnEndDrawing() {
		print ("anim end");
		animator.PlayAnimation("idle");
	}
	
	void Awake() {
		candyScript = GameObject.Find ("GameManager").GetComponent<CandyWizardGameManager>();
		animator = GetComponentInChildren<Animator2D>();
	}
	
	void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.CompareTag("Candy")) {
			collision.gameObject.SetActive(false);
			candyScript.OnCandyEaten();	
		}
	}
}
