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
	
	public string[] gameList;
	Texture[] menuOptionTextures;
	Texture[] previewTextures;
	
	GUIStyle NoStyle = new GUIStyle();
	
	
	Rect[] rect;
	Rect gamePreviewButtonRect;
	
	
	
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
			gamePreviewButtonRect = new Rect(Screen.width/2 - (Screen.width/30 + Screen.width/3) , Screen.height/4 + Screen.width/45, Screen.width - Screen.width/4 , Screen.width/2 );
#else	
		
		if(aspect >= 1.23f && aspect <= 1.25f){ // 5/4 aspect ratio
				
				gamePreviewButtonRect = new Rect(Screen.width/2 - (Screen.width/30 + Screen.width/3) , Screen.height/4 + Screen.width/45, Screen.width - Screen.width/4 , Screen.width/2 );
				
			}
		else if(aspect >= 1.32f && aspect <= 1.34f){ // 4/3 aspect ratio
				
				gamePreviewButtonRect = new Rect(Screen.width/2 - (Screen.width/30 + Screen.width/3) , Screen.height/4 + Screen.width/45, Screen.width - Screen.width/4 , Screen.width/2 );
				
			}
		else if(aspect >= 1.49f && aspect <= 1.50f){ // 3/2 aspect ratio
				
				gamePreviewButtonRect = new Rect(Screen.width/2 - (Screen.width/30 + Screen.width/3) , Screen.height/4 + Screen.width/45, Screen.width - Screen.width/4 , Screen.width/2 );		
				
			}
		else if(aspect >= 1.6f && aspect <= 1.61f){ // web play aspect ratio
				
				gamePreviewButtonRect = new Rect(Screen.width/2 - (Screen.width/30 + Screen.width/3) , Screen.height/6 + Screen.width/45, Screen.width - Screen.width/4 , Screen.width/2 );		
				
			}
		else{// all other ratios
				
				gamePreviewButtonRect = new Rect(Screen.width/2 -Screen.width/3 , Screen.height/4 + Margin, Screen.width, Screen.height*4);	
				
			}
#endif
		
		//ScreenWidth -= TotalMargin;
		ButtonWidth = ScreenWidth/NumberOfButtons;
		
		for (int i = 0; i < NumberOfButtons; i++) {
			
			
			if(aspect >= 1.23f && aspect <= 1.25f){ // 5/4 aspect ratio
				
				rect[i] = new Rect(i * (ButtonWidth) + Margin, ScreenHeight/25, ButtonWidth - Margin*2, ButtonWidth - Margin*2);
				
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
			if (GUI.Button(gamePreviewButtonRect, previewTextures[selectedGame], NoStyle)) {
				Application.LoadLevel("TutorialScene");
				Camera.DontDestroyOnLoad(Camera.main.audio);
			}
		} else {
			
			float ButtonArea = Screen.height/4;
#if UNITY_IPONE
			if(GUI.Button(new Rect(Screen.width/2 -Screen.width/4 , Screen.height/4 + Screen.width/10, Screen.width/2 , Screen.width/2 -Screen.width/2), gameTitleTexture, NoStyle)){
				print ("main button");	
			}
#else
			
			if(aspect >= 1.24f && aspect <= 1.25f){ // 5/4 aspect ratio
				
				if(GUI.Button(new Rect(Screen.width/3 -Screen.width/10 , Screen.height/4 + Margin*3, Screen.width/9 + Screen.width*6, Screen.width/2 -Screen.width/100 ), gameTitleTexture, NoStyle)){
					print ("main button");	
				}
			}
			else if(aspect >= 1.32f && aspect <= 1.34f){ // 4/3 aspect ratio
				
				if(GUI.Button(new Rect(Screen.width/3 -Screen.width/10 , Screen.height/4 + Margin*2, Screen.width/10 + Screen.width*6, Screen.width/2 -Screen.width/100 ), gameTitleTexture, NoStyle)){
					print ("main button");	
				}
			}
			else if(aspect >= 1.49f && aspect <= 1.50f){ // 3/2 aspect ratio
				
				if(GUI.Button(new Rect(Screen.width/3 -Screen.width/16 , Screen.height/4 + Margin*2, Screen.width/10 + Screen.width*5, Screen.width/2 -Screen.width/20 ), gameTitleTexture, NoStyle)){
					print ("main button");	
				}
			}
			else{// all other ratios
				
				if(GUI.Button(new Rect(Screen.width/2 -Screen.width/5 , Screen.height/4 + Margin, Screen.width/2 , Screen.width/2 -Screen.width/9 ), gameTitleTexture, NoStyle)){
					print ("main button");	
				}
			}
			
			
#endif
		}	
		if (GUI.Button(new Rect(Margin,ScreenHeight - (ButtonWidth - Margin*2), Screen.width/9, Screen.width/9), gameExitTexture, NoStyle)) {
			Debug.Log("exit");
			Application.Quit();
		}
		if (GUI.Button(new Rect(Screen.width -Screen.width/6 + Margin, ScreenHeight - (ButtonWidth - Margin*2) , Screen.width/9, Screen.width/9), gameCreditsTexture, NoStyle)) {
			
		}	
	}
}
