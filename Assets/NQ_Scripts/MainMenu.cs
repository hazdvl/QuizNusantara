using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public Camera cam;
	public RaycastHit2D hit;

	public GameObject[] darkSide;

	public GameObject dialogExit;
	public GameObject dialogReset;
	public GameObject dialogCredits;
	public GameObject creditsText;

	public GameObject popUp;
	public GameObject sliderBtn;
	public GameObject btnReset;

	public AudioSource audio;
	public AudioSource audioClick;

	public float maxX;
	public float minX;
	private float range;
	private float volume;

	bool showOpt;

	// Use this for initialization
	void Start () {
		darkSide = GameObject.FindGameObjectsWithTag("Wall");
		//btnReset.renderer.material.color = new Color(1f,1f,1f,0.1f);

		dialogExit.SetActive(false);
		dialogReset.SetActive(false);
		dialogCredits.SetActive(false);

		range = maxX - minX;
		volume = PlayerPrefs.GetFloat("bgmVolume");
		if (volume < 0) volume = 0;

		//showOpt = true;
		//OnOptions();
		showOpt = false;
		popUp.SetActive(false);
		sliderBtn.transform.position = new Vector3(volume*range + minX,
		                                           sliderBtn.transform.position.y,
		                                           sliderBtn.transform.position.z);

		OnVolume(volume*range + minX);

		audio.Play();

		//load number of unlocked stages
		GLOBAL.UNLOCKEDSTAGES = PlayerPrefs.GetInt("unlocked");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0)){
			
			hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
			if(hit.collider != null){
				switch (hit.transform.name){
				case "btnSlider":
					Debug.Log("slidee");
					
					if (cam.ScreenToWorldPoint(Input.mousePosition).x <= maxX && 
					    cam.ScreenToWorldPoint(Input.mousePosition).x >= minX){
						hit.transform.position = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x,
						                                     hit.transform.position.y,
						                                     hit.transform.position.z);
					}
					
					OnVolume(hit.transform.position.x);
					
					break;
				}
			}
		}else
		if (Input.GetMouseButtonUp(0)){
			
			hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
			
			if(hit.collider != null)
			{
				Debug.Log("object hovered: "+hit.collider.tag);
				
				switch (hit.transform.name){
				case "btnPlay":
					Debug.Log("lets playyy");
					Application.LoadLevel("StageSelect");
					
					break;
				case "btnQuit":
					//Application.Quit();
					dialogExit.SetActive(true);
					break;
				case "btnOpt":
					OnOptions();
					break;
				case "btnInfo":
					OnInfo();
					break;
				case "bg":
					if (showOpt) OnOptions();
					break;
				case "btnYesQuit":
					Application.Quit();
					break;
				case "btnNoQuit":
					dialogExit.SetActive(false);
					break;
				case "btnReset":
					dialogReset.SetActive(true);
					break;
				case "btnYesReset":
					ResetGame();
					dialogReset.SetActive(false);
					break;
				case "btnNoReset":
					dialogReset.SetActive(false);
					break;
				case "btnOkCredits":
					dialogCredits.SetActive(false);
					break;
				}
				
				audioClick.Play();
			}


			//darken background if a dialog is up
			if (dialogExit.activeSelf || dialogReset.activeSelf || dialogCredits.activeSelf) 
				foreach (GameObject d in darkSide)
					d.renderer.material.color = new Color(0.2f,0.2f,0.2f);
			else
				foreach (GameObject d in darkSide)
					d.renderer.material.color = new Color(1f,1f,1f);
		}

		if (Input.GetKeyUp(KeyCode.Escape)){
			//if (!dialogExit.activeSelf && !dialogReset.activeSelf && !dialogCredits.activeSelf) dialogExit.SetActive(true);
			//else dialogExit.SetActive(false);

			if(dialogReset.activeSelf) dialogReset.SetActive(false);
			else
			if(dialogCredits.activeSelf) dialogCredits.SetActive(false);
			else
			if (dialogExit.activeSelf) dialogExit.SetActive(false);
			else
				dialogExit.SetActive(true);

			//darken background if a dialog is up
			if (dialogExit.activeSelf || dialogReset.activeSelf || dialogCredits.activeSelf)
				foreach (GameObject d in darkSide)
					d.renderer.material.color = new Color(0.2f,0.2f,0.2f);
			else
				foreach (GameObject d in darkSide)
					d.renderer.material.color = new Color(1f,1f,1f);
		}
	}

	public void OnOptions(){
		showOpt = !showOpt;

		if (showOpt){
			popUp.SetActive(true);
			sliderBtn.transform.position = new Vector3(volume*range + minX,
			                                     sliderBtn.transform.position.y,
			                                     sliderBtn.transform.position.z);
			btnReset.renderer.material.color = new Color(1f,1f,1f,0.1f);
		}else
			popUp.SetActive(false);
	}

	public void OnVolume(float sliderPos){
		if (sliderPos < minX+0.1) {
			audio.mute = true;
			audioClick.mute = true;
		}else{
			audio.mute = false;
			audioClick.mute = false;
		}
		
		volume = (sliderPos - minX) / range;
		audio.volume = volume;
		audioClick.volume = volume;
		GLOBAL.VOLUME = volume;
		
		PlayerPrefs.SetFloat("bgmVolume",volume);
	}

	public void OnInfo(){
		dialogCredits.SetActive(true);
		//creditsText.animation["ScrollingCredits"].wrapMode = WrapMode.Loop;
		//creditsText.animation.Play("ScrollingCredits");
	}

	public void ResetGame(){
		//todo 
		//reset collected stars
		for (int s=0; s<10; ++s){
			PlayerPrefs.SetInt("stars"+s, 0); //todo reset stars on all stage

			for (int i=0; i<18; ++i){
				PlayerPrefs.SetInt("s"+s+"q"+i, 0);
			}
		}

		PlayerPrefs.SetInt("unlocked", 1);
	}
}
