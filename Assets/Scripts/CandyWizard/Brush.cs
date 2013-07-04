using UnityEngine;
using System.Collections;

public class Brush : MonoBehaviour {
	
	public float smoothingSpeed = 0.01f;
	public float minSegmentLength = 0.05f;
	
	public Material blockMaterial;
	public PhysicMaterial blockPhysicsMaterial;
	public PhysicMaterial colliderMeshMaterial;
	
	
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
			brushDown = true;
		}
		
		//continue drawing
		else if(Input.GetMouseButton(0) && brushDown) {
			DrawTo(smoothedCursorPosition);
		}
		
		else if(! Input.GetMouseButton(0) && brushDown) {
			DrawTo(smoothedCursorPosition);
			FinishDraw();
			brushDown = false;
		}
		
		//update smoothed user input
		smoothedCursorPosition = GetCursorPosition();//Vector2.Lerp(smoothedCursorPosition, GetCursorPosition(), smoothingSpeed);
		//print ("smooth pos = "+ smoothedCursorPosition);
	}
	
	
	// start drawing of line at given position, pos - position in gameWorld, without z axis, as it is 0
	protected void StartDraw(Vector2 pos) {
		Debug.Log("Started drawing");
		brushPosition = pos;
	}
	
	//continue drawing the line to the point given, called every frame
	protected void DrawTo(Vector2 pos) {
		//print ((brushPosition - pos).magnitude);
		if( (brushPosition - pos).magnitude > minSegmentLength) {
			Debug.Log("Drawing...");
			createBox(toVector3(brushPosition), toVector3(pos));
			createBoxWithoutFrontSide(toVector3(pos), toVector3(brushPosition));
			
			brushPosition = pos;
		}
	}
	
	//finish drawing the line at last position
	protected void FinishDraw() {
		Debug.Log("Finished drawing");
	}
	
	Vector2 brushPosition;
	
	
	
	
	// Use this for initialization
	void Start () {
	
	}
	

	//get cursor position on gameworld
	Vector2 GetCursorPosition() {
		Vector2 screenPos = InputManager.Instance().GetCursorPosition();
		print (screenPos);
		Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 10));
		return new Vector2(worldPos.x, worldPos.y);//getting rid of z component
	}
	
	Vector3 toVector3(Vector2 vec) {
		return new Vector3(vec.x, vec.y);	
	}
	
	void createBoxWithoutFrontSide (Vector3 p1, Vector3 p2)
	{	
		
		//TODO: recalculate border. Add one more side.
		// 
		Vector3 topLeftFront = p1;
		Vector3 topRightFront = p2;
		Vector3 topLeftBack = p1;
		Vector3 topRightBack = p2;
				
		Vector3 bottomLeftFront;
		Vector3 bottomRightFront;
		// TODO: check
		Vector3 backLeft = p1;
		Vector3 backRight = p2;
		
		
		
		topRightFront.z = 0.5f;
		topLeftFront.z = 0.5f;
		topLeftBack.z = -0.5f;
		topRightBack.z = -0.5f;
		
		float l = Vector3.Magnitude (p1 - p2);		
		float b = Mathf.Sqrt (l * l + blockHeight * blockHeight);
		float x1 = l * blockHeight / b;
		float y1 = blockHeight * blockHeight / b;		
		
		bottomLeftFront = topLeftFront;
		bottomRightFront = topRightFront;
		
		/*	
		bottomLeftFront.y -= y1;
		bottomLeftFront.x -= x1;
		bottomRightFront.y -= y1;
		bottomRightFront.x -= x1;
		*/
		
		bottomLeftFront.y -= blockHeight;
		bottomRightFront.y -= blockHeight;
		
		backLeft = bottomLeftFront;
		backLeft.z = -0.5f;		
		backRight = bottomRightFront;
		backRight.z = -0.5f;
		
		
		GameObject newLedge = new GameObject ();
		Mesh newMesh = new Mesh ();
		newLedge.AddComponent<MeshFilter> ();
		newLedge.AddComponent<MeshRenderer> ();
		
		newMesh.vertices = new Vector3[] {topLeftFront, topRightFront, topLeftBack, topRightBack,
			bottomLeftFront, bottomRightFront, backLeft, backRight};
		
		Vector2[] uvs = new Vector2[newMesh.vertices.Length];
		for (int i = 0; i < uvs.Length; i++) {
			uvs [i] = new Vector2 (newMesh.vertices [i].x, newMesh.vertices [i].z);
		}		
		
		newMesh.uv = uvs;		
		newMesh.triangles = new int[] {5,4,6, 6,7,5};
		newMesh.RecalculateNormals ();		
		
		newLedge.GetComponent<MeshFilter> ().mesh = newMesh;
		addMaterial(newLedge);		
	}
	
	float blockHeight = 0f;
	
	void createBox (Vector3 p1, Vector3 p2)
	{	
		
		//TODO: recalculate border. Add one more side.
		// 
		Vector3 topLeftFront = p1;
		Vector3 topRightFront = p2;
		Vector3 topLeftBack = p1;
		Vector3 topRightBack = p2;
				
		Vector3 bottomLeftFront;
		Vector3 bottomRightFront;
		// TODO: check
		Vector3 backLeft = p1;
		Vector3 backRight = p2;
		
		
		
		topRightFront.z = 0.5f;
		topLeftFront.z = 0.5f;
		topLeftBack.z = -0.5f;
		topRightBack.z = -0.5f;
		
		float l = Vector3.Magnitude (p1 - p2);		
		float b = Mathf.Sqrt (l * l + blockHeight * blockHeight);
		float x1 = l * blockHeight / b;
		float y1 = blockHeight * blockHeight / b;		
		
		bottomLeftFront = topLeftFront;
		bottomRightFront = topRightFront;
		
		/*	
		bottomLeftFront.y -= y1;
		bottomLeftFront.x -= x1;
		bottomRightFront.y -= y1;
		bottomRightFront.x -= x1;
		*/
		bottomLeftFront.y -= blockHeight;
		bottomRightFront.y -= blockHeight;
		
		backLeft = bottomLeftFront;
		backLeft.z = -0.5f;		
		backRight = bottomRightFront;
		backRight.z = -0.5f;
		
		
		GameObject newLedge = new GameObject ();
		Mesh newMesh = new Mesh ();
		newLedge.AddComponent<MeshFilter> ();
		newLedge.AddComponent<MeshRenderer> ();
		
		newMesh.vertices = new Vector3[] {topLeftFront, topRightFront, topLeftBack, topRightBack,
			bottomLeftFront, bottomRightFront, backLeft, backRight};
		
		Vector2[] uvs = new Vector2[newMesh.vertices.Length];
		for (int i = 0; i < uvs.Length; i++) {
			uvs [i] = new Vector2 (newMesh.vertices [i].x, newMesh.vertices [i].z);
		}		
		
		newMesh.uv = uvs;		
		newMesh.triangles = new int[] {5,4,0, 0,1,5, 0,2,3, 3,1,0, 5,4,6, 6,7,5};
		newMesh.RecalculateNormals ();		
		
		newLedge.GetComponent<MeshFilter> ().mesh = newMesh;
		addMaterial(newLedge);
	}
	
	void  addMaterial(GameObject go)
	{
		if (blockMaterial) 
		go.renderer.material = blockMaterial;
		go.AddComponent<MeshCollider> ();
		if (blockPhysicsMaterial) 
			go.GetComponent<MeshCollider> ().material = blockPhysicsMaterial;	
		go.GetComponent<MeshCollider> ().material  = colliderMeshMaterial;
		
		//go.transform.parent = GameObject.Find ("UserMeshes").transform;
	}
	
	bool brushDown = false;
	Vector2 smoothedCursorPosition;
}
