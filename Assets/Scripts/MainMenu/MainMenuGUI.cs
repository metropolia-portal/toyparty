using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenuGUI : MonoBehaviour {
	
	
	//public float MidMargin = ScreenHeight/Multiplyer;
	

	float ScreenHeight = Screen.height;
	float ScreenWidth = Screen.width;
	int Multiplyer = 6;
	float ButtonWidth;
	float Margin = 8;
	int NumberOfButtons;
	float TotalMargin;
	Texture gameTitleTexture;
	
	public string[] gameList;
	Texture[] menuOptionTextures;
	Texture[] previewTextures;
	
	Rect[] rect;
	
	int selectedGame = -1;
	
	// Use this for initialization
	void Start () {
		
		TotalMargin = 2F * NumberOfButtons * Margin;
		NumberOfButtons = gameList.Length;
		menuOptionTextures = new Texture[NumberOfButtons];
		previewTextures = new Texture[NumberOfButtons];
		rect = new Rect[NumberOfButtons];
		
		//ScreenWidth -= TotalMargin;
		ButtonWidth = ScreenWidth/NumberOfButtons;
		
		for (int i = 0; i < NumberOfButtons; i++) {
			rect[i] = new Rect(i * (ButtonWidth) + Margin, ScreenHeight/8 - (ButtonWidth - Margin*2)/2, ButtonWidth - Margin*2, ButtonWidth - Margin*2);	
			menuOptionTextures[i] = (Texture)Resources.Load("MainMenu/mainMenu_"+gameList[i]);
			previewTextures[i] = (Texture)Resources.Load("MainMenu/preview_"+gameList[i]);
			Debug.Log("mainMenu_"+gameList[i]);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
		
		//string message = "Main Menu";
		
		for (int i=0; i< NumberOfButtons; i++) 
			if (GUI.Button(rect[i], menuOptionTextures[i])) 
				selectedGame = i;
	
		
		
		
		if (selectedGame > -1 ) {
			Debug.Log(gameList[selectedGame]);
			if (GUI.Button(new Rect(ScreenWidth/8, ScreenHeight/3, 3*ScreenWidth/4, ScreenHeight), previewTextures[selectedGame]))
				Application.LoadLevel(gameList[selectedGame]+"LevelOneTutorial");
		} else {
			GUI.Button(new Rect(ScreenWidth/8, ScreenHeight/3, 3*ScreenWidth/4, ScreenHeight), gameTitleTexture);
		}
		
		
		// This is run only while the game is on the main menu
		
 
		//GUI.Box(new Rect(0, 0, Screen.width, Screen.height), message);
		
		
		/*
		if (GUI.Button(new Rect(0 + Margin, 0, TotalMargin, (ScreenHeight/Multiplyer)), tex[0])) {
			
			Application.LoadLevel("BrickGameMenu");
		}
		
		if (GUI.Button(new Rect(ScreenWidth/Multiplyer + Margin  , 0, TotalMargin , (ScreenHeight/Multiplyer)), tex[1])) {
			
			Application.LoadLevel("FlipsGameMenu");
		}
		if (GUI.Button(new Rect(ScreenWidth/Multiplyer * 2 , 0, ScreenWidth/Multiplyer , (ScreenHeight/Multiplyer)), tex[6])) {
			
				Application.LoadLevel("FlipsGameMenu");
		}
			
		if (GUI.Button(new Rect(ScreenWidth/Multiplyer * 3 , 0, ScreenWidth/Multiplyer , (ScreenHeight/Multiplyer)), tex[3])) {
				
				Application.LoadLevel("FlipsGameMenu");
		}
		if (GUI.Button(new Rect(ScreenWidth/Multiplyer * 4 , 0, ScreenWidth/Multiplyer , (ScreenHeight/Multiplyer)), tex[4])) {
				
				Application.LoadLevel("FlipsGameMenu");
		}
		if (GUI.Button(new Rect(ScreenWidth/Multiplyer * 5 , 0, ScreenWidth/Multiplyer , (ScreenHeight/Multiplyer)), tex[5])) {
				
				Application.LoadLevel("FlipsGameMenu");
		}
		if (GUI.Button(new Rect(ScreenWidth/2, ScreenWidth/2, ScreenWidth/Multiplyer, (ScreenHeight/Multiplyer)), tex[7])) {
				
				Application.LoadLevel("FlipsGameMenu");
		}
		*/
			
			
			GUI.enabled = true;	
	}
}
