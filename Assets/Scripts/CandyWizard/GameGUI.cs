using UnityEngine;
using System.Collections;

public class GameGUI : MonoBehaviour {
	CandyWizardGameManager gameManager;
	
	public bool enableSpeedup = true;
	public bool enableEraser = true;
	
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
	
	//TODO separate class for positioning, and method to return coords
	public Texture eraserTexture;
	public float eraserHeightToScreenHeightRation = 0.1f;
	public float eraserXRelativePos = 0.8f; //relative to screen width
	public float eraserYRelativePos = 0.2f; //relative to screen height
	
	public float eraserSelectionHeightToScreenHeightRation = 0.1f;
	public float eraserSelectionXRelativePos = 0.8f; //relative to screen width
	public float eraserSelectionYRelativePos = 0.2f; //relative to screen height
	
	
	
	bool speedupOn = false;
	float speedupLeft = 1f;
	
	bool eraserOn = false;
	
	float selectionTime = 0;
	float eraserSelectionTime = 0;
	
	// Variable for reloading
	public Texture2D reload;
	Rect reloadRect;
	void Start(){
		gameManager = GameObject.Find ("GameManager").GetComponent<CandyWizardGameManager>();
		float eraserHeight = Screen.height * eraserHeightToScreenHeightRation;
		float eraserWidth = eraserHeight * eraserTexture.width / eraserTexture.height;
		reloadRect = new Rect(eraserXRelativePos * Screen.width, eraserYRelativePos * Screen.height + 1.5f*eraserHeight, eraserWidth,  eraserHeight);
	}
	
	// from 0 to 1;
	public void SetSpeedupLeft(float val) {
		speedupLeft = val;
	}
	
	void OnGUI() {
		if(GUI.Button (reloadRect,reload,MGUI.NoStyle))
		{
			Vector3 vec = gameManager.GetStartPoint();
			gameManager.SetCandy(vec);
			enableSpeedup = true;
			enableEraser = true;
			gameManager.Start();
		}
		if(enableSpeedup) {
			//box dimensiont is 1/6 of button dimension
			
			float flaskHeight = Screen.height * flaskHeightToScreenHeightRation;
			float flaskWidth = flaskHeight * (speedupSpriteSheet.width /(float) speedupSpritesAmount) / speedupSpriteSheet.height;
			
			GUI.BeginGroup(new Rect( Screen.width * flaskXRelativePos, Screen.height * flaskYRelativePos,flaskWidth ,flaskHeight)); 
			
			//we should take size of the whole sprite, so that one frame dimension is our needed height and width
			float buttonWidth = flaskWidth * speedupSpritesAmount; 
			
			if (GUI.Button(new Rect( - ( (int)( (1 - speedupLeft) * (speedupSpritesAmount - 1))) * flaskWidth, 0, buttonWidth,  buttonWidth), speedupSpriteSheet, MGUI.NoStyle)) {
				speedupOn = !speedupOn;
				eraserOn = false;
				
				//TODO maybe notofications instead?
				gameManager.SetSpeedUpBrushActive(speedupOn);
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
		
		if(enableEraser) {
		
			float eraserHeight = Screen.height * eraserHeightToScreenHeightRation;
			float eraserWidth = eraserHeight * eraserTexture.width / eraserTexture.height;
		
			//TODO common methods for graphics
			
			if (GUI.Button(new Rect(eraserXRelativePos * Screen.width, eraserYRelativePos * Screen.height, eraserWidth,  eraserHeight), eraserTexture, MGUI.NoStyle)) {
					eraserOn = !eraserOn;
					speedupOn = false;
				
					gameManager.SetRubberBrushActive(eraserOn);
			}
			
						
			if(eraserOn) {
					if(eraserSelectionTime == 0)
						eraserSelectionTime = Time.timeSinceLevelLoad;
					
					int currFrame =  (int) ((Time.timeSinceLevelLoad - eraserSelectionTime) * selectionAnimationFPS) % selectionSpritesAmout;
					
					GUI.DrawTextureWithTexCoords(
						new Rect(
							Screen.width * eraserSelectionXRelativePos, 
							Screen.height * eraserSelectionYRelativePos, 
							Screen.height * eraserSelectionHeightToScreenHeightRation * selectionSpriteSheet.width /  selectionSpriteSheet.height / selectionSpritesAmout,
							Screen.height * eraserSelectionHeightToScreenHeightRation ),
						selectionSpriteSheet, new Rect((float) currFrame / selectionSpritesAmout, 0f, 1f / selectionSpritesAmout, 1f),true);
				} else
					eraserSelectionTime = 0;		
		}
		
	}

}
