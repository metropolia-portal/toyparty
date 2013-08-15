using UnityEngine;
using System.Collections;

public class MagicLineRenderer : MonoBehaviour {
	public float tilesPerMeter = 2;
	public float minSegmentLength = 0.001f;
		
	Vector3 prevPos;
	float length = 0;
	int vertices = 1;

	
	LineRenderer lineR;
	// Use this for initialization
	void Start () {
		prevPos = transform.position;
		lineR = GetComponent<LineRenderer>();
		lineR.SetPosition(0, transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		if((transform.position - prevPos).magnitude > minSegmentLength) {
			length += (transform.position - prevPos).magnitude;
			prevPos = transform.position;
			
	
			vertices++;
			lineR.SetVertexCount(vertices);
			lineR.SetPosition( vertices-1, transform.position);
			
			renderer.material.mainTextureScale= new Vector2(length*tilesPerMeter, 1);
		}
	}
}
