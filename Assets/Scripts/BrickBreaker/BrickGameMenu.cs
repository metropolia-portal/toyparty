using UnityEngine;
using System.Collections;

public class BrickGameMenu : MonoBehaviour {
	
	public string MainMenuLevelId = "BrickGameMenu";
	public string LevelId = "BrickGameLevelOneTutorial";
	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
		GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "BRICK GAME TITLE");
		if (GUI.Button (new Rect(Screen.width/2, Screen.height/3, Screen.width/2, Screen.height/3), "START")) {
			Application.LoadLevel(2);
		}
		if (GUI.Button (new Rect(Screen.width/2, (Screen.height/3)*2, Screen.width/2, Screen.height/3), "BACK")) {
			Application.LoadLevel(MainMenuLevelId);
		}
	}
}
