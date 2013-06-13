using UnityEngine;
using System.Collections;

public class SphereSounds : MonoBehaviour {
	
	public AudioClip wallHitSound;
	public AudioClip padHitSound;
	public AudioClip deathZoneSound;
	public AudioClip brickHitSound;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter(Collision col) {
		AudioClip sound = null;
		
		switch(col.collider.tag) {
			case "Brick" :
				sound = brickHitSound;
				break;
			case "Side" :
				sound = wallHitSound;
				break;
			case "Paddle" :
				sound = padHitSound;
				break;
		}
		
		audio.PlayOneShot(sound);	
	}
	
	void OnTriggerEnter(Collider other) {		
    	if(other.CompareTag("DeathZone"))
			audio.PlayOneShot(deathZoneSound);			
	}
}
