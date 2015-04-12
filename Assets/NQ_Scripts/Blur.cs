using UnityEngine;
using System.Collections;

public class Blur : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.gameObject.renderer.material.mainTexture.mipMapBias = 3;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
