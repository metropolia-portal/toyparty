using UnityEngine;
using System.Collections;

public class ItemGenerator : MonoBehaviour {
	
	public GameObject toyPrefab;
	public GameObject bombPrefab;
	
	
	public float bombCountMax = 6;
	public float bombCountMin = 3;
	float bombDelay = 0;
	public float itemDelay = 0.7f;
	public float itemSpawnAcceleration = 0.01f;
	public float fallSpeedAcceleration = 0.01f;
	
	public float fallSpeed = 1f;
	
	float delayToNextItem = 0;
	bool stopped = false;
	int ironCount = 0;
	float timeLeft;
	float timeTotal;
	float toyRatio;
	
	DropsGameManager gameManager;
	
	// Use this for initialization
	void Start () {
		stopped = false;
		gameManager = GameObject.Find("GameManager").GetComponent<DropsGameManager>();
		timeTotal = gameManager.GetTimeLeft();		
		timeLeft = timeTotal;
		bombDelay = Random.Range(timeTotal/bombCountMax, timeTotal/bombCountMin);
	}
	
	// Update is called once per frame
	void Update () {
		if (stopped) return;
		if (bombDelay > 0) bombDelay -= Time.deltaTime;
		delayToNextItem -= Time.deltaTime;
		if (delayToNextItem <=0) 
			GenerateItem();
		timeLeft = gameManager.GetTimeLeft();
	}
	
	public void Stop() {
		stopped = true;
	}
	
	void GenerateItem() {
		FallingItem newItem;
		float speed = fallSpeed * Random.Range(0.5f, 1.5f);
		
		if (bombDelay > 0)
			newItem = ((GameObject)Instantiate(toyPrefab)).GetComponent<FallingItem>();
		else{
			
			newItem = ((GameObject)Instantiate(bombPrefab)).GetComponent<FallingItem>();
			ironCount += 1;	//A tally of how many irons fall to make sure at least 3 irons fall.
			Debug.Log(ironCount);
			bombDelay = Random.Range(timeTotal/bombCountMax, timeTotal/bombCountMin);
		}
		
		newItem.SetFallSpeed(speed);
		
		delayToNextItem = 1f/speed + itemDelay * Random.Range(0.5f, 1.5f);
		itemDelay -= itemSpawnAcceleration;
		if (itemDelay <=0) itemDelay = 0;
		fallSpeed += fallSpeedAcceleration;
		
	}
	
}
