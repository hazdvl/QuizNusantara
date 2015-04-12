using UnityEngine;
using System.Collections;

public class BtnStage : MonoBehaviour {

	public GameObject lockSprite;
	public GameObject angkaSprite;
	public GameObject starSprite;
	public TextMesh statText;

	public int stage;
	public bool locked;

	int starsCollected;

	// Use this for initialization
	void Start () {
		statText.text = "";

		//check if this stage was unlocked
		GLOBAL.UNLOCKEDSTAGES = PlayerPrefs.GetInt("unlocked");
		if (GLOBAL.UNLOCKEDSTAGES >= stage) locked = false;

		/////
		if (stage > 3) locked = true;
		/////

		if (!locked) {
			lockSprite.SetActive(false);

			starsCollected = PlayerPrefs.GetInt("stars"+stage);
			statText.text = starsCollected+"/75";

		}else{
			starSprite.SetActive(false);
			angkaSprite.SetActive(false);

			renderer.material.color = new Color(0.529f,0.529f,0.529f);
		}


	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseUp(){
		if (!locked) {

			transform.localScale = new Vector3(1f,1f,1f);

			GLOBAL.CURRENTSTAGE = stage;
			Application.LoadLevel("GameScene");

		}
	}
}
