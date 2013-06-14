using UnityEngine;
using System.Collections;

public class TutorialGUI : MonoBehaviour {
	
	GUIStyle NoStyle = new GUIStyle();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
		GUI.Box(new Rect(0, 0, Screen.width, Screen.height), MainMenuGUI.selectedGameName.ToUpper() + " GAME LEVEL "+MainMenuGUI.currentLevel.ToString()+" TUTORIAL");
		if (GUI.Button (new Rect(0, (Screen.height/3)*2, Screen.width/2, Screen.height/3), "Main Menu")) {
			Application.LoadLevel("MainMenu");
		}
		if (GUI.Button (new Rect(Screen.width/2, (Screen.height/3)*2, Screen.width/2, Screen.height/3), "START")) {
			Debug.LogWarning(MainMenuGUI.selectedGameName);
			Application.LoadLevel(MainMenuGUI.selectedGameName+"_level_"+MainMenuGUI.currentLevel);
		}
	}
}
