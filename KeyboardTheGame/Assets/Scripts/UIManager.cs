using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

	private GUISkin mainGUISkin;

	public string mainTextString = "";

    public bool isAzerty = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI () {
        bool newIsAzerty = GUILayout.Toggle(isAzerty, "Use AZERTY keyboard");

        if (newIsAzerty != isAzerty)
        {
            GetComponent<LevelManager>().UseAZERTY(newIsAzerty);
            ResetGame();
            isAzerty = newIsAzerty;
        }


		if(mainGUISkin == null)
			mainGUISkin = Resources.Load<GUISkin>("MainGUISkin");
		GUI.skin = mainGUISkin;

		//GUILayout.BeginArea(); // Overall container
		const int TOP_BAR_HEIGHT = 175;
		const int BOTTOM_BAR_HEIGHT = 210;
		const int ANIMATED_BOX_WIDTH = 175;
		const int BUTTON_WIDTH = 150;
		const int BUTTON_HEIGHT = 60;

		GUILayout.BeginArea(new Rect(0, 0, Screen.width, TOP_BAR_HEIGHT)); // Top bar
			GUILayout.BeginArea(new Rect(Screen.width - ANIMATED_BOX_WIDTH, 0, ANIMATED_BOX_WIDTH, TOP_BAR_HEIGHT));
			//GUILayout.Box("ANIMATED GUY HERE", GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));
			GUILayout.EndArea();
		GUILayout.EndArea(); // Top bar end
		
		GUILayout.BeginArea(new Rect(0, TOP_BAR_HEIGHT, Screen.width, Screen.height - TOP_BAR_HEIGHT - BOTTOM_BAR_HEIGHT)); // Main play area
		GUILayout.EndArea(); // Main play area end
		
		//GUILayout.BeginArea(new Rect(0, Screen.height - BOTTOM_BAR_HEIGHT, Screen.width, BOTTOM_BAR_HEIGHT)); // Bottom bar
		GUILayout.BeginArea(new Rect(0, Screen.height - BUTTON_HEIGHT, BUTTON_WIDTH, BUTTON_HEIGHT));
			if(GUILayout.Button("Restart", GUILayout.Width(BUTTON_WIDTH), GUILayout.Height(BUTTON_HEIGHT))) ResetGame();
		GUILayout.EndArea();
		GUILayout.BeginArea (new Rect(BUTTON_WIDTH, Screen.height - BOTTOM_BAR_HEIGHT, Screen.width - BUTTON_WIDTH - BUTTON_WIDTH, BOTTOM_BAR_HEIGHT));
			GUILayout.Label(mainTextString, GUILayout.Width(Screen.width - BUTTON_WIDTH - BUTTON_WIDTH), GUILayout.Height (BOTTOM_BAR_HEIGHT));
		GUILayout.EndArea();
		GUILayout.BeginArea(new Rect(Screen.width - BUTTON_WIDTH, Screen.height - BUTTON_HEIGHT, BUTTON_WIDTH, BUTTON_HEIGHT));
			if(GUILayout.Button("Exit", GUILayout.Width(BUTTON_WIDTH), GUILayout.Height(BUTTON_HEIGHT))) QuitGame();
		GUILayout.EndArea();
		//GUILayout.EndArea(); // Bottom bar end

		//GUILayout.EndArea(); // Overall container end
	}

	public void ResetGame()
	{
		// Refresh LevelManager
		GetComponent<LevelManager>().Reset();
		// Refresh the Player
		GameObject.Find("/Player").GetComponent<PlayerControl>().Reset();
	}

	public void QuitGame()
	{
		Application.Quit();
	}

}
