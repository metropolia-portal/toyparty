using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class FlipsGameManager : GameManager {
	
	public float revealTime = 3; // The time, in seconds, for which the cards are revealed at the beginning of the level
	
	public InputManager inputManager;
	public LevelGenerator levelGenerator;
	public GUIText statusLine;
	
	float goTimer;

	Card firstCard = null; // Handles to the two cards the player is currently flipping
	Card secondCard = null;
	
	int cardsTotal;
	int cardsGuessed = 0;
	int flips = 0;
	
	float endGameTimer = -1;
	bool doEndGame = false;
	
	public Texture2D catTextureNormal;	
	
	// Use this for initialization
	public override void Start () 
	{
		base.Start ();
		cardsTotal = levelGenerator.CardCount();
		SetGameState(GameState.Pregame);
		statusLine.pixelOffset = new Vector2(Screen.width/2, -Screen.height /2 );
	}
	
	// Update is called once per frame
	void Update () {
		
		if (GetGameState() == GameState.Pregame) {
			
			if (inputManager.IsEscapeButtonDown()) PauseGame();
			revealTime -= Time.deltaTime;
			if (revealTime <=0) {
				goTimer = 1;
				HideAllCards();
				base.SetGameState(GameState.Running);
				UpdateStatus();
			}
			
			UpdateStatus();
		}
		
		if (GetGameState() == GameState.Running) {
			UpdateStatus();
			
			if (doEndGame) {
				endGameTimer -= Time.deltaTime;
				if (endGameTimer <= 0) {
					EndGame();
				}
			}
			
			if (inputManager.IsEscapeButtonDown()) PauseGame();

			Camera cam = Camera.main;
			
			Ray ray = cam.ScreenPointToRay(inputManager.GetCursorPosition());
	   		RaycastHit hit;
		
	        if (inputManager.IsButtonDown() && Physics.Raycast(ray, out hit) && !secondCard){
				if (hit.collider.CompareTag("Card")) {
					Card card = hit.collider.gameObject.transform.parent.GetComponent<Card>();
					if (card.IsFaceDown()) {
						flips ++;
						card.Rotate();
						if (firstCard==null) firstCard = card;
						else {
							secondCard = card;
						}
					}
				}
			}
			
			if (secondCard != null) {
				if (firstCard.IsFaceUp() && secondCard.IsFaceUp()) {
					if (firstCard.GetSuit() == secondCard.GetSuit()) {
						cardsGuessed += 2;
						firstCard.Disappear();
						secondCard.Disappear();
						if (cardsGuessed >= cardsTotal) {
							if (GetSuccessRatio()<0.1f) SetMedal(Medal.None);
							else if (GetSuccessRatio()<0.3f) SetMedal(Medal.Bronze);
							else if (GetSuccessRatio()<0.6f) SetMedal(Medal.Silver);
							else SetMedal(Medal.Gold);
							endGameTimer = 1;
							doEndGame = true;
							return;
						}
					}else{
						firstCard.Rotate();
						secondCard.Rotate ();
					}
					
					firstCard = null;
					secondCard = null;
					
					UpdateStatus();
					
				}
			}
		}
		
		EnableCheats();
	}
	
	void EnableCheats() {
		if(Input.GetKey(KeyCode.B)) {
			SetMedal(Medal.Bronze);
			EndGame();
		}
		if(Input.GetKey(KeyCode.S)) {
			SetMedal(Medal.Silver);
			EndGame();
		}
		if(Input.GetKey(KeyCode.G)) {
			SetMedal(Medal.Gold);
			EndGame();
		}
	}
	
	public float GetSuccessRatio() {
		return (float)cardsGuessed/(float)flips;
	}
	
	public void ShowAllCards() {
		Card card;
		foreach (GameObject cardBack in GameObject.FindGameObjectsWithTag("Card")) {
			card = cardBack.transform.parent.GetComponent<Card>();
			if (card.IsFaceDown()) card.Rotate();
		}
	}
	
	
	public void HideAllCards() {
		Card card;
		foreach (GameObject cardBack in GameObject.FindGameObjectsWithTag("Card")) {
			card = cardBack.transform.parent.GetComponent<Card>();
			if (card.IsFaceUp()) card.Rotate();
		}
	}
	
	
	public void UpdateStatus() {
		
		switch (GetGameState()) {
			case GameState.Pregame:
				
				statusLine.guiText.material.color = new Color(1f,1f,1f,1.5f - (Mathf.Ceil(revealTime) - revealTime));
				statusLine.text = (Mathf.Ceil(revealTime)).ToString();
				statusLine.fontSize = (int)(80 + 100*(Mathf.Ceil(revealTime) - revealTime));
			break;			
				
			case GameState.Running:
				if (goTimer > 0) {
					
				
					revealTime = goTimer;
					statusLine.text = "Go!";
					statusLine.guiText.material.color = new Color(1f,1f,1f,1.5f - (Mathf.Ceil(revealTime) - revealTime));
					statusLine.fontSize = (int)(80 + 100*(Mathf.Ceil(revealTime) - revealTime));
				goTimer -= Time.deltaTime;
				}else {
				statusLine.text=  "";
			}
			break;
		}
		
	}
	
}
