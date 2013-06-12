using UnityEngine;
using System.Collections;

public class FlipsGameManager : GameManager {
	
	public float revealTime = 3; // The time, in seconds, for which the cards are revealed at the beginning of the level
	
	public InputManager inputManager;
	public LevelGenerator levelGenerator;
	public GUIText statusLine;
	
	Card firstCard = null; // Handles to the two cards the player is currently flipping
	Card secondCard = null;
	
	int cardsTotal;
	int cardsGuessed = 0;
	int flips = 0;
	
	
	
	// Use this for initialization
	void Start () {
		cardsTotal = levelGenerator.CardCount();
		gameState = GameState.Pregame;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown(KeyCode.Escape)) { Application.Quit(); }
		
		if (gameState == GameState.Pregame) {
			UpdateStatus();
			revealTime -= Time.deltaTime;
			if (revealTime <=0) {
				HideAllCards();
				gameState = GameState.Running;
				UpdateStatus();
			}
		}
		
		if (gameState == GameState.Running) {

			Camera cam = (Camera)FindObjectOfType(typeof(Camera));
			
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
							OnGameOver(); 
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
	}
	
	void OnGameOver() {
		//Debug.LogWarning("VICTORY");
		//Debug.Log ((Mathf.Ceil(GetSuccessRatio()*100)).ToString()+"% Accuracy");
		gameState = GameState.Victory;
		UpdateStatus();
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
		
		switch (gameState) {
			case GameState.Pregame:
				statusLine.text = "Look at the cards... "+(Mathf.Ceil(revealTime)).ToString();
			break;			
				
			case GameState.Running:
				statusLine.text = "Accuracy so far: ";
				if (flips > 1)
					statusLine.text += (Mathf.Ceil(GetSuccessRatio()*100)).ToString()+"%";
				else
					statusLine.text += "N/A";
			break;
			case GameState.Victory:
				statusLine.text = ("Victory! "+Mathf.Ceil(GetSuccessRatio()*100)).ToString()+"% Accuracy";
			break;
		}
		
	}
	
}
