﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
public class TileScript : MonoBehaviour {

    protected UIManager UIManager_;
    protected LevelManager LevelManager_;
    protected TileManager TileManager_;
    public string metadata = "";

    public int TileIndexX;
    public int TileIndexY;

    public bool hasBeenVisited = false;
    public bool shouldFog = false;
    protected GameObject spawnedFOW;

	protected bool hasAppliedTriggerAction = false;
    bool firstUpdate = true;

    QuickTimer wiggleTimer;
    GameObject player;

    public void OnUnfog()
    {
        hasBeenVisited = true;
        RemoveFOW();
        StartCoroutine(broadcastUnfogNearby());
    }
    

	public void OnEnterTile()
	{
		hasAppliedTriggerAction = true;
        hasBeenVisited = true;
        RemoveFOW();

        StartCoroutine(broadcastUnfogNearby());
    }
    
    IEnumerator broadcastUnfogNearby()
    {
        yield return new WaitForSeconds(0.1f);
        transform.parent.BroadcastMessage("UnfogNearbyTiles", transform.position);
    }

    public void OnExitTile()
    {
    }

    public void UnfogNearbyTiles(Vector3 position)
    {
        float distanceToPosition = (transform.position - position).magnitude;

        if (spawnedFOW != null && distanceToPosition < 1.5f * TileManager.tileWidth)
        {

            RemoveFOW();
        }
    }

    protected void Start()
    {
        UIManager_ = GameObject.Find("/Managers").GetComponent<UIManager>();
        LevelManager_ = GameObject.Find("/Managers").GetComponent<LevelManager>();
        TileManager_ = GameObject.Find("/Managers").GetComponent<TileManager>();

        wiggleTimer = new QuickTimer();

        player = GameObject.Find("Player");
    }


	public void AddFOW()
    {
        Object fowGameObject = Resources.Load("FogOfWar");
        Quaternion newRotation = transform.rotation;
        newRotation *=  Quaternion.AngleAxis(180, Vector3.right);
        spawnedFOW = GameObject.Instantiate(fowGameObject, transform.position, newRotation) as GameObject;
        spawnedFOW.transform.parent = transform;
    }

    public Tile GetTile()
    {
        return LevelManager_.levels[TileManager_.currentLevel][TileIndexX][TileIndexY];
    }

    public Tile GetNeighbour(PlayerControl.MovementDirection dir)
    {
        int x = TileIndexX;
        int y = TileIndexY;

        switch(dir)
        {
            case PlayerControl.MovementDirection.TopLeft:
                x = TileIndexX - 1;
                y = TileIndexY;
                break;
            case PlayerControl.MovementDirection.TopRight:
                x = TileIndexX - 1;
                y = TileIndexY + 1;
                break;
            case PlayerControl.MovementDirection.Left:
                x = TileIndexX;
                y = TileIndexY - 1;
                break;
            case PlayerControl.MovementDirection.Right:
                x = TileIndexX;
                y = TileIndexY + 1;
                break;
            case PlayerControl.MovementDirection.BottomLeft:
                x = TileIndexX + 1;
                y = TileIndexY - 1;
                break;
            case PlayerControl.MovementDirection.BottomRight:
                x = TileIndexX + 1;
                y = TileIndexY;
                break;
        }

        if (y < 0)
            return null;
        if (x < 0 || x > 3)
            return null;
        if (x == 3 && y >= 10)
            return null;
        if (x == 2 && y >= 11)
            return null;
        if (x < 2 && y >= 12)
            return null;

        if (LevelManager_.levels[TileManager_.currentLevel][x][y].type == Tile.TYPES.Blank)
            return null;

        return LevelManager_.levels[TileManager_.currentLevel][x][y];
    }

    protected void RemoveFOW()
    {
        shouldFog = false;
        LevelManager_.levels[TileManager_.currentLevel][TileIndexX][TileIndexY].isFogged = false;
        GameObject.Destroy(spawnedFOW);
        spawnedFOW = null;
    }

    public void Wiggle()
    {
        wiggleTimer.Start(0.3f);
    }

	// Update is called once per frame
	protected void Update () {

        if (wiggleTimer.IsStarted())
        {
            Vector3 positionTile = transform.localPosition;
            Vector3 positionPlayer = player.transform.localPosition;

            float ratio = 0.5f * wiggleTimer.GetElapsedTime() / 0.1f - 0.7f;
            transform.Translate(new Vector3(0f, 0f, 0.4f) * ratio);
            player.transform.Translate(new Vector3(0f, 0f, 0.4f) * ratio);

            if (wiggleTimer.IsElapsed())
            {
                wiggleTimer.Stop();

                positionTile.z = 0;
                positionPlayer.z = 0;
                transform.localPosition = positionTile;
                player.transform.localPosition = positionPlayer;
            }
        }

        if (!hasBeenVisited && firstUpdate && shouldFog)
        {
            firstUpdate = false;
            if (GetTile().type != Tile.TYPES.Blank)
            {
                AddFOW();
            }
        }

        if(hasAppliedTriggerAction)
		{
			hasAppliedTriggerAction = false;
			UIManager_.mainTextString = "";
		}
	}

}
