using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour {
	
	public GameObject cardPrefab;
	public int colCount = 4;
	public int rowCount = 2;
	public float cardDist = 0;
	public Material[] textures; // List of materials for each available card suit
	
	float cardW = 2.5f; // The default dimensions of a single card, used for calculations
	float cardH = 3.9f;
	float ratioAddition = 1; // this addition places the cards to the left side of the screen. Its needed due to the different row sizes. Its set by SetRatios
	float cardScaleW = 0;
	float cardScaleH = 0;
	
	const float defaultRowCount = 3f;
	const float defaultColCount = 2f;
	
	Material[] cardTextures;
	
	void Awake() {
		ShuffleMaterials();
		PlaceCards();		
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public int CardCount () {
		return (int) colCount*rowCount;
	}
	
	void ShuffleMaterials() {
		AssignMaterials ();
		int i;
		Material buf;
		
		for (i=0; i < cardTextures.Length; i++) { // This "shuffles" the texture array by swapping each element with another one at random
			int newPos = Random.Range (0, cardTextures.Length - 1);
			buf = cardTextures[newPos];
			cardTextures[newPos] = cardTextures[i];
			cardTextures[i] = buf;
		}
	}
	
	void AssignMaterials() { // This method fills the card texture array with the available suit textures
		int count = CardCount ();
		cardTextures = new Material[count];
		int i;
		for (i=0; i < count; i++) {
			cardTextures[i] = 
				textures[(i/2) % textures.Length]; 
			// For three suits, this expression will generate the following repeating sequence of indices: 0,0,1,1,2,2,0,0,1,1,2,2,0,0,1,1,2,2,...
		}
	}
	
	void PlaceCards() {		
		
		GameObject card;
		Transform cardBack;
		Vector3 cardPosition;
		int count = 0;
		
		float i, j;
		
		float shiftW = (colCount-1f)/2f; // We calculate these shifts here to reduce the calculations inside the loop
		float shiftH = (rowCount-1f)/2f;
		
		float ratio = 0.7f;
		
		if (colCount >= defaultRowCount) { // We want to resize the cards to fit the screen regardless of the dimensions of the field
			ratio *= defaultRowCount / colCount;
		}
		if (rowCount*ratio >= defaultColCount) { // The ratio is calculated in relation to the "default" dimensions (2x3)
			ratio *= defaultColCount / (rowCount * ratio);
		}
		
		GameObject.Find("Background").transform.localScale = // Resize the background image to fit behind the cards
			new Vector3(colCount*ratio*cardW/10, rowCount*ratio*cardH/10, 1);
		//Debug.Log(new Vector3(colCount*ratio*cardW/10, rowCount*ratio*cardH/10, 1));
		
		
		for (j = -shiftH; j <= shiftH; j++)
			for (i = -shiftW; i <= shiftW; i++) {
				
				SetRatios();
				cardPosition =  new Vector3(((cardW)*i*ratio) - ratioAddition, (cardH)*j*ratio, 0f); // Calculate the card's position
				card = (GameObject)Instantiate(cardPrefab, cardPosition, Quaternion.identity); // Place the card prefab onto the stage
				card.transform.localScale = new Vector3(ratio + cardScaleW, ratio + cardScaleH, 1); // Resize the card according to the calculated ratio
			
				cardBack = card.transform.Find("Face"); 
				cardBack.renderer.material = new Material(Shader.Find("Diffuse"));
				cardBack.renderer.material = cardTextures[count]; // Apply the appropriate material to the card's face
				
				card.GetComponent<Card>().SetSuit(cardTextures[count].name); // Save the card's suit in the card object (used to determine the card's suit later)
			
				count ++;
			}
	}
	void SetRatios(){ 				// this is used to set the card to the left side of the screen depending on how many columns there are.
		if(colCount == 3){
			ratioAddition = 1.3f;	// adjusting this changes the cards postion from the left of the screen
			cardW = 2.9f; 			// adjusting this adds more room to the sides of the cards
			cardH = 3.8f; 			// adjusting this adds more room to the top and bottom of the cards
			cardScaleH =0.05f;		// adjusting this makes the card taller or shorter
			cardScaleW = 0.09f;		// adjusting this makes the card wider or thinner
		}
		if(colCount == 4){
			ratioAddition = 1.4f;
			cardW = 2.7f;
			cardH = 4.2f;
			cardScaleH =0.1f;
			cardScaleW = 0.02f;
		}
		if(colCount == 4 && rowCount == 3){
			ratioAddition = 1.2f;
			cardW = 2.8f;
			cardH = 3.6f;
			cardScaleH =0.02f;
			cardScaleW = 0.08f;
		}
		if(colCount == 4 && rowCount == 4){
			ratioAddition = 1.2f;
			cardW = 3.5f;
			cardH = 3.6f;
			cardScaleH =0.02f;
			cardScaleW = 0.18f;
		}
	}
}
