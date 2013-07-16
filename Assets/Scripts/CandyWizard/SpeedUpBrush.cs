using UnityEngine;
using System.Collections;

public class SpeedUpBrush : Brush {
	float brushRadius = 1f;

	protected override void DrawSegment(Vector2 from, Vector2 to) {
		Collider[] colliders = Physics.OverlapSphere(from, brushRadius, 1 << LayerMask.NameToLayer("LineSegments") );
		foreach( Collider collider in colliders) {
			print ("speeding up a segment");
			if(! collider.gameObject.GetComponentInChildren<SpeedUpSegment>())
				collider.gameObject.AddComponent<SpeedUpSegment>();
		}
	}

}
