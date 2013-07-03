using UnityEngine;
using System.Collections;

public class Brush : MonoBehaviour {
	
	public void Enable() {
		
	}
	
	public void Disable() {
		
	}
	
	// start drawing of line at given position, pos - position in gameWorld, without z axis, as it is 0
	protected void StartDraw(Vector2 pos) {
		
	}
	
	//continue drawing the line to the point given, called every frame
	protected void DrawTo(Vector2 pos) {
		
	}
	
	//finish drawing the line at last position
	protected void FinishDraw() {
		
	}
	
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//started drawing
		if(InputManager.Instance().IsButtonDown() && !brushDown) {
			StartDraw(GetCursorPosition());
			brushDown = true;
		}
		
		//continue drawing
		if(InputManager.Instance().IsButtonDown() && brushDown) {
			DrawTo();
		}
		
		//
	
	}
	
	//get cursor position on gameworld
	Vector2 GetCursorPosition() {
		Vector2 screenPos = InputManager.Instance().GetCursorPosition();
		Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 0));
		return new Vector2(worldPos.x, worldPos.y);//getting rid of z component
	}
	
	bool brushDown = false;
}
