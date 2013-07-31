using UnityEngine;
using System.Collections;

public class GameMenu : MonoBehaviour {

	public string GameName = "";
	public string GameLevel = "";

	
	void OnGUI() {
		GUI.Box(new Rect(0, 0, Screen.width, Screen.height), GameName);
		if (GUI.Button (new Rect(Screen.width/2, Screen.height/3, Screen.width/2, Screen.height/3), "START")) {
			GameName = GameName + "GameLevel" + GameLevel;
			Application.LoadLevel(GameName);
		}
		if (GUI.Button (new Rect(Screen.width/2, (Screen.height/3)*2, Screen.width/2, Screen.height/3), "Main Menu")) {
			Application.LoadLevel("MainMenu");
		}
	}
}
