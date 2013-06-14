using UnityEngine;
using System.Collections;

public class MazeGameManager : GameManager {
	
	public GUIText statusLine;
	public GameObject world;
	public GameObject mouse;
	public Camera cam;
	public Bounds cameraBounds;
	public InputManager inputManager;

	
	float cameraBoundsHalfWidth;
	float cameraBoundsHalfHeight;
	
	
	// Use this for initialization
	void Start () {
		SetGameState(GameManager.GameState.Running);
		cameraBoundsHalfWidth = Mathf.Abs((cam.camera.ScreenToWorldPoint(new Vector3(Screen.width,0,1)) - cam.camera.ScreenToWorldPoint(Vector3.up)).x) / 2;
		cameraBoundsHalfHeight = Mathf.Abs((cam.camera.ScreenToWorldPoint(new Vector3(0,Screen.height,1)) - cam.camera.ScreenToWorldPoint(Vector3.up)).z) / 2;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (IsGameRunning()) {
		
		if (inputManager.IsEscapeButtonDown()) {
			PauseGame();
		}
			
			if (inputManager.IsApplicationPaused()) {
				PauseGame ();
			}
			
			statusLine.text = GetTimeLeft().ToString();			
			if (GetTimeLeft() <= 0) 
			{
				EndGame(false);
			}	
			
			cam.camera.transform.localPosition = new Vector3(
				Mathf.Clamp(mouse.transform.localPosition.x, cameraBounds.min.x + cameraBoundsHalfWidth, cameraBounds.max.x - cameraBoundsHalfWidth),
				cam.camera.transform.localPosition.y,
				Mathf.Clamp(mouse.transform.localPosition.z, cameraBounds.min.z + cameraBoundsHalfHeight, cameraBounds.max.z - cameraBoundsHalfHeight));

			
		}
		
	}
	

	
	public int GetTimeLeft() {
		return (int)Mathf.Floor (mouse.GetComponent<Mouse>().GetWindupLeft());
	}
	
	public void EndGame(bool victory) {
		PauseGame ();
		SetGameState(GameState.Over);
		if (victory) {
			
			SetMedal(Medal.Gold);
			statusLine.text = "Victory!\nTime score bonus: " + (GetTimeLeft()*55).ToString(); 
		} else {
			SetMedal(Medal.None);
			statusLine.text = "Defeat!\n"; 
		}
	}
}
