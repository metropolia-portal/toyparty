using UnityEngine;
using System.Collections;

public class MovingBrick : MonoBehaviour {
	
	public GameObject movementBounds;
	public float movementSpeed = 20f;
	public float maxRange = 0.1f;
	
	bool moveToRight = true;
	float leftBound;
	float rightBound;
	// Use this for initialization
	void Start () {
		float width = movementBounds.transform.localScale.x;
		leftBound  = movementBounds.transform.position.x - width/2;
		rightBound = leftBound + width;
	}
	
	// Update is called once per frame
	void Update () {
		float target = leftBound;
		if(moveToRight) target = rightBound;
		
		if(Mathf.Abs((float) transform.position.x - target) < maxRange) moveToRight = ! moveToRight;
		
		transform.position = new Vector3( Mathf.Lerp( transform.position.x, target, Time.deltaTime ), transform.position.y, transform.position.z );
	}
}
