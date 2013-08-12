using UnityEngine;
using System.Collections;

public class GameGUI : MonoBehaviour {
	CandyWizardGameManager candyScript;
	
	public bool enableSpeedup = true;
	
	public Texture speedupSpriteSheet;
	public int speedupSpritesAmount = 7;
	
	public Texture selectionSpriteSheet;
	public int selectionSpritesAmout = 3;
	public float selectionAnimationFPS = 2;
	
	public float flaskHeightToScreenHeightRation = 0.1f;
	public float flaskXRelativePos = 0.8f; //relative to screen width
	public float flaskYRelativePos = 0.2f; //relative to screen height
	
	public float selectionHeightToScreenHeightRation = 0.1f;
	public float selectionXRelativePos = 0.8f; //relative to screen width
	public float selectionYRelativePos = 0.2f; //relative to screen height
	
	bool speedupOn = false;
	float speedupLeft = 1f;
	
	float selectionTime = 0;
	
	void Start(){
		candyScript = GameObject.Find ("GameManager").GetComponent<CandyWizardGameManager>();
	}
	
	// from 0 to 1;
	public void SetSpeedupLeft(float val) {
		speedupLeft = val;
	}
	
	void OnGUI() {
		if(enableSpeedup) {
			//box dimensiont is 1/6 of button dimension
			
			float flaskHeight = Screen.height * flaskHeightToScreenHeightRation;
			float flaskWidth = flaskHeight * (speedupSpriteSheet.width /(float) speedupSpritesAmount) / speedupSpriteSheet.height;
			
			GUI.BeginGroup(new Rect( Screen.width * flaskXRelativePos, Screen.height * flaskYRelativePos,flaskWidth ,flaskHeight)); 
			
			//we should take size of the whole sprite, so that one frame dimension is our needed height and width
			float buttonWidth = flaskWidth * speedupSpritesAmount; 
			
			if (GUI.Button(new Rect( - ( (int)( (1 - speedupLeft) * (speedupSpritesAmount - 1))) * flaskWidth, 0, buttonWidth,  buttonWidth), speedupSpriteSheet, MGUI.NoStyle)) {
				speedupOn = !speedupOn;
				candyScript.SetSpeedUpBrushActive(speedupOn);
			}
		
			GUI.EndGroup();
			
			if(speedupOn) {
				if(selectionTime == 0)
					selectionTime = Time.timeSinceLevelLoad;
				
				int currFrame =  (int) ((Time.timeSinceLevelLoad - selectionTime) * selectionAnimationFPS) % selectionSpritesAmout;
				
				GUI.DrawTextureWithTexCoords(
					new Rect(
						Screen.width * selectionXRelativePos, 
						Screen.height * selectionYRelativePos, 
						Screen.height * selectionHeightToScreenHeightRation * selectionSpriteSheet.width /  selectionSpriteSheet.height / selectionSpritesAmout,
						Screen.height * selectionHeightToScreenHeightRation ),
					selectionSpriteSheet, new Rect((float) currFrame / selectionSpritesAmout, 0f, 1f / selectionSpritesAmout, 1f),true);
			} else
				selectionTime = 0;	
		}
	}

}
