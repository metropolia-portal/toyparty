using UnityEngine;
using System.Collections;

public class FlipsGUIBackground : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<GUITexture>().pixelInset = new Rect(-Screen.width/2, -Screen.height/2, Screen.width, Screen.height);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
