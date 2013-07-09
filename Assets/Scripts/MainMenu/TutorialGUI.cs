using UnityEngine;
using System.Collections;

public class TutorialGUI : MonoBehaviour {
	
	GUIStyle NoStyle = new GUIStyle();
	Texture Background, PlayButton, MainMenuButton;
	float Margin = Screen.width/45;
	

	// Use this for initialization
	void Start () {
		
		Background = (Texture)Resources.Load("TutorialMenu/Previews/" + MainMenuGUI.selectedGameName);
		PlayButton = (Texture)Resources.Load("MenuCommon/play_" + MainMenuGUI.selectedGameName);
		MainMenuButton = (Texture)Resources.Load("MenuCommon/home_" + MainMenuGUI.selectedGameName);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
		GUI.Box(new Rect(0, 0, Screen.width, Screen.height), Background);
		if (GUI.Button (new Rect(Margin, Screen.height - (Screen.width/6), Screen.width/7, Screen.width/7), MainMenuButton, NoStyle)) {
			Camera.main.audio.Stop();
			Application.LoadLevel("MainMenu");
		}
		if (GUI.Button (new Rect(Screen.width -Screen.width/6, Screen.height - (Screen.width/6), Screen.width/7, Screen.width/7), PlayButton, NoStyle)) {
			Debug.LogWarning(MainMenuGUI.selectedGameName);
			Camera.DestroyObject(Camera.main.audio);
			Application.LoadLevel(MainMenuGUI.selectedGameName+"_level_"+MainMenuGUI.currentLevel);
			
		}
	}
}
