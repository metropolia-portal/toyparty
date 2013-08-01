using UnityEngine;
using System.Collections;

public class DelayedSpawner : MonoBehaviour {
	
	public GameObject item;
	public float delayDefault = 0;
	public float delayRandom = 0;
	public int charges = 1;
	public bool activeScript = true;
	
	float delay;

	// Use this for initialization
	void Start () {
		delay = delayDefault + Random.Range(0f, delayRandom);
	}
	
	// Update is called once per frame
	void Update () {
		if (!activeScript) return;
		delay -= Time.deltaTime;
		if (delay <=0) {
			charges --;
			((GameObject)Instantiate(item, transform.position, transform.rotation)).transform.parent = transform;
			if (charges == 0) activeScript = false;
			delay = delayDefault + Random.Range(0f, delayRandom);
		}
	
	}
}
