using UnityEngine;
using System.Collections;

public abstract class Sphere : MonoBehaviour {

	// The sphere moves in 2d space at a constant speed
	public float speed = 3;
#if UNITY_EDITOR
	public float testingSpeed = 10;
#endif
	public bool testingEnabled = true;
	
	public float velocityFixZRation = 0.1f;// Fixes the speed by changing z pos by velocityFixZRation*speed
	
	public GameObject explodeEffect;
	
	//TODO move it to Main Sphere
	protected ScoreManager gameScore;
	
	// This method is called when the sphere leaves the level bounds and collides with the KillZone object
	protected abstract void OnSphereLost();
	
	// Modify the direction of the sphere without of changing the speed
	//TODO put this method inside Launch, as it is used only there
	public void setDirection(Vector2 direction) {
		direction.Normalize();
		transform.rigidbody.isKinematic = false; // "Unfreeze" the sphere
		transform.rigidbody.velocity =  new Vector3(direction.x * speed, 0,  direction.y * speed);
	}
	
	public void Launch() { // launches the sphere forward from its current position
		setDirection(Vector2.up);
	}
	
	// Freeze the sphere by disabling physics for it
	public void Freeze() {
		transform.rigidbody.isKinematic = true;
	}
	
	//explodes spheres at the end of the game
	public void Explode() {
		gameObject.SetActive(false);
		Instantiate(explodeEffect, transform.position, Quaternion.identity);
	}
	
	protected virtual void Start() {
#if UNITY_EDITOR
		if(testingEnabled) speed = testingSpeed;
#endif		
		gameScore = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
	}
	

	protected virtual void FixedUpdate () {
		if (transform.rigidbody.isKinematic) return;
			
		transform.rigidbody.velocity = transform.rigidbody.velocity.normalized * speed; // This preserves the sphere's speed
	
		RaycastHit hit;
		//TODO check that  speed * Time.fixedDeltaTime is reliable check on a phone
		if (rigidbody.SweepTest (rigidbody.velocity, out hit, speed * Time.fixedDeltaTime)) { // SweepTest searches for objects in front of the sphere
			//on each collision we will add sort of gravity force, so that there will be no endless reflection situation and ball will return back faster
			rigidbody.velocity = new Vector3(rigidbody.velocity.x,rigidbody.velocity.y, rigidbody.velocity.z - velocityFixZRation * speed);
			rigidbody.velocity  = rigidbody.velocity.normalized * speed; //fix the speed
			
			//Debug.DrawRay(transform.position, rigidbody.velocity * 5, Color.blue,1);	
			// We want to detect and freeze bricks right before the sphere collides with them
			// By doing so, we ensure that bricks don't get pushed away by the sphere, but can still abide by the laws of physics (fall down) when the sphere is away
            SphereCollision(hit); 		
        }
	}	
	
	virtual protected void SphereCollision(RaycastHit hit) {
		// However, the only case we actually want to handle is the Brick collision
		// In all other situations, Unity's built-in physics engine is sufficient
		if(hit.collider.CompareTag("Brick")) { 
			//print ("brick hit!");
			Brick brick =  hit.collider.GetComponent<Brick>();
	       	brick.OnHit();	
//			gameScore.OnSphereScore(brick.GetScore());
    	}
		
//		if(hit.collider.CompareTag("Paddle"))
//			ReleaseComboScore();	
	}

	protected virtual void OnTriggerEnter(Collider other) {		
    	if(other.CompareTag("DeathZone")) {
//			ReleaseComboScore(); 
			OnSphereLost();
		}
	}
	
	void Update() {
		AnimateRotate();
	}  
	
	Vector3 eulerAngleVelocity = -Vector3.right * 100;
	float rotationCoeff = Mathf.PI;

	void AnimateRotate() {
		transform.Rotate(new Vector3(rigidbody.velocity.z, 0, -rigidbody.velocity.x) * rotationCoeff,  Space.World);
	}

	//releases accumulated combo score to score manager when combo is lost
	//100, 400, 1000, (x + box) * 2 

}
