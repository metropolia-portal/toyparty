using UnityEngine;
using System.Collections;

public class Animator2D : MonoBehaviour {
	
	public bool autoStart = false;
	public string defaultAnimation = "idle";
	
	Animation2D currentAnimation;
	
	public void PauseAnimation() {
		if (currentAnimation)
			currentAnimation.Stop();
	}
	
	public void ResumeAnimation() {
		if (currentAnimation)
			currentAnimation.Play();
	}
	
	public void RestartAnimation() {
		if (currentAnimation) 
			currentAnimation.Restart();
	}
	
	public void SetLooping(bool looping) {
		if (currentAnimation)
			currentAnimation.looping = looping;
	}
	
	
	
	public void SwitchAnimation(string name) {
		foreach (Animation2D anim in gameObject.GetComponents<Animation2D>()) {
			if (anim.GetName()==name) {
				currentAnimation = anim;
				break;
			}
		}	
	}
	
	public void PlayAnimation(string name) {
		PauseAnimation();
		SwitchAnimation(name);
		RestartAnimation();
		ResumeAnimation();
	}
	
	public Animator2D Child(string name) {
		return transform.FindChild(name).GetComponent<Animator2D>();
	}

	// Use this for initialization
	void Start () {
		if (autoStart)
			PlayAnimation(defaultAnimation);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
