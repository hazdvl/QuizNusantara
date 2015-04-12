using UnityEngine;
using System.Collections;

public class BtnAnim : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseEnter(){
		if (GLOBAL.INGAME && transform.name == "QuizObject") return;

		animation["BtnHover"].speed = 1;
		animation.Play("BtnHover");

	}
	
	void OnMouseExit(){
		if (GLOBAL.INGAME && transform.name == "QuizObject") return;

		animation["BtnHover"].speed = -1;
		animation["BtnHover"].time = animation["BtnHover"].length;
		animation.Play("BtnHover");

	}

}
