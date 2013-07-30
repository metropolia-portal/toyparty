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
	
	Transform tr;
	Rigidbody rig;
	SphereSounds soundScript;
	// Modify the direction of the sphere without of changing the speed
	//TODO put this method inside Launch, as it is used only there
	public void setDirection(Vector2 direction) {
		rig.isKinematic = false; // "Unfreeze" the sphere
		direction.Normalize();
		rig.velocity =  new Vector3(direction.x * speed, 0,  direction.y * speed);
	}
	
	public void Launch() { // launches the sphere forward from its current position
		setDirection(Vector2.up);
	}
	
	// Freeze the sphere by disabling physics for it
	public void Freeze() {
		rig.isKinematic = true;
		tr.localRotation = Quaternion.identity; //to reset marble rotation
	}
	
	//explodes spheres at the end of the game
	public void Explode() {
		gameObject.SetActive(false);
		Instantiate(explodeEffect, tr.position, Quaternion.identity);
	}
	
	protected virtual void Awake() {
		tr = GetComponent<Transform>();
		rig = GetComponent<Rigidbody>();
		if(rig == null)
		{
			rig = gameObject.AddComponent<Rigidbody>();
		}
		soundScript = GetComponent<SphereSounds>();
		if(soundScript == null)
		{
			soundScript = gameObject.AddComponent<SphereSounds>();
		}
#if UNITY_EDITOR
		if(testingEnabled) speed = testingSpeed;
#endif		
		gameScore = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
	}
	

	protected virtual void FixedUpdate () {
		if (rig.isKinematic) return;
			
		rig.velocity = rig.velocity.normalized * speed; // This preserves the sphere's speed
	
		RaycastHit hit;
		//TODO check that  speed * Time.fixedDeltaTime is reliable check on a phone
		if (rig.SweepTest (rig.velocity, out hit, speed * Time.fixedDeltaTime)) { // SweepTest searches for objects in front of the sphere
			rig.velocity  = rig.velocity.normalized * speed; //fix the speed
			
			// We want to detect and freeze bricks right before the sphere collides with them
			// By doing so, we ensure that bricks don't get pushed away by the sphere, but can still abide by the laws of physics (fall down) when the sphere is away
            SphereCollision(hit); 
			
        }
		// if the velocity of the ball is too close to horizontal we apply a little push down
		float angle = Mathf.Abs(Vector3.Dot(rig.velocity.normalized,Vector3.right));
		if(angle > 0.95){
			Vector3 vel = rig.velocity;
			vel.z -= velocityFixZRation * speed;
			rig.velocity = vel;
		}
	}	
	
	virtual protected void SphereCollision(RaycastHit hit) {
		if(hit.collider.CompareTag("Brick")) { 
			Brick brick =  hit.collider.GetComponent<Brick>();
	       	brick.OnHit();	
			
    	}
		else{
			Vector3 velocity = rig.velocity;
			velocity = velocity - 2 * hit.normal * Vector3.Dot(velocity, hit.normal);
			rig.velocity = velocity;
		}
		soundScript.PlaySound(hit.collider.tag);
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
		if(rig.velocity.magnitude > 0) tr.Rotate(new Vector3(rig.velocity.z, 0, -rig.velocity.x) * rotationCoeff,  Space.World);
	}

	//releases accumulated combo score to score manager when combo is lost
	//100, 400, 1000, (x + box) * 2 

}
