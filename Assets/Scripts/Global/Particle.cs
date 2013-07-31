using UnityEngine;
using System.Collections;

public class Particle : MonoBehaviour {
	
	public float life = 3f;
	public float spin = 0.5f;
	public float speed = 0.5f;
	public bool randomRotation = true;
	Vector3 direction;

	// Use this for initialization
	void Start () {
		direction =  Quaternion.Euler(0, Random.Range(0,360),0) * Vector3.forward;
		transform.rotation = Quaternion.Euler(0, Random.Range(0, 360),0);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += direction*speed*Time.deltaTime;
		transform.rotation *= Quaternion.Euler(0, spin, 0);
		speed -= Time.deltaTime*0.5f;
		life -= Time.deltaTime;
		if (speed <=0) speed = 0;
		if (life <=0) Destroy(gameObject);
	}
}
