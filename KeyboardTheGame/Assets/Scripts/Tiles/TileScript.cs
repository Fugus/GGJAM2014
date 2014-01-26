using UnityEngine;
using System.Collections;

public class TileScript : MonoBehaviour {

	protected UIManager UIManager_;
	public string metadata = "";

    public int TileIndexX;
    public int TileIndexY;

    public bool hasBeenVisited = false;
    GameObject spawnedFOW;

	protected bool hasAppliedTriggerAction = false;
    bool firstUpdate = true;

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
        if (spawnedFOW != null && (transform.position - position).magnitude < 1.5f * TileManager.tileWidth)
        {
            RemoveFOW();
        }
    }

    protected void Start()
    {
        UIManager_ = GameObject.Find("/Managers").GetComponent<UIManager>();
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
        GameObject.Destroy(spawnedFOW);
    }

	// Update is called once per frame
	protected void Update () {
        if (!hasBeenVisited && firstUpdate)
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
