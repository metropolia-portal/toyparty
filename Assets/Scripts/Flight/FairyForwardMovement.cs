using UnityEngine;
using System.Collections;

public class FairyForwardMovement : MonoBehaviour {
	
	public float speed = 0.03f;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += -transform.right*speed;
	}
}
