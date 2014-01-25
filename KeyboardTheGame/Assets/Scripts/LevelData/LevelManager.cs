using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

	public Dictionary<string, Level> levels = new Dictionary<string, Level>();

	// Use this for initialization
	void Start () {
		levels = new Dictionary<string, Level>();
		BuildLevelsFromData();
		GetComponent<TileManager>().TileFiller();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void BuildLevelsFromData()
	{
		if(levels.Count > 0) throw new System.Exception("Tried to build levels more than once.");

		// First level
		string[,] tempTypes;
		
		tempTypes = new string[4, 12] {
			{"P", "P", "P", "P", "P", "P", "P", "P", "P", "P", "P", "B"},
			{"P", "P", "P", "P", "P", "P", "P", "P", "T", "T", "P", "B"},
			{"P", "P", "P", "P", "P", "P", "P", "P", "P", "P", "P", "B"},
			{"P", "P", "P", "P", "P", "P", "P", "P", "P", "P", "", ""}
		};
		ProcessLevelData("First", tempTypes);
		levels["First"][1][8].metadata = "I print text on the screen!";
		levels["First"][1][9].metadata = "BUT SO DO I BITCHES!";
		
		/*tempTypes = new string[4, 12] {
			{"P", "P", "P", "P", "P", "P", "P", "P", "P", "P", "P", "B"},
			{"P", "P", "P", "P", "P", "P", "P", "P", "P", "T", "P", "B"},
			{"P", "P", "P", "P", "P", "P", "P", "P", "P", "P", "P", "B"},
			{"P", "P", "P", "P", "P", "P", "P", "P", "P", "P", "", ""}
		};
		ProcessLevelData("Antechamber", tempTypes);
		levels["Antechamber"][1][9].metadata = "Test text second level.";*/
	}

	private void ProcessLevelData(string level_name, string[,] tile_types)
	{
		levels.Add(level_name, new Level());
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
				}
			}
		}
	}

}
