using UnityEngine;
using System.Collections;

public class Wizard : MonoBehaviour {
	public Transform eatLeftPosition;
	public float eatDuration;
	public float swallowTime; //time during eatDuration, when candy is swallowed
	public int[] lookFramesClockwise = {4,3,5,2,0,1};  //starting from Vector2.up
	public float closeLookDistance = 5f;
	
	public AudioClip onEat;
	
	CandyWizardGameManager gameManager;
	Animator2D animator;
	Candy candy;
	
	bool eating = false;
	
	
	public void OnStartDrawing() {
		animator.PlayAnimation("cast");
	}
	
	public void OnEndDrawing() {
		animator.PlayAnimation("idle");
	}
	
	void Awake() {
		gameManager = GameObject.Find ("GameManager").GetComponent<CandyWizardGameManager>();
		candy= GameObject.Find ("Candy").GetComponent<Candy>();
		animator = GetComponentInChildren<Animator2D>();
	}
	
	void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.CompareTag("Candy")) {
			EatCandy();	
		}
	}
	
	void EatCandy() {
		eating = true;
		candy.rigidbody.isKinematic = true;
		
		//to the right of wizard
		if(candy.transform.position.x < transform.position.x) {
			animator.PlayAnimation("eatLeft");
			candy.transform.position = eatLeftPosition.position;
		} else {
			animator.PlayAnimation("eatRight");
			candy.transform.position = new Vector3(2*transform.position.x - eatLeftPosition.position.x , eatLeftPosition.position.y, eatLeftPosition.position.z);
		}	
		
		Invoke("SwallowCandy", swallowTime);
		Invoke("DestroyCandy", eatDuration);
		
		audio.PlayOneShot(onEat);
	}
	
	void DestroyCandy() {			
		gameManager.OnCandyEaten();
	}
	
	void SwallowCandy() {
		candy.gameObject.SetActive(false);	
	}
	
	void Update() {
		if(candy.IsDropped() && !eating) { // candy is falling, look for it
			Vector3 candyVector = candy.transform.position - transform.position; 
			float angle = Vector3.Angle(Vector3.up, candyVector);
			//since Angle gives [0,180] check if it's actually [180, 360]
			if(candyVector.x < 0)  //lefter than wizard
				angle = 360 - angle;
			
			int frameOffset = 0; // for far distance, no frame offset
			if(candyVector.magnitude < closeLookDistance)
				frameOffset = 6; //switch to close distance frames 
				
			animator.SetAnimationFrame("look", frameOffset + lookFramesClockwise[(int)(angle / 360 * lookFramesClockwise.Length)]);
		} else if(animator.GetCurrentAnimation().GetName() == "look")
			animator.PlayAnimation("idle");
	}
}
