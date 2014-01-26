﻿using UnityEngine;
using System.Collections;

public class TileSeeTowerScript : TileScript {

	GameObject[] waitingForReenable;

	// Update is called once per frame
	new void Update () {
		if(hasAppliedTriggerAction)
		{
			hasAppliedTriggerAction = false;
			// temporarily disable fog of war
			waitingForReenable = GameObject.FindGameObjectsWithTag("FOW");
			foreach(GameObject obj in waitingForReenable)
			{
				obj.GetComponent<FogOfWarScript>().DisableFOW();
			}
		}

        base.Update();
	}

	new public void OnExitTile()
	{
		// re-enable fog of war
		foreach(GameObject obj in waitingForReenable)
		{
			if(obj != null) obj.GetComponent<FogOfWarScript>().EnableFOW();
		}
		waitingForReenable = null;
	}

}
