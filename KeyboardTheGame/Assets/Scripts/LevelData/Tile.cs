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
		Gateway // a gateway to another level
	}
	public TYPES type;

	public string metadata;

	public Tile ()
	{
		this.type = TYPES.Plain;
		metadata = "";
	}

}