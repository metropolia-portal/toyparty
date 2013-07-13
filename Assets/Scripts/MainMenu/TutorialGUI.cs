using UnityEngine;
using System.Collections;

public class TutorialGUI : MonoBehaviour {
	
	GUIStyle NoStyle = new GUIStyle();
	Texture Background, PlayButton, MainMenuButton;
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
		if (MGUI.HoveredButton (new Rect(MGUI.Margin, Screen.height - MGUI.menuButtonWidth - MGUI.Margin, MGUI.menuButtonWidth, MGUI.menuButtonWidth), MainMenuButton)) {
			Application.LoadLevel("MainMenu");
		}
		if (MGUI.HoveredButton (new Rect(Screen.width - MGUI.menuButtonWidth - MGUI.Margin, Screen.height - MGUI.menuButtonWidth - MGUI.Margin, MGUI.menuButtonWidth, MGUI.menuButtonWidth), PlayButton)) {
			StartCoroutine(WaitForEndOfMusic());
			
		}
	}
	IEnumerator WaitForEndOfMusic(){
		yield return StartCoroutine(audioSource.FadeOutVolume());
		Application.LoadLevel(MainMenuGUI.selectedGameName+"_level_"+MainMenuGUI.currentLevel);
	}
}
