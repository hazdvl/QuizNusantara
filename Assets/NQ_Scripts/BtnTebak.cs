using UnityEngine;
using System.Collections;

public class BtnTebak : MonoBehaviour {

	public GameController gameController;
	public QuizView qv;
	// Use this for initialization
	void Start () {
		//qv = GameObject.FindWithTag("Enemy").GetComponent<QuizView>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseEnter(){
		//todo: animate scale
		transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
		//transform.Scale(new Vector3(1.1f,1.1f,1.1f), new Vector3(1.2f,1.2f,1.2f));
	}

	void OnMouseExit(){
		//todo: animate scale
		transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
	}
}
