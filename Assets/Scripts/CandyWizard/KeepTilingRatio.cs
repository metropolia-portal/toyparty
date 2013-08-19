using UnityEngine;
using System.Collections;

[ExecuteInEditMode()]
public class KeepTilingRatio : MonoBehaviour {
	
	public float tilesPerMeter = 5;
	public Material material;
	
	// Update is called once per frame
	void Update () {
		Vector2 targetScale = new Vector2(transform.localScale.x*tilesPerMeter, 1);
		if(renderer.material.mainTextureScale != targetScale ) { //to block touching scene always
			renderer.material = material;
			renderer.material.mainTextureScale =  targetScale;
		}
	}
}
