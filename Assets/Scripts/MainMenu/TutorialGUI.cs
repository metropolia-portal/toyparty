using UnityEngine;
using System.Collections;

public class TutorialGUI : MonoBehaviour {
	
	GUIStyle NoStyle = new GUIStyle();
	Texture Background, PlayButton, MainMenuButton;
	float Margin = Screen.width/45;
	AudioScript audioSource;

	// Use this for initialization
	void Start () {
		
		Background = (Texture)Resources.Load("TutorialMenu/Previews/" + MainMenuGUI.selectedGameName);
		PlayButton = (Texture)Resources.Load("MenuCommon/play_" + MainMenuGUI.selectedGameName);
		MainMenuButton = (Texture)Resources.Load("MenuCommon/home_" + MainMenuGUI.selectedGameName);
		audioSource = (AudioScript)GameObject.FindObjectOfType(typeof(AudioScript));
	}
	
	void OnGUI() {
		GUI.Box(new Rect(0, 0, Screen.width, Screen.height), Background);
		if (GUI.Button (new Rect(Margin, Screen.height - (Screen.width/6), Screen.width/7, Screen.width/7), MainMenuButton, NoStyle)) {
			Application.LoadLevel("MainMenu");
		}
		if (GUI.Button (new Rect(Screen.width -Screen.width/6, Screen.height - (Screen.width/6), Screen.width/7, Screen.width/7), PlayButton, NoStyle)) {
			StartCoroutine(WaitForEndOfMusic());
			
		}
	}
	IEnumerator WaitForEndOfMusic(){
		yield return StartCoroutine(audioSource.FadeOutVolume());
		Application.LoadLevel(MainMenuGUI.selectedGameName+"_level_"+MainMenuGUI.currentLevel);
	}
}
