using UnityEngine;
using System.Collections;

public class TileScript : MonoBehaviour {

	protected UIManager UIManager_;
	public string metadata = "";

    bool hasBeenVisited = false;
    GameObject spawnedFOW;

	protected bool hasAppliedTriggerAction = false;
	public void OnEnterTile()
	{
		hasAppliedTriggerAction = true;
        RemoveFOW();
    }

    public void OnExitTile()
    {
    }

    protected void Start()
    {
        UIManager_ = GameObject.Find("/Managers").GetComponent<UIManager>();
        AddFOW();
    }

	void AddFOW()
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
		if(hasAppliedTriggerAction)
		{
			hasAppliedTriggerAction = false;
			UIManager_.mainTextString = "";
		}
	}

}
