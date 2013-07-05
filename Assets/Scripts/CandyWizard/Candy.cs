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
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
