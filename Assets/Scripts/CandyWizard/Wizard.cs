using UnityEngine;
using System.Collections;

public class Wizard : MonoBehaviour {
	public Transform eatLeftPosition;
	public float eatDuration;
	public float swallowTime; //time during eatDuration, when candy is swallowed
	
	CandyWizardGameManager gameManager;
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
		gameManager = GameObject.Find ("GameManager").GetComponent<CandyWizardGameManager>();
		animator = GetComponentInChildren<Animator2D>();
	}
	
	void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.CompareTag("Candy")) {

			EatCandy(collision.gameObject);	
		}
	}
	
	GameObject tempCandy;
	void EatCandy(GameObject candy) {
		//to the right of wizard
		candy.rigidbody.isKinematic = true;

		if(candy.transform.position.x < transform.position.x) {
			animator.PlayAnimation("eatLeft");
			candy.transform.position = eatLeftPosition.position;
		} else {
			animator.PlayAnimation("eatRight");
			candy.transform.position = new Vector3(2*transform.position.x - eatLeftPosition.position.x , eatLeftPosition.position.y, eatLeftPosition.position.z);
		}	
		
		tempCandy = candy;
		Invoke("SwallowCandy", swallowTime);
		Invoke("DestroyCandy", eatDuration);
	}
	
	void DestroyCandy() {			
		gameManager.OnCandyEaten();
	}
	
	void SwallowCandy() {
		tempCandy.SetActive(false);	
	}
}
