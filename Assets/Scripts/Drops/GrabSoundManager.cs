/// <summary>
/// Grab sound manager.
/// The script is attached to the game manager on each grab level.
/// It plays sound for grabbingright items, iron or dropping items.
/// </summary>
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class GrabSoundManager : MonoBehaviour {

	public AudioClip grabSound;
	public AudioClip ironSound;
	public AudioClip dropSound;
	AudioSource audioSource;
	
	void Start(){
		audioSource = GetComponent<AudioSource>();
		audioSource.loop = false;
		audio.volume = 1;
	}
	public void PlayGrab(){
		audioSource.clip = grabSound;
		audioSource.Play();
	}
	public void PlayDrop(){
		audioSource.clip = dropSound;
		audioSource.Play();
	}
	public void PlayIron(){
		audioSource.clip = ironSound;
		audioSource.Play();
	}
}
