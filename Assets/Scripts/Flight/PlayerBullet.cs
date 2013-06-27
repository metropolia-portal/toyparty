using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour {
	
	public float forwardSpeed = 0.5f;
	public float life = 5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.right * forwardSpeed;
		life -= Time.deltaTime;
		if (life < 0) Destroy(gameObject);
	}
	
	public void Damage() {
		Destroy(gameObject);
	}
}
