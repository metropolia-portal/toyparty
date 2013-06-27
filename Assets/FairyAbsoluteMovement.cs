using UnityEngine;
using System.Collections;

public class FairyAbsoluteMovement : MonoBehaviour {
	
	public Vector2 startPosition;
	public Vector2 endPosition;
	public float speed;

	// Use this for initialization
	void Start () {
		transform.position = new Vector3(startPosition.x, 0, startPosition.y);
	}
	
	void FixedUpdate() {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
