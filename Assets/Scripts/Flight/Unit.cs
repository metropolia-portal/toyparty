using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {
	int thatOtherThing = 0;
	float timer = 5;
	float moveSpeed = 1;
	float detectionRange = 0.01f;
	bool teleportAtDetectionRange = false;
	public Animator2D animator;
	public GameObject bullet;
	FlightGameManager gameManager;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager").GetComponent<FlightGameManager>();
		StartCoroutine(MainScript());	
	}
	
	IEnumerator MainScript() {
		yield return StartCoroutine (MoveTo(0.5f,0.5f));
		yield return StartCoroutine (Idle(2,"attack"));
		Shoot (bullet);
		yield return StartCoroutine (Idle(2));
		yield return StartCoroutine (MoveTo(1,1));
	}
	
	IEnumerator Idle(float time, string animation="idle") {
		animator.PlayAnimation(animation);
		timer = time;
		while (timer > 0) {
			timer -= Time.deltaTime;
			yield return null;
		}
	}
	
	IEnumerator MoveTo(float x, float y) {
		Debug.Log(gameManager.worldBounds.extents.x);
		x = x*gameManager.worldBounds.extents.x*2 - gameManager.worldBounds.extents.x;
		y = y*gameManager.worldBounds.extents.z*2 - gameManager.worldBounds.extents.z;
		animator.PlayAnimation("walk");
		while (new Vector2 (x-transform.position.x, y-transform.position.z).sqrMagnitude > detectionRange) {
			Vector2 direction = new Vector2 (x-transform.position.x, y-transform.position.z);
			direction /= direction.magnitude;
			Vector2 shift = direction * moveSpeed * Time.deltaTime;
			transform.position += new Vector3 (shift.x, 0, shift.y);
			yield return null;
		}
		if (teleportAtDetectionRange) {
			transform.position = new Vector3 (x, 0, y);
		}
		animator.PlayAnimation("idle");
	}
	
	GameObject Shoot(GameObject projectilePrefab) {
		return (GameObject) Instantiate(projectilePrefab, transform.position, transform.rotation);
	}
	

}
