using UnityEngine;
using System.Collections;

public class PickupInfo {
	public Material graphic;
	public int score = 0;
	public int count = 1;
	public string type;
	
	public void Add (PickupInfo other) {
		score += other.score * other.count;
		count += other.count;
	}
}
