using UnityEngine;
using System.Collections;

public class TileFillerScript : MonoBehaviour
{
    public static float tileWidth = 35.4f / 2;
    public static float tileHeight = 15f;

    public LevelManager levelManager;

    // Use this for initialization
    void Start()
    {

        Vector3 rowOrigin = Vector3.zero;
        for (int j = 0; j < 4; ++j)
        {
            for (int i = 0; i < 12; ++i)
            {
                if (j == 3 && (i >= 10))
                    break;
                Tile tile  = levelManager.levels["First"][j][i];

                GameObject prefab = Resources.Load(tile.type.ToString()) as GameObject;
                GameObject addedTile = GameObject.Instantiate(prefab, rowOrigin + Vector3.right * i * tileWidth, prefab.transform.rotation) as GameObject;
                addedTile.transform.parent = gameObject.transform;
            }
            rowOrigin.x += tileWidth / 2.0f;
            rowOrigin.y += tileHeight;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
