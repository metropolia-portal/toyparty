using UnityEngine;
using System.Collections;

public class Candy : MonoBehaviour {
	InputManager input;
	CandyWizardGameManager candyScript;
	
	void Start(){
		GameObject gm = GameObject.Find ("GameManager");
		candyScript = gm.GetComponent<CandyWizardGameManager>();
		input = gm.GetComponent<InputManager>();
	}
	//drops the candy
	public void Drop() {
		droppped = true;
		rigidbody.isKinematic = false;
		rigidbody.useGravity = true;
	}
	
	void OnTriggerEnter(Collider collider) {
		if(collider.CompareTag("Star")) {
			collider.gameObject.SetActive(false);
			candyScript.OnStarCollected();
		}
	}
	
	void Update() {
		if(input.IsCursorButtonDown()) {
			Vector2 screenPos = input.GetCurrentCursorPosition();
			if(Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(screenPos.x, screenPos.y)), Mathf.Infinity, 1 << gameObject.layer)) {
				candyScript.OnCandyClicked();
			}
		}
	}
	
	void FixedUpdate() {
		if(droppped) {
			//if candy has 0 velocity for two frames in row - it is stuck
			if(stopped && rigidbody.velocity.magnitude == 0) candyScript.OnCandyStuck();	
			stopped = rigidbody.velocity.magnitude == 0;
		}
	}
	bool droppped = false;
	
	bool stopped = false;

}
