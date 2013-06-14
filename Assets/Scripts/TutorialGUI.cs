using UnityEngine;
using System.Collections;

public class TutorialGUI : MonoBehaviour {
	
	GUIStyle NoStyle = new GUIStyle();
	Texture Background, PlayButton, MainMenuButton;
	

	// Use this for initialization
	void Start () {
		string name1 = MainMenuGUI.selectedGameName+"_level_"+MainMenuGUI.currentLevel;
		Debug.LogWarning("name: " + name);
		Background = (Texture)Resources.Load("TutorialMenu/Previews/" + MainMenuGUI.selectedGameName);
		PlayButton = (Texture)Resources.Load("MenuCommon/play_" + MainMenuGUI.selectedGameName);
		MainMenuButton = (Texture)Resources.Load("MenuCommon/home_" + MainMenuGUI.selectedGameName);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
		GUI.Box(new Rect(0, 0, Screen.width, Screen.height), Background);
		if (GUI.Button (new Rect(0, (Screen.height/3)*2, Screen.width/2, Screen.height/3), MainMenuButton, NoStyle)) {
			Application.LoadLevel("MainMenu");
		}
		if (GUI.Button (new Rect(Screen.width/1.32f, (Screen.height/3)*2, Screen.width/2, Screen.height/3), PlayButton, NoStyle)) {
			Debug.LogWarning(MainMenuGUI.selectedGameName);
			Application.LoadLevel(MainMenuGUI.selectedGameName+"_level_"+MainMenuGUI.currentLevel);
		}
	}
}
