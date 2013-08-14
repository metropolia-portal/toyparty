using UnityEngine;
using System.Collections;

public class FallingEnemy : MonoBehaviour {
	
	public GameObject explosion;
	
	float rotationSpeed = 0.4f;
	float fallSpeed = 2;
	float rotationPhase = 0;
	FlightGameManager gameManager;
	

	// Use this for initialization
	void Start () {
		rotationPhase = Random.Range(0f, 1f);
		Instantiate(explosion, transform.position, /*Quaternion.Euler(0, Random.Range(0,360f),0)*/ Quaternion.identity);
		gameManager = GameObject.Find("GameManager").GetComponent<FlightGameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		rotationPhase += Time.deltaTime * rotationSpeed;
		if (rotationPhase > 1) rotationPhase --;
		transform.position += Vector3.back * Time.deltaTime * fallSpeed;
		transform.localRotation = Quaternion.Euler(0, rotationPhase*360, 0);
	}

	
	void FixedUpdate() {
		if (gameManager.IsOutside(transform.position*0.3f)) Destroy(gameObject);		
	}

	
}
