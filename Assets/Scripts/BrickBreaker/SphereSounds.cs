using UnityEngine;
using System.Collections;

public class SphereSounds : MonoBehaviour {
	
	public AudioClip wallHitSound;
	public AudioClip padHitSound;
	public AudioClip deathZoneSound;
	public AudioClip brickHitSound;
	
	void OnTriggerEnter(Collider other) {		
    	if(other.CompareTag("DeathZone"))
			audio.PlayOneShot(deathZoneSound);			
	}
	public void PlaySound(string collider){
		AudioClip sound = null;
		
		switch(collider) {
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
}
