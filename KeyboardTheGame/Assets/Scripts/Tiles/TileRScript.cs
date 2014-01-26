using UnityEngine;
using System.Collections;

public class TileRScript : TileScript {
	
	// Update is called once per frame
	new void Update () {
		if(hasAppliedTriggerAction)
		{
			hasAppliedTriggerAction = false;
			UIManager_.mainTextString = "<3<3<3";
		}

        base.Update();
	}

}
