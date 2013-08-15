using UnityEngine;
using System.Collections;

public class LineBrush : Brush {	
	CandyWizardGameManager gameManager;
	public float smoothingSpeed = 0.01f;
	public float smoothingSpeedPC = 0.1f;
	
	public GameObject segmentPlanePrefub;
	
	public GameObject trailRendererPrefub;
	
	public GameObject linesContainer;
	protected override void Start(){
		base.Start ();
		gameManager = GameObject.Find ("GameManager").GetComponent<CandyWizardGameManager>();
	}
	override protected void MoveDrawingPosition(ref Vector2 refDrawPosition) {

		refDrawPosition = Vector2.Lerp(refDrawPosition, GetCursorPosition(),
#if UNITY_IPHONE || UNITY_ANDROID
			smoothingSpeed
#else
			smoothingSpeedPC		
#endif
			);
	}
	
	override protected void StartDraw(Vector2 pos) {
		if(gameManager.CanDrawLineAt(toVector3(pos))) {
			base.StartDraw(pos);
			
			lineCount++;
			lineContainer = new GameObject("Line"+lineCount.ToString());
			lineContainer.transform.parent = linesContainer.transform;
			
			trailRenderer = (GameObject) GameObject.Instantiate(trailRendererPrefub, pos, Quaternion.identity);
			trailRenderer.transform.parent = lineContainer.transform;
		}
	}
	
	override protected void DrawTo(Vector2 pos) {
		if(gameManager.CanDrawLineAt(toVector3(pos))) {
			base.DrawTo(pos);	
		}
		else
			FinishDraw();
	}
	
	protected override void DrawSegment(Vector2 from, Vector2 to) {
		makeColliderSegment(toVector3(from), toVector3(to));	
		trailRenderer.transform.position = to;
	}
	
	Vector3 toVector3(Vector2 vec) {
		return new Vector3(vec.x, vec.y);	
	}
	
	
	void makeColliderSegment (Vector3 fromPos, Vector3 toPos)
	{	
		GameObject newSegment = (GameObject) Instantiate(segmentPlanePrefub, transform.position, Quaternion.identity);
		
		//creating two-sided plane mesh
		Mesh newMesh = new Mesh ();
		
		Vector3 fromPosFront = Vector3.zero;
		Vector3 toPosFront = toPos - fromPos;
		Vector3 fromPosBack = Vector3.zero;
		Vector3 toPosBack = toPos - fromPos;
		
		toPosFront.z = 0.5f;
		fromPosFront.z = 0.5f;
		fromPosBack.z = -0.5f;
		toPosBack.z = -0.5f;

		newMesh.vertices = new Vector3[] {fromPosFront, toPosFront, fromPosBack, toPosBack};
		
		//two sides, reverse numbering of vertices creates triangles on the opposite side
		newMesh.triangles = new int[] {1,0,2, 2,3,1, 2,0,1, 1,3,2};
		
		//setting meshcollider mesh to be the same as our generated mesh
		newSegment.GetComponent<MeshCollider> ().sharedMesh = newMesh;
		
		newSegment.transform.position = fromPos;
		newSegment.transform.parent = lineContainer.transform;
	}
	
	GameObject trailRenderer;
	
	int lineCount = 0;
	GameObject lineContainer;
}
