using UnityEngine;
using System.Collections;

public abstract class Brush : MonoBehaviour {
	
	public float segmentLength = 0.05f;
	public GameObject brushEffectPrefub; //appears at the drawing position
	
	public AudioClip onDraw;
	
	InputManager input;
	Wizard wizard;
	
	protected virtual void Awake() {
		GameObject gm = GameObject.Find("GameManager");
		input = gm.GetComponent<InputManager>();
		wizard = GameObject.Find("Wizard").GetComponent<Wizard>();
	}
	
	protected virtual void Start(){
	}
	
	public void SetEnable(bool enable) {
		enabled = enable;
		
		if(!enable) {
			FinishDraw();
			wizard.OnEndDrawing();	
		}
		
		if(!enable && brushEffect)
			Destroy(brushEffect);
	}
	
	// Update is called once per frame
	virtual protected void Update () {	
		//started drawing
		if(input.IsCursorButtonDown() && !brushDown) {
			StartDraw(GetCursorPosition());
			
			//TODO bring smoothing to a separate module
			drawingPosition = GetCursorPosition(); //start drawing line straight where user points
			
		}
		
		//continue drawing
		else if(input.IsCursorButtonHold() && brushDown) {
			DrawTo(drawingPosition);
		}
		
		else if(input.IsCursorButtonUp() && brushDown) {
			DrawTo(drawingPosition);
			FinishDraw();
		}		
		
		if(brushEffectPrefub) {
			if(brushDown && ! brushEffect)
				brushEffect = (GameObject) Instantiate(brushEffectPrefub);
			if(!brushDown && brushEffect)
				Destroy(brushEffect);
			
			if(brushEffect) brushEffect.transform.position = drawingPosition;
		}
		
		//update smoothed user input
		MoveDrawingPosition(ref drawingPosition);
	}
	
	//override it to add smoothing, etc.
	protected virtual void MoveDrawingPosition(ref Vector2 refDrawPosition) {
		refDrawPosition = GetCursorPosition();
	}
	
	
	// start drawing of line at given position, pos - position in gameWorld, without z axis, as it is 0
	virtual protected void StartDraw(Vector2 pos) {
		brushDown = true;
		brushPosition = pos;
		
		wizard.OnStartDrawing();
		
		audio.loop = true;
		audio.clip = onDraw;
		audio.Play();
	}
	
	//continue drawing the line to the point given, called every frame
	virtual protected void DrawTo(Vector2 pos) {
		if(brushEffect) brushEffect.transform.position = new Vector3(pos.x, pos.y);
		
		while((brushPosition - pos).magnitude > segmentLength) {
		
			Vector2 to = brushPosition + (pos-brushPosition).normalized * segmentLength;
			DrawSegment(brushPosition, to);
			
			brushPosition = to;
		}
	}
	
	Vector2 brushPosition;
	
	protected abstract void DrawSegment(Vector2 from, Vector2 to);
	
	//finish drawing the line at last position
	virtual protected void FinishDraw() {
		brushDown = false; 
		wizard.OnEndDrawing();
		
		audio.Stop();
	}

	//get cursor position on gameworld
	protected Vector2 GetCursorPosition() {
		Vector2 screenPos = input.GetCurrentCursorPosition();
		//check what the pos is gonna be at z=0, therefore 3rd argument is cameras z distance to it
		Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, Mathf.Abs (Camera.main.transform.position.z))); 
		return new Vector2(worldPos.x, worldPos.y);//getting rid of z component
	}
	
	
	
	bool brushDown = false;
	private Vector2 drawingPosition;
	private GameObject brushEffect;
}
