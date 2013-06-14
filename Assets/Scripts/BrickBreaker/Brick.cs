using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {	
	
	public int score = 100;//score for destroying the brick
	
	protected BrickGameManager manager;
	protected ScoreManager gameScore;
	
	bool isActive = false;
	
	
	public int GetScore() {
		return score;
	}
	
	public bool IsActive() {
		return isActive;
	}
	
	// Use this for initialization
	void Start () {
		manager = GameObject.Find("GameManager").GetComponent<BrickGameManager>();
		gameScore = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
		isActive = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	// This will actually happen right before the sphere collides with the brick
	public void OnHit() {			
		if (isActive) { // The brick will exist for 0.1 seconds after being hit and we don't want it to get hit for a second time
	
			gameObject.transform.rigidbody.isKinematic = true; // Freeze the brick so that the sphere doesn't push it on impact
			isActive = false;

			// Keep the object alive one frame more to let the sphere bounce off of it
			StartCoroutine(DestroyNextFrame());
		}
	}
	
	protected virtual IEnumerator DestroyNextFrame() {
		yield return new WaitForFixedUpdate (); // Skipping this fixed frame
		yield return new WaitForFixedUpdate (); // And waiting one more
	
		gameScore.AddScore(score);
		gameScore.ShowFloatingScore(score, transform.position);
		
		OnBrickDestroyed();
		Destroy(gameObject); 
	}
	
	protected virtual void OnBrickDestroyed() {
		manager.OnBrickDestroyed(transform.position);
	}
}
