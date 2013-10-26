using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenuGUI : MonoBehaviour 
{
	#region MEMBERS
	public Texture gameTitleTexture;
	public Texture gameExitTexture;
	public Texture gameCreditsTexture;
	
	public static int currentLevel = 1;
	public static string selectedGameName;
	
	public float barHeightToScreenHeightRatio = 0.25f;
	public float gamePreviewWidthToScreenWidthRatio = 0.75f;
	public float gamePreviewArrowHeightRation = 0.2f;// height ration of the white speach arrow pointing to character to total height of preview screen
	
	float buttonBarHeight;
	int gamesNumber;
	bool callOptions = false;
	
	bool Sound= true;

	public string[] gameList;
	Texture[] gameSelectionTextures;
	Texture[] previewTextures;
	Texture[] playButtonsTextures;
	Texture soundON, soundOff, credits, options;
		
	Rect gamePreviewButtonRect;
	Rect gamePreviewRect;	
	Rect soundButtonRect;
	Rect creditsButtonRect;
	Rect[] gameSelectionRects;
	Rect quitRect;
	Rect creditsRect;
	Rect gameTitleRect;
	
	int selectedGame = -1;
	
	#endregion
	
	#region UNITY_METHODS
	void Start () 
	{
		selectedGameName = "";
		
		currentLevel = 1;
		gamesNumber = gameList.Length;
		buttonBarHeight = barHeightToScreenHeightRatio * Screen.height;
		gameSelectionTextures = new Texture[gamesNumber];
		previewTextures = new Texture[gamesNumber];
		playButtonsTextures = new Texture[gamesNumber];
		gameSelectionRects = new Rect[gamesNumber];
		
		float buttonWidth = (Screen.width - MGUI.Margin*(gamesNumber+1)) / gamesNumber;
		
		for (int i = 0; i < gamesNumber; i++) 
		{
			gameSelectionTextures[i] = (Texture)Resources.Load("MainMenu/Buttons/" + gameList[i]);
			previewTextures[i] = (Texture)Resources.Load("MainMenu/Previews/" + gameList[i]);
			playButtonsTextures[i] = (Texture)Resources.Load("MenuCommon/play_" + gameList[i]);
			
			
			float buttonHeight = buttonWidth * gameSelectionTextures[i].height / gameSelectionTextures[i].width;
			gameSelectionRects[i] = new Rect(i * (buttonWidth) + (i+1) * MGUI.Margin, centerPosition(buttonHeight, buttonBarHeight ), buttonWidth, buttonHeight);
		}
		
		soundON =(Texture)Resources.Load("MainMenu/Buttons/soundon");
		soundOff =(Texture)Resources.Load("MainMenu/Buttons/soundoff");
		options =(Texture)Resources.Load("MainMenu/Buttons/options_credits_btn");
		credits =(Texture)Resources.Load("MainMenu/Buttons/roll_credits_btn");
		gameExitTexture =(Texture)Resources.Load("MainMenu/Buttons/button_exit");
		
		float titleHeight = Screen.height - buttonBarHeight - MGUI.Margin;
		float titleWidth = titleHeight * gameTitleTexture.width / gameTitleTexture.width;
		gameTitleRect = new Rect(centerPosition(titleWidth, Screen.width), Screen.height - titleHeight, titleWidth, titleHeight);
		
		quitRect = new Rect(MGUI.Margin, Screen.height - MGUI.Margin - MGUI.menuButtonWidth, MGUI.menuButtonWidth, MGUI.menuButtonWidth);
		
		creditsRect = new Rect(Screen.width - MGUI.Margin - MGUI.menuButtonWidth, Screen.height - MGUI.Margin - MGUI.menuButtonWidth, MGUI.menuButtonWidth, MGUI.menuButtonWidth);
					
		float previewRatio = (float) previewTextures[0].width / previewTextures[0].height;	
		float previewWidth = Screen.width * gamePreviewWidthToScreenWidthRatio;
		float previewHeight = Mathf.Min ( previewWidth / previewRatio, Screen.height - buttonBarHeight - 2 * MGUI.Margin);	
		gamePreviewRect = new Rect(centerPosition(previewWidth, Screen.width), buttonBarHeight + MGUI.Margin , previewWidth, previewHeight);
		soundButtonRect = MGUI.centerInRect(new Rect(Screen.width/7, gamePreviewArrowHeightRation * gamePreviewRect.height, MGUI.menuButtonWidth, MGUI.menuButtonWidth), gamePreviewRect);
		creditsButtonRect = MGUI.centerInRect(new Rect(Screen.width/9 - Screen.width/4, gamePreviewArrowHeightRation * gamePreviewRect.height, MGUI.menuButtonWidth, MGUI.menuButtonWidth), gamePreviewRect);
		//TODO make centerRect method
		gamePreviewButtonRect = MGUI.centerInRect(new Rect(0, gamePreviewArrowHeightRation * gamePreviewRect.height, MGUI.menuButtonWidth, MGUI.menuButtonWidth), gamePreviewRect);// new Rect(centerPosition(), centerPosition(), Screen.width/7, Screen.width/7);			
	}
	
	void OnGUI() 
	{
		for (int i=0; i< gamesNumber; i++) 
		{
			if (MGUI.HoveredButton(gameSelectionRects[i], gameSelectionTextures[i])) 
			{
				callOptions = false;
				SelectGame(i);
			}
		}
		
		if (selectedGame > -1 && callOptions == false) {

			GUI.DrawTexture(gamePreviewRect,  previewTextures[selectedGame]);		
				
			if (MGUI.HoveredButton(gamePreviewButtonRect, playButtonsTextures[selectedGame])) 
			{
				Application.LoadLevel("TutorialScene");
			}
		}
		else if(callOptions == true)
		{
			GUI.DrawTexture(gamePreviewRect,  previewTextures[1]);
			if(Sound == true){
				
				if (MGUI.HoveredButton(soundButtonRect, soundON)){
					
					PlayerPrefs.SetString("sound", "false");
					Sound = false;
					EnableSound();
				}
			}
			else{
				if (MGUI.HoveredButton(soundButtonRect, soundOff))
				{
					PlayerPrefs.SetString("sound", "true");
					Sound = true;
					EnableSound();
				}
			}
			if (MGUI.HoveredButton(creditsButtonRect, credits)) 
			{
				Application.LoadLevel("CreditsScreen");	
			}
		}
		else{
			GUI.DrawTexture(gameTitleRect, gameTitleTexture);
		}
		
		if (MGUI.HoveredButton( quitRect, gameExitTexture)) 
		{
			Application.Quit();
		}
		
		if (MGUI.HoveredButton(creditsRect, options)) 
		{
			callOptions = true;
		}
	}
	#endregion
	
	#region METHODS
	static float centerPosition(float itemLength, float totalLength) 
	{
		return (totalLength - itemLength)/2;
	}
	
	void SelectGame(int i) 
	{
		selectedGame = i;
		selectedGameName = gameList[i];
	}
	
	void EnableSound()
	{
		if(AudioListener.volume == 0)
		{
			AudioListener.volume = 1;
		}
		else
		{
				AudioListener.volume = 0;
		} 			
	}
	#endregion
}
