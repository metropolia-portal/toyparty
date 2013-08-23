using UnityEngine;
using System.Collections;

public class CandyWizardGameManager : GameManager {
	public float minVelocity = 0.01f;
	//TODO use gameobject.find for every object that is present in every scene
	public Brush lineBrush;
	public Brush speedupBrush;
	public Brush eraserBrush;
	
	public bool speedupAllowed = false;
	
	public LayerMask forbidDrawLineLayerMask;
		
	//TODO check if that's reset if going to main menu then back here
	int collectedStars = 0;
			
	Candy candy;
	Wizard wizard;
	
	override protected void Awake() {
		base.Awake();
		
		wizard = GameObject.Find("Wizard").GetComponent<Wizard>();
		candy = GameObject.Find("Candy").GetComponent<Candy>();
		eraserBrush = GameObject.Find("EraserBrush").GetComponent<Brush>(); 
	}
	
	// Use this for initialization
	public override void Start () {
		SetGameState(GameState.Pregame);
		
		lineBrush.SetEnable(true);
		speedupBrush.SetEnable(false);
		eraserBrush.SetEnable(false);
		
		GetComponent<GameGUI>().enableSpeedup = speedupAllowed;
		GetComponent<GameGUI>().enableEraser = true;
	}
	
	public void ReplayLevel() {
		candy.Respawn();
		Start();		
	}
	
	// Update is called once per frame
	void Update () {	
#if UNITY_EDITOR
		EnableCheats();		
#endif
	}
	public void OnStarCollected() {
		collectedStars ++;
	}
	
	public void OnCandyEaten() {
		
		Medal medal = Medal.None;
		switch (collectedStars) {
			case 0:
				medal = Medal.Bronze;
				break;
			case 1:
				medal = Medal.Silver;
				break;
			case 2:
				medal = Medal.Gold;
				break;
		}
		FinishGame(medal);

	}
	
	public void OnCandyStuck() {
		FinishGame(Medal.None);	
	}

	public void OnCandyClicked() {
		StartGame();
	}
	
	//checks that a segment of line can be at particular point in game world and relevant screen position
	public bool CanDrawLineAt(Vector3 pos) {
		if(gameState != GameState.Pregame)
			return false;			
		if(Physics.OverlapSphere(pos, 0.01f, forbidDrawLineLayerMask).Length > 0)
			return false;
		else
			return true;
	}
	
	public void SetSpeedUpBrushActive(bool active) {
		speedupBrush.SetEnable(active);
		lineBrush.SetEnable(!active);
		eraserBrush.SetEnable(false);
	}
	
	public void SetRubberBrushActive(bool active) {
		eraserBrush.SetEnable(active);
		lineBrush.SetEnable(!active);
		speedupBrush.SetEnable(false);	
	}
	
	void FinishGame(Medal medal) {
		SetMedal(medal);
		SetGameState(GameState.Over);
	}
	
	void EnableCheats() {
		if(Input.GetKeyUp(KeyCode.R))
			RestartGame();	
		
		if(Input.GetKeyUp(KeyCode.Space))
			StartGame();
			
		if(Input.GetKeyUp(KeyCode.C)) {
			ReplayLevel();		
		}
		
		if(Input.GetKeyUp(KeyCode.RightArrow))
			Time.timeScale *= 2;
	}
	
	void StartGame() {
		SetGameState(GameState.Running);
		
		GetComponent<GameGUI>().enableSpeedup = false;
		GetComponent<GameGUI>().enableEraser = false;
		
		lineBrush.SetEnable(false);
		speedupBrush.SetEnable(false);
		eraserBrush.SetEnable(false);
		
		candy.Drop ();
	}
	
	public void SetCandy(Vector3 position)
	{
		candy.transform.position = position;
		candy.rigidbody.isKinematic = true;
	}
}
