using UnityEngine;
using System.Collections;

public class MazeTrap : MonoBehaviour {
	
	bool activeMouse = true;
	
	float holdTime = 3;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!activeMouse) {
			holdTime -= Time.deltaTime;
			if (holdTime <= 0){
				GameObject.Find("Mouse").GetComponent<Mouse>().SetSpeedModifier(1);
				Destroy(gameObject);
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		if (!activeMouse) return;
		if (other.gameObject.tag == "Player") {
			GameObject.Find("GameManager").GetComponent<MazeGameManager>().OnTrap();
			GameObject.Find("Mouse").GetComponent<Mouse>().SetSpeedModifier(0);		
			activeMouse = false;
		}
	}	
	
}
