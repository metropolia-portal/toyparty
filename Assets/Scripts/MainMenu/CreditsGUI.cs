using UnityEngine;
using System.Collections;

public class CreditsGUI : MonoBehaviour {
	
	Texture creditsTexture, MainMenuButton, characterTexture;
	float Margin = Screen.width/45;
	GUIStyle NoStyle = new GUIStyle();
	
	// Use this for initialization
	void Start () {
		Debug.Log(MainMenuGUI.selectedGameName);
		if (MainMenuGUI.selectedGameName!= "") 
			characterTexture = (Texture)Resources.Load("MedalMenu/characters/" + MainMenuGUI.selectedGameName);
			
		creditsTexture = (Texture)Resources.Load("Credits/creditsMenu");
		MainMenuButton = (Texture)Resources.Load("MenuCommon/home_brick");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {	
		
			
		
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), creditsTexture);
		if (MainMenuGUI.selectedGameName!= "") 
			GUI.DrawTexture(new Rect(Screen.width/11 , Screen.height/4 -Screen.height/16, Screen.width/6, Screen.height/3), characterTexture);
		
		if (GUI.Button (new Rect(Margin, Screen.height - (Screen.width/6), Screen.width/7, Screen.width/7), MainMenuButton, NoStyle)) {
			Camera.DestroyObject(Camera.main.audio);
			Application.LoadLevel("MainMenu");
		}
	}
}
