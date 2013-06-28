using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenuGUI : MonoBehaviour {
	
	
	//public float MidMargin = ScreenHeight/Multiplyer;
	

	float ScreenHeight = Screen.height;
	float ScreenWidth = Screen.width;
	float ButtonWidth;
	float Margin = Screen.width/45;
	int NumberOfButtons;
	
	
	public Texture gameTitleTexture;
	public Texture gameExitTexture;
	public Texture gameCreditsTexture;
	
	public string[] gameList;
	Texture[] menuOptionTextures;
	Texture[] previewTextures;
	
	GUIStyle NoStyle = new GUIStyle();
	
	
	Rect[] rect;
	
	Rect gamePreviewButtonRect = new Rect(Screen.width/2 - (Screen.width/30 + Screen.width/3) , Screen.height/4 + Screen.width/45, Screen.width - Screen.width/4 , Screen.width/2 );
	
	int selectedGame = -1;
	public static int currentLevel = 1;
	public static string selectedGameName;
	
	// Use this for initialization
	void Start () {
		
		currentLevel = 1;
		NumberOfButtons = gameList.Length;
		
		menuOptionTextures = new Texture[NumberOfButtons];
		previewTextures = new Texture[NumberOfButtons];
		rect = new Rect[NumberOfButtons];
		
		//ScreenWidth -= TotalMargin;
		ButtonWidth = ScreenWidth/NumberOfButtons;
		
		for (int i = 0; i < NumberOfButtons; i++) {
			rect[i] = new Rect(i * (ButtonWidth) + Margin, ScreenHeight/45, ButtonWidth - Margin*2, ButtonWidth - Margin*2);	
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
			}
		} else {
			
			float ButtonArea = Screen.height/4;
#if UNITY_IPONE
			if(GUI.Button(new Rect(Screen.width/2 -Screen.width/4 , Screen.height/4 + Screen.width/10, Screen.width/2 , Screen.width/2 -Screen.width/2), gameTitleTexture, NoStyle)){
				print ("main button");	
			}
#else
			if(GUI.Button(new Rect(Screen.width/2 -Screen.width/5 , Screen.height/4 + Margin, Screen.width/2 , Screen.width/2 -Screen.width/10 ), gameTitleTexture, NoStyle)){
				print ("main button");	
			}
			
			/*
			  if(GUI.Button(new Rect(ScreenWidth/4 + ScreenWidth/20 , Screen.height/4 + 25 , Screen.width/2 + 20, ScreenHeight/2 + 110), gameTitleTexture, NoStyle)){
				print ("main button");	
			}
			 */
#endif
		}	
		if (GUI.Button(new Rect(Margin,ScreenHeight - (ButtonWidth - Margin*2), ButtonWidth - Margin*2, ButtonWidth - Margin*2), gameExitTexture, NoStyle)) {
			Debug.Log("exit");
			Application.Quit();
		}
		if (GUI.Button(new Rect(Screen.width -Screen.width/6 + Margin, ScreenHeight - (ButtonWidth - Margin*2) , ButtonWidth - Margin*2, ButtonWidth - Margin*2), gameCreditsTexture, NoStyle)) {
			
		}	
	}
}
