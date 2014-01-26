using UnityEngine;
using System.Collections;

public class TileRecordScript : TileScript {

	private PlayerControl PlayerControl_;

	public PlayerControl.RecordableSounds soundToRecord;
	public int soundLength;
	public AudioClip recordedClip;
	
	private QuickTimer timer;

	private bool standingOnTile = false;
	private bool recordedOnceAlready = false;

	new void Start()
	{
		timer = new QuickTimer();
		soundToRecord = (PlayerControl.RecordableSounds) System.Enum.Parse(typeof(PlayerControl.RecordableSounds), metadata);
		PlayerControl_ = GameObject.Find ("/Player").GetComponent<PlayerControl>();
		soundLength = PlayerControl_.soundLengths[soundToRecord];
		base.Start();
	}

	// Update is called once per frame
	new void Update () {
		if(hasAppliedTriggerAction)
		{
			hasAppliedTriggerAction = false;
			UIManager_.mainTextString = "Press Space and record " + metadata + " sounds.";
			recordedOnceAlready = false;
		}

		if(standingOnTile)
		{
			if ((! recordedOnceAlready) && Input.GetKeyUp(KeyCode.Space))
			{
				timer.Start((float) soundLength);
				recordedOnceAlready = true;
				Debug.Log("legnth :" + soundLength);
				recordedClip = Microphone.Start(null, false, soundLength, 44100);
			}

			if (Microphone.IsRecording(null))
			{
				UIManager_.mainTextString = "" + timer.GetElapsedTime().ToString("F1") + " seconds.";
			}
			
			if (Microphone.IsRecording(null) && timer.IsElapsed()) //timer.IsStarted() && timer.IsElapsed()
			{
				StopRecording();
				PlayerControl_.AddSound(soundToRecord, recordedClip);
				UIManager_.mainTextString = "";
			}
		}

		base.Update();
	}

	new void OnEnterTile()
	{
		standingOnTile = true;
		base.OnEnterTile();
	}

	new void OnExitTile()
	{
		if(Microphone.IsRecording(null)) StopRecording();
		standingOnTile = false;
		base.OnExitTile();
	}

	void StopRecording()
	{
		timer.Stop();
		Microphone.End(null);
	}

}
