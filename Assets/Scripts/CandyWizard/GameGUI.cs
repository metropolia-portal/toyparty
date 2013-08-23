using UnityEngine;
using System.Collections;

public class GameGUI : MonoBehaviour {
	CandyWizardGameManager gameManager;
	
	public bool enableSpeedup = true;
	public bool enableEraser = true;
	
	public Texture speedupSpriteSheet;
	public RelativeTexturePosition flaskPos;
	public int speedupSpritesAmount = 7;
	
	public Texture selectionSpriteSheet;
	public RelativeTexturePosition flaskSelectionPos;
	public int selectionSpritesAmout = 3;
	public float selectionAnimationFPS = 2;
	
	public Texture eraserTexture;
	public RelativeTexturePosition eraserPos;
	public RelativeTexturePosition eraserSelectionPos;
	
	public Texture2D reloadTexture;
	public RelativeTexturePosition reloadPos;
	

	float speedupLeft = 1f;
	
	bool speedupOn = false;
	bool eraserOn = false;

	MGUIAnimatedTexture speedupSelection;
	MGUIAnimatedTexture eraserSelection;
	
	void Start(){
		gameManager = GameObject.Find ("GameManager").GetComponent<CandyWizardGameManager>();
		
		speedupSelection = new MGUIAnimatedTexture();
		eraserSelection = new MGUIAnimatedTexture();
	}
	
	// from 0 to 1;
	public void SetSpeedupLeft(float val) {
		speedupLeft = val;
	}
	
	void OnGUI() {
		if(GUI.Button (reloadPos.getRect(reloadTexture), reloadTexture, MGUI.NoStyle)) {
			gameManager.ReplayLevel();
		}
		
		if(enableSpeedup) {
			//box dimensiont is 1/6 of button dimension
			
			float flaskHeight = flaskPos.getHeight();
			float flaskWidth = flaskPos.getWidth(speedupSpriteSheet)/(float) speedupSpritesAmount;
			
			GUI.BeginGroup(new Rect(flaskPos.getX(),flaskPos.getY(),flaskWidth ,flaskHeight)); 
			
			//we should take size of the whole sprite, so that one frame dimension is our needed height and width
			float buttonWidth = flaskPos.getWidth(speedupSpriteSheet); 
			
			if (GUI.Button(new Rect( - ( (int)( (1 - speedupLeft) * (speedupSpritesAmount - 1))) * flaskWidth, 0, buttonWidth,  buttonWidth), speedupSpriteSheet, MGUI.NoStyle)) {
				speedupOn = !speedupOn;
				eraserOn = false;
				
				//TODO maybe notofications instead?
				gameManager.SetSpeedUpBrushActive(speedupOn);
			}
		
			GUI.EndGroup();
			
			
			if(speedupOn)
				speedupSelection.OnGUI(flaskSelectionPos.getRect(selectionSpriteSheet), selectionSpriteSheet, selectionSpritesAmout, selectionAnimationFPS );
		}
		
		if(enableEraser) {
			
			if (GUI.Button(eraserPos.getRect(eraserTexture), eraserTexture, MGUI.NoStyle)) {
					eraserOn = !eraserOn;
					speedupOn = false;
				
					gameManager.SetRubberBrushActive(eraserOn);
			}
					
			if(eraserOn)
				eraserSelection.OnGUI(eraserSelectionPos.getRect(selectionSpriteSheet), selectionSpriteSheet, selectionSpritesAmout, selectionAnimationFPS );
		}
		
	}


}

public class MGUIAnimatedTexture {
	float selectionTime = 0;
	
	public void OnGUI(Rect rect, Texture spriteSheet, int sprites, float fps) {
		if(selectionTime == 0)
			selectionTime = Time.timeSinceLevelLoad;
		
		int currFrame =  (int) ((Time.timeSinceLevelLoad - selectionTime) * fps) % sprites;
		
		GUI.DrawTextureWithTexCoords(
			new Rect(
				rect.x, rect.y, rect.width / sprites, rect.height  ),
			spriteSheet, new Rect((float) currFrame / sprites, 0f, 1f / sprites, 1f),true);
	}
	
}

[System.Serializable]
public class RelativeTexturePosition
{
	public float XtoScreenW;
	public float YtoScreenH;
	public float heightToScreenH;
	
	
	public int getX() {
		return (int) (Screen.width * XtoScreenW);
	}
	
	public int getY() {
		return (int)  (Screen.height * YtoScreenH);
	}
	
	public int getWidth(Texture texture) {
		return (int)  (getHeight() * (float) (texture.width / texture.height));
	}
	
	public int getHeight() {
		return (int)  (Screen.height * heightToScreenH);
	}
	
	public Rect getRect(Texture texture) {
		return new Rect(getX(), getY(), getWidth(texture), getHeight());
	}
	// H is determined by texture scale
}