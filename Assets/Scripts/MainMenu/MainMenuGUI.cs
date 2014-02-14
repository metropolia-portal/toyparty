using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenuGUI : MonoBehaviour 
{
	#region MEMBERS
	bool _logo, _buttons, _transit,_loadingNext;
	public Texture2D logo;
	
	Rect _windowRect; 
	
	
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
	Rect _logoRect;
	
	int selectedGame = -1;
	
	float _move = 0;
	bool _quit = false;
	
	public FadeScreenScript fadeObject;
	public AudioScript audioScript;
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
			gameSelectionRects[i] = new Rect(i * (buttonWidth) + (i+1) * MGUI.Margin+Screen.width, Screen.height / 2 - buttonHeight / 2, buttonWidth, buttonHeight);
		}
		
		soundON =(Texture)Resources.Load("MainMenu/Buttons/soundon");
		soundOff =(Texture)Resources.Load("MainMenu/Buttons/soundoff");
		options =(Texture)Resources.Load("MainMenu/Buttons/options_credits_btn");
		credits =(Texture)Resources.Load("MainMenu/Buttons/roll_credits_btn");
		gameExitTexture =(Texture)Resources.Load("MainMenu/Buttons/button_exit");
			
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
		
		_logo = true;
		float __ratio = 1.5f;
		float __heightLogo = Screen.height / __ratio;
		float __widthLogo = Screen.width / __ratio;
		_logoRect = new Rect(Screen.width / 2 - __widthLogo / 2, Screen.height / 2 - __heightLogo / 2, __widthLogo, __heightLogo);
		_windowRect = new Rect(Screen.width / 2 - 200, Screen.height / 2 - 100, 400, 200);
	}
	
	void OnGUI() 
	{
		if(_loadingNext) return;
		if(_quit)
		{
			 _windowRect = GUI.Window(0,_windowRect,DoMyWindow, "Really");
			return;
		}
		if(_logo)
		{
			if(MGUI.HoveredButton(_logoRect, logo))
			{
				_transit = true;
				_buttons = true;
			}
		}
		if(_buttons)
		{
			for (int i=0; i< gamesNumber; i++) 
			{
				if (MGUI.HoveredButton(gameSelectionRects[i], gameSelectionTextures[i])) 
				{
					callOptions = false;
					SelectGame(i);
				}
			}
		}
		if(_transit)
		{
			float __move = Time.deltaTime * 1000f;
			_move += __move;
			
			
			if(_move > Screen.width){
				_transit = false;
				_logo = false;
			}
			
			_logoRect.x -= __move;
			for(int i = 0; i < gamesNumber; i++)
			{
				gameSelectionRects[i].x -= __move;
			}		
		}
		
		if (selectedGame > -1 && callOptions == false) {

			GUI.DrawTexture(gamePreviewRect,  previewTextures[selectedGame]);		
				
			if (MGUI.HoveredButton(gamePreviewButtonRect, playButtonsTextures[selectedGame])) 
			{
				StartCoroutine(_LoadNext());
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
		if (MGUI.HoveredButton( quitRect, gameExitTexture)) 
		{
			_quit = true;
		}
		
		if (MGUI.HoveredButton(creditsRect, options)) 
		{
			callOptions = true;
		}
	}
	#endregion
	
	#region METHODS
	void DoMyWindow(int windowID) 
	{
        if (GUI.Button(new Rect(20, 100, 100, 20), "Yes"))
			Application.Quit();
		if (GUI.Button(new Rect(280, 100, 100, 20), "No"))
            _quit = false;
    }
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
	private  IEnumerator _LoadNext()
	{
		float speed = Time.deltaTime * 2f;
		string str = selectedGameName + "_level_" + currentLevel;
		_loadingNext = true;
		while(true)
		{
			bool __audio = audioScript.FadeOutVolume(speed) ;
			bool __screen = fadeObject.FadeBlkScreen(speed);
			if(__audio && __screen)
			{
				break;
			}
			yield return null;
		}
		Application.LoadLevel(str);
	}
	#endregion
}
