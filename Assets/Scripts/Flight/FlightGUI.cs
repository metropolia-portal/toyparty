using UnityEngine;
using System.Collections;

public class FlightGUI : MonoBehaviour {
	
	public Texture2D PadTexture;
	public Texture2D PadHandleTexture;
	
	float padHandleRadius;
	
	float padTriggerRadius = Screen.height/5;
	float padReleaseRadius = Screen.height/2.5f;
	float padMargin = Screen.height / 30;
	
	Vector2 padCenter;
	
	Vector2 padDirection;
	
	Vector2 pcPadDir;
	
	bool drag = false;
	
	public Vector2 GetPadDirection() {
		return padDirection;
	}
	
	// Use this for initialization
	void Start () {
		padCenter = new Vector2(padMargin + padTriggerRadius, Screen.height - padTriggerRadius - padMargin);
		padHandleRadius = padTriggerRadius / 5;
	}
	
	// Update is called once per frame
	void Update () {
#if UNITY_ANDROID || UNITY_IOS
		bool padTouched = false;
		if (Input.touchCount > 0)
			for (int i=0; i < Input.touchCount; i++) {
				Touch touch = Input.GetTouch(i);
				if ( (touch.phase != TouchPhase.Canceled) && (touch.phase != TouchPhase.Ended) ) {
					Vector2 position = new Vector2(touch.position.x, Screen.height-touch.position.y);
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
			padDirection = new Vector2(0,0,0);
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
		
		padDirection = pcPadDir.normalized;
#endif
	}
	
	void OnGUI() {
		GUI.color = new Color(1,1,1,0.5f);
		if (padDirection.magnitude>0) GUI.color = new Color(1,1,1,0.9f);
		GUI.DrawTexture(new Rect(padCenter.x - padTriggerRadius, padCenter.y - padTriggerRadius, padTriggerRadius*2, padTriggerRadius*2), PadTexture);
		Vector2 padHandleCenter = padCenter + padDirection*padTriggerRadius;
		GUI.color = new Color(1,1,1,1);
		GUI.DrawTexture(new Rect(padHandleCenter.x - padHandleRadius, padHandleCenter.y - padHandleRadius, padHandleRadius*2, padHandleRadius*2), PadHandleTexture);
	}
}
