using UnityEngine;
using System.Collections;

public class MarbleExplosion : MonoBehaviour {
	
	public AudioClip explosionSound;
	// Use this for initialization
	void Start () {
		audio.PlayOneShot(explosionSound);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
