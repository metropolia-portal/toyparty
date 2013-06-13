using UnityEngine;
using System.Collections;

public class Lazer : MonoBehaviour {
	
	public AudioClip lazerShootSound;
	public AudioClip brickDestroySound;
	
	//called from LazerGunPowerup to notify the lazer that it destroys a brick
	public void OnBrickDestroy() {
		AudioSource.PlayClipAtPoint(brickDestroySound, transform.position); //cannot use audio.PlayOneShot, bacause the lazer will be destroyed immediately after that
	}
	
	// Use this for initialization
	void Start () {
		GetComponent<LineRenderer>().SetPosition(0, transform.position);
		
		RaycastHit hit;	
		if(Physics.Raycast(transform.position, Vector3.forward, out hit)) {
			GetComponent<LineRenderer>().SetPosition(1, hit.point);
		}
		
		audio.PlayOneShot(lazerShootSound);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
