using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenuGUI : MonoBehaviour {
	
	
	//public float MidMargin = ScreenHeight/Multiplyer;
	

	float ScreenHeight = Screen.height;
	float ScreenWidth = Screen.width;
	float ButtonWidth;
	float Margin = 8;
	int NumberOfButtons;
	float ButtonPanelHeight;
	
	public Texture gameTitleTexture;
	public Texture gameExitTexture;
	public Texture gameCreditsTexture;
	
	public string[] gameList;
	Texture[] menuOptionTextures;
	Texture[] previewTextures;
	
	GUIStyle NoStyle = new GUIStyle();
	
	
	Rect[] rect;
	
	Rect gamePreviewButtonRect = new Rect(Screen.width / 12 , Screen.height/4, 7 * Screen.width / 8, Screen.height);
	
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
		ButtonPanelHeight = ButtonWidth;
		for (int i = 0; i < NumberOfButtons; i++) {
			rect[i] = new Rect(i * (ButtonWidth) + Margin, ScreenHeight/8 - (ButtonWidth - Margin*2)/2, ButtonWidth - Margin*2, ButtonWidth - Margin*2);	
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
			
			if(GUI.Button(new Rect(ScreenWidth/4 , Screen.height/4 + 25 , Screen.width/2 + 20, ScreenHeight/2 + 110), gameTitleTexture, NoStyle)){
				print ("main button");	
			}
		}	
		if (GUI.Button(new Rect(16,Screen.height - (Screen.width/6 - 16), Screen.width/7, Screen.width/7), gameExitTexture, NoStyle)) {
			Debug.Log("exit");
			Application.Quit();
		}
		if (GUI.Button(new Rect(Screen.width -Screen.width/6,Screen.height - (Screen.width/6 - 16), Screen.width/7, Screen.width/7), gameCreditsTexture, NoStyle)) {
			
		}	
	}
}
