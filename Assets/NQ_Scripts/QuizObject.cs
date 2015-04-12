using UnityEngine;
using System.Collections;

public class QuizObject : MonoBehaviour {

	//public GameObject quizView;
	//public QuizView qv;
	public GameController gameController;
	public QuizObjectData data;

	public GameObject checkSprite;

	// Use this for initialization
	void Start () {
		if (!data.check) checkSprite.SetActive(false);

		gameController = GameObject.Find("_GameController").GetComponent<GameController>();
		//gameController = g.GetComponent<
	}
	
	// Update is called once per frame
	void Update () {




	}

	void OnMouseUp(){
		if (!GLOBAL.INGAME) {
			//quizView = GameObject.FindWithTag("QuizView");
			//quizView = GameObject.FindWithTag("Enemy");
			//qv = quizView.GetComponent<QuizView>();
			//GameObject g = GameObject.Find("QuizView");
			//QuizView q = g.GetComponent<QuizView>();
			gameController.Show(gameObject);
		}else{
			Debug.Log("this is a game!");
		}

	}

	public void Check(){
		data.check = true;

		checkSprite.SetActive(true);

		//save data
		PlayerPrefs.SetInt("s"+GLOBAL.CURRENTSTAGE+"q"+this.data.id, 1);
	}
	
	public void Uncheck(){
		data.check = false;
		
		checkSprite.SetActive(false);
		
		//save data
		PlayerPrefs.SetInt("s"+GLOBAL.CURRENTSTAGE+"q"+this.data.id, 0);
		
	}
}
