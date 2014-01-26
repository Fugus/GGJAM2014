using UnityEngine;
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
    GameObject spawnedFOW;

	protected bool hasAppliedTriggerAction = false;
    bool firstUpdate = true;

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

//       Debug.Log("Broadcasting from " + TileIndexX + " " + TileIndexY);
        StartCoroutine(broadcastUnfogNearby());
//       Debug.Log("***************");
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
        if (distanceToPosition < 1.5f * TileManager.tileWidth)
//            Debug.Log("inrange " + TileIndexX + " " + TileIndexY);

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
    }


	public void AddFOW()
    {
        Object fowGameObject = Resources.Load("FogOfWar");
        Quaternion newRotation = transform.rotation;
        newRotation *=  Quaternion.AngleAxis(180, Vector3.right);
        spawnedFOW = GameObject.Instantiate(fowGameObject, transform.position, newRotation) as GameObject;
        spawnedFOW.transform.parent = transform;
    }

    void RemoveFOW()
    {
        shouldFog = false;
        LevelManager_.levels[TileManager_.currentLevel][TileIndexX][TileIndexY].isFogged = false;
        GameObject.Destroy(spawnedFOW);
        spawnedFOW = null;
    }

	// Update is called once per frame
	protected void Update () {
        if (!hasBeenVisited && firstUpdate && shouldFog)
        {
            firstUpdate = false;
            AddFOW();
        }

        if(hasAppliedTriggerAction)
		{
			hasAppliedTriggerAction = false;
			UIManager_.mainTextString = "";
		}
	}

}
