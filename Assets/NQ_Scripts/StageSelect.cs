using UnityEngine;
using System.Collections;

public class StageSelect : MonoBehaviour {

	public AudioSource audio;

	// Use this for initialization
	void Start () {
		//set audio volume
		Debug.Log("audio vol "+audio.volume);
		audio.volume = PlayerPrefs.GetFloat("bgmVolume");
		Debug.Log("conf vol "+GLOBAL.VOLUME);
		if (audio.volume <= 0.1) audio.mute = true;
		else audio.mute = false;
		
		audio.Play();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
