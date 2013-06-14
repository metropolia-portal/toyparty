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
	
	Rect gamePreviewButtonRect = new Rect( Screen.width / 8 , Screen.height/4, 7 * Screen.width / 8, Screen.height);
	
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
			
			
			Vector2 size = new Vector2(2 * Screen.width / 3, Screen.height/3*2);
			float x =  ScreenWidth/6;
			float y = ButtonPanelHeight + Margin * 4;
			float width = ScreenWidth / 3 * 2;
			float height = ScreenHeight / 2 * 4;
			if(GUI.Button(new Rect( x, y ,width , height), gameTitleTexture, NoStyle)){
				print ("main button");	
			}
		}	
		if (GUI.Button(new Rect(-0.07f * (ButtonWidth) + Margin, ScreenHeight/1.1f - (ButtonWidth - Margin*2)/2, ButtonWidth - Margin*2, ButtonWidth - Margin*2), gameExitTexture, NoStyle)) {
			Debug.Log("exit");
			Application.Quit();
		}
		if (GUI.Button(new Rect(5.1f * (ButtonWidth) + Margin, ScreenHeight/1.1f - (ButtonWidth - Margin*2)/2, ButtonWidth - Margin*2, ButtonWidth - Margin*2), gameCreditsTexture, NoStyle)) {
			
		}	
	}
}
