using UnityEngine;
using System.Collections;

public class FallingItem : MonoBehaviour {
	
	float fallSpeed = 1;
	
	public float rotationSpeed = 180;
	
	float phase = 0;
	
	public void SetFallSpeed(float s) {
		fallSpeed = s;
	}
	
	// Use this for initialization
	void Start () {
		transform.position = new Vector3(Random.Range(-5f,5f), 1, 4.5f);
	}
	
	// Update is called once per frame
	void Update () {
		phase += Time.deltaTime;
		transform.rotation = Quaternion.Euler(0, phase*rotationSpeed, 0);
		
		transform.position += Vector3.back * fallSpeed * Time.deltaTime;
		if (transform.position.z<-5.5f) 
			Destroy(gameObject);
	}
}
