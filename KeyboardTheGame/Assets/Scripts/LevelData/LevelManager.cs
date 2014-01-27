using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

	public Dictionary<string, Level> levels = new Dictionary<string, Level>();

    bool useAZERTY = false;

	// Use this for initialization
	void Start () {
		Reset();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Reset()
	{
		levels = new Dictionary<string, Level>();
		BuildLevelsFromData();
		GetComponent<TileManager>().LoadLevel(FIRST_LEVEL);

        GameObject playerGameObject = GameObject.FindGameObjectWithTag("Player");
        PlayerControl playerControlScript = playerGameObject.GetComponent<PlayerControl>();
        playerControlScript.Reset();

	}

	private const string FIRST_LEVEL = "Glade";

	private void BuildLevelsFromData()
	{
		if(levels.Count > 0) throw new System.Exception("Tried to build levels more than once.");
		
		string[,] tempTypes;

		/*
		// EXAMPLE
		tempTypes = new string[4, 12] {
			{"P", "P", "P", "P", "P", "P", "P", "P", "P", "P", "P", "P"},
			{"P", "P", "P", "P", "P", "P", "P", "P", "P", "P", "P", "P"},
			{"P", "P", "P", "P", "P", "P", "P", "P", "P", "P", "P", "P"},
			{"P", "P", "P", "P", "P", "P", "P", "P", "P", "P", "", ""}
		};
		ProcessLevelData("EXAMPLE", "Example nice name", tempTypes);
		levels["EXAMPLE"][0][0].metadata = "Test Meta";
		*/

		/*
		// First
		tempTypes = new string[4, 12] {
			{"P", "P", "S", "P", "P", "P", "P", "P", "P", "P", "P", "B"},
			{"P", "P", "L", "P", "P", "S", "P", "P", "T", "T", "P", "B"},
			{"S", "L", "L", "P", "P", "R", "P", "P", "P", "S", "P", "B"},
			{"G", "P", "C", "S", "P", "P", "P", "P", "P", "P", "", ""}
		};
		ProcessLevelData("First", tempTypes);
		levels["First"][1][8].metadata = "I print text on the screen!";
		levels["First"][1][9].metadata = "BUT SO DO I BITCHES!";
		levels["First"][3][0].metadata = "Antechamber";
		levels["First"][2][0].metadata = "teleporting";
		levels["First"][3][3].metadata = "surveying";
		levels["First"][2][9].metadata = "reading";
		levels["First"][0][2].metadata = "confused";
		levels["First"][1][5].metadata = "victory";
        levels["First"][1][8].walls[0] = true;
        levels["First"][1][8].walls[1] = true;
        levels["First"][1][8].walls[2] = true;
        levels["First"][1][8].walls[3] = true;
        levels["First"][1][8].walls[4] = true;
        levels["First"][1][8].walls[5] = true;

		// Antechamber
		tempTypes = new string[4, 12] {
			{"P", "S", "P", "P", "P", "P", "P", "P", "P", "P", "P", "B"},
			{"P", "P", "P", "L", "L", "L", "L", "L", "L", "T", "P", "B"},
			{"P", "P", "P", "P", "P", "P", "P", "P", "P", "P", "P", ""},
			{"G", "P", "C", "P", "P", "P", "P", "P", "P", "P", "", ""}
		};
		ProcessLevelData("Antechamber", tempTypes);
		levels["Antechamber"][1][9].metadata = "Test text second level.";
		levels["Antechamber"][3][0].metadata = "First";
		levels["Antechamber"][0][1].metadata = "jumping";
		 */

		// Glade
		tempTypes = new string[4, 12] {
			{"P", "T", "P", "B", "B", "P", "P", "G", "P", "P", "P", "G"},
			{"B", "B", "T", "C", "B", "R", "S", "P", "P", "B", "P", "P"},
			{"B", "B", "P", "G", "P", "P", "P", "B", "B", "B", "B", "T"},
			{"B", "P", "P", "P", "P", "G", "P", "B", "B", "B", "", ""}
		};
		ProcessLevelData("Glade", "Tranquil Glade", tempTypes);
		levels["Glade"][3][5].metadata = "Basin";
		levels["Glade"][2][3].metadata = "Way";
		levels["Glade"][0][7].metadata = "Way";
		levels["Glade"][0][11].metadata = "Inner";
		levels["Glade"][0][1].metadata = "Walk across the keyboard with your fingers.";
		levels["Glade"][1][2].metadata = "Sometimes, with a little effort, we can push back the veil of ignorance.";
		levels["Glade"][2][11].metadata = "The hardest journeys can be the most rewarding.";

		levels["Glade"][1][6].metadata = "victory"; // temp for the demo

		levels["Glade"][2][3].walls[1] = true;
		levels["Glade"][2][3].walls[2] = true;
		levels["Glade"][2][3].walls[3] = true;
		levels["Glade"][2][3].walls[4] = true;
		levels["Glade"][3][3].walls[5] = true;
		levels["Glade"][3][4].walls[4] = true;
		levels["Glade"][3][4].walls[5] = true;
		levels["Glade"][3][5].walls[0] = true;
		levels["Glade"][3][5].walls[4] = true;
		levels["Glade"][3][5].walls[5] = true;

		// Basin
		tempTypes = new string[4, 12] {
			{"G", "P", "P", "P", "P", "P", "B", "B", "B", "B", "P", "P"},
			{"P", "P", "P", "P", "P", "P", "P", "B", "P", "P", "G", "P"},
			{"P", "P", "B", "L", "P", "P", "P", "T", "S", "P", "P", "P"},
			{"P", "P", "B", "B", "P", "G", "P", "B", "B", "P", "", ""}
		};
		ProcessLevelData("Basin", "Dusty Basin", tempTypes);
		levels["Basin"][3][5].metadata = "Glade";
		levels["Basin"][0][0].metadata = "Inner";
		levels["Basin"][1][10].metadata = "Indiana";
		levels["Basin"][2][7].metadata = "That gateway sure was quiet...";
		levels["Basin"][2][8].metadata = "teleporting";

		// Indiana
		tempTypes = new string[4, 12] {
			{"P", "P", "P", "P", "P", "P", "P", "P", "P", "P", "P", "P"},
			{"P", "P", "P", "P", "P", "P", "P", "P", "P", "P", "G", "P"},
			{"P", "P", "P", "P", "P", "P", "P", "P", "P", "P", "P", "P"},
			{"P", "P", "P", "P", "P", "P", "P", "P", "P", "P", "", ""}
		};
		ProcessLevelData("Indiana", "Indiana Gulf", tempTypes);
		levels["Indiana"][1][10].metadata = "Basin";

		// Inner
		tempTypes = new string[4, 12] {
			{"G", "P", "P", "P", "P", "P", "P", "P", "P", "P", "P", "G"},
			{"P", "P", "P", "P", "P", "P", "P", "P", "P", "P", "P", "P"},
			{"P", "P", "P", "P", "P", "P", "P", "P", "P", "P", "G", "P"},
			{"P", "P", "P", "P", "P", "P", "P", "P", "P", "P", "", ""}
		};
		ProcessLevelData("Inner", "Inner Desert", tempTypes);
		levels["Inner"][0][0].metadata = "Basin";
		levels["Inner"][0][11].metadata = "Glade";
		levels["Inner"][2][10].metadata = "Deep";

		// Deep
		tempTypes = new string[4, 12] {
			{"P", "P", "P", "P", "P", "G", "P", "P", "P", "P", "P", "P"},
			{"P", "P", "P", "P", "P", "P", "P", "P", "P", "P", "P", "P"},
			{"P", "P", "P", "P", "P", "P", "P", "P", "P", "P", "G", "P"},
			{"P", "P", "P", "P", "P", "P", "P", "P", "P", "P", "", ""}
		};
		ProcessLevelData("Deep", "Deep Desert", tempTypes);
		levels["Deep"][2][10].metadata = "Inner";
		levels["Deep"][0][5].metadata = "Way";

		// Way
		tempTypes = new string[4, 12] {
			{"P", "P", "P", "P", "P", "G", "P", "G", "P", "P", "P", "P"},
			{"P", "P", "P", "P", "P", "P", "P", "P", "P", "P", "P", "P"},
			{"P", "P", "P", "G", "L", "P", "P", "P", "P", "P", "P", "P"},
			{"P", "P", "P", "P", "P", "P", "P", "P", "P", "P", "", ""}
		};
		ProcessLevelData("Way", "The Winding Way", tempTypes);
		levels["Way"][2][3].metadata = "Glade";
		levels["Way"][0][7].metadata = "Glade";
		levels["Way"][0][5].metadata = "Deep";
	}

    public void UseAZERTY(bool value)
    {
        useAZERTY = value;
    }

    private void AssignLetterToTile(Level level)
    {
        if (useAZERTY)
        {
            string[] firstRow = { KeyCode.Alpha1.ToString(), KeyCode.Alpha2.ToString(), KeyCode.Alpha3.ToString(), KeyCode.Alpha4.ToString(), KeyCode.Alpha5.ToString(), KeyCode.Alpha6.ToString(), KeyCode.Alpha7.ToString(), KeyCode.Alpha8.ToString(), KeyCode.Alpha9.ToString(), KeyCode.Alpha0.ToString(), KeyCode.Minus.ToString(), KeyCode.Equals.ToString() };
            string[] secondRow = { "A", "Z", "E", "R", "T", "Y", "U", "I", "O", "P", KeyCode.LeftBracket.ToString(), KeyCode.RightBracket.ToString() };
            string[] thirdRow = { "Q", "S", "D", "F", "G", "H", "J", "K", "L", "M", KeyCode.Quote.ToString() };
            string[] fourthRow = { "W", "X", "C", "V", "B", "N", KeyCode.Question.ToString(), KeyCode.Comma.ToString(), KeyCode.Semicolon.ToString(), KeyCode.Colon.ToString() };

            for (int j = 0; j < 12; j++)
            {
                level[0][j].letter = firstRow[j];
                level[1][j].letter = secondRow[j];
            }

            for (int j = 0; j < 11; j++)
            {
                level[2][j].letter = thirdRow[j];
            }

            for (int j = 0; j < 10; j++)
            {
                level[3][j].letter = fourthRow[j];
            }
        }
        else
        {
            string[] firstRow = { KeyCode.Alpha1.ToString(), KeyCode.Alpha2.ToString(), KeyCode.Alpha3.ToString(), KeyCode.Alpha4.ToString(), KeyCode.Alpha5.ToString(), KeyCode.Alpha6.ToString(), KeyCode.Alpha7.ToString(), KeyCode.Alpha8.ToString(), KeyCode.Alpha9.ToString(), KeyCode.Alpha0.ToString(), KeyCode.Minus.ToString(), KeyCode.Equals.ToString() };
            string[] secondRow = { "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", KeyCode.LeftBracket.ToString(), KeyCode.RightBracket.ToString() };
            string[] thirdRow = { "A", "S", "D", "F", "G", "H", "J", "K", "L", KeyCode.Semicolon.ToString(), KeyCode.Quote.ToString() };
            string[] fourthRow = { "Z", "X", "C", "V", "B", "N", "M", KeyCode.Comma.ToString(), KeyCode.Period.ToString(), KeyCode.Slash.ToString() };

            for (int j = 0; j < 12; j++)
            {
                level[0][j].letter = firstRow[j];
                level[1][j].letter = secondRow[j];
            }

            for (int j = 0; j < 11; j++)
            {
                level[2][j].letter = thirdRow[j];
            }

            for (int j = 0; j < 10; j++)
            {
                level[3][j].letter = fourthRow[j];
            }

        }

    }

	private void ProcessLevelData(string level_name, string level_nice_name, string[,] tile_types)
	{
        Level newLevel = new Level();
		newLevel.nice_name = level_nice_name;
		levels.Add(level_name, newLevel);
		for(int i = 0; i < 4; i++)
		{
			//levels["First"].Add(i, new SortedList<int, Tile>());
			for(int j = 0; j < 12; j++)
			{
				if(i == 3 && j >= 10) break;
				//Tile newTile = new Tile();
				//levels["First"][i].Add(j, newTile);
				Tile newTile = levels[level_name][i][j];
				switch(tile_types[i,j])
				{
				case "P":
					newTile.type = Tile.TYPES.Plain;
					break;
				case "B":
					newTile.type = Tile.TYPES.Blank;
					break;
				case "T":
					newTile.type = Tile.TYPES.Text;
					break;
				case "L":
					newTile.type = Tile.TYPES.Labyrinth;
					break;
                case "G":
                    newTile.type = Tile.TYPES.Gateway;
                    break;
                case "R":
                    newTile.type = Tile.TYPES.RTile;
                    break;
				case "C":
					newTile.type = Tile.TYPES.SeeTower;
					break;
				case "S":
					newTile.type = Tile.TYPES.Record;
					break;
                }
			}
		}
        AssignLetterToTile(newLevel);
	}

}
