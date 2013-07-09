using UnityEngine;
using System.Collections;

public class Candy : MonoBehaviour {
	//drops the candy
	public void Drop() {
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
			Vector2 screenPos = InputManager.Instance().GetCursorPosition();	
			if(Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(screenPos.x, screenPos.y)),Mathf.Infinity, LayerMask.NameToLayer("Candy"))) {
				CandyWizardGameManager.Instance().OnCandyClicked();
			}
			
		}
	}
	
	// Use this for initialization
	void Start () {
	
	}

}
