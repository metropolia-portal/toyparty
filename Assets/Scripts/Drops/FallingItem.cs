using UnityEngine;
using System.Collections;

public class FallingItem : MonoBehaviour {
	
	public bool wave = false;
	
	float fallSpeed = 1;
	
	float startingX;
	
	
	public float rotationSpeed = 180;
	
	float phase = 0;
	
	public void SetFallSpeed(float s) {
		fallSpeed = s;
	}
	
	// Use this for initialization
	void Start () {
		DropsGameManager gameManager = GameObject.Find("GameManager").GetComponent<DropsGameManager>();
		transform.position = new Vector3(Random.Range(-gameManager.maxDistanceFromCenter,gameManager.maxDistanceFromCenter), 1, 4.5f);
		startingX = transform.position.x;
		transform.localScale = transform.localScale*Random.Range(0.8f, 1.2f);
		rotationSpeed = rotationSpeed * Random.Range(-1f,1f);
		Vector3 newPosition = transform.position;
		transform.rotation = Quaternion.Euler(0, phase*rotationSpeed, 0);
		
		newPosition += Vector3.back * fallSpeed * Time.deltaTime;
		if (wave) {
			newPosition.x = startingX + Mathf.Sin(transform.position.z);
		}
		
		transform.position = newPosition;
	}
	
	// Update is called once per frame
	void Update () {
		phase += Time.deltaTime;
		Vector3 newPosition = transform.position;
		transform.rotation = Quaternion.Euler(0, phase*rotationSpeed, 0);
		
		newPosition += Vector3.back * fallSpeed * Time.deltaTime;
		if (wave) {
			newPosition.x = startingX + Mathf.Sin(transform.position.z);
		}
		
		transform.position = newPosition;
		if (transform.position.z<-5.5f) 
			Destroy(gameObject);
	}
}
