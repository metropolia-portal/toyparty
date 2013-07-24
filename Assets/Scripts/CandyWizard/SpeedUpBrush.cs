using UnityEngine;
using System.Collections;

public class SpeedUpBrush : Brush {
	public float paintLength = 5f;
	public float brushRadius = 1f;

	public GameObject speedupEffectElementPrefub;
	public LineBrush lineBrush;
	
	public GameGUI gui;
	//public Material speededUpElementMaterial;
	
	float initialPaintLength;
	
	public void Start() {
		initialPaintLength = paintLength;
	}

	protected override void DrawSegment(Vector2 from, Vector2 to) {
		if(paintLength > 0) {
			Collider[] colliders = Physics.OverlapSphere(from, brushRadius, 1 << LayerMask.NameToLayer("LineSegments") );
			foreach( Collider collider in colliders) {
				//print ("speeding up a segment");
				if(! collider.gameObject.GetComponentInChildren<SpeedUpSegment>()) {
					collider.gameObject.AddComponent<SpeedUpSegment>();
					paintLength -= lineBrush.segmentLength;
					
					GameObject speedupEffectElement = (GameObject) Instantiate(speedupEffectElementPrefub, collider.transform.position, speedupEffectElementPrefub.transform.rotation);
					speedupEffectElement.transform.parent = collider.transform;
				}
			}
			 				
			paintLength = Mathf.Max(0,paintLength);
		}
	}
	
	override protected void Update() {
		base.Update();
		 
		//TODO use C# Event system
		gui.SetSpeedupLeft(paintLength / initialPaintLength);
	}

}
