using UnityEngine;
using System.Collections;

public class Candy : MonoBehaviour {
	//drops the candy
	public void Drop() {
		droppped = true;
		rigidbody.isKinematic = false;
		rigidbody.useGravity = true;
	}
	
	void OnTriggerEnter(Collider collider) {
		if(collider.CompareTag("Star")) {
			collider.gameObject.SetActive(false);
			CandyWizardGameManager.Instance().OnStarCollected();
		}
	}
	
	void Update() {
		if(InputManager.Instance().IsCursorButtonDown()) {
			Vector2 screenPos = InputManager.Instance().GetCurrentCursorPosition();
			if(Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(screenPos.x, screenPos.y)), Mathf.Infinity, 1 << gameObject.layer)) {
				CandyWizardGameManager.Instance().OnCandyClicked();
			}
		}
	}
	
	void FixedUpdate() {
		if(droppped) {
			//if candy has 0 velocity for two frames in row - it is stuck
			if(stopped && rigidbody.velocity.magnitude == 0) CandyWizardGameManager.Instance().OnCandyStuck();	
			stopped = rigidbody.velocity.magnitude == 0;
		}
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	bool droppped = false;
	
	bool stopped = false;

}
