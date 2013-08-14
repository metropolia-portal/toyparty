using UnityEngine;
using System.Collections;

public class FlightEnemySpawner : MonoBehaviour {
	public GameObject enemy;	
	public float startTime = 0;
	public float spawnDelay = 1;
	public float endTime = 1;
	public float charges = 0;
	public float spawnDelayRandom = 0;
	public Bounds position;

	
	float timeLeft;

	// Use this for initialization
	void Start () {
		timeLeft = startTime;	
	}
	
	// Update is called once per frame
	void Update () {

			timeLeft -= Time.deltaTime;
			if (timeLeft <=0) {
				timeLeft = spawnDelay + Random.Range(-spawnDelayRandom, +spawnDelayRandom);
				Vector3 pos = new Vector3 (Random.Range(position.min.x,position.max.x), transform.position.y, Random.Range(position.min.z,position.max.z));
				Instantiate(enemy, pos, transform.rotation);
			}					
			
		if (endTime > 0) {
			endTime -= Time.deltaTime;
			if (endTime <=0) enabled = false;
		}
	}
}
