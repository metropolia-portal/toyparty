using UnityEngine;
using System.Collections;

public class TutorialGUI : MonoBehaviour {
	
	GUIStyle NoStyle = new GUIStyle();
	Texture Background, PlayButton, MainMenuButton;
	

	// Use this for initialization
	void Start () {
		string name1 = MainMenuGUI.selectedGameName+"_level_"+MainMenuGUI.currentLevel;
		Background = (Texture)Resources.Load("TutorialMenu/Previews/" + MainMenuGUI.selectedGameName);
		PlayButton = (Texture)Resources.Load("MenuCommon/play_" + MainMenuGUI.selectedGameName);
		MainMenuButton = (Texture)Resources.Load("MenuCommon/home_" + MainMenuGUI.selectedGameName);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
		GUI.Box(new Rect(0, 0, Screen.width, Screen.height), Background);
		if (GUI.Button (new Rect(16, Screen.height - (Screen.width/6 - 16), Screen.width/9, Screen.width/9), MainMenuButton, NoStyle)) {
			Application.LoadLevel("MainMenu");
		}
		if (GUI.Button (new Rect(Screen.width -Screen.width/6, Screen.height - (Screen.width/6 - 16), Screen.width/9, Screen.width/9), PlayButton, NoStyle)) {
			Debug.LogWarning(MainMenuGUI.selectedGameName);
			Application.LoadLevel(MainMenuGUI.selectedGameName+"_level_"+MainMenuGUI.currentLevel);
			Camera.main.audio.Stop();
		}
	}
}
