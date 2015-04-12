using UnityEngine;
using System.Collections;

public class BtnAnimMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseEnter(){

		animation["BtnHoverMainMenu"].speed = 1;
		animation.Play("BtnHoverMainMenu");
		
	}
	
	void OnMouseExit(){

		animation["BtnHoverMainMenu"].speed = -1;
		animation["BtnHoverMainMenu"].time = animation["BtnHoverMainMenu"].length;
		animation.Play("BtnHoverMainMenu");

	}

	void OnMouseUp(){
		switch (transform.name){
		case "btnBackMenu":
			Application.LoadLevel("MainMenu");
			break;
		case "btnBackStage":
			Application.LoadLevel("StageSelect");
			break;
		default:
			break;
		}
	}
}
