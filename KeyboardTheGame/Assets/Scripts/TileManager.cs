using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileManager : MonoBehaviour
{
    public static float tileWidth = 35.4f / 2 + 1.7f;
    public static float tileHeight = 15f + 1.7f;

    public GameObject emptyTile;

	public string currentLevel;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

	public void LoadLevel(string name)
	{
		GameObject tilesObject = GameObject.Find ("/Tiles");
		if(tilesObject) GameObject.Destroy(tilesObject);
		tilesObject = new GameObject("Tiles");

		// spawn the tiles
		Vector3 rowOrigin = Vector3.zero;
		for (int j = 0; j < 4; ++j)
		{
			for (int i = 0; i < 12; ++i)
			{
				if (j == 3 && i >= 10) break;
				Tile tile = GetComponent<LevelManager>().levels[name][j][i];
				
				GameObject prefab = Resources.Load<GameObject>(tile.type.ToString());
				GameObject addedTile = GameObject.Instantiate(prefab, rowOrigin + Vector3.right * i * tileWidth, emptyTile.transform.rotation) as GameObject;
				TileScript tileScript = addedTile.GetComponent<TileScript>();
                if (tileScript != null)
                {
                    tileScript.metadata = tile.metadata;
                    tileScript.TileIndexX = j;
                    tileScript.TileIndexY = i;
                    tileScript.shouldFog = tile.isFogged;
                }

				addedTile.transform.parent = tilesObject.transform;
			}
			rowOrigin.x += tileWidth / 2.0f;
			rowOrigin.y -= tileHeight;
		}

		currentLevel = name;
	}
}
