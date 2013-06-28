using UnityEngine;
using System.Collections;

public class FlightGUI : MonoBehaviour {
	
	public Texture2D PadTexture;
	public Texture2D PadHandleTexture;
	public Texture2D buttonATexture;
	public Texture2D buttonBTexture;
	
	GameManager gameManager;
	
	float padHandleRadius;
	
	float padTriggerRadius = Screen.height/5;
	float padReleaseRadius = Screen.height/2.5f;
	float padMargin = Screen.height / 30;
	
	float buttonARadius = Screen.height / 10;
	float buttonBRadius = Screen.height / 15;
	
	
	
	Vector2 buttonACenter;
	Vector2 buttonBCenter;
	
	Vector2 padCenter;
	
	Vector2 padDirection;
	
	Vector2 pcPadDir;
	
	bool drag = false;
	
	bool buttonADown = false;
	bool buttonBDown = false;
	
	public Vector2 GetPadDirection() {
		return padDirection;
	}
	
	public bool GetButtonA() {
		return buttonADown;
	}
	
	public bool GetButtonB() {
		return buttonBDown;
	}
	
	// Use this for initialization
	void Start () {
		buttonACenter = new Vector2(Screen.width - (padMargin + buttonARadius*3), Screen.height - (padMargin + buttonARadius));
		buttonBCenter = new Vector2(Screen.width - (padMargin + buttonBRadius*2), Screen.height - (padMargin + buttonARadius*2 + buttonBRadius));
		
		padCenter = new Vector2(padMargin + padTriggerRadius, Screen.height - padTriggerRadius - padMargin);
		padHandleRadius = padTriggerRadius / 5;
		
		gameManager = GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
#if UNITY_ANDROID || UNITY_IOS
		bool padTouched = false;
		buttonADown = false;
	    buttonBDown = false;
		if (Input.touchCount > 0)
			for (int i=0; i < Input.touchCount; i++) {
				Touch touch = Input.GetTouch(i);
				if ( (touch.phase != TouchPhase.Canceled) && (touch.phase != TouchPhase.Ended) ) {
					Vector2 position = new Vector2(touch.position.x, Screen.height-touch.position.y);
					float distanceA = Vector2.Distance(position, buttonACenter);
					float distanceB = Vector2.Distance(position, buttonBCenter);
				
					if (distanceA < buttonARadius) buttonADown = true;
				
					if (distanceB < buttonBRadius) buttonBDown = true;
					
					if (!padTouched) {
						float distance = Vector2.Distance(position, padCenter);
						if (distance < padTriggerRadius) {
							drag = true;
							padTouched = true;
							padDirection = (position - padCenter) / padTriggerRadius;
						} else if (drag && (distance < padReleaseRadius)) {
							padTouched = true;
							padDirection = (position - padCenter).normalized;
						}
					}
				}
			}
		if (drag && !padTouched) {
			drag = false;
			padDirection = new Vector2(0,0);
		}
		
		
		
		
		
#else
		
		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			pcPadDir += new Vector2(-1, 0);
		}
		if (Input.GetKeyDown(KeyCode.RightArrow)) {
			pcPadDir += new Vector2(1, 0);
		}
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			pcPadDir += new Vector2(0, -1);
		}
		if (Input.GetKeyDown(KeyCode.DownArrow)) {
			pcPadDir += new Vector2(0, 1);
		}
		if (Input.GetKeyDown(KeyCode.Z)) {
			buttonADown = true;
		}
		if (Input.GetKeyDown(KeyCode.X)) {
			buttonBDown = true;
		}
		
		
		if (Input.GetKeyUp(KeyCode.LeftArrow)) {
			pcPadDir -= new Vector2(-1, 0);
		}
		if (Input.GetKeyUp(KeyCode.RightArrow)) {
			pcPadDir -= new Vector2(1, 0);
		}
		if (Input.GetKeyUp(KeyCode.UpArrow)) {
			pcPadDir -= new Vector2(0, -1);
		}
		if (Input.GetKeyUp(KeyCode.DownArrow)) {
			pcPadDir -= new Vector2(0, 1);
		}
		if (Input.GetKeyUp(KeyCode.Z)) {
			buttonADown = false;
		}
		if (Input.GetKeyUp(KeyCode.X)) {
			buttonBDown = false;
		}
		
		
		
		padDirection = pcPadDir.normalized;
#endif
	}
	
	void OnGUI() {
		
		if (!gameManager.IsGameRunning()) return;
		
		Color transparent = new Color(1,1,1,0.5f);
		Color opaque = new Color(1,1,1,0.9f);
		
		if (padDirection.magnitude>0) 
			GUI.color = opaque;
		else
			GUI.color = transparent;
		
		GUI.DrawTexture(new Rect(padCenter.x - padTriggerRadius, padCenter.y - padTriggerRadius, padTriggerRadius*2, padTriggerRadius*2), PadTexture);
		
		Vector2 padHandleCenter = padCenter + padDirection*padTriggerRadius;
		GUI.color = opaque;
		GUI.DrawTexture(new Rect(padHandleCenter.x - padHandleRadius, padHandleCenter.y - padHandleRadius, padHandleRadius*2, padHandleRadius*2), PadHandleTexture);
		
		if (buttonADown)
			GUI.color = opaque;
		else
			GUI.color = transparent;
		GUI.DrawTexture(new Rect(buttonACenter.x - buttonARadius, buttonACenter.y - buttonARadius, buttonARadius*2, buttonARadius*2), buttonATexture);
		
		if (buttonBDown)
			GUI.color = opaque;
		else
			GUI.color = transparent;
		GUI.DrawTexture(new Rect(buttonBCenter.x - buttonBRadius, buttonBCenter.y - buttonBRadius, buttonBRadius*2, buttonBRadius*2), buttonBTexture);
	}
}
