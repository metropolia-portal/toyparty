using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {
	static InputManager instance = null;
	
	Vector3 acceleration;
	Vector2 cursorPosition;
	
	bool isButtonDown = false;
	bool isButtonUp = true;
	
	bool isSecondButtonDown = false;
	bool isSecondButtonUp = true;
	
	bool isEscapeButtonDown = false;
	bool isApplicationPaused = false;
	

	
	public float sensitivity = 40;
	bool inputEnabled = true;
	
	bool tapDown = false;
	
	//we have only one instance each game
	static public InputManager Instance() {
		return instance;
	}
	
	// Use this for initialization
	void Start () {
		instance = this;
		
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		cursorPosition = new Vector2(0,0);
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKeyDown(KeyCode.Escape)){
			isEscapeButtonDown = true;
		} else {
			isEscapeButtonDown = false;
		}
		
#if UNITY_ANDROID || UNITY_IOS
		acceleration = Input.acceleration;
		
		//button 1
		if(Input.touchCount == 0) {
			isButtonUp = true; 
			isButtonDown = 	false;
		}
		
		if(Input.touchCount == 1) {
			if(isButtonUp) isButtonDown = true;
			isButtonUp = false;
		}
		
		//button 2
		
		if(Input.touchCount == 2) {
			if(isSecondButtonUp) isSecondButtonDown = true;
			isSecondButtonUp = false;
		} else if(Input.touchCount < 2) {
			isSecondButtonUp = true;
			isSecondButtonDown = false;
		}
		
		//position
		if (Input.touchCount==1) {
			cursorPosition = Input.GetTouch(0).position;
		}
		
#else
		acceleration = new Vector3 (2*Input.mousePosition.x/Screen.width - 1, 2*Input.mousePosition.y/Screen.height - 1, 0);
		cursorPosition = Input.mousePosition;
		
		isButtonDown = Input.GetMouseButtonDown(0);
		isSecondButtonDown = isButtonDown;
#endif
		
		acceleration.x = Mathf.Clamp(acceleration.x, -0.5f, 0.5f) * sensitivity;
		acceleration.y = Mathf.Clamp(acceleration.y, -0.5f, 0.5f) * sensitivity;
		
	}
	
	void  OnApplicationPause(bool pauseStatus) {
		isApplicationPaused = pauseStatus;
	}
	
	public bool IsApplicationPaused() {
		return isApplicationPaused;
	}
	
	public Vector3 GetAcceleration() {
		return acceleration;
	}
	
	//returns true if mouse left button is hold, or touch is hold
	public bool IsCursorButtonDown() {
		return isButtonDown;
	}
	
	//TODO Depricated, does not do what it says, rename it, and use smth else
	public bool IsButtonDown() {
		//TODO fix so that more that one can use this, it resets!
        if (isButtonDown) {
            isButtonDown = false;
            return true;
        } else {
            return false;
        }
	}
	
	//tap while holding other finger on touch devices
	public bool IsSecondButtonDown() {
		if (isSecondButtonDown) {
            isSecondButtonDown = false;
            return true;
        } else {
            return false;
        }
	}
	
	public bool IsEscapeButtonDown() {
		return isEscapeButtonDown;
	}
	public Vector2 GetCursorPosition() {
		return cursorPosition;
	}
	public void DisableInput() {
		inputEnabled = false;
	}
	
	public void EnableInput() {
		inputEnabled = true;
	}
	
}
