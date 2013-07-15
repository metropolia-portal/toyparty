using UnityEngine;
using System.Collections;

public class GameGUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	bool speedupOn = false;
	// Update is called once per frame
	void OnGUI() {
		bool newVal = GUI.Toggle(new Rect(Screen.width - MGUI.menuButtonWidth, Screen.height/5, MGUI.menuButtonWidth, MGUI.menuButtonWidth),speedupOn, "Speedup glue");
		if(newVal!= speedupOn) {
			speedupOn = newVal;
			CandyWizardGameManager.Instance().SetSpeedUpBrushActive(speedupOn);
		}
	}
}
