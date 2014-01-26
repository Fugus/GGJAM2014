using UnityEngine;
using System.Collections;

public class TileRScript : TileScript {

    PlayerControl playerControl;

    Quaternion initialRotation;

    void Start()
    {
        base.Start();
        playerControl = GameObject.Find ("/Player").GetComponent<PlayerControl>();
        initialRotation = transform.rotation;
    }

	// Update is called once per frame
	new void Update () {
		if(hasAppliedTriggerAction)
		{
			hasAppliedTriggerAction = false;
			UIManager_.mainTextString = "<3<3 Together at last! <3<3";
			playerControl.PlaySound(PlayerControl.RecordableSounds.victory);
		}

		// this tile should never have fog of war
		if(spawnedFOW) RemoveFOW();

        Vector3 toPlayer = (playerControl.transform.position - transform.position);

        if (toPlayer.magnitude < TileManager.tileWidth * 2.0f)
        {
            float angle = Vector3.Angle(Vector3.right, toPlayer.normalized);
            Quaternion newRot = transform.rotation;
            Vector3 newEulerAngles = newRot.eulerAngles;
            newEulerAngles.z = 120.0f + Mathf.Round(angle / 60.0f) * 60.0f;
            newRot.eulerAngles = newEulerAngles;
            transform.rotation = newRot;
        }
        else
        {
            transform.rotation = initialRotation;
        }

        base.Update();
	}

}
