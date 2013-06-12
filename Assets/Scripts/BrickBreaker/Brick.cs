using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour 
{	
	
	BrickGameManager gameManager;
	bool isActive = false;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find("GameManager").GetComponent<BrickGameManager>();
		isActive = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	// This will actually happen right before the sphere collides with the brick
	public void OnHit() {			
		if (isActive) { // The brick will exist for 0.1 seconds after being hit and we don't want it to get hit for a second time
	
			gameObject.transform.rigidbody.isKinematic = true; // Freeze the brick so that the sphere doesn't push it on impact
			
			// Keep the object alive one frame more to let the sphere bounce off of it
			StartCoroutine(DestroyNextFrame());
		}
	}
	
	protected IEnumerator DestroyNextFrame() {
		yield return new WaitForFixedUpdate (); // Skipping this fixed frame
		yield return new WaitForFixedUpdate (); // And waiting one more
		
		isActive = false;
		
		gameManager.OnBrickDestroyed(transform.position);
		Destroy(gameObject); 
	}
}
