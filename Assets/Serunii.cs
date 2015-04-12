using UnityEngine;
using System.Collections;

public class Serunii : MonoBehaviour {

	public tk2dTextMesh textChat;
	public tk2dTextMesh textInfo;
	public string hint;
	public string info;
	public Sprite gambar;
	
	public bool isCheck;
	public bool isOpenInfo;
	public bool isShowHint;

	// Use this for initialization
	void Start () {
		isCheck = false;
		isOpenInfo = false;
		
		textChat.text = "";
		textInfo.text = "";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseEnter(){
		animation.PlayQueued("s_hover");
	}
	
	void OnMouseExit(){
		animation.Play("s_standby");
	}

	void OnMouseUp(){
		textChat.text = "\" Hint : "+hint+"\"";
		if (!isCheck) ShowHint(); else//animation.Play("s_hint"); else
		if (!isOpenInfo) ShowInfoClick(); 
		else
			HideInfo();
	}
			
	public void Chat(string chat){
		textChat.text = "\""+chat+"\"";
		animation.Play("ChatPop");
	}
	
	public void ShowHint(){
		Chat (hint);
		isShowHint = true;
	}
	
	public void HideHint(){
		animation.Play ("HideChat");
		isShowHint = false;
	}
	
	public void ShowInfo(){
		textInfo.text = info;
		animation.Play("s_infopedia");
		isOpenInfo = true;
	}
	
	public void HideInfo(){
		animation.Play("s_infopedia_close");
		isOpenInfo = false;
	}
	
	public void ShowInfoClick(){
		textInfo.text = info;
		animation.Play("s_infopedia_click");
		isOpenInfo = true;
	}
	
	public void SetHint(string hint){
		this.hint = hint;
	}
	
	public void SetInfo(string info){
		this.info = info;
	}
	
	public void SetGambar(Sprite gambar){
		this.gambar = gambar;
	}
	
	public void Appear(){
		animation.Blend("s_appear");
	}
	
	public void Disappear(){
		animation.PlayQueued("s_disappear");
		//animation.
		//animation.Blend("s_disappear");
	}
	
	
}
