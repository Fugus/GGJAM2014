using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile
{
	public enum TYPES {
		Plain, // the standard tile
		Blank, // an empty tile
		Text // a tile that triggers text when stepped on
	}
	public TYPES type;

	public string metadata;

	public Tile ()
	{
		this.type = TYPES.Plain;
		metadata = "";
	}

}