using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {
	
	float life = 0.5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		life -= Time.deltaTime;
		if (life < 0) Destroy (gameObject);
	}
}
