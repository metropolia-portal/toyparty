using UnityEngine;
using System.Collections;

public class CreditsGUI : MonoBehaviour {
	
	Texture creditsTexture, MainMenuButton;
	float Margin = Screen.width/45;
	GUIStyle NoStyle = new GUIStyle();
	
	// Use this for initialization
	void Start () {
		
		creditsTexture = (Texture)Resources.Load("Credits/creditsMenu");
		MainMenuButton = (Texture)Resources.Load("MenuCommon/home_brick");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {	
		
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), creditsTexture);
		
		
		if (GUI.Button (new Rect(Margin, Screen.height - (Screen.width/6), Screen.width/7, Screen.width/7), MainMenuButton, NoStyle)) {
			Camera.DestroyObject(Camera.main.audio);
			Application.LoadLevel("MainMenu");
		}
	}
}
