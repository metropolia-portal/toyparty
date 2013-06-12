using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PickupManager : MonoBehaviour {
	
	Dictionary<string, PickupInfo> acquiredPickups = new Dictionary<string, PickupInfo>();
	public GUIText statusText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		statusText.text = "";
		foreach(string key in acquiredPickups.Keys) {
			PickupInfo pickup = acquiredPickups[key];
			statusText.text += pickup.type + " x" + pickup.count.ToString() + "\n";
		}
		statusText.text += "--------------\nTotal score: "+TotalScore().ToString();
	}
	
	public int TotalScore() {
		int score = 0;
		foreach(string key in acquiredPickups.Keys) {
			score += acquiredPickups[key].score;
		}
		return score;
	}
	
	public void OnAcquirePickup(PickupInfo pickup) {
		if (acquiredPickups.ContainsKey(pickup.type)) {
			acquiredPickups[pickup.type].Add (pickup);
		}else{
			acquiredPickups[pickup.type] = pickup;
		}
	}
}
