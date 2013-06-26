using UnityEngine;
using System.Collections;

public class FlightGameManager : MonoBehaviour {
	
	public GameObject dragonPrefab;
	public GUIText statusLine;
	
	Dragon dragon;
	FlightGUI flightGUI;
	
	// Use this for initialization
	void Start () {
		GameObject dragonObject = (GameObject) Instantiate(dragonPrefab);
		flightGUI = GetComponent<FlightGUI>();
		dragon = dragonObject.GetComponent<Dragon>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 padData = flightGUI.GetPadDirection();
		
		Vector3 dragonShift = new Vector3 (padData.x, 0, -padData.y) * 0.1f;
			
		dragon.transform.position += dragonShift;
		
		statusLine.text = Input.touchCount.ToString() + "\n";
		for (int i=0; i< Input.touchCount; i++) {
			statusLine.text += Input.touches[i].position.ToString() +" "+Input.touches[i].phase.ToString()+"\n";
		}
			
	}
}
