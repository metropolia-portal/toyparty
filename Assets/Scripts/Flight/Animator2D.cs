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
	
	public Animation2D GetCurrentAnimation() {
		return currentAnimation;
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
	
	
	public void SetAnimationFrame(string name, int frame) {
		PauseAnimation();
		SwitchAnimation(name);
		if (currentAnimation) {
			currentAnimation.InitMaterial();
			currentAnimation.SetFrame(frame);
		}
	}
	
	public void SetFrame(int frame) {
		if (currentAnimation) {
			currentAnimation.SetFrame(frame);
		}
	}
	
	
	public bool SwitchAnimation(string name) {
		foreach (Animation2D anim in gameObject.GetComponents<Animation2D>()) {
			if (anim.GetName()==name) {
				currentAnimation = anim;
				return true;
			}
		}	
		return false;
	}
	
	public void PlayAnimation(string name) {
		PauseAnimation();
		if (SwitchAnimation(name))
		RestartAnimation();
		ResumeAnimation();
	}
	
	public Animator2D Child(string name) {
		return transform.FindChild(name).GetComponent<Animator2D>();
	}

	// Use this for initialization
	void Start () {
		SwitchAnimation(defaultAnimation);
		if (autoStart)
			PlayAnimation(defaultAnimation);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
