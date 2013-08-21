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
	
	protected override void Start() {
		base.Start ();
		initialPaintLength = paintLength;
	}
	
	protected override void StartDraw(Vector2 pos) {
		base.StartDraw(pos);
		
		DrawSegment(pos, pos); //to apply it immediately
	}

	protected override void DrawSegment(Vector2 from, Vector2 to) {
		if(paintLength > 0) {
			Collider[] colliders = Physics.OverlapSphere(from, brushRadius, 1 << LayerMask.NameToLayer("LineSegments") );
			foreach( Collider collider in colliders) {
				//print ("speeding up a segment");
				if(! collider.gameObject.GetComponentInChildren<SpeedUpSegment>()) {
					collider.gameObject.AddComponent<SpeedUpSegment>();
					paintLength -= lineBrush.segmentLength;
					
					GameObject speedupEffectElement = (GameObject) Instantiate(speedupEffectElementPrefub, collider.transform.position,
						speedupEffectElementPrefub.transform.rotation);
//						Quaternion.Euler(
//							new Vector3 (collider.transform.locaR.x, 
//							collider.transform.rotation.y, 
//							speedupEffectElementPrefub.transform.rotation.z)));
					speedupEffectElement.transform.parent = collider.transform;
				    //speedupEffectElement.transform.localRotation = speedupEffectElementPrefub.transform.rotation;
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
