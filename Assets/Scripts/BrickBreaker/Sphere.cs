using UnityEngine;
using System.Collections;

public abstract class Sphere : MonoBehaviour {

	// The sphere moves in 2d space at a constant speed
	public float speed = 20;
	
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
	

	protected virtual void FixedUpdate () {
		if (transform.rigidbody.isKinematic) return;
			
		transform.rigidbody.velocity = transform.rigidbody.velocity.normalized * speed; // This preserves the sphere's speed
	
		RaycastHit hit;
		//TODO check that  speed * Time.fixedDeltaTime is reliable check on a phone
		if (rigidbody.SweepTest (transform.rigidbody.velocity, out hit, speed * Time.fixedDeltaTime)) { // SweepTest searches for objects in front of the sphere
			
			
			// if(IsSphereIdleLoop());
			//Debug.DrawRay(transform.position, rigidbody.velocity * 5, Color.blue,1);	
            SphereCollision(hit); // We want to detect and freeze bricks right before the sphere collides with them
			// By doing so, we ensure that bricks don't get pushed away by the sphere, but can still abide by the laws of physics (fall down) when the sphere is away
        }
	}	
	
//	bool IsSphereIdleLoop() {
//		Vector3 backDir = rigidbody.velocity;
//		backDir.x *= -1;
//		if(	Vector3. rigidbody.velocity.
//			Physics.Raycast(transform.position, rigidbody.velocity, LayerMask.NameToLayer("Bricks")) && 
//			Physics.Raycast(transform.position, backDir, LayerMask.NameToLayer("Bricks")) 	 
//	}
	// This is called from the Update method to handle different collision situations
	void SphereCollision(RaycastHit hit) {
		// However, the only case we actually want to handle is the Brick collision
		// In all other situations, Unity's built-in physics engine is sufficient
		if(hit.collider.CompareTag("Brick")) { 
	        hit.collider.GetComponent<Brick>().OnHit();	
    	}
	}
	
//	void OnCollisionExit(Collision collision) {
//		if(collision.collider.CompareTag("Brick")) {
//			collision.collider.GetComponent<Brick>().OnHitEnd();		
//		}
//	}

	protected virtual void OnTriggerEnter(Collider other) {		
    	if(other.gameObject.name == "DeathZone")	
			OnSphereLost();
	}
	
	void OnCollisionEnter( Collision collision) {
		//Debug.DrawRay(transform.position, rigidbody.velocity * 5, Color.red,1);	
	}
		
}
