using UnityEngine;
using System.Collections;

[ExecuteInEditMode()]
	
public class KeepPlaneScaleToTextureScale : MonoBehaviour {

	// Use this for initialization
	void Update () {
		Texture texture = GetComponent<MeshRenderer>().sharedMaterial.mainTexture;
		Vector3 newScale = new Vector3(transform.localScale.x,transform.localScale.y, transform.localScale.x * (float) texture.height / texture.width);
		if(newScale != transform.localScale)  transform.localScale = newScale; //to edit value only when needed, prevents from keeping scene in edited mode all the time
	}

}
