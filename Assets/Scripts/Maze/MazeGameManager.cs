using UnityEngine;
using System.Collections;

public class MazeGameManager : GameManager {
	
	public GUIText statusLine;
	public GameObject world;
	public GameObject mouse;
	public Camera cam;
	public Bounds cameraBounds;
	public InputManager inputManager;
	
	public int life = 3;

	PickupManager pickupManager;

	
	float cameraBoundsHalfWidth;
	float cameraBoundsHalfHeight;
	
	
	public void OnTrap() {
		life --;
	}
	
	// Use this for initialization
	void Start () {
		GetComponent<ScoreGUI>().SetMedalRequirements(bronzeMedalScore, silverMedalScore, goldMedalScore);
		pickupManager = GetComponent<PickupManager>();
		ResumeGame();
		cameraBoundsHalfWidth = Mathf.Abs((cam.camera.ScreenToWorldPoint(new Vector3(Screen.width,0,1)) - cam.camera.ScreenToWorldPoint(Vector3.up)).x) / 2;
		cameraBoundsHalfHeight = Mathf.Abs((cam.camera.ScreenToWorldPoint(new Vector3(0,Screen.height,1)) - cam.camera.ScreenToWorldPoint(Vector3.up)).z) / 2;
	}
	
	// Update is called once per frame
	void Update () {
		
		
		
		if (IsGameRunning()) {
						if (life <= 0) 
			{
				int totalScore = pickupManager.TotalScore();
				
				if (totalScore < bronzeMedalScore) SetMedal(Medal.None);
				else if (totalScore < silverMedalScore) SetMedal(Medal.Bronze);
				else if (totalScore < goldMedalScore) SetMedal(Medal.Silver);
				else SetMedal(Medal.Gold);
				
				EndGame();
				
			}	
			
			cam.camera.transform.localPosition = new Vector3(
				Mathf.Clamp(mouse.transform.localPosition.x, cameraBounds.min.x + cameraBoundsHalfWidth, cameraBounds.max.x - cameraBoundsHalfWidth),
				cam.camera.transform.localPosition.y,
				Mathf.Clamp(mouse.transform.localPosition.z, cameraBounds.min.z + cameraBoundsHalfHeight, cameraBounds.max.z - cameraBoundsHalfHeight));

			
		}
		
	}
	

}
