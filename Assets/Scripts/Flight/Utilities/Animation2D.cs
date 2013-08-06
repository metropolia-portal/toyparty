using UnityEngine;
using System.Collections;

public class Animation2D : MonoBehaviour {
	
	public string animName;
	public Texture2D frames;
	public int frameCount;
	public int startFrame = 1;
	public int endFrame = 0;
	public float frameRate = 1;
	public bool looping = true;
	
	float frameWidth;
	int currentFrame = 0;
	
	bool playing = false;
	float phase = 0;
	
	public void Restart() {
		currentFrame = startFrame;
	}
	
	public void Play() {
		playing = true;
		InitMaterial();
	}
	
	public string GetName() {
		return animName;
	}
	
	public void Stop() {
		playing = false;
	}
	
	// Use this for initialization
	void Start () {
		currentFrame = startFrame;
		frameWidth = frames.width / frameCount; 
	}
	
	public void InitMaterial() {
		renderer.material.SetTexture("_MainTex", frames);
	}
	
	public void SetFrame(int frame) {
		float frameRelativeWidth = frameWidth / renderer.material.GetTexture("_MainTex").width;
		renderer.material.SetTextureOffset("_MainTex", new Vector2(frameRelativeWidth*currentFrame, 0));
		renderer.material.SetTextureScale("_MainTex", new Vector2(frameRelativeWidth, 1));
		currentFrame = frame;
	}
	
	// Update is called once per frame
	void Update () {
		if (playing) {
			phase += Time.deltaTime;
			while (phase > 1/frameRate) {
				phase -= 1/frameRate;
				currentFrame ++;
				if (currentFrame > endFrame) {
					if (looping)
						currentFrame = startFrame;
					else {
						currentFrame = endFrame;
						Stop();
					}
				}
			}
			SetFrame(currentFrame);
		}
	}
}
