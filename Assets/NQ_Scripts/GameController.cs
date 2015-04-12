using UnityEngine;
using System.Collections;
using System.Text;


public class GameController : MonoBehaviour {

	public Camera cam;
	private RaycastHit2D hit;

	public GLOBAL stageData;
	public StageData currentStage;
	public QuizObjectData[] quizObjectData;

	public QuizObject quizObject;
	public QuizObject[] quizObjects;

	public QuizView quizView;
	public QuizObject currentQuiz;

	public AudioSource audio;
	public AudioSource audioClick;
	public AudioSource audioWin;

	public int starsCollected;
	public int starsToCollect;


	/// <summary>
	public GUISkin skin;
	
	//public GameController gameController;
	//public QuizObject quizObject;

	public Sprite[] btnBackStages;
	public Sprite[] bannerStages;
	public GameObject btnBackStage;
	public GameObject bannerStage;

	public GameObject btnNext;
	public GameObject btnPrev;
	public GameObject btnTebak;
	public GameObject btnUlang;
	public GameObject check;
	public GameObject rect;
	public GameObject rectShadow;
	public string textInput = "";
	private bool showHint = false;
	public TextMesh textStat;
	public GameObject dialogCongrats;
	
	//public Camera cam;
	//private RaycastHit2D hit;
	
	//public GUIText guiText;
	public GUIText hintText;
	public GameObject popUp;
	public tk2dTextMesh guiText;

	public GameObject rateBg;
	public GameObject[] rateStars;
	
	public float slideTimeOut;
	private float timeSinceLastSlide;
	private int slideNum = 1;
	
	private int wrongAnswer = 0;
	private int rating = 5;
private bool hintShown = false;
	
	public Serunii seruni;

	/// </summary>

	// Use this for initialization
	void Start () {
		dialogCongrats.SetActive(false);

		//for testing only
		//GLOBAL.CURRENTSTAGE = 3;
		//

		//initializing stage data
		
		switch (GLOBAL.CURRENTSTAGE){
		case 1 : 
			//currentStage = stageData.stages[0];
			quizObjectData = stageData.stages[0].data; 
			btnBackStage.GetComponent<SpriteRenderer>().sprite = btnBackStages[0];
			bannerStage.GetComponent<SpriteRenderer>().sprite = bannerStages[0];
			break;
		case 2 : 
			quizObjectData = stageData.stages[1].data; 
			btnBackStage.GetComponent<SpriteRenderer>().sprite = btnBackStages[1];
			bannerStage.GetComponent<SpriteRenderer>().sprite = bannerStages[1];
			break;
		case 3 : 
			quizObjectData = stageData.stages[2].data;
			btnBackStage.GetComponent<SpriteRenderer>().sprite = btnBackStages[2];
			bannerStage.GetComponent<SpriteRenderer>().sprite = bannerStages[2];
			break;
		default: break;
		}
		
		
		//currentStage = stageData.stages[GLOBAL.CURRENTSTAGE];
		//quizObjectData = currentStage.data;

		//initializing quiz objects
		quizObjects = new QuizObject[quizObjectData.Length];

		int i=0, posx=0, posy=0;
		foreach(QuizObjectData qd in quizObjectData){

			Vector2 pos = new Vector2((posx*2.5f)+1.8f, (posy*-2.2f)+5.6f);

			quizObjects[i] = (QuizObject)Instantiate(quizObject, pos, transform.rotation);

			quizObjects[i].data = qd;
			quizObjects[i].data.id  = i;
			quizObjects[i].data.rating = PlayerPrefs.GetInt("s"+GLOBAL.CURRENTSTAGE+"qr"+i, 5);

			int check = PlayerPrefs.GetInt("s"+GLOBAL.CURRENTSTAGE+"q"+i, 0);
			if (check == 1) quizObjects[i].data.check = true; else quizObjects[i].data.check = false;

			quizObjects[i].GetComponent<SpriteRenderer>().sprite = qd.gambar;

			i++;
			posx++;
			if (posx >= 5){posx=0; posy++;}
		}

		//get total collected stars
		starsCollected = PlayerPrefs.GetInt("stars"+GLOBAL.CURRENTSTAGE);
		textStat.text = starsCollected+"/90";
		rateBg.SetActive(false);

		//set audio volume
		Debug.Log("audio vol "+audio.volume);
		audio.volume = PlayerPrefs.GetFloat("bgmVolume");
		audio.volume -= audio.volume/3f;
		audioClick.volume = PlayerPrefs.GetFloat("bgmVolume");
		audioWin.volume = PlayerPrefs.GetFloat("bgmVolume");
		Debug.Log("conf vol "+GLOBAL.VOLUME);
		if (audio.volume <= 0.1) {
			audio.mute = true;
			audioClick.mute = true;
			audioWin.mute = true;
		}else{
			audio.mute = false;
			audioClick.mute = false;
			audioWin.mute = false;
		}

		audio.Play();


		/////////////////////
		//Hide();
		popUp.SetActive(false);
		
		timeSinceLastSlide = slideTimeOut;
		//rect.animation.Play("Fade");

		//starsCollected = PlayerPrefs.GetInt("stars"+GLOBAL.CURRENTSTAGE);
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonUp(0))	
		{
	
			hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
			
			if(hit.collider != null)
			{
				Debug.Log("object hovered: "+hit.collider.tag);
				//iTween.PunchScale(hit.transform.gameObject, new Vector3(0.5f,0.5f,0), 0.5f);

				switch (hit.transform.name){
				case "btnBackToStageSelect":
					Debug.Log("back to stage select");

					Application.LoadLevel("StageSelect");

					break;
				case "btnTebak":
					Debug.Log("TEBAK?");
					//iTween.PunchPosition(btnTebak
					Tebak();
					break;
				case "btnNext" :
					Debug.Log("Next");
					Next();
					break;
				case "btnPrev":
					Debug.Log("Prev");
					Prev();
					break;
				case "btnBack":
					Debug.Log("Back");
					Hide();
					break;
				case "btnHint":
					Hint();
					break;
				case "btnOkCongrats":
					dialogCongrats.SetActive(false);
					break;
				case "btnUlang":
					Retry();
					break;
				case "Seruni":
					hintShown = true;
					break;
				}
			}
		}
		
		if (Input.GetKeyUp(KeyCode.RightArrow)){
			Next();
		}else
		if (Input.GetKeyUp(KeyCode.LeftArrow)){
			Prev();
		}
		
		if (Input.GetKeyDown(KeyCode.Return)) Tebak();
		
		//slide picture
		/*
		timeSinceLastSlide += Time.deltaTime;
		if (GLOBAL.INGAME)
		if (timeSinceLastSlide > slideTimeOut){
			//iTween.fade

			slideNum++;
			if (slideNum >= quizObject.data.gambar.Length && quizObject.data.gambar.Length > 0) slideNum = 1;
			rect.GetComponent<SpriteRenderer>().sprite = quizObject.data.gambar[slideNum];

			timeSinceLastSlide = 0;

			rect.animation.Blend("Fade");//Play("Fade");
		}*/

		//if (GLOBAL.INGAME) rect.GetComponent<SpriteRenderer>().sprite = quizObject.data.gambar;
	}

	void OnGUI(){
		
		//todo : disable text input after correct answer
		if (quizObject){
			if (!quizObject.data.check){
				GUI.SetNextControlName("MyTextField");
				GUI.FocusControl("MyTextField");

				GUI.skin.settings.cursorColor = Color.black;
				
				textInput = GUI.TextField(new Rect(Screen.width/2-300, quizView.transform.position.y*125, 600, 50), 
				                          textInput.Replace("\n",""), 50, skin.textField);
				                          
				//if (Input.GetKeyDown(KeyCode.Return)) Tebak();                         
				
				if (textInput.Length > 0){
					for (int i=0; i<textInput.Length; ++i){
						if (textInput[i] == '\n'){
							textInput = textInput.Replace("\n","");
							Tebak();
						}
					}
				/*
					if (textInput[textInput.Length-1] == '\n'){
						textInput = textInput.Replace("\n","");
						Tebak();
					}
					textInput = textInput.Replace("\n","");
				*/
				}
			}
		}
	}

	public void Show(GameObject g){
		Debug.Log("show must go on");

		audioClick.Play();
		
		//reset rate stars

		ResetRating();
		hintShown = false;
		wrongAnswer = 0;
		
		if (seruni.isOpenInfo) seruni.HideInfo();
		
		//show button tebak
		btnTebak.SetActive(true);
		btnUlang.SetActive(false);
		
		//set current quiz object to g
		quizObject = g.GetComponent<QuizObject>();
		rect.GetComponent<SpriteRenderer>().sprite = quizObject.data.gambar;
		
		//set current quiz pic
		timeSinceLastSlide = slideTimeOut;
		rect.animation.Stop();
		
		//hide check if not completed
		if (!quizObject.data.check) {
			check.SetActive(false);
			seruni.isCheck = false;
			rateBg.SetActive(false);
		}else {
			rating = quizObject.data.rating;
			seruni.isCheck = true;
			Win();
			
			//seruni.Chat("Klik disini untuk melihat infopedia");
		}
				
		//reset textinput
		textInput = "";
		
		//hide hint if was shown
		//if (showHint) Hint();
		
		seruni.SetHint(quizObject.data.hint);
		seruni.SetInfo(quizObject.data.info);
		//seruni.animation["s_hint"].time = 4.5f;
		//if (!seruni.animation.isPlaying) seruni.animation.Play("s_hint");
		//if (seruni.isShowHint) seruni.HideHint();
		//else
		//	seruni.HideInfo();
		
		//show guitext if check
		if (quizObject.data.check) guiText.text = quizObject.data.jawaban[0];
		
		//hide prev btn if no quiz before
		if (quizObject.data.id == 0) btnPrev.SetActive(false); else btnPrev.SetActive(true);
		if (quizObject.data.id == 14) btnNext.SetActive(false); else btnNext.SetActive(true);
		
		//play show animation
		if (!GLOBAL.INGAME){
			quizView.animation.Play("QuizViewAnimation_show");
			//seruni.Appear();
		}
		
		//change global var
		GLOBAL.INGAME = true;
		
		//play sfx
		
	}

	public void Hide(){
		audioClick.Play();

		//play hide animation
		if (quizObject) quizView.animation.Play("QuizViewAnimation_hide");
		//seruni.Disappear();
		
		//if (seruni.isOpenInfo) seruni.HideInfo();
		//reset rate stars
		//ResetRating();
		
		//set current quiz object to null
		quizObject = null;
		
		//set global var
		GLOBAL.INGAME = false;
		
		//animation.Play("CheckAnimation");
		
		//hide hint if it is shown
		//if (showHint) Hint();
		
		//reset textinput
		textInput = "";
		guiText.text = "";
	}

	public void Win(){
		if (quizObject.data.check == false) audioWin.Play();

		//activate renderer
		check.SetActive(true);
		
		//show check sign
		quizView.animation.Play("CheckAnimation");

		//show rating
		rateBg.SetActive(true);
		Rate (quizObject.data.rating);
		
		quizObject.Check();
		seruni.isCheck = true;
		//disable input text and button

		guiText.text = quizObject.data.jawaban[0];
		
		//hide tebak button
		btnTebak.SetActive(false);
		btnUlang.SetActive(true);

		//Save data
		//SaveData();
	}

	public void Lose(){
		//todo wrong answer animation with flashing red on picture
		//quizView.animation.Blend("FailAnimation");
		
		wrongAnswer++;
		rectShadow.animation.Play("FailAnimation");
		/*
		if (wrongAnswer > 2)
			seruni.Chat("Klik disini untuk melihat hint");
		else if(wrongAnswer % 2 == 0)
			seruni.Chat("Ayo coba lagi");
		else if(textInput == "")
			seruni.Chat("Isi dulu jawabannya");
			*/
			
	}

	public void Next(){
		//deactivate checksprite
		check.SetActive(false);
		
		//reset rate stars
		//ResetRating();
		
		//hide hint if was shown
		//if (showHint) Hint();
		
		//set to next quiz
		quizObject = quizObjects[quizObject.data.id+1];
		Show(quizObject.gameObject);
		
		//animation
		
		//reset textfield
		textInput = "";
		guiText.text = "";
		
		//show guitext if check
		if (quizObject.data.check) guiText.text = quizObject.data.jawaban[0];
	}

	public void Prev(){
		//deactivate checksprite
		check.SetActive(false);
		
		//reset rate stars
		//ResetRating();
		
		//hide hint if was shown
		//if (showHint) Hint();
		
		//set to next quiz
		quizObject = quizObjects[quizObject.data.id-1];
		Show(quizObject.gameObject);
		
		//animation
		
		//reset textfield
		textInput = "";
		guiText.text = "";
		
		//show guitext if check
		if (quizObject.data.check) guiText.text = quizObject.data.jawaban[0];
	}
	
	public void Retry(){
		btnUlang.SetActive(false);
		btnTebak.SetActive(true);
		
		quizObject.Uncheck();
		//quizObject.data.check = false;
		quizObject.data.rating = 0;
		//quizObject.checkSprite.SetActive(false);
		
		guiText.text = "";
		rateBg.SetActive(false);
	}

	public void Hint(){
		showHint = !showHint;
		
		if (showHint){
			audioClick.Play();

			popUp.SetActive(true);
			
			//wrap text
			string tmp = quizObject.data.hint;
			int r = 0, s = 0, t = 0;
			for (int i=0; i<tmp.Length-1; ++i){
				if (tmp[i] == ' ') { 
					s++; 
					t = i;
				}
				if (r > 15){
					StringBuilder sb = new StringBuilder(tmp);
					sb[t] = '\n';
					tmp = sb.ToString();
					
					r=0; s=0;
				}
				
				r++;
			}
			
			hintText.text = tmp;
		}else{
			popUp.SetActive(false);
			hintText.text = "";
		}
		
		//if (!hintShown) quizObject.data.rating--;
		hintShown = true;
		
	}

	public void Tebak(){
		
		if (textInput == ""){
			Lose ();
			return;
		}
		
		Debug.Log("tebak!");
		
		int rate=0;
		
		int i;
		for (i=0; i<quizObject.data.jawaban.Length; ++i){
			//int n = CekString( quizObject.data.jawaban[i].Replace(" ","").ToLower(), textInput.Replace(" ","").ToLower()  );
			if ( quizObject.data.jawaban[i].Replace(" ","").ToLower() == textInput.Replace(" ","").ToLower() ) break;
			//if (i == 0) break;
		}
		
		if (i < quizObject.data.jawaban.Length)
			rate = 5;
		else 
			rate = 0;
		/*if (n == 0) rate = 5; else
		if (n == 1)  rate = 4; else
		if (n == 2) rate = 3; else
		if (n == 3) rate = 2; else
		if (n == 3) rate = 2; else*/
		
		//if (n < 5) rate = 5-n; else
		//rate = 0;
		if (wrongAnswer == 1) rate --; else
		if (wrongAnswer > 1) rate-=2;
		if (hintShown) rate--;
		
		if (rate > 0) {
			quizObject.data.rating = rate;
			
			quizObject.data.lastTebakan = textInput;
			
			Win();
			SaveData();
			
		//	seruni.ShowInfo();
		}else{
			Lose();
		}
		
		//if (rate > 2 && wrongAnswer > 0) seruni.ShowInfo();
		/*
		if (textInput.Replace(" ","").ToLower() == quizObject.data.jawaban.Replace(" ","").ToLower() ||
		    (textInput.Replace(" ","").ToLower() == quizObject.data.jawaban.Replace(" ","").ToLower() &&
		    quizObject.data.jawaban != "") ){
			Debug.Log("benar!");
			//SaveData();
			Win();
			SaveData();
		}else{
			Debug.Log("salah!");
			Lose();
		}*/
	}

	public void Rate(int n){
		rating = n;

		rateBg.SetActive(true);
		/*
		for (int i=0; i<5; ++i){
			//rateStars[i].animation.Stop();
			if (i < rating){
				//rateStars[i].animation["Play"].enabled = true;
				rateStars[i].SetActive(true);
				rateStars[i].animation.Play("Star");
			}else{
				rateStars[i].SetActive(false);
			}
		}*/
		int i=0;
		
		while (i < 5){
			rateStars[i].SetActive(false);
			i++;
		}
		
		i=0;
		while (i < n) {
			rateStars[i].SetActive(true);
			//rateStars[i].animation.Play("Star");
			i++;
		}

		if (!quizObject.data.check) {
			starsCollected += rating;
			quizObject.data.rating = rating;
		}
	}

	public void ResetRating(){
		/*
		foreach (GameObject star in rateStars){
			star.renderer.material.color = new Color(124,76,0,255);
			Debug.Log("wtf dude??");
		}*/
		for (int i=0; i<rateStars.Length; ++i){
			//rateStars[i].renderer.material.color = new Color(1,1,1);
			//rateStars[i].animation.Play("Star2w");
			//rateStars[i].animation.Stop();
		}

		rating = 5;
		hintShown = false;
		wrongAnswer = 0;
	}

	public int CekString(string q, string a){
	
		int m = q.Length;
		int n = a.Length;
		int x=0, y=0;

		int r=0; int f=0;

		int p=0;
		while (x < m){
			Debug.Log("qx : "+q[x]);
			Debug.Log("ay : "+a[y]);
			p = y;
			while(p < n){
				if (a[p] == q[x]){ r++; y=p; break;}
				else
					p++;
			}
			x++;
		}
		
		f = m - r;
		if (n > m) f += n - m;
		
		Debug.Log(f);
		return f;
		//return 0;
	}

	public void SaveData(){
		//to change
		//starsCollected=0;
		//
		foreach (QuizObject q in quizObjects){
			if (q.data.check){
				//starsCollected+=q.data.rating;

				//save check data
				PlayerPrefs.SetInt("s"+GLOBAL.CURRENTSTAGE+"q"+q.data.id, 1);
				//save rating data
				PlayerPrefs.SetInt("s"+GLOBAL.CURRENTSTAGE+"qr"+q.data.id, q.data.rating);
			}
			else
				PlayerPrefs.SetInt("s"+GLOBAL.CURRENTSTAGE+"q"+q.data.id, 0);
		}

		PlayerPrefs.SetInt("stars"+GLOBAL.CURRENTSTAGE, starsCollected);
		textStat.text = starsCollected+"/90";

		//check if stars collected > minimum stars to complete stage
		if (starsCollected >= starsToCollect && GLOBAL.CURRENTSTAGE == GLOBAL.UNLOCKEDSTAGES) {
			//GLOBAL.UNLOCKEDSTAGES++;
			PlayerPrefs.SetInt("unlocked",++GLOBAL.UNLOCKEDSTAGES);

			dialogCongrats.SetActive(true);
		}
	}


}
