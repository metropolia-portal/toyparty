using UnityEngine;
using System.Collections;

public class BrickGameTutorial : MonoBehaviour {
	
	public string MenuLevelId = "BrickGameMenu";
	public string LevelId = "BrickGameLevelOne";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
		GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "BRICK GAME LEVEL ONE TUTORIAL");
		if (GUI.Button (new Rect(0, (Screen.height/3)*2, Screen.width/2, Screen.height/3), "Main Menu")) {
			Application.LoadLevel("MainMenu");
		}
		if (GUI.Button (new Rect(Screen.width/2, (Screen.height/3)*2, Screen.width/2, Screen.height/3), "START")) {
			Application.LoadLevel(LevelId);
		}
	}
}
