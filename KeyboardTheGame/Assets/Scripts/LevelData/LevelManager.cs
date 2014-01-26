﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

	public Dictionary<string, Level> levels = new Dictionary<string, Level>();

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
		GetComponent<TileManager>().LoadLevel("First");
	}

	private void BuildLevelsFromData()
	{
		if(levels.Count > 0) throw new System.Exception("Tried to build levels more than once.");
		
		string[,] tempTypes;

		// First
		tempTypes = new string[4, 12] {
			{"P", "P", "P", "P", "P", "P", "P", "P", "P", "P", "P", "B"},
			{"P", "P", "L", "P", "P", "P", "P", "P", "T", "T", "P", "B"},
			{"P", "L", "L", "P", "P", "R", "P", "P", "P", "P", "P", "B"},
			{"G", "P", "C", "P", "P", "P", "P", "P", "P", "P", "", ""}
		};
		ProcessLevelData("First", tempTypes);
		levels["First"][1][8].metadata = "I print text on the screen!";
		levels["First"][1][9].metadata = "BUT SO DO I BITCHES!";
		levels["First"][3][0].metadata = "Antechamber";
        levels["First"][1][8].walls[0] = true;
        levels["First"][1][8].walls[1] = true;
        levels["First"][1][8].walls[2] = true;
        levels["First"][1][8].walls[3] = true;
        levels["First"][1][8].walls[4] = true;
        levels["First"][1][8].walls[5] = true;

		// Antechamber
		tempTypes = new string[4, 12] {
			{"P", "P", "P", "P", "P", "P", "P", "P", "P", "P", "P", "B"},
			{"P", "P", "P", "L", "L", "L", "L", "L", "L", "T", "P", "B"},
			{"P", "P", "P", "P", "P", "P", "P", "P", "P", "P", "P", "B"},
			{"G", "P", "C", "P", "P", "P", "P", "P", "P", "P", "", ""}
		};
		ProcessLevelData("Antechamber", tempTypes);
		levels["Antechamber"][1][9].metadata = "Test text second level.";
		levels["Antechamber"][3][0].metadata = "First";
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
                }
			}
		}
	}

}
