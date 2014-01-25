using UnityEngine;
using System.Collections;

public class TileWalking : MonoBehaviour {
	Hashtable keys = new Hashtable(); //each tiles array of neighbors
			//tiles[anyKey] = newValue;                          // insert or change the value for the given key
			//ValueType thisValue = (ValueType)tiles[theKey];    // retrieve a value for the given key
			//int howBig = tiles.Count;                          // get the number of items in the Hashtable
			//tiles.Remove(theKey); 


	string activeTile; //the lit up tile
	Hashtable tiles = new Hashtable(); //list of tile game objects

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
		string[] associatedY = new string[]{"t","u","g","h"}; 
		keys["y"] = associatedY;
		string[] associatedU = new string[]{"y","i","h","j"}; 
		keys["u"] = associatedU;
		string[] associatedI = new string[]{"u","o","j","k"}; 
		keys["i"] = associatedI;
		string[] associatedO = new string[]{"i","p","k","l"}; 
		keys["o"] = associatedO;
		string[] associatedP = new string[]{"o","l"}; 
		keys["p"] = associatedP;

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
		string[] associatedH = new string[]{"y","u","g","j","b","n"};
		keys["h"] = associatedH;
		string[] associatedJ = new string[]{"u","i","h","k","n","m"};
		keys["j"] = associatedJ;
		string[] associatedK = new string[]{"i","o","j","l","m"};
		keys["k"] = associatedK;
		string[] associatedL = new string[]{"o","p","k"};
		keys["l"] = associatedL;

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
		string[] associatedN = new string[]{"h","j","b","m"}; 
		keys["n"] = associatedN;
		string[] associatedM = new string[]{"j","k","n"}; 
		keys["m"] = associatedM;

		//references to tile game objects
		int i=0;
		foreach (Transform child in transform){
			tiles[i] = child.gameObject as GameObject;
			i++;
		}
		(tiles[0] as GameObject).renderer.material.shader = Shader.Find("Reflective/Bumped Specular"); //light up strating tile
	}

	void Update () {
		if (Input.GetKey(activeTile)){ //holding down active tile
			string[] associated = (string[])keys[activeTile];

			for(int i=0; i<associated.Length; i++){
				if (Input.GetKeyDown(associated[i])){ //make sure pressed key is in active tile's associated
					Debug.Log(associated[i]);
					activeTile = associated[i];

					//find the tile object to light it up
					for(int j=0; j<tiles.Count; j++){
						(tiles[j] as GameObject).renderer.material.shader = Shader.Find("Bumped Diffuse");//reset

						string tileName = tiles[j].ToString();
						if(tileName.Substring(4, 1).ToLower() == activeTile){
							(tiles[j] as GameObject).renderer.material.shader = Shader.Find("Reflective/Bumped Specular");//light up
						}
					}
				}
			}

		}
	}
}
