using UnityEngine;
using System.Collections;

[ExecuteInEditMode()]
public class KeepTilingRatio : MonoBehaviour {
	
	public float tilesPerMeter = 5;
	public Material material;
	
	public bool checkThisWhenMaterialIsChanged = false;
	
	// Update is called once per frame
	void Update () {
		Vector2 targetScale = new Vector2(transform.localScale.x*tilesPerMeter / transform.localScale.y, 1);
		if(checkThisWhenMaterialIsChanged || renderer.material.mainTextureScale.x != targetScale.x ) { //to block touching scene always
			renderer.material = material;
			renderer.material.mainTextureScale =  targetScale;
		}
	}
}
