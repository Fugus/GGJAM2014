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

    public bool HasWall(int wallIndex, int x, int y)
    {
        if (y < 0)
            return true;
        if (x < 0 || x > 3)
            return true;
        if (x == 3 && y >= 10)
            return true;
        if (x < 3 && y >= 12)
            return true;

        Tile tile = GetComponent<LevelManager>().levels[currentLevel][x][y];

        return tile.walls[wallIndex];
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
                AddWalls(tile, addedTile);

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

    void AddWalls(Tile tile, GameObject addedTile)
    {
        GameObject wallPrefab = Resources.Load<GameObject>("glassWallPrefab");
        Vector3 tilePosition = Vector3.right * tileWidth / 2.0f;
        Quaternion wallRotation = wallPrefab.transform.rotation;

        Quaternion sideRot = Quaternion.AngleAxis(60, Vector3.up);

        for (int i = 0; i < 6; ++i)
        {
            if (tile.walls[i])
            {
                GameObject addedWall = GameObject.Instantiate(wallPrefab, addedTile.transform.position + wallRotation * tilePosition, wallRotation) as GameObject;
                addedWall.transform.parent = addedTile.transform;
                wallRotation *= sideRot;
            }
        }
    }

}
