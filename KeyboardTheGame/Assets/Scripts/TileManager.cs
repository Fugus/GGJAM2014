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
		Debug.Log (GetTilePosition(3, 2));
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
                Vector3 tilePosition = rowOrigin + Vector3.right * i * tileWidth;
                GameObject addedTile = GameObject.Instantiate(prefab, tilePosition, emptyTile.transform.rotation) as GameObject;
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

        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>().unfogFirstTile = false;
	}

	public Vector3 GetTilePosition(int row, int column)
	{
		return new Vector3(
			column * (tileWidth / 2.0f),
			row * (-tileHeight),
			0);
	}

}
