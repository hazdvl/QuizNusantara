using UnityEngine;
using System.Collections;

[System.Serializable]
public class QuizObjectData {
	public int id;
	public Sprite gambar;
	public string[] jawaban;
	public string lastTebakan;
	public string hint;
	public int rating;
	public bool check;

	public string info;
	public Sprite infoPics;
}

[System.Serializable]
public class StageData {
	public QuizObjectData[] data;
	public int stars;
}

public class GLOBAL : MonoBehaviour {

	static public bool INGAME = false;
	static public float VOLUME = 0.5f;

	public StageData[] stages;

	static public int CURRENTSTAGE = 3;
	static public int UNLOCKEDSTAGES = 1;

}
