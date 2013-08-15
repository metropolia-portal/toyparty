using UnityEngine;
using System.Collections;

public class HoverSquirrel : MonoBehaviour {
	float speed = 0;
	float acceleration = 2;
	float fallSpeed = 0.5f;
	float speedCap = 5;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (speed < speedCap)
			speed += acceleration * Time.deltaTime;
		if (speed > speedCap) 
			speed = speedCap;
		transform.position += Vector3.left * speed * Time.deltaTime + Vector3.back * fallSpeed * Time.deltaTime;
	}
}
