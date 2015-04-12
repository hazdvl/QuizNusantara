using UnityEngine;
using System.Collections;

public class BtnBackToQuizSelect : MonoBehaviour {

	public QuizView qv;

	// Use this for initialization
	void Start () {
	
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
