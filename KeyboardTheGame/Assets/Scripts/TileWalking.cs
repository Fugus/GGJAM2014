using UnityEngine;
using System.Collections;

public class TileWalking : MonoBehaviour {
	Hashtable keys = new Hashtable(); //each tiles array of neighboors
			//tiles[anyKey] = newValue;                          // insert or change the value for the given key
			//ValueType thisValue = (ValueType)tiles[theKey];    // retrieve a value for the given key
			//int howBig = tiles.Count;                          // get the number of items in the Hashtable
			//tiles.Remove(theKey); 

	string activeTile;

	//list of tile objects
	Transform[] tiles;

	void Start () {
		activeTile = "a";

		string[] associatedQ = new string[]{"w","a"}; //tiles connected to q
		keys["q"] = associatedQ;
		string[] associatedW = new string[]{"q","e","a","s"}; //etc...
		keys["w"] = associatedW;
		string[] associatedE = new string[]{"w","r","s","d"}; 
		keys["e"] = associatedE;
		string[] associatedR = new string[]{"e","t","d","f"}; 
		keys["r"] = associatedR;
		string[] associatedT = new string[]{"r","y","f","g"}; 
		keys["t"] = associatedT;

		string[] associatedA = new string[]{"q","w","s","z"};
		keys["a"] = associatedA;
		string[] associatedS = new string[]{"w","e","a","d","z","x"};
		keys["s"] = associatedS;
		string[] associatedD = new string[]{"e","r","s","f","x","c"};
		keys["d"] = associatedD;
		string[] associatedF = new string[]{"r","t","d","g","c","v"};
		keys["f"] = associatedF;
		string[] associatedG = new string[]{"t","y","f","h","v","b"};
		keys["g"] = associatedG;

		string[] associatedZ = new string[]{"a","s","x"};
		keys["z"] = associatedZ;
		string[] associatedX = new string[]{"s","d","z","c"};
		keys["x"] = associatedX;
		string[] associatedC = new string[]{"d","f","x","v"}; 
		keys["c"] = associatedC;
		string[] associatedV = new string[]{"f","g","c","b"}; 
		keys["v"] = associatedV;
		string[] associatedB = new string[]{"g","h","v","n"}; 
		keys["b"] = associatedB;

		int i=0;
		/*foreach (Transform child in transform)
		{
			Debug.Log (child.gameObject.name);
			tiles[i] = child;
			i++;
		}*/
	}

	void Update () {
		if (Input.GetKey(activeTile)){ //holding down active tile
			string[] associated = (string[])keys[activeTile];

			for(int i=0; i<associated.Length; i++){
				if (Input.GetKeyDown(associated[i])){ //make sure pressed key is in active tile's associated
					Debug.Log(associated[i]);
					activeTile = associated[i];
				}
			}

		}
	}
}
