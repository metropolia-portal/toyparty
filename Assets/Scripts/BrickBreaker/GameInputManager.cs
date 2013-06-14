using UnityEngine;
using System.Collections;

public class GameInputManager : MonoBehaviour {
	
#if UNITY_ANDROID || UNITY_IPHONE	
	Vector3 lastTouchPosition = Vector3.zero;
#endif
	bool inputEnabled = true;
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
#if UNITY_ANDROID || UNITY_IPHONE
		if( Input.touchCount < 2 )
			lastTouchPosition = Input.GetTouch(0).position;
#endif
	}
	
	public void DisableInput() {
		inputEnabled = false;
	}
	
	public void EnableInput() {
		inputEnabled = true;
	}
	
	public Vector3 GetCursorPosition(){
#if UNITY_ANDROID || UNITY_IPHONE
		if( Input.touchCount < 2 )
			return Input.GetTouch(0).position;
		else
			return lastTouchPosition; 
#else	
		return Input.mousePosition;
#endif
	}
	
	public bool IsButtonDown(){
		if(inputEnabled) {
#if UNITY_ANDROID || UNITY_IPHONE
		return false;
#else
		return Input.GetMouseButtonDown(1);		
#endif
		}
		else
			return false;
		
	}

}
