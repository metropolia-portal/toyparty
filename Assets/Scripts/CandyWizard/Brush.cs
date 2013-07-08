using UnityEngine;
using System.Collections;

public abstract class Brush : MonoBehaviour {
	
	public float smoothingSpeed = 0.01f;
	public float minSegmentLength = 0.05f;
	
	
	public void Enable() {
		enabled = true;
	}
	
	public void Disable() {
		enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		//started drawing
		if(Input.GetMouseButton(0) && !brushDown) {
			StartDraw(GetCursorPosition());
			//TODO bring smoothing to a separate module
			smoothedCursorPosition = GetCursorPosition(); //start drawing line straight where user points
			
		}
		
		//continue drawing
		else if(Input.GetMouseButton(0) && brushDown) {
			DrawTo(smoothedCursorPosition);
		}
		
		else if(! Input.GetMouseButton(0) && brushDown) {
			DrawTo(smoothedCursorPosition);
			FinishDraw();
		}
		
		//update smoothed user input
		smoothedCursorPosition = Vector2.Lerp(smoothedCursorPosition, GetCursorPosition(), smoothingSpeed);
		//print ("smooth pos = "+ smoothedCursorPosition);
	}
	
	
	// start drawing of line at given position, pos - position in gameWorld, without z axis, as it is 0
	virtual protected void StartDraw(Vector2 pos) {
		Debug.Log("Started drawing");
		brushDown = true;
		brushPosition = pos;
	}
	
	//continue drawing the line to the point given, called every frame
	virtual protected void DrawTo(Vector2 pos) {
		//print ((brushPosition - pos).magnitude);
		if( (brushPosition - pos).magnitude > minSegmentLength) {
			Debug.Log("Drawing...");
			DrawSegment(brushPosition, pos);
			
			brushPosition = pos;
		}
	}
	
	Vector2 brushPosition;
	
	protected abstract void DrawSegment(Vector2 from, Vector2 to);
	
	//finish drawing the line at last position
	virtual protected void FinishDraw() {
		Debug.Log("Finished drawing");
		brushDown = false;
	}

	//get cursor position on gameworld
	Vector2 GetCursorPosition() {
		Vector2 screenPos = InputManager.Instance().GetCursorPosition();
		//check what the pos is gonna be at z=0, therefore 3rd argument is cameras z distance to it
		Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, Mathf.Abs (Camera.main.transform.position.z))); 
		return new Vector2(worldPos.x, worldPos.y);//getting rid of z component
	}
	
	
	bool brushDown = false;
	Vector2 smoothedCursorPosition;
}
