using UnityEngine;
using System.Collections;

public class TileScript : MonoBehaviour {

	protected UIManager UIManager_;
	public string metadata = "";

	protected bool hasAppliedTriggerAction = false;
	public void OnEnterTile()
	{
		hasAppliedTriggerAction = true;
	}
	
	// Use this for initialization
	protected void Start () {
		UIManager_ = GameObject.Find("/Managers").GetComponent<UIManager>();
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
