using UnityEngine;
using System.Collections;
using System.Text;

public class QuizView : MonoBehaviour {
	/*
	public GUISkin skin;

	public GameController gameController;
	public QuizObject quizObject;

	public GameObject btnNext;
	public GameObject btnPrev;
	public GameObject btnTebak;
	public GameObject check;
	public GameObject rect;
	public string textInput = "";
	private bool showHint = false;

	public Camera cam;
	private RaycastHit2D hit;

	public GUIText guiText;
	public GUIText hintText;
	public GameObject popUp;

	public GameObject[] rateStars;

	public float slideTimeOut;
	private float timeSinceLastSlide;
	private int slideNum = 1;

	private int wrongAnswer = 0;
	private bool hintShown = false;
*/
	// Use this for initialization
	void Start () {

		//Hide();
//		popUp.SetActive(false);

//		timeSinceLastSlide = slideTimeOut;
	}
	
	// Update is called once per frame
	void Update () {
	/*
		if (Input.GetMouseButtonUp(0)){
			
			hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

			if(hit.collider != null)
			{
				Debug.Log("object clicked: "+hit.collider.tag);

				switch (hit.transform.name){
				case "btnTebak":
					Debug.Log("TEBAK?");
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
				}
			}
		}

		if (Input.GetKeyUp(KeyCode.RightArrow)){
			Next();
		}else
		if (Input.GetKeyUp(KeyCode.LeftArrow)){
			Prev();
		}else
		if (Input.GetKeyUp(KeyCode.Return) && quizObject.data.check){
			Next();
		}

		//slide picture
		timeSinceLastSlide += Time.deltaTime;
		if (timeSinceLastSlide > slideTimeOut && GLOBAL.INGAME){
			rect.GetComponent<SpriteRenderer>().sprite = quizObject.data.gambar[slideNum++];
			if (slideNum >= quizObject.data.gambar.Length) slideNum = 1;
			timeSinceLastSlide = 0;
		}
		*/
	}
	/*
	void OnGUI(){

		//todo : disable text input after correct answer
		if (quizObject){
			if (!quizObject.data.check){
				GUI.SetNextControlName("MyTextField");
				GUI.FocusControl("MyTextField");

				textInput = GUI.TextField(new Rect(Screen.width/2-300, transform.position.y*105, 600, 50), 
				                          textInput, 100, skin.textField);
				if (textInput.Length > 0)
					if (textInput[textInput.Length-1] == '\n'){
						textInput = textInput.Replace("\n","");
						Tebak();
					}
			}
		}

	}

	public void Show(GameObject g){
		Debug.Log("show must go on");

		//reset rate stars
		//ResetRating();

		//show button tebak
		btnTebak.SetActive(true);

		//set current quiz object to g
		quizObject = g.GetComponent<QuizObject>();

		//set current quiz pic
		//rect.GetComponent<SpriteRenderer>().sprite = quizObject.data.gambar[1];
		//StartCoroutine("SlideShow");
		//reset slideshow time
		timeSinceLastSlide = slideTimeOut;

		//hide check if not completed
		if (!quizObject.data.check) {
			check.SetActive(false);
		}
		else Win();

		//reset textinput
		textInput = "";

		//hide hint if was shown
		if (showHint) Hint();

		//show guitext if check
		if (quizObject.data.check) guiText.text = quizObject.data.jawaban;

		//hide prev btn if no quiz before
		if (quizObject.data.id == 0) btnPrev.SetActive(false); else btnPrev.SetActive(true);
		if (quizObject.data.id == 17) btnNext.SetActive(false); else btnNext.SetActive(true);

		//play show animation
		if (!GLOBAL.INGAME) animation.Play("QuizViewAnimation_show");

		//change global var
		GLOBAL.INGAME = true;

		//play sfx

	}


	public void Hide(){
		//play hide animation
		if (quizObject) animation.Play("QuizViewAnimation_hide");

		//reset rate stars
		//ResetRating();

		//set current quiz object to null
		quizObject = null;

		//set global var
		GLOBAL.INGAME = false;

		//animation.Play("CheckAnimation");

		//hide hint if it is shown
		if (showHint) Hint();

		//reset textinput
		textInput = "";
		guiText.text = "";
	}

	public void Win(){
		//activate renderer
		check.SetActive(true);

		//show check sign
		animation.Play("CheckAnimation");

		//fade picture
		Color c = rect.renderer.material.color;
		c.a = 240;
		rect.renderer.material.color = c;

		//update check to quiz object
		//quizObject.data.check = true;
		quizObject.Check();

		//show rating
		Rate(quizObject.data.rating);

		//disable input text and button


		//play winning music
		guiText.text = quizObject.data.jawaban;

		//hide tebak button
		btnTebak.SetActive(false);

	}

	public void Lose(){
		//todo wrong answer animation with flashing red on picture
		animation.Play("FailAnimation");

		wrongAnswer++;

		if (wrongAnswer >= 5) quizObject.data.rating -= 3;
		else if (wrongAnswer >= 3) quizObject.data.rating -= 2;
		else quizObject.data.rating -= 1;
	}
	
	public void Next(){
		//deactivate checksprite
		check.SetActive(false);

		//reset rate stars
		ResetRating();

		//hide hint if was shown
		if (showHint) Hint();

		//set to next quiz
		quizObject = gameController.quizObjects[quizObject.data.id+1];
		Show(quizObject.gameObject);

		//animation

		//reset textfield
		textInput = "";
		guiText.text = "";

		//show guitext if check
		if (quizObject.data.check) guiText.text = quizObject.data.jawaban;
	}

	public void Prev(){
		//deactivate checksprite
		check.SetActive(false);

		//reset rate stars
		ResetRating();

		//hide hint if was shown
		if (showHint) Hint();

		//set to next quiz
		quizObject = gameController.quizObjects[quizObject.data.id-1];
		Show(quizObject.gameObject);

		//animation
		
		//reset textfield
		textInput = "";
		guiText.text = "";

		//show guitext if check
		if (quizObject.data.check) guiText.text = quizObject.data.jawaban;
	}

	public void Hint(){
		showHint = !showHint;

		if (showHint){
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

		if (!hintShown) quizObject.data.rating--;
		hintShown = true;

	}

	public void Tebak(){
		if (gameController.Tebak(textInput, quizObject.data.jawaban))
			Win();
		else
			Lose();
	}

	public void Rate(int rating){
		for (int i=0; i<rating; ++i){
			rateStars[i].animation.Play("Star");

		}
	}

	public void ResetRating(){

		for (int i=0; i<rateStars.Length; ++i){
			rateStars[i].renderer.material.color = new Color(0.4862f,0.2980f,0);
		}
	}

	public void SlideShow(){
		int i = 1;

		while (GLOBAL.INGAME){
			rect.GetComponent<SpriteRenderer>().sprite = quizObject.data.gambar[i];
			i++; if (i >= quizObject.data.gambar.Length) i = 1;
		}
	}

*/
}
