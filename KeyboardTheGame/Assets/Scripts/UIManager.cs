using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

	private const int MAIN_TEXT_MARGIN = 100;
	private const int MAIN_TEXT_TOP = 30;
	private const int MAIN_TEXT_HEIGHT = 200;
	private Rect? mainTextRect;

	public string mainTextString = "";

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI () {
		if(mainTextRect == null)
			mainTextRect = new Rect(MAIN_TEXT_MARGIN, MAIN_TEXT_TOP, Screen.width - MAIN_TEXT_MARGIN * 2, MAIN_TEXT_HEIGHT);
		GUI.Label(mainTextRect.GetValueOrDefault(), mainTextString);
	}
	
}
