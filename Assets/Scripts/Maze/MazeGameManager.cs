using UnityEngine;
using System.Collections;

public class MazeGameManager : MonoBehaviour {
	
	public GUIText statusLine;
	public GameObject world;
	public GameObject mouse;
	public Camera cam;
	public Bounds cameraBounds;
	public InputManager inputManager;

	enum GameState {Running, Paused, Victory, Defeat};
	GameState gameState = GameState.Running;
	
	
	float cameraBoundsHalfWidth;
	float cameraBoundsHalfHeight;
	
	
	// Use this for initialization
	void Start () {
		cameraBoundsHalfWidth = Mathf.Abs((cam.camera.ScreenToWorldPoint(new Vector3(Screen.width,0,1)) - cam.camera.ScreenToWorldPoint(Vector3.up)).x) / 2;
		cameraBoundsHalfHeight = Mathf.Abs((cam.camera.ScreenToWorldPoint(new Vector3(0,Screen.height,1)) - cam.camera.ScreenToWorldPoint(Vector3.up)).z) / 2;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (inputManager.IsEscapeButtonDown()) {
			RestartGame();
		}
		
		if (gameState == GameState.Paused) {
			statusLine.text = "Paused!\n"+GetTimeLeft();
			if (inputManager.IsButtonDown()) {
				ResumeGame();
			}
		}
		
		if (gameState == GameState.Running) {
			
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
	
	public void PauseGame() {

		gameState = GameState.Paused;
		Time.timeScale = 0;
	}
	
	public void ResumeGame() {
		gameState = GameState.Running;
		Time.timeScale = 1;
	}
	
	public void RestartGame() {
		ResumeGame();
		Application.LoadLevel("MazeLevel");
	}
	
	public int GetTimeLeft() {
		return (int)Mathf.Floor (mouse.GetComponent<Mouse>().GetWindupLeft());
	}
	
	public void EndGame(bool victory) {
		PauseGame ();
		
		if (victory) {
			gameState = GameState.Victory;
			statusLine.text = "Victory!\nTime score bonus: " + (GetTimeLeft()*55).ToString(); 
		} else {
			gameState = GameState.Defeat; 
			statusLine.text = "Defeat!\n"; 
		}
	}
}
