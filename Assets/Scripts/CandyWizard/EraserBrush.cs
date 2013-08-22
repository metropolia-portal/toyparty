using UnityEngine;
using System.Collections;

public class EraserBrush : Brush {
	public float brushRadius = 1f;
	
	protected override void StartDraw(Vector2 pos) {
		base.StartDraw(pos);
		
		DrawSegment(pos, pos); //to apply it immediately
	}
	//common brush class with speedup
	protected override void DrawSegment(Vector2 from, Vector2 to) {
			Collider[] colliders = Physics.OverlapSphere(from, brushRadius, 1 << LayerMask.NameToLayer("LineSegments") );
			foreach( Collider collider in colliders) {
				//print ("speeding up a segment");
				if(collider)
					GameObject.Destroy(collider.transform.parent.gameObject);
			}
		}
}
