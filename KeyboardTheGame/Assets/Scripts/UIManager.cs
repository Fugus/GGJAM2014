using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

	private GUISkin mainGUISkin;

	private const int MAIN_TEXT_MARGIN = 100;
	private const int MAIN_TEXT_TOP = 30;
	private const int MAIN_TEXT_HEIGHT = 200;
	private Rect? mainTextRect;

	private Rect? resetRect;

	public string mainTextString = "";

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI () {
		if(mainGUISkin == null)
			mainGUISkin = Resources.Load<GUISkin>("MainGUISkin");

		if(mainTextRect == null)
			mainTextRect = new Rect(MAIN_TEXT_MARGIN, MAIN_TEXT_TOP, Screen.width - MAIN_TEXT_MARGIN * 2, MAIN_TEXT_HEIGHT);
		//GUI.Label(mainTextRect.GetValueOrDefault(), mainTextString, mainGUISkin.label);
		GUI.Box (mainTextRect.GetValueOrDefault(), mainTextString, mainGUISkin.box);

		if(resetRect == null)
			resetRect = new Rect(0, 0, 100, 20);
		if(GUI.Button(resetRect.GetValueOrDefault(), "Reset")) {
			ResetGame();
		};
	}

	public void ResetGame()
	{
		// Refresh LevelManager

	}

}
