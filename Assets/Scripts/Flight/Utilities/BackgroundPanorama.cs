using UnityEngine;
using System.Collections;

public class BackgroundPanorama : MonoBehaviour {
	
	public float scrollingSpeed = 1f;
	float w;

	// Use this for initialization
	void Start () {
		w = 30;
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition += Vector3.left * Time.deltaTime * scrollingSpeed;
		while (transform.localPosition.x < -w) 
			transform.localPosition += Vector3.right * w;
	}
}
