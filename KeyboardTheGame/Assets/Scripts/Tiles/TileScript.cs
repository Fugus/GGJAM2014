using UnityEngine;
using System.Collections;

public class TileScript : MonoBehaviour {

	public UIManager UIManager_;
	public string metadata = "";

	protected bool hasAppliedTriggerAction = false;
	public void OnApplyTriggerAction()
	{
		hasAppliedTriggerAction = true;
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(hasAppliedTriggerAction)
		{
			hasAppliedTriggerAction = false;
			UIManager_.mainTextString = "";
		}
	}

}
