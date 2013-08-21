using UnityEngine;
using System.Collections;

public class SpeedUpSegment : MonoBehaviour {
	
	public float acceleration = 5f;
	// Use this for initialization
	
	void Start () {
		gameObject.tag = "SpeedUpSegment";
	}
	
	void OnCollisionEnter(Collision collision) {
		SpeedUp(collision);
	}
	
	// Update is called once per frame
	void OnCollisionStay(Collision collision) {
		SpeedUp(collision);
	}
	
	void OnCollisionExit(Collision collision) {
		SpeedUp(collision);
	}
	
	void SpeedUp(Collision collision) {
		GameObject col = collision.collider.gameObject;
		if(col.CompareTag("Candy")) {
			//col.rigidbody.velocity *= ( 1 + 5f * Time.deltaTime);
			//col.rigidbody.velocity += collision.frictionForceSum;
			Vector3[] vert = gameObject.GetComponent<MeshCollider>().sharedMesh.vertices;
			vert[0].z = 0; vert[1].z = 0;
			
			
//			Vector3 extraVelocity = (-vert[0] + vert[1]).normalized * col.rigidbody.velocity.magnitude * 2f * Time.deltaTime ; 
//			Debug.DrawRay(col.transform.position,extraVelocity*5f,Color.red, 0.5f);
//					
//			//extraVelocity.y -= Mathf.Abs(extraVelocity.y)* 0.3f;
//			col.rigidbody.velocity += extraVelocity;
			
			//projecting speed on the direction of the segment, candy will speedup in any direction
			col.rigidbody.velocity = Vector3.Project(col.rigidbody.velocity, vert[1] - vert[0]).normalized * (col.rigidbody.velocity.magnitude + acceleration * Time.deltaTime);
			
			//col.rigidbody.AddTorque(Vector2.right * 5f * Time.deltaTime);
			
//			Debug.DrawRay(col.transform.position,collision.frictionForceSum*20f,Color.red, 0.5f);
//			Debug.DrawRay(col.transform.position,collision.impactForceSum*10f,Color.blue, 0.5f);
			
		}
	}
}
