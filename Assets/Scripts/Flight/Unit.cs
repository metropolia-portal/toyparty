using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {
	
	
	public int life = 5;
	public int score = 1;	
	int thatOtherThing = 0;
	float timer = 5;
	float moveSpeed = 1;
	float detectionRange = 0.01f;
	bool teleportAtDetectionRange = false;
	public Animator2D animator;
	public GameObject bullet;
	public float invulnurabilityTime = 1.5f;
	FlightGameManager gameManager;
	float invulTimeLeft = 0;
	public GameObject fallingObject;	

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager").GetComponent<FlightGameManager>();
		StartCoroutine(MainScript());	
		transform.position = new Vector3(transform.position.x*gameManager.cam.aspect,transform.position.y,transform.position.z);
	}
	
	IEnumerator MainScript() {
		yield return MoveTo(0.5f,0.5f);
		yield return Idle(2,"attack");
		Shoot (bullet);
		yield return Idle(2);
		yield return MoveTo(1,1);
	}
	
	Coroutine Idle(float time, string animation="idle") {
		return StartCoroutine(CoroutineIdle(time, animation));
	}
	
	Coroutine MoveTo(float x, float y) {
		return StartCoroutine(CoroutineMoveTo(x, y));
	}
	
	Coroutine MoveTo(Vector2 point) {
		return StartCoroutine(CoroutineMoveTo(point.x, point.y));
	}
	
	Coroutine MoveTo(Vector3 point) {
		return StartCoroutine(CoroutineMoveTo(point.x, point.z));
	}
		
	
	IEnumerator CoroutineIdle(float time, string animation="idle") {
		animator.PlayAnimation(animation);
		timer = time;
		while (timer > 0) {
			timer -= Time.deltaTime;
			yield return null;
		}
	}
	
	IEnumerator CoroutineMoveTo(float x, float y) {
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
	
		void Die() {
		
		Destroy(gameObject);
		Instantiate(fallingObject, transform.position, Quaternion.identity);
	}
	
	void FixedUpdate() {
		if (gameManager.IsOutside(transform.position*0.3f)) Destroy(gameObject);	
		if (invulTimeLeft > 0) { 
			invulTimeLeft -= Time.fixedDeltaTime;
			int phase = ((int)((invulTimeLeft*20)%10))%2;
			if (phase == 0) {
				animator.gameObject.SetActive(false);
			} else {
				animator.gameObject.SetActive(true);
			}
		} else {
			animator.gameObject.SetActive(true);
		}
	}
	
	void Damage() {
		if (invulTimeLeft > 0) return;
		invulTimeLeft = invulnurabilityTime;
		life --;
		if (life<=0) {
			gameManager.OnFairyDeath(score*2);
			Die();
		}	
	}
	
	
	
	
	
	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("PlayerBullet")) {
			other.GetComponent<FlightPlayerBullet>().Damage();
			Damage ();

		} else if (other.CompareTag("Player")) {
			gameManager.PlayerDamage(1);
		} 
	}
	

}
