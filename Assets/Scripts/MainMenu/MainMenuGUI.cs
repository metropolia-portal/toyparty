using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenuGUI : MonoBehaviour {
	
	
	//public float MidMargin = ScreenHeight/Multiplyer;
	

	float ScreenHeight = Screen.height;
	float ScreenWidth = Screen.width;
	float ButtonWidth;
	float Margin = Screen.width/45;
	float aspect = 0.0f;
		
	int NumberOfButtons;
	
	
	public Texture gameTitleTexture;
	public Texture gameExitTexture;
	public Texture gameCreditsTexture;
	
	Texture PlayButton;
	public string[] gameList;
	Texture[] menuOptionTextures;
	Texture[] previewTextures;
	
	GUIStyle NoStyle = new GUIStyle();
	
	
	Rect[] rect;
	Rect gamePreviewButton;
	
	
	
	int selectedGame = -1;
	public static int currentLevel = 1;
	public static string selectedGameName;
	
	// Use this for initialization
	void Start () {
		
		currentLevel = 1;
		NumberOfButtons = gameList.Length;
		aspect = (float)Screen.width / Screen.height;
		
		menuOptionTextures = new Texture[NumberOfButtons];
		previewTextures = new Texture[NumberOfButtons];
		rect = new Rect[NumberOfButtons];
		
		
		
#if UNITY_IPONE
			gamePreviewButton = new Rect(Screen.width/2 - Screen.width/18, Screen.height/12 + Screen.height/2, Screen.width/7, Screen.width/7);
#else	
		
		if(aspect >= 1.23f && aspect <= 1.25f){ // 5/4 aspect ratio
				
				gamePreviewButton = new Rect(Screen.width/2 - Screen.width/18, Screen.height/12 + Screen.height/2, Screen.width/7, Screen.width/7);
				
			}
		else if(aspect >= 1.32f && aspect <= 1.34f){ // 4/3 aspect ratio
				
				gamePreviewButton = new Rect(Screen.width/2 - Screen.width/18, Screen.height/12 + Screen.height/2, Screen.width/7, Screen.width/7);
				
			}
		else if(aspect >= 1.49f && aspect <= 1.50f){ // 3/2 aspect ratio
				
				gamePreviewButton = new Rect(Screen.width/2 - Screen.width/18, Screen.height/12 + Screen.height/2, Screen.width/7, Screen.width/7);		
				
			}
		else if(aspect >= 1.6f && aspect <= 1.61f){ // web play aspect ratio
				
				gamePreviewButton = new Rect(Screen.width/2 - Screen.width/18, Screen.height/12 + Screen.height/2, Screen.width/7, Screen.width/7);		
				
			}
		else{// all other ratios
				
				gamePreviewButton = new Rect(Screen.width/2 - Screen.width/18, Screen.height/12 + Screen.height/2, Screen.width/7, Screen.width/7);
				
			}
#endif
		
		//ScreenWidth -= TotalMargin;
		ButtonWidth = ScreenWidth/(NumberOfButtons-0.09f);
		
		for (int i = 0; i < NumberOfButtons; i++) {
			
			
			if(aspect >= 1.23f && aspect <= 1.25f){ // 5/4 aspect ratio
				
				rect[i] = new Rect(i * ButtonWidth, ScreenHeight/30, ButtonWidth , ButtonWidth);
				
			}
			else if(aspect >= 1.32f && aspect <= 1.34f){ // 4/3 aspect ratio
				
				rect[i] = new Rect(i * (ButtonWidth) + Margin, ScreenHeight/25, ButtonWidth - Margin*2, ButtonWidth - Margin*2);
				
			}
			else if(aspect >= 1.49f && aspect <= 1.50f){ // 3/2 aspect ratio
				
				rect[i] = new Rect(i * (ButtonWidth) + Margin, ScreenHeight/40, ButtonWidth - Margin*2, ButtonWidth - Margin*2);	
				
			}
			else{// all other ratios
				
				rect[i] = new Rect(i * (ButtonWidth) + Margin, ScreenHeight/55, ButtonWidth - Margin*2, ButtonWidth - Margin*2);	
				
			}

				
			menuOptionTextures[i] = (Texture)Resources.Load("MainMenu/Buttons/"+gameList[i]);
			previewTextures[i] = (Texture)Resources.Load("MainMenu/Previews/"+gameList[i]);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void SelectGame(int i) {
		selectedGame = i;
		selectedGameName = gameList[i];
		Debug.Log(selectedGameName);
	}
	
	void OnGUI() {
		
		for (int i=0; i< NumberOfButtons; i++) 
			if (GUI.Button(rect[i], menuOptionTextures[i], NoStyle)) 
				SelectGame(i);
		
		
		
		if (selectedGame > -1 ) {
			
			GUI.DrawTexture(new Rect(Screen.width/2 - (Screen.width/30 + Screen.width/3) , Screen.height/4 + Screen.width/45, Screen.width - Screen.width/4 , Screen.width/2 ), previewTextures[selectedGame]);	
			PlayButton = (Texture)Resources.Load("MenuCommon/play_" + selectedGameName);
			if (GUI.Button(gamePreviewButton, PlayButton, NoStyle)) {
				Application.LoadLevel("TutorialScene");
				Camera.DontDestroyOnLoad(Camera.main.audio);
			}
		} else {
			
			float ButtonArea = Screen.height/4;
#if UNITY_IPONE
			GUI.DrawTexture(new Rect(Screen.width/3 -Screen.width/16 , Screen.height/4 + Margin*2, Screen.width/2, Screen.height/2 + Screen.height/6 ), gameTitleTexture);
#else
	
			GUI.DrawTexture(new Rect(Screen.width/3 -Screen.width/16 , Screen.height/4 + Margin*2, Screen.width/2, Screen.height/2 + Screen.height/6 ), gameTitleTexture); 
			
#endif
		}	
		if (GUI.Button(new Rect(Margin,Screen.height - (Screen.width/6), Screen.width/7, Screen.width/7), gameExitTexture, NoStyle)) {
			Debug.Log("exit");
			Application.Quit();
		}
		
		if (GUI.Button(new Rect(Screen.width -Screen.width/6 + Margin/2, Screen.height - (Screen.width/6), Screen.width/7, Screen.width/7), gameCreditsTexture, NoStyle)) {
			Debug.Log("credits");
			Application.LoadLevel("CreditsSCreen");
			Camera.DontDestroyOnLoad(Camera.main.audio);
		}	
	}
}
