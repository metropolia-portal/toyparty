using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour {
	
	public float randomPosition = 0.01f;
	public float randomRotation = 3;
	
	enum CardState {FaceDown, RotatingRight, RotatingLeft, FaceUp, Disappearing};
	CardState state = CardState.FaceUp;
	
	Quaternion currentRotation;
	Quaternion nextRotation;
	Quaternion buf;
	AudioClip flipClick;
	AudioClip allRotateSound;
	AudioClip cardGoing;
	bool firstFlip = true;
	
	float timer = 0; // The timer used for the flipping animation
	
	string suit = "";
	float sizeReduction;

	// Use this for initialization
	void Start () {
		transform.rotation = Quaternion.Euler(0,0,Random.Range(-randomRotation, randomRotation));
		transform.position += Vector3.up*Random.Range(-randomPosition, randomPosition);
		transform.position += Vector3.right*Random.Range(-randomPosition, randomPosition);
		currentRotation = transform.rotation;
		nextRotation = Quaternion.Euler (0, 180, 0)*transform.rotation;
		flipClick = (AudioClip)Resources.Load("SoundFx/shuffle-01");
		allRotateSound = (AudioClip)Resources.Load("SoundFx/FlipTurn",(typeof(AudioClip)));
		cardGoing = (AudioClip)Resources.Load ("SoundFx/FlipSwift",(typeof(AudioClip)));
	}
	
	// Update is called once per frame
	void Update () {
		
		if (state == CardState.RotatingLeft || state == CardState.RotatingRight) { // This rotates the card 180 degrees around the vertical axis
			timer += Time.deltaTime;
			transform.rotation = Quaternion.Lerp (transform.rotation, nextRotation, timer);	
			if (timer >= 1) {
				if (state == CardState.RotatingLeft) state = CardState.FaceUp;
				if (state == CardState.RotatingRight) state = CardState.FaceDown;
				buf = currentRotation;
				currentRotation = nextRotation;
				nextRotation = buf;
				timer = 0;
			}
		}
		
		if (state == CardState.Disappearing) { // This makes the card grow smaller and destroys the object when the card is less than 1% of it's original size
			sizeReduction += Time.deltaTime / 2;
			transform.localScale -= new Vector3(sizeReduction, sizeReduction, sizeReduction);
			if (transform.localScale.x < 0.01f) {
				Destroy(gameObject);
			}
			
		}
	}
	
	
	public void Rotate() {
		if (state == CardState.FaceDown) {
			
			Camera.main.audio.PlayOneShot(flipClick, 0.5f);
			state = CardState.RotatingLeft;
		}
		if (state == CardState.FaceUp) {
			if(!firstFlip){
				
				Camera.main.audio.PlayOneShot(flipClick, 0.5f);
				
			}
			else{
				Camera.main.audio.PlayOneShot(allRotateSound, 0.3f);
				firstFlip = false;
			}
			
			state = CardState.RotatingRight;
		}
	}
	
	public bool IsFaceUp() {
		return state == CardState.FaceUp;
	}
	
	public bool IsFaceDown() {
		return state == CardState.FaceDown;
	}	
	
	public void SetSuit(string suit) {
		this.suit = suit;
	}
	
	public string GetSuit() {
		return suit;
	}
	
	public void Disappear() {
		state = CardState.Disappearing;
		Camera.main.audio.PlayOneShot(cardGoing, 0.5f);
	}
	
}
