using UnityEngine;
using System.Collections;

public class Candy : MonoBehaviour {
	public AudioClip onLaunch;
	public AudioClip onCollectedStar;
	public AudioClip onSpeedUp;
	
	InputManager input;
	CandyWizardGameManager gameManager;
	
	void Start(){
		GameObject gm = GameObject.Find ("GameManager");
		gameManager = gm.GetComponent<CandyWizardGameManager>();
		input = gm.GetComponent<InputManager>();
	}
	//drops the candy
	public void Drop() {
		droppped = true;
		rigidbody.isKinematic = false;
		rigidbody.useGravity = true;
		
		audio.PlayOneShot(onLaunch);
	}
	
	void OnTriggerEnter(Collider collider) {
		if(collider.CompareTag("Star")) {
			collider.gameObject.SetActive(false);
			gameManager.OnStarCollected();
			
			audio.PlayOneShot(onCollectedStar);
		}
	}
	
	void Update() {
		if(input.IsCursorButtonDown()) {
			Vector2 screenPos = input.GetCurrentCursorPosition();
			if(Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(screenPos.x, screenPos.y)), Mathf.Infinity, 1 << gameObject.layer)) {
				gameManager.OnCandyClicked();
			}
		}
	}
	
	void FixedUpdate() {
		if(droppped) {
			//if candy has 0 velocity for two frames in row - it is stuck
			if(!rigidbody.isKinematic && stopped && rigidbody.velocity.magnitude == 0) gameManager.OnCandyStuck();	
			stopped = rigidbody.velocity.magnitude == 0;
		}
	}
	
	void OnCollisionEnter(Collision collision) {
		if(collision.collider.CompareTag("SpeedUpSegment") && !audio.isPlaying) {
			audio.clip = onSpeedUp;
			audio.loop = false;
			audio.Play();
		}
	}
	
	bool droppped = false;
	
	bool stopped = false;

}
