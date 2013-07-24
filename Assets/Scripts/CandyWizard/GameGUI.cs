using UnityEngine;
using System.Collections;

public class GameGUI : MonoBehaviour {
	
	public bool enableSpeedup = true;
	public Texture speedupSpriteSheet;
	
	bool speedupOn = false;
	float speedupLeft = 1f;
	
	void SetSpeedupLeft(float val) {
		speedupLeft = val;
	}
	
	void OnGUI() {
		if(enableSpeedup) {
			bool newVal = GUI.Toggle(new Rect(Screen.width - MGUI.menuButtonWidth, Screen.height/5, MGUI.menuButtonWidth, MGUI.menuButtonWidth),speedupOn, "Speedup glue");
			if(newVal!= speedupOn) {
				speedupOn = newVal;
				CandyWizardGameManager.Instance().SetSpeedUpBrushActive(speedupOn);
			}
		}
	}
}
