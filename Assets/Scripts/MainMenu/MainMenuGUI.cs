using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenuGUI : MonoBehaviour {

	public Texture gameTitleTexture;
	public Texture gameExitTexture;
	public Texture gameCreditsTexture;
	
	public static int currentLevel = 1;
	public static string selectedGameName;
	
	float ScreenHeight = Screen.height;
	float ScreenWidth = Screen.width;
	float ButtonWidth;
	float Margin = Screen.width/45;
	float aspect = 0.0f;
		
	int NumberOfButtons;	

	Texture PlayButton;
	public string[] gameList;
	Texture[] menuOptionTextures;
	Texture[] previewTextures;
	
	GUIStyle NoStyle = new GUIStyle();
	
	Rect gamePreviewButton;
	
	Rect [] rect;
	Rect [] rectBig;
	Rect [] rectSmall;
	
	Rect [] menuRect = 	new Rect[2];
	Rect [] menuRectBig = new Rect[2];
	Rect [] menuRectSmall = new Rect[2];
	float diffRect;
	float width;
	
	int selectedGame = -1;
	
	// Use this for initialization
	void Start () {
		
		currentLevel = 1;
		NumberOfButtons = gameList.Length;
		aspect = (float)Screen.width / Screen.height;
		
		menuOptionTextures = new Texture[NumberOfButtons];
		previewTextures = new Texture[NumberOfButtons];
		rect = new Rect[NumberOfButtons];
		rectBig = new Rect[NumberOfButtons];
		rectSmall = new Rect[NumberOfButtons];
		
	#if UNITY_IPONE
			gamePreviewButton = new Rect(Screen.width/2 - Screen.width/18, Screen.height/12 + Screen.height/2, Screen.width/7, Screen.width/7);
	#else	
		
		if(aspect >= 1.23f && aspect <= 1.25f)// 5/4 aspect ratio
		{ 
			gamePreviewButton = new Rect(Screen.width/2 - Screen.width/18, Screen.height/12 + Screen.height/2, Screen.width/7, Screen.width/7);		
		}
		else if(aspect >= 1.32f && aspect <= 1.34f)// 4/3 aspect ratio
		{ 
			gamePreviewButton = new Rect(Screen.width/2 - Screen.width/18, Screen.height/12 + Screen.height/2, Screen.width/7, Screen.width/7);
				
		}
		else if(aspect >= 1.49f && aspect <= 1.50f)// 3/2 aspect ratio
		{ 
			gamePreviewButton = new Rect(Screen.width/2 - Screen.width/18, Screen.height/12 + Screen.height/2, Screen.width/7, Screen.width/7);		
				
		}
		else if(aspect >= 1.6f && aspect <= 1.61f)// web play aspect ratio
		{ 
			gamePreviewButton = new Rect(Screen.width/2 - Screen.width/18, Screen.height/12 + Screen.height/2, Screen.width/7, Screen.width/7);		
				
		}
		else if(aspect >= 1.77f && aspect <= 1.78f)// 16/9 aspect ratio
		{ 
			gamePreviewButton = new Rect(Screen.width/2 - Screen.width/18, Screen.height/6 + Screen.height/3, Screen.width/7, Screen.width/7);		
				
		}
		else// all other ratios
		{	
			gamePreviewButton = new Rect(Screen.width/2 - Screen.width/18, Screen.height/12 + Screen.height/2, Screen.width/7, Screen.width/7);	
		}
	#endif
	
		ButtonWidth = ScreenWidth/(NumberOfButtons-0.09f);
		
		for (int i = 0; i < NumberOfButtons; i++) {

			if(aspect >= 1.23f && aspect <= 1.25f) // 5/4 aspect ratio
			{
				rect[i] = new Rect(i * ButtonWidth, ScreenHeight/30, ButtonWidth , ButtonWidth);	
			}
			else if(aspect >= 1.32f && aspect <= 1.34f)// 4/3 aspect ratio
			{ 
				rect[i] = new Rect(i * (ButtonWidth) + Margin, ScreenHeight/25, ButtonWidth - Margin*2, ButtonWidth - Margin*2);
			}
			else if(aspect >= 1.49f && aspect <= 1.50f)// 3/2 aspect ratio
			{ 
				rect[i] = new Rect(i * (ButtonWidth) + Margin, ScreenHeight/40, ButtonWidth - Margin*2, ButtonWidth - Margin*2);		
			}
			else// all other ratios
			{
				rect[i] = new Rect(i * (ButtonWidth) + Margin, ScreenHeight/55, ButtonWidth - Margin*2, ButtonWidth - Margin*2);		
			}
	
			menuOptionTextures[i] = (Texture)Resources.Load("MainMenu/Buttons/"+gameList[i]);
			previewTextures[i] = (Texture)Resources.Load("MainMenu/Previews/"+gameList[i]);
		}
		for (int i = 0; i < NumberOfButtons; i++) 
		{
			rectSmall[i] = rect[i];
		}

		float widthNormal = rect[0].width;
		float widthBig = widthNormal * 1.2f;
		float diffWidth = (widthBig - widthNormal) / 2f;
		for (int i = 0; i < NumberOfButtons; i++) 
		{
			rectBig[i] = rect[i];
			rectBig[i].height = rectBig[i].width = widthBig;
			rectBig[i].x = rectBig[i].x - diffWidth;
			rectBig[i].y = rectBig[i].y - diffWidth ;
		}
		menuRect[0] = new Rect(Margin,ScreenHeight - ButtonWidth, 
			widthNormal,widthNormal);
		menuRectSmall[0] = new Rect(menuRect[0]);
		menuRectBig[0] = new Rect(menuRect[0].x - diffWidth, menuRect[0].y - diffWidth,widthBig ,widthBig);
		
		menuRect[1] = new Rect(Screen.width -Screen.width/6, ScreenHeight - ButtonWidth, 
			widthNormal,widthNormal);
		menuRectSmall[1] = new Rect(menuRect[1]);
		menuRectBig[1] = new Rect(menuRect[1].x - diffWidth, menuRect[1].y - diffWidth,widthBig ,widthBig);
	}
	
	void SelectGame(int i) {
		selectedGame = i;
		selectedGameName = gameList[i];
	}
	
	void OnGUI() {
		var mousePos = InputManager.MouseScreenToGUI(); 
		for(int i=0; i< NumberOfButtons; i++) 
		{		
			if(rect[i].Contains(mousePos))
			{
				rect[i] = rectBig[i];

			}else{
				rect[i] = rectSmall[i] ;
			}
		}
		
		for (int i=0; i< NumberOfButtons; i++) 
		{
			if (GUI.Button(rect[i], menuOptionTextures[i], NoStyle)) 
			{
				SelectGame(i);
			}
		}
		if (selectedGame > -1 ) {
			
			if(aspect >= 1.77 && aspect <= 1.78){// 16/9 aspect ratio
				
				GUI.DrawTexture(new Rect(Screen.width/2 - (Screen.width/30 + Screen.width/3) , Screen.height/4 + Screen.width/45, Screen.width - Screen.width/4 , Screen.width/3 ), previewTextures[selectedGame]);	 
			}
			else
				GUI.DrawTexture(new Rect(Screen.width/2 - (Screen.width/30 + Screen.width/3) , Screen.height/4 + Screen.width/45, Screen.width - Screen.width/4 , Screen.width/2 ), previewTextures[selectedGame]);	
			PlayButton = (Texture)Resources.Load("MenuCommon/play_" + selectedGameName);
			
			if (GUI.Button(gamePreviewButton, PlayButton, NoStyle)) 
			{
				Application.LoadLevel("TutorialScene");
				//Camera.DontDestroyOnLoad(Camera.main.audio);
			}
		}
		else
		{
			float ButtonArea = Screen.height/4;
		#if UNITY_IPONE
			GUI.DrawTexture(new Rect(Screen.width/3 -Screen.width/16 , Screen.height/4 + Margin*2, Screen.width/2, Screen.height/2 + Screen.height/6 ), gameTitleTexture);
		#else
			
				GUI.DrawTexture(new Rect(Screen.width/3 -Screen.width/16 , Screen.height/4 + Margin*2, Screen.width/2, Screen.height/2 + Screen.height/6 ), gameTitleTexture); 
		#endif
		}	
		/*if (GUI.Button(new Rect(Margin,Screen.height - (Screen.width/6), Screen.width/7, Screen.width/7), gameExitTexture, NoStyle)) {
			Application.Quit();
		}
		
		if (GUI.Button(new Rect(Screen.width -Screen.width/6 + Margin/2, Screen.height - (Screen.width/6), Screen.width/7, Screen.width/7), gameCreditsTexture, NoStyle)) {
			//Application.LoadLevel("CreditsSCreen");
			//Camera.DontDestroyOnLoad(Camera.main.audio);
		}*/
		
		// Drawing of the two bottom buttons
		if(menuRect[0].Contains(mousePos))
		{
			menuRect[0] = menuRectBig[0];
		}else
		{
			menuRect[0] = menuRectSmall[0];
		}
		if (GUI.Button( menuRect[0], gameExitTexture, NoStyle)) 
		{
			Application.Quit();
		}

		if(menuRect[1].Contains(mousePos))
		{
			menuRect[1] = menuRectBig[1];
		}else
		{
			menuRect[1] = menuRectSmall[1];
		}
		if (GUI.Button(menuRect[1], gameCreditsTexture, NoStyle)) 
		{
			if(AudioListener.volume == 0){
				AudioListener.volume = 1;
			}else{
				AudioListener.volume = 0;
			} 
				
		}
	}
}
