using UnityEngine;
using System.Collections;

public class ItemGenerator : MonoBehaviour {
	
	public GameObject toyPrefab;
	public GameObject bombPrefab;
	
	public float bombRatio = 0.1f;
	public float itemDelay = 0.7f;
	public float itemSpawnAcceleration = 0.01f;
	public float fallSpeedAcceleration = 0.01f;
	
	public float fallSpeed = 1f;
	
	float delayToNextItem = 0;
	
	bool stopped = false;
	
	// Use this for initialization
	void Start () {
		stopped = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (stopped) return;
		delayToNextItem -= Time.deltaTime;
		if (delayToNextItem <=0) 
			GenerateItem();
	}
	
	public void Stop() {
		stopped = true;
	}
	
	void GenerateItem() {
		FallingItem newItem;
		float speed = fallSpeed * Random.Range(0.5f, 1.5f);
		
		if (Random.Range(0f,1f) > bombRatio)
			newItem = ((GameObject)Instantiate(toyPrefab)).GetComponent<FallingItem>();
		else
			newItem = ((GameObject)Instantiate(bombPrefab)).GetComponent<FallingItem>();
		
		newItem.SetFallSpeed(speed);
		
		delayToNextItem = 1f/speed + itemDelay * Random.Range(0.5f, 1.5f);
		itemDelay -= itemSpawnAcceleration;
		if (itemDelay <=0) itemDelay = 0;
		fallSpeed += fallSpeedAcceleration;
		
	}
}
