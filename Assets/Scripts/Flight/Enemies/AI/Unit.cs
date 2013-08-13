using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {
	
	
	public int life = 9;
	public int score = 1;
	
	public float invulnurabilityTime = 1.5f;
	
	public Animator2D animator;
	public GameObject bullet;
	public GameObject fallingObject;	
	public Vector3 TurretPosition;
	
	

	float moveSpeed = 1;
	float detectionRange = 0.01f;
	bool teleportAtDetectionRange = false;
	
	protected bool interrupt = false;	
	
	protected bool interruptOnDamage = false;
	protected int interruptWhenHealthEquals = 0;
	protected int healthAtLastInterrupt = 0;
	protected int interruptWhenHealthLoweredBy = -1;

	FlightGameManager gameManager;
	float invulTimeLeft = 0;
	float timer;	


	

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager").GetComponent<FlightGameManager>();
		StartCoroutine(MainScript());	
		transform.position = new Vector3(transform.position.x*gameManager.cam.aspect,transform.position.y,transform.position.z);
		healthAtLastInterrupt = life;
	}
	
	protected Vector2 FindDragonPosition() {
		return new Vector2 (0.5f + gameManager.GetDragon().transform.position.x / (gameManager.worldBounds.extents.x*2), 0.5f + gameManager.GetDragon().transform.position.z / (gameManager.worldBounds.extents.z*2));
	}
	
	protected virtual IEnumerator MainScript() {
		Debug.Log("ha@");
		return null;
	}
	
	protected void SetInterruptWhenHealthLoweredBy(int treshold) {
		interruptWhenHealthLoweredBy = treshold;
		healthAtLastInterrupt = life;
	}
	 
	protected void SetMovementSpeed (float speed) {
		moveSpeed = speed;
	}
	/// <summary>
	/// Idle the specified time and animation.
	/// </summary>
	/// <param name='time'>
	/// Time.
	/// </param>
	/// <param name='animation'>
	/// Animation.
	/// </param>
	protected Coroutine Idle(float time, string animation="idle") {
		return StartCoroutine(CoroutineIdle(time, animation));
	}
	
	protected Coroutine MoveTo(float x, float y) {
		return StartCoroutine(CoroutineMoveTo(x, y));
	}
	
	protected Coroutine MoveTo(Vector2 point) {
		return StartCoroutine(CoroutineMoveTo(point.x, point.y));
	}
	
	protected Coroutine MoveTo(Vector3 point) {
		return StartCoroutine(CoroutineMoveTo(point.x, point.z));
	}
		
	
	protected IEnumerator CoroutineIdle(float time, string animation="idle") {
		interrupt = false;
		animator.PlayAnimation(animation);
		timer = time;
		while (timer > 0) {
			timer -= Time.deltaTime;
			yield return null;
			if (interrupt) {
				break;
			}
		}
	}
	
	protected IEnumerator CoroutineMoveTo(float x, float y) {
		interrupt = false;
		x = x*gameManager.worldBounds.extents.x*2 - gameManager.worldBounds.extents.x;
		y = y*gameManager.worldBounds.extents.z*2 - gameManager.worldBounds.extents.z;
		animator.PlayAnimation("walk");
		while (new Vector2 (x-transform.position.x, y-transform.position.z).sqrMagnitude > detectionRange) {
			Vector2 direction = new Vector2 (x-transform.position.x, y-transform.position.z);
			direction /= direction.magnitude;
			Vector2 shift = direction * moveSpeed * Time.deltaTime;
			transform.position += new Vector3 (shift.x, 0, shift.y);
			yield return null;
			if (interrupt) {
				break;
			}
		}
		if (teleportAtDetectionRange && !interrupt) {
			transform.position = new Vector3 (x, 0, y);
		}
		animator.PlayAnimation("idle");
	}
	
	protected GameObject Shoot(GameObject projectilePrefab) {
		return (GameObject) Instantiate(projectilePrefab, transform.position + TurretPosition, transform.rotation);
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
	
	protected void Damage() {
		if (invulTimeLeft > 0) return;
		invulTimeLeft = invulnurabilityTime;
		life --;
		if ((interruptOnDamage)||(interruptWhenHealthEquals == life)
			|| (life == healthAtLastInterrupt - interruptWhenHealthLoweredBy)) { 
			interrupt = true;
			healthAtLastInterrupt = life;
		}		
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
			other.GetComponent<Dragon>().Damage(1);
		} 
	}
	

}
