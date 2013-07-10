using UnityEngine;
using System.Collections;

public class Particle : MonoBehaviour {
	
	public float life = 3f;
	public float spin = 0.5f;
	public float speed = 0.5f;
	float rup;
	float rright;

	// Use this for initialization
	void Start () {
		rup = Random.Range(0.5f,1.5f);
		rright = Random.Range(0.5f,1.5f);
		transform.rotation = Quaternion.Euler(0, Random.Range(0, 360),0);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += (Vector3.right*rup+Vector3.forward*rright)*speed*Time.deltaTime;
		transform.rotation *= Quaternion.Euler(0, spin, 0);
		speed -= Time.deltaTime*0.5f;
		if (speed <=0) Destroy(gameObject);
	}
}
