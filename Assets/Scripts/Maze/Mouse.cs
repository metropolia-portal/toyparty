using UnityEngine;
using System.Collections;

public class Mouse : MonoBehaviour {
	
	
	public float windupAtStart = 30;
	public GameObject keyObject;
	public InputManager inputManager;
	
	float rotSpeed = 10;
	
	float windupLeft;
		
	public float defaultSpeed = 0.5f;
	float speedModifier = 1;
	bool controlsEnabled = true;
	Vector3 savedVelocity;
	Transform model;

	// Use this for initialization
	void Start () {
		windupLeft = windupAtStart;
		model = transform.Find("MouseModel").transform;
	}
	
	public float GetWindupLeft() {
		return windupLeft;
	}
	
	public void SetSpeedModifier(float m) {
		speedModifier = m;
	}
	
	public void EnableControls() {
		controlsEnabled = true;
	}
	
	public void DisableControls() {
		if (!controlsEnabled) return;
		controlsEnabled = false;
		savedVelocity = rigidbody.velocity / speedModifier;
	}
	
	// Update is called once per frame
	void Update () {
		float speed = speedModifier * defaultSpeed;
		RaycastHit hit;
		Vector3 planarVelocity;
		
		windupLeft -= Time.deltaTime;
	
		if (controlsEnabled) {
			rigidbody.velocity = new Vector3 (inputManager.GetAcceleration().x*speed,0,inputManager.GetAcceleration().y*speed);
		} else {
			rigidbody.velocity = savedVelocity * speedModifier;
		}
		
		planarVelocity = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);
		
		if (planarVelocity.magnitude > 0.5f)
			model.rotation = Quaternion.Slerp(model.rotation, Quaternion.LookRotation(planarVelocity), rotSpeed*Time.deltaTime);
		
		keyObject.transform.rotation = Quaternion.Euler(0, 180*windupLeft,0);
		
	}
}
