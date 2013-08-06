using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {
	
	Vector3 acceleration;
	Vector2 cursorPosition;
	

	Vector3 accel = new Vector3(0,0,0);
	bool isButtonDown = false;
	bool isSecondButtonDown = false;
#if UNITY_ANDROID || UNITY_IOS
	bool isButtonUp = true;
	bool isSecondButtonUp = true;
#endif
	bool isEscapeButtonDown = false;
	bool isApplicationPaused = false;
	
	public float sensitivity = 40;
	
	Rect ignoreButtonsRect;
	
	// Use this for initialization
	void Start () {
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
		
		//position
		if (Input.touchCount==1) {
			cursorPosition = Input.GetTouch(0).position;
		}
		
		//ignore clicks on marked rect (for GUI)
		if(!ignoreButtonsRect.Contains(cursorPosition)) {
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
		}
		

		
#else

		accel = new Vector3(0,0,0);
		if (Input.GetKey(KeyCode.UpArrow)) {
			accel += new Vector3(0,0.5f,0);
		} else if (Input.GetKey(KeyCode.DownArrow)) {
			accel -= new Vector3(0,0.5f,0);
		}
		
		if (Input.GetKey(KeyCode.LeftArrow)) {
			accel -= new Vector3(0.5f,0,0);
		} else if (Input.GetKey(KeyCode.RightArrow)) {
			accel += new Vector3(0.5f,0,0);
		} 
		
		acceleration = accel;
		
		cursorPosition = Input.mousePosition;
		
		if(!ignoreButtonsRect.Contains(cursorPosition)) {
			isButtonDown = Input.GetMouseButtonDown(0);
			isSecondButtonDown = isButtonDown;
		}
		else print ("in rect");
#endif
		
		acceleration.x = Mathf.Clamp(acceleration.x, -0.5f, 0.5f) * sensitivity;
		acceleration.y = Mathf.Clamp(acceleration.y, -0.5f, 0.5f) * sensitivity;
		
	}
	
	void  OnApplicationPause(bool pauseStatus) {
		isApplicationPaused = pauseStatus;
	}
	
	public void IgnoreButtonEventsAt(Rect rect) {
		//converting GUI to Input reference system
		ignoreButtonsRect = new Rect(rect.x, Screen.height - rect.y - rect.height, rect.width, rect.height);
	}
	
	public bool IsApplicationPaused() {
		return isApplicationPaused;
	}
	
	public Vector3 GetAcceleration() {
		return acceleration;
	}
	
	//returns true if mouse left button is just pressed, or touch is pressed
	public bool IsCursorButtonDown() {
		return Input.GetMouseButtonDown(0);
	}
	
	public bool IsCursorButtonUp() {
		return Input.GetMouseButtonUp(0);	
	}
	
	public bool IsCursorButtonHold() {
		return Input.GetMouseButton(0);	
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
	
	//TODO merge with GetCursorPosition, which does not work correctly
	public Vector2 GetCurrentCursorPosition() {
		return Input.mousePosition;
	}
	
	public static Vector3 MouseScreenToGUI(){
		var mousePos = new Vector3(Input.mousePosition.x, Screen.height - Input.mousePosition.y,0);
		return mousePos;
	}
}
