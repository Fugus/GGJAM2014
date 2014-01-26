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

    public bool isAzerty = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI () {
		/*if(mainGUISkin == null)
			mainGUISkin = Resources.Load<GUISkin>("MainGUISkin");

        bool newIsAzerty = GUILayout.Toggle(isAzerty, "Use AZERTY keyboard");

        if (newIsAzerty != isAzerty)
        {
            GetComponent<LevelManager>().UseAZERTY(newIsAzerty);
            ResetGame();
            isAzerty = newIsAzerty;
        }

		if(mainTextRect == null)
			mainTextRect = new Rect(MAIN_TEXT_MARGIN, MAIN_TEXT_TOP, Screen.width - MAIN_TEXT_MARGIN * 2, MAIN_TEXT_HEIGHT);*/
		//GUI.Label(mainTextRect.GetValueOrDefault(), mainTextString, mainGUISkin.label);
		//GUI.Box (mainTextRect.GetValueOrDefault(), mainTextString, mainGUISkin.box);


		if(resetRect == null)
			resetRect = new Rect(0, 20, 100, 20);
		if(GUI.Button(resetRect.GetValueOrDefault(), "Reset")) {
			ResetGame();
		};


		if(mainGUISkin == null)
			mainGUISkin = Resources.Load<GUISkin>("MainGUISkin");
		GUI.skin = mainGUISkin;

		//GUILayout.BeginArea(); // Overall container
		const int TOP_BAR_HEIGHT = 175;
		const int BOTTOM_BAR_HEIGHT = 210;
		const int ANIMATED_BOX_WIDTH = 175;
		const int BUTTON_WIDTH = 150;

		GUILayout.BeginArea(new Rect(0, 0, Screen.width, TOP_BAR_HEIGHT)); // Top bar
			GUILayout.BeginArea(new Rect(Screen.width - ANIMATED_BOX_WIDTH, 0, ANIMATED_BOX_WIDTH, TOP_BAR_HEIGHT));
			GUILayout.Box("ANIMATED GUY HERE", GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));
			GUILayout.EndArea();
		GUILayout.EndArea(); // Top bar end
		
		GUILayout.BeginArea(new Rect(0, TOP_BAR_HEIGHT, Screen.width, Screen.height - TOP_BAR_HEIGHT - BOTTOM_BAR_HEIGHT)); // Main play area
		GUILayout.EndArea(); // Main play area end
		
		GUILayout.BeginArea(new Rect(0, Screen.height - BOTTOM_BAR_HEIGHT, Screen.width, BOTTOM_BAR_HEIGHT)); // Bottom bar
			GUILayout.BeginHorizontal();
			if(GUILayout.Button("Restart", GUILayout.Width(BUTTON_WIDTH))) ResetGame();
			GUILayout.Label("BLAH BLAH BIG LABEL", GUILayout.Width(Screen.width - BUTTON_WIDTH - BUTTON_WIDTH));
			if(GUILayout.Button("Exit", GUILayout.Width(BUTTON_WIDTH))) QuitGame();
			GUILayout.EndHorizontal();
		GUILayout.EndArea(); // Bottom bar end

		//GUILayout.EndArea(); // Overall container end
	}

	public void ResetGame()
	{
		// Refresh LevelManager
		GetComponent<LevelManager>().Reset();
		// Refresh the Player
		
	}

	public void QuitGame()
	{
		Application.Quit();
	}

}
