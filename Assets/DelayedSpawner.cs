using UnityEngine;
using System.Collections;

public class DelayedSpawner : MonoBehaviour {
	
	public int charges = 1;
	public float delay = 1f;
	public float cycle = 1f;
	public GameObject prefab;
	float timeLeft;
	

	// Use this for initialization
	void Start () {
		timeLeft = cycle;
	}
	
	// Update is called once per frame
	void Update () {
		if (delay>0) {
			delay -= Time.deltaTime;
			return;
		}
		
		timeLeft -= Time.deltaTime;
		if (timeLeft<=0) {
			timeLeft = cycle;
			Instantiate(prefab, transform.position, transform.rotation);
			charges --;
			if (charges == 0) {
				Destroy(gameObject);
			}
		}
	}
}
