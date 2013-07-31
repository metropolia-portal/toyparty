using UnityEngine;
using System.Collections;

public class Candy : MonoBehaviour {
	CandyWizardGameManager candyScript;
	void Start(){
		candyScript = GameObject.Find ("GameManager").GetComponent<CandyWizardGameManager>();
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
		if(InputManager.Instance().IsCursorButtonDown()) {
			Vector2 screenPos = InputManager.Instance().GetCurrentCursorPosition();
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
