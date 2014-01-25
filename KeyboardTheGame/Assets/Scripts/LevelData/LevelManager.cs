﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

	public Dictionary<string, Level> levels;

	// Use this for initialization
	void Start () {
		levels = new Dictionary<string, Level>();
		BuildLevelsFromData();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void BuildLevelsFromData()
	{
		if(levels.Count > 0) throw new System.Exception("Tried to build levels more than once.");

		// First level
		string[,] firstTypes = new string[4, 12] {
			{"P", "P", "P", "P", "P", "P", "P", "P", "P", "P", "P", "B"},
			{"P", "P", "P", "P", "P", "P", "P", "P", "P", "T", "P", "B"},
			{"P", "P", "P", "P", "P", "P", "P", "P", "P", "P", "P", "B"},
			{"P", "P", "P", "P", "P", "P", "P", "P", "P", "P", "", ""}
		};
		ProcessLevelData("First", firstTypes);
	}

	private void ProcessLevelData(string level_name, string[,] tile_types)
	{
		levels.Add(level_name, new Level());
		for(int i = 0; i < 4; i++)
		{
			//levels["First"].Add(i, new SortedList<int, Tile>());
			for(int j = 0; j < 12; j++)
			{
				if(i == 3 && (j == 10 || j == 11)) break;
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
