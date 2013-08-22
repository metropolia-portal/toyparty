using UnityEngine;
using System.Collections;

public class FlightSoundManager : MonoBehaviour {
	
	public AudioClip OwlDeath;
	public AudioClip SquirrelDeath;
	public AudioClip DragonDeath;
	public AudioClip OwlBossDamage;
	public AudioClip OwlBossDeath;
	public AudioClip OwlBossShot;
	public AudioClip Pickup;
	public AudioClip AttackB;
	public AudioClip AttackA;
	AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
		audioSource.loop = false;
		audio.volume = 1;		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void PlaySound(AudioClip sound) {
		if (sound == null) return;
		if (audioSource.isPlaying) return;
		audioSource.clip = sound;
		audioSource.Play();		
	}
	
}
