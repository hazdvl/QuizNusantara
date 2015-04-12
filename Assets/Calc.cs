using UnityEngine;
using System.Collections;

public class Calc : MonoBehaviour {

	public Camera cam;
	private RaycastHit2D hit;
	
	public int angka1;
	public int angka2;
	
	public GameObject[] tombol;
	public tk2dTextMesh text; 

	// Use this for initialization
	void Start () {
	
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
				case "1":
					ketik(1);
					break;
				case "2":
					ketik(2);
					break;
				case "3":
					ketik(3);
					break;
				case "4":
					ketik(4);
					break;
				case "5":
					ketik(5);
					break;
				case "6":
					ketik(6);
					break;
				case "7":
					ketik(7);
					break;
				case "8":
					ketik(8);
					break;
				case "9":
					ketik(9);
					break;
				case "0":
					ketik(0);
					break;
				case "plus":
					plus();
					break;
				case "eq":
					eq();
					break;
					
				}
			}
			
			//text.text = "asdad";
		}
	
	}
	
	public void ketik(int n){
		text.text = text.text+n+"";
	}
	
	public void plus(){
		text.text = text.text+"+";
	}
	
	public void eq(){
		text.text = angka1+angka2+"";
	}
	
	public void reset(){
		text.text = "";
	}
}
