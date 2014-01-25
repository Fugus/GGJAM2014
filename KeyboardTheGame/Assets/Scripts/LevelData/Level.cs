using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level : SortedDictionary<int, SortedList<int, Tile>>
{
	const int NUM_ROWS = 4; // how many rows are in a level

	public Level () : base()
	{
		// generate the tiled structure
		for(int i = 0; i < NUM_ROWS; i++)
		{
			Add(i, new SortedList<int, Tile>());
			int columns_in_row = 0;
			switch(i) // how many tiles in each row
			{
			case 0:
			case 1:
			case 2:
				columns_in_row = 12;
				break;
			case 3:
				columns_in_row = 10;
				break;
			default:
				columns_in_row = 0;
				break;
			}
			for(int j = 0; j < columns_in_row; j++)
			{
				this[i].Add(j, new Tile());
			}
		}
	}
}