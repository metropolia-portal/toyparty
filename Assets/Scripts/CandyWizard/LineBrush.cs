using UnityEngine;
using System.Collections;

public class LineBrush : Brush {	
	
	public Material blockMaterial;
	public PhysicMaterial blockPhysicsMaterial;
	public PhysicMaterial colliderMeshMaterial;
	
	public GameObject trailRendererPrefub;
	
	public GameObject linesContainer;
	
	int lineCount = 0;
	GameObject lineContainer;
	
	override protected void StartDraw(Vector2 pos) {
		base.StartDraw(pos);
		
		lineCount++;
		lineContainer = new GameObject("Line"+lineCount.ToString());
		lineContainer.transform.parent = linesContainer.transform;
		
		trailRenderer = (GameObject) GameObject.Instantiate(trailRendererPrefub, pos, Quaternion.identity);
		trailRenderer.transform.parent = lineContainer.transform;
	}
	
	protected override void DrawSegment(Vector2 from, Vector2 to) {
		createBox(toVector3(from), toVector3(to));
		createBoxWithoutFrontSide(toVector3(from), toVector3(to));
		
		trailRenderer.transform.position = to;
	}
	
	
	
	Vector3 toVector3(Vector2 vec) {
		return new Vector3(vec.x, vec.y);	
	}
	
	void createBoxWithoutFrontSide (Vector3 p1, Vector3 p2)
	{	
//		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
//		cube.transform.position = p1;
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
		
		go.transform.parent = lineContainer.transform;
	}
	
	GameObject trailRenderer;
}
