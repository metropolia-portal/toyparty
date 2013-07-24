using UnityEngine;
using System.Collections;

public class GameGUI : MonoBehaviour {
	
	public bool enableSpeedup = true;
	public Texture speedupSpriteSheet;
	public int sprites = 6;
	public float flaskHeightToScreenHeightRation = 0.1f;
	public float flaskButtonHeighToTextureHeightRation = 0.1f;
	
	bool speedupOn = false;
	float speedupLeft = 1f;
	
	// from 0 to 1;
	public void SetSpeedupLeft(float val) {
		speedupLeft = val;
	}
	
	void OnGUI() {
		if(enableSpeedup) {
			//box dimensiont is 1/6 of button dimension
			
			float flaskHeight = Screen.height * flaskHeightToScreenHeightRation;
			float flaskWidth = flaskHeight * (speedupSpriteSheet.width /(float) sprites) / speedupSpriteSheet.height; 
			
			//we should take size of the whole sprite, so that one frame dimension is our needed height and width
			float buttonWidth = flaskWidth * sprites; 
			
			GUI.BeginGroup(new Rect(50,40,flaskWidth ,flaskHeight)); 
			
		//	first argument - offset of target frame
			print(( (int)( (1 - speedupLeft) * sprites)));
			if (GUI.Button(new Rect( - ( (int)( (1 - speedupLeft) * (sprites-1))) * flaskWidth, 0, buttonWidth,  buttonWidth), speedupSpriteSheet, MGUI.NoStyle)) {
				speedupOn = !speedupOn;
				CandyWizardGameManager.Instance().SetSpeedUpBrushActive(speedupOn);
			}
		
			GUI.EndGroup();
		}
	}
}
