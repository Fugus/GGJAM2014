using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile
{
	public enum TYPES {
		Plain, // the standard tile
		Blank, // an empty tile
		Text, // a tile that triggers text when stepped on
		Labyrinth, // a tile with an arrow that rotates when you step on it
        Gateway, // a gateway to another level
        RTile, // the tile with R
		SeeTower, // the tower where C can see
		Record // where the player can record a new in game sound
	}
	public TYPES type;

    public bool[] walls = {false, false, false, false, false, false};

	public string metadata;
    public bool isFogged = true;
    public string letter;

	public Tile ()
	{
		this.type = TYPES.Plain;
		metadata = "";
	}

}