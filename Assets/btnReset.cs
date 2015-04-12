using UnityEngine;
using System.Collections;

public class btnReset : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//transform.renderer.material.color = new Color(1f,1f,1f,0.1f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseUp(){
		//show up confirmation dialog
		Debug.Log("RESET!");
	}

	void OnMouseEnter(){
		transform.renderer.material.color = new Color(1f,1f,1f,2f);
	}

	void OnMouseExit(){
		transform.renderer.material.color = new Color(1f,1f,1f,1f);
	}
}
