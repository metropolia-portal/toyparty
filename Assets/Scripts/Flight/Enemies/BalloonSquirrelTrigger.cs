using UnityEngine;
using System.Collections;

public class BalloonSquirrelTrigger : MonoBehaviour {
	
	FlightGameManager gameManager;
	public float triggerWidth = 2;
	public float triggerHeight = 2;
	public float delay = 3;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find("GameManager").GetComponent<FlightGameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if (delay > 0) delay -= Time.deltaTime;
		else 
		if (Mathf.Abs(gameManager.GetDragon().transform.position.x - transform.position.x) < triggerWidth || Mathf.Abs(gameManager.GetDragon().transform.position.z - transform.position.z) < triggerHeight) {
			GetComponent<Death>().Die();
		}
	}
}
