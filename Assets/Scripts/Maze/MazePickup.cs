using UnityEngine;
using System.Collections;

public class MazePickup : MonoBehaviour {

	public Material graphic;
	public int score = 0;
	public int count = 1;
	public string type;
	
	PickupInfo pickupInfo = new PickupInfo();
	
	PickupManager pickupManager;
	

	// Use this for initialization
	void Start () {
		pickupManager = GameObject.Find("GameManager").GetComponent<PickupManager>();
		pickupInfo.graphic = graphic;
		pickupInfo.count = count;
		pickupInfo.score = score;
		pickupInfo.type = type;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void OnPickup() {
		pickupManager.OnAcquirePickup(pickupInfo);
		Destroy(gameObject);
	}
	
	void OnTriggerEnter(Collider other) {
		
		if (other.gameObject.tag == "Player") {
			OnPickup();
		}
	}
}
