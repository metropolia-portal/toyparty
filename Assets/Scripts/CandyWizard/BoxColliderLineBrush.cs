using UnityEngine;
using System.Collections;

public class BoxColliderLineBrush : LineBrush {
	public float width = 0.1f;
	
	Vector2 prevSegmentPLeft;
	Vector2 prevSegmentPRight;
	bool firstSegment = true;
	
	override protected void StartDraw(Vector2 pos) {
	  base.StartDraw(pos);
		
	 // prevSegmentPLeft = pos;	
	  firstSegment = true;
	}
	
	override protected void MakeColliderSegment (Vector3 fromPos, Vector3 toPos) {
		
		Vector2 from = fromPos;
		Vector2 to = toPos;
		
		if(firstSegment) {
			firstSegment = false;
			
			Vector2 centerVec = to - from;
			prevSegmentPLeft = from + (new Vector2(-centerVec.y, centerVec.x)).normalized*width/2;
			prevSegmentPRight = from - (new Vector2(-centerVec.y, centerVec.x)).normalized*width/2;
			//Debug.DrawLine(prevSegmentPLeft, prevSegmentPRight, Color.red, 1f);
		}
		
		Vector2 tangentOrigin;
		
		if((to-prevSegmentPLeft).magnitude > (to-prevSegmentPRight).magnitude)
			tangentOrigin = prevSegmentPLeft;
		else
			tangentOrigin = prevSegmentPRight;
		
		Vector2 vecToCenter = to - tangentOrigin;
		float distToCenter = vecToCenter.magnitude;
		float alpha = Mathf.Asin(width/2 / distToCenter);
		float beta = Mathf.Atan2(vecToCenter.y, vecToCenter.x);
		
		float theta = beta - alpha;
		Vector2 tangentA = to + new Vector2(width/2 * Mathf.Sin(theta),width/2  * -Mathf.Cos(theta));
		
		theta= beta + alpha;
		Vector2 tangentB = to + new Vector2( width/2 * -Mathf.Sin(theta),width/2  * Mathf.Cos(theta));
		
//		Debug.DrawLine(tangentOrigin, tangentA, Color.white, 50f);
//		Debug.DrawLine(tangentOrigin, tangentB, Color.white, 50f);
		
		Vector2 newSegmentPLeft;
		Vector2 newSegmentPRight;
		Vector2 segmentToBack;

		
		if(tangentOrigin == prevSegmentPLeft) {
			newSegmentPLeft = (tangentA - prevSegmentPRight).magnitude > (tangentB - prevSegmentPRight).magnitude ? tangentA : tangentB;
			newSegmentPRight = newSegmentPLeft + (to - newSegmentPLeft)*2;
			
			segmentToBack = prevSegmentPLeft - newSegmentPLeft;
		}
		
		else {
			newSegmentPRight = (tangentA - prevSegmentPLeft).magnitude > (tangentB - prevSegmentPLeft).magnitude ? tangentA : tangentB;
			newSegmentPLeft = newSegmentPRight + (to - newSegmentPRight)*2;
			
			segmentToBack = prevSegmentPRight - newSegmentPRight;
		}
		
		
				
		Vector2 newSegmentBackLeft = newSegmentPLeft + segmentToBack;
		Vector2 newSegmentBackRight = newSegmentPRight + segmentToBack;
	 
		
//		Debug.DrawLine(newSegmentPLeft, tangentOrigin, Color.red, 50f);
//		Debug.DrawLine(newSegmentPRight, tangentOrigin, Color.red, 50f);
//		Debug.DrawLine(newSegmentPRight, newSegmentPLeft, Color.red, 50f);
//		Debug.DrawLine(prevSegmentPLeft, prevSegmentPRight,  Color.blue, 50f );
//		//Debug.DrawLine(prevSegmentPLeft, prevSegmentPRight, Color.red, 1f);
//		
//		Debug.DrawLine(newSegmentPLeft, newSegmentPRight, Color.red, 50f);
//		Debug.DrawLine(newSegmentPRight, newSegmentBackRight, Color.red, 50f);
//		Debug.DrawLine(newSegmentBackRight, newSegmentBackLeft, Color.red, 50f);
//		Debug.DrawLine(newSegmentBackLeft, newSegmentPLeft, Color.red, 50f);
		
		GameObject go = (GameObject) GameObject.Instantiate(segmentPlanePrefub);
		go.transform.position = to + segmentToBack/2;
		go.transform.localScale = new Vector3( 1 , width, segmentToBack.magnitude);
		go.transform.localRotation= Quaternion.LookRotation(-segmentToBack);
		
		go.transform.parent = lineContainer.transform;
		
		prevSegmentPLeft = newSegmentPLeft;
		prevSegmentPRight = newSegmentPRight;
	}
}
