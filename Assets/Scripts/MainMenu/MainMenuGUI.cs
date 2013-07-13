using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenuGUI : MonoBehaviour {

	public Texture gameTitleTexture;
	public Texture gameExitTexture;
	public Texture gameCreditsTexture;
	
	public static int currentLevel = 1;
	public static string selectedGameName;
	
	public float barHeightToScreenHeightRatio = 0.25f;
	public float gamePreviewWidthToScreenWidthRatio = 0.75f;
	public float gamePreviewArrowHeightRation = 0.2f;// height ration of the white speach arrow pointing to character to total height of preview screen
	
	public float hoverButtonSizeIncrease = 1.2f;
	
	//float ScreenHeight = Screen.height;
	//float ScreenWidth = Screen.width;
	
	float buttonWidth;
	float buttonBarHeight;
	
	float Margin = Screen.width/45;
		
	int gamesNumber;	

	public string[] gameList;
	Texture[] gameSelectionTextures;
	Texture[] previewTextures;
	Texture[] playButtonsTextures;
		
	Rect gamePreviewButtonRect;
	Rect gamePreviewRect;	
	Rect[] gameSelectionRects;
	Rect quitRect;
	Rect creditsRect;
	Rect gameTitleRect;
	
	GUIStyle NoStyle = new GUIStyle();
		
	int selectedGame = -1;
	
	// Use this for initialization
	void Start () {
		
		currentLevel = 1;
		gamesNumber = gameList.Length;
		buttonBarHeight = barHeightToScreenHeightRatio * Screen.height;
		
		gameSelectionTextures = new Texture[gamesNumber];
		previewTextures = new Texture[gamesNumber];
		playButtonsTextures = new Texture[gamesNumber];
		gameSelectionRects = new Rect[gamesNumber];


		buttonWidth = (Screen.width - Margin*(gamesNumber+1)) / gamesNumber;
		
		for (int i = 0; i < gamesNumber; i++) {
			gameSelectionTextures[i] = (Texture)Resources.Load("MainMenu/Buttons/" + gameList[i]);
			previewTextures[i] = (Texture)Resources.Load("MainMenu/Previews/" + gameList[i]);
			playButtonsTextures[i] = (Texture)Resources.Load("MenuCommon/play_" + gameList[i]);
			
			float buttonHeight = buttonWidth * gameSelectionTextures[i].height / gameSelectionTextures[i].width;
			gameSelectionRects[i] = new Rect(i * (buttonWidth) + (i+1) * Margin, centerPosition(buttonHeight, buttonBarHeight ), buttonWidth, buttonHeight);
		}
		
		float titleHeight = Screen.height - buttonBarHeight - Margin;
		float titleWidth = titleHeight * gameTitleTexture.width / gameTitleTexture.width;
		gameTitleRect = new Rect(centerPosition(titleWidth, Screen.width), Screen.height - titleHeight, titleWidth, titleHeight);
		
		quitRect = new Rect(Margin, Screen.height - Margin - buttonWidth, buttonWidth, buttonWidth);
		
		creditsRect = new Rect(Screen.width - Margin - buttonWidth, Screen.height - Margin - buttonWidth, buttonWidth, buttonWidth);
					
		float previewRatio = (float) previewTextures[0].width / previewTextures[0].height;	
		float previewWidth = Screen.width * gamePreviewWidthToScreenWidthRatio;
		float previewHeight = Mathf.Min ( previewWidth / previewRatio, Screen.height - buttonBarHeight - 2 * Margin);	
		gamePreviewRect = new Rect(centerPosition(previewWidth, Screen.width), buttonBarHeight + Margin , previewWidth, previewHeight);
		//TODO make centerRect method
		gamePreviewButtonRect = centerInRect(new Rect(0, gamePreviewArrowHeightRation * gamePreviewRect.height, buttonWidth, buttonWidth), gamePreviewRect);// new Rect(centerPosition(), centerPosition(), Screen.width/7, Screen.width/7);			
	}
	
	//inner.x and y are used for offset
	static Rect centerInRect(Rect inner, Rect outer) {
		return new Rect(outer.x + centerPosition(inner.width, outer.width)+ inner.x, outer.y + centerPosition(inner.height, outer.height) +  inner.y,inner.width, inner.height);
	}
	
	static float centerPosition(float itemLength, float totalLength) {
		return (totalLength - itemLength)/2;
	}
	
	void SelectGame(int i) {
		selectedGame = i;
		selectedGameName = gameList[i];
	}
	
	void OnGUI() {
		for (int i=0; i< gamesNumber; i++) 
		{
			if (ButtonWithHover(gameSelectionRects[i], gameSelectionTextures[i])) 
			{
				SelectGame(i);
			}
		}
		
		if (selectedGame > -1 ) {

			GUI.DrawTexture(gamePreviewRect,  previewTextures[selectedGame]);		
				
			if (ButtonWithHover(gamePreviewButtonRect, playButtonsTextures[selectedGame])) 
			{
				Application.LoadLevel("TutorialScene");
			}
		}
		else
		{
			GUI.DrawTexture(gameTitleRect, gameTitleTexture); 
		}	
		
		if (ButtonWithHover( quitRect, gameExitTexture)) 
		{
			Application.Quit();
		}
		
		if (ButtonWithHover(creditsRect, gameCreditsTexture)) 
		{
			if(AudioListener.volume == 0){
				AudioListener.volume = 1;
			}else{
				AudioListener.volume = 0;
			} 		
		}
	}
	
	public bool ButtonWithHover(Rect pos, Texture image) {
		Rect rect = pos;
		if(pos.Contains(InputManager.MouseScreenToGUI())) {
			rect = new Rect(pos.x - pos.width * (hoverButtonSizeIncrease - 1) / 2f, pos.y - pos.height * (hoverButtonSizeIncrease - 1) / 2f, pos.width * hoverButtonSizeIncrease , pos.height * hoverButtonSizeIncrease);
		}
		
		return GUI.Button(rect, image, NoStyle);
	}
}
