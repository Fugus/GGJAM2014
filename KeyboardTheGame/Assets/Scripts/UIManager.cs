using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

    enum UIState
    {
        MainMenu,
        Game,
        EndScreen
    };

    UIState currentState = UIState.MainMenu;

	private GUISkin mainGUISkin;

	public string mainTextString = "";

    public bool isAzerty = false;
    public bool? newIsAzerty;

    public bool isOculus = false;
    public bool? newIsOculus;


    public Texture BeginTex;
    public Texture AzertyTex;
    public Texture OculusTex;
    public Texture QwertyTex;
    public Texture StandardTex;
    public Texture ExitTex;
    public Texture RestartTex;
    public Texture SplashExitTex;
    public Texture CreditBGTex;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}

    void DisplayMainMenu()
    {
        //GUILayout.BeginArea(); // Overall container
        const int TOP_BAR_HEIGHT = 175;
        const int BOTTOM_BAR_HEIGHT = 210;
        const int ANIMATED_BOX_WIDTH = 175;
        const int BUTTON_WIDTH = 150;
        const int BUTTON_HEIGHT = 60;

        const int BG_WIDTH = 1024;
        const int BG_HEIGHT = 640;

        float widthFactor = (float)Screen.width / (float)BG_WIDTH;
        float heighFactor = (float)Screen.height / (float)BG_HEIGHT;

        GUI.DrawTexture(new Rect(0.0f, 0.0f, Screen.width, Screen.height), CreditBGTex);

        float beginX = .17f * Screen.width;
        float beginY = .79f * Screen.height;
        GUILayout.BeginArea(new Rect(beginX, beginY, beginX + widthFactor * 1.5f * BUTTON_WIDTH, beginY + heighFactor * 1.5f * BUTTON_HEIGHT));
        if (GUILayout.Button(BeginTex, GUILayout.Width(widthFactor * 1.5f * BUTTON_WIDTH), GUILayout.Height(heighFactor * 1.5f * BUTTON_HEIGHT)))
        {
            currentState = UIState.Game;
            ResetGame();
        }
        GUILayout.EndArea();

        float exitX = .37f * Screen.width;
        float exitY = .8f * Screen.height;
        GUILayout.BeginArea(new Rect(exitX, exitY, exitX + widthFactor * BUTTON_WIDTH, exitY + heighFactor * BUTTON_HEIGHT));
        if (GUILayout.Button(SplashExitTex, GUILayout.Width(widthFactor * BUTTON_WIDTH), GUILayout.Height(heighFactor * BUTTON_HEIGHT)))
        {
            QuitGame();
        }
        GUILayout.EndArea();


#region AZERTY
        newIsAzerty = null;
        //if (!isAzerty)
        //{
        float azertyX =  (1f -.27f) * Screen.width;
        float azertyY = (.79f) * Screen.height;

        GUILayout.BeginArea(new Rect(azertyX, azertyY, azertyX + .7f * BUTTON_WIDTH, azertyY + .7f * BUTTON_HEIGHT));
        if (GUILayout.Button(AzertyTex, GUILayout.Width(.7f * BUTTON_WIDTH), GUILayout.Height(.7f * BUTTON_HEIGHT)))
            {
                newIsAzerty = true;
            }
            GUILayout.EndArea();
        //}

        //if (isAzerty)
        //{
            float qwertyX = (1 - .14f) * Screen.width;
            float qwertyY = .79f * Screen.height;

            GUILayout.BeginArea(new Rect(qwertyX, qwertyY, qwertyX + .7f * BUTTON_WIDTH, qwertyY + 0.7f * BUTTON_HEIGHT));
            if (GUILayout.Button(QwertyTex, GUILayout.Width(.7f * BUTTON_WIDTH), GUILayout.Height(.7f * BUTTON_HEIGHT)))
            {
                newIsAzerty = false;
            }
            GUILayout.EndArea();
        //}

        if (newIsAzerty != null && newIsAzerty.GetValueOrDefault() != isAzerty)
        {
            GetComponent<LevelManager>().UseAZERTY(newIsAzerty.GetValueOrDefault());
            ResetGame();
            isAzerty = newIsAzerty.GetValueOrDefault();
        }
#endregion

#region AZERTY
        newIsOculus = null;

        float oculusX = (1f - .27f) * Screen.width;
        float oculusY = (.84f) * Screen.height;

        //if (!isOculus)
        //{
        GUILayout.BeginArea(new Rect(oculusX, oculusY, oculusX + .7f * BUTTON_WIDTH, oculusY + .7f * BUTTON_HEIGHT));
        if (GUILayout.Button(OculusTex, GUILayout.Width(.7f * BUTTON_WIDTH), GUILayout.Height(.7f * BUTTON_HEIGHT)))
            {
                newIsOculus = true;
            }
            GUILayout.EndArea();
        //}
        //if (isOculus)
        //{

            float regularX = (1 - .14f) * Screen.width;
            float regularY = .84f * Screen.height;

            GUILayout.BeginArea(new Rect(regularX, regularY, regularX + .7f * BUTTON_WIDTH, regularY + .7f * BUTTON_HEIGHT));
            if (GUILayout.Button(StandardTex, GUILayout.Width(.7f * BUTTON_WIDTH), GUILayout.Height(.7f * BUTTON_HEIGHT)))
            {
                newIsOculus = false;
            }
            GUILayout.EndArea();
        //}

        if (newIsOculus != null && newIsOculus.GetValueOrDefault() != isOculus)
        {
            isOculus = newIsOculus.GetValueOrDefault();
        }
#endregion

        //GUILayout.EndArea(); // Bottom bar end

        //GUILayout.EndArea(); // Overall container end
    }

    void DisplayGame()
    {
        //GUILayout.BeginArea(); // Overall container
        const int TOP_BAR_HEIGHT = 175;
        const int BOTTOM_BAR_HEIGHT = 210;
        const int ANIMATED_BOX_WIDTH = 175;
        const int BUTTON_WIDTH = 380 / 4;
        const int BUTTON_HEIGHT = 203 / 4;

        GUILayout.BeginArea(new Rect(0, 0, Screen.width, TOP_BAR_HEIGHT)); // Top bar
        GUILayout.BeginArea(new Rect(Screen.width - ANIMATED_BOX_WIDTH, 0, ANIMATED_BOX_WIDTH, TOP_BAR_HEIGHT));
        //GUILayout.Box("ANIMATED GUY HERE", GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));
        GUILayout.EndArea();
        GUILayout.EndArea(); // Top bar end

        GUILayout.BeginArea(new Rect(0, TOP_BAR_HEIGHT, Screen.width, Screen.height - TOP_BAR_HEIGHT - BOTTOM_BAR_HEIGHT)); // Main play area
        GUILayout.EndArea(); // Main play area end

        //GUILayout.BeginArea(new Rect(0, Screen.height - BOTTOM_BAR_HEIGHT, Screen.width, BOTTOM_BAR_HEIGHT)); // Bottom bar
        GUILayout.BeginArea(new Rect(0, Screen.height - BUTTON_HEIGHT, BUTTON_WIDTH, BUTTON_HEIGHT));
        if (GUILayout.Button(RestartTex, GUILayout.Width(BUTTON_WIDTH), GUILayout.Height(BUTTON_HEIGHT)))
        {
            ResetGame();
        }
        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect(BUTTON_WIDTH, Screen.height - BOTTOM_BAR_HEIGHT, Screen.width - BUTTON_WIDTH - BUTTON_WIDTH, BOTTOM_BAR_HEIGHT));
        GUILayout.Label(mainTextString, GUILayout.Width(Screen.width - BUTTON_WIDTH - BUTTON_WIDTH), GUILayout.Height(BOTTOM_BAR_HEIGHT));
        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect(Screen.width - BUTTON_WIDTH, Screen.height - BUTTON_HEIGHT, BUTTON_WIDTH, BUTTON_HEIGHT));
        if (GUILayout.Button(ExitTex, GUILayout.Width(BUTTON_WIDTH), GUILayout.Height(BUTTON_HEIGHT)))
        {
            currentState = UIState.MainMenu;
        }
        GUILayout.EndArea();
        //GUILayout.EndArea(); // Bottom bar end

        //GUILayout.EndArea(); // Overall container end
    }


    void DisplayEndScreen()
    {
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
        if (GUILayout.Button(RestartTex, GUILayout.Width(BUTTON_WIDTH), GUILayout.Height(BUTTON_HEIGHT))) ResetGame();
        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect(BUTTON_WIDTH, Screen.height - BOTTOM_BAR_HEIGHT, Screen.width - BUTTON_WIDTH - BUTTON_WIDTH, BOTTOM_BAR_HEIGHT));
        GUILayout.Label(mainTextString, GUILayout.Width(Screen.width - BUTTON_WIDTH - BUTTON_WIDTH), GUILayout.Height(BOTTOM_BAR_HEIGHT));
        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect(Screen.width - BUTTON_WIDTH, Screen.height - BUTTON_HEIGHT, BUTTON_WIDTH, BUTTON_HEIGHT));
        if (GUILayout.Button(ExitTex, GUILayout.Width(BUTTON_WIDTH), GUILayout.Height(BUTTON_HEIGHT))) QuitGame();
        GUILayout.EndArea();
        //GUILayout.EndArea(); // Bottom bar end

        //GUILayout.EndArea(); // Overall container end
    }

    void OnGUI()
    {
        if (mainGUISkin == null)
            mainGUISkin = Resources.Load<GUISkin>("MainGUISkin");
        GUI.skin = mainGUISkin;

        if (currentState == UIState.MainMenu)
        {
            DisplayMainMenu();
        }
        else if (currentState == UIState.Game)
        {
            DisplayGame();
        }
        else if (currentState == UIState.EndScreen)
        {
            DisplayEndScreen();
        }
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
