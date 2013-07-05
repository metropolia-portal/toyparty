using UnityEngine;
using System.Collections;

public class ThreeFairySpawner : MonoBehaviour {
	
	public GameObject fairyPrefab;
	public int fairyCount = 3;
	public float fairyDelayAvg = 1f;
	public float positionShift = 1;
	float fairyDelay;

	// Use this for initialization
	void Start () {
		//fairyCount = 3;
		fairyDelay = fairyDelayAvg * Random.Range(0.9f,1.1f);
	}
	
	// Update is called once per frame
	void Update () {
		fairyDelay -= Time.deltaTime;
		if (fairyDelay < 0) {
			fairyCount --;
			Vector3 randomPos = new Vector3 (Random.Range(0f,1f), 0, Random.Range(0f,1f)) * positionShift; 
			Instantiate(fairyPrefab, transform.position + randomPos, transform.rotation);
			if (fairyCount <= 0) Destroy(gameObject);
			fairyDelay = fairyDelayAvg * Random.Range(0.8f,1.2f);
		}
	}
}
