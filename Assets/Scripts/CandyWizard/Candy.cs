using UnityEngine;
using System.Collections;

public class Candy : MonoBehaviour {
	//drops the candy
	public void Drop() {
		rigidbody.isKinematic = false;
		rigidbody.useGravity = true
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
